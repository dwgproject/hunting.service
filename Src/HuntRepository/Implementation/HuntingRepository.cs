using System;
using System.Collections.Generic;
using System.Linq;
using GravityZero.HuntingSupport.Repository.Configuration;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Repository.Model;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GravityZero.HuntingSupport.Repository
{
    public class HuntingRepository : IHuntingRepository
    {
        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(HuntingRepository));
        private string TAG = "RH";

        public HuntingRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
        }

        public RepositoryResult<Hunting> Add(Hunting hunting)
        {

            //var result = new RepositoryResult<Hunting>(false, new Hunting());
            var listQuarries = new List<Quarry>();
            var partialHunting = new List<PartialHunting>();
            var randomNumberList = new List<int>();
            randomNumberList = GenerateList(hunting.Users.Count);
            IDbContextTransaction tx = null;   
            try{
                tx = context.Database.BeginTransaction();
                hunting.Identifier = Guid.NewGuid();
                hunting.Issued = DateTime.Now;
                hunting.Leader = context.Users.FirstOrDefault(x=>x.Identifier == hunting.Leader.Identifier);
                hunting.Status = Status.Create;
                foreach(var quarry in hunting.Quarries){
                    var newQuarry = context.Animals.FirstOrDefault(x=>x.Identifier == quarry.Animal.Identifier);
                    listQuarries.Add(new Quarry(){Animal=newQuarry, Amount = quarry.Amount});
                }

                foreach(var part in hunting.PartialHuntings){
                    int i =0;
                    var partialHunters = new List<PartialHuntersList>();
                    foreach(var user in part.PartialHunters){
                        var tmpUser = context.Users.FirstOrDefault(x=>x.Identifier == user.User.Identifier);
                        partialHunters.Add(new PartialHuntersList(){User=tmpUser,HunterNumber = randomNumberList[i] });
                        i++;
                    }
                    partialHunting.Add(new PartialHunting(){PartialHunters=partialHunters,Status=Status.Create,Number=part.Number});
                }


                hunting.Quarries = listQuarries;
                hunting.PartialHuntings = partialHunting;
                context.Huntings.Add(hunting);
                context.SaveChanges();
                tx.Commit();
                log.Info($"Dodano nowe polowanie {hunting}");
                return new RepositoryResult<Hunting>(true, hunting, TAG+"01");
            }
            catch(Exception ex){
                log.Error($"Nie udało sie dodać nowego polowania {hunting}, {ex}");
                return new RepositoryResult<Hunting>(true, null, TAG+"02");;
            }
            finally{
                tx?.Dispose();
            }
            
        }

        public RepositoryResult<string> Delete(Guid identifier)
        {
            //var result = new RepositoryResult<string>(false, "",TAG);
            var tmpHunting = context.Huntings.Find(identifier);
            if(tmpHunting!=null){
                IDbContextTransaction tx=null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Remove(tmpHunting);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Usunięto polowanie {identifier}");
                    return new RepositoryResult<string>(true,"",TAG+"05");
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunąc polowania {identifier}, {ex}");
                    return new RepositoryResult<string>(false,ex.Message.ToString(),TAG+"06");
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                return new RepositoryResult<string>(false,"",TAG+"11");
            }
        }

        public RepositoryResult<Hunting> Find(Guid identifier)
        {
            try{
                var found = context.Huntings.Include(l=>l.Leader).Include(s=>s.Status).Include(u=>u.Users).Include(q=>q.Quarries).Include(p=>p.PartialHuntings).FirstOrDefault(i=>i.Identifier==identifier);
                return found != null ?
                                new RepositoryResult<Hunting>(true, found, TAG+"07"):
                                new RepositoryResult<Hunting>(false, null, TAG+"08");
                            
            }
            catch(Exception ex){
                log.Error($"{ex}");
                return new RepositoryResult<Hunting>(false, null);
            }
            finally{}
        }

        public RepositoryResult<Hunting> Finish(Hunting hunting)
        {
            //var result = new RepositoryResult<Hunting>(false, new Hunting());
            var tmpHunting = context.Huntings.Find(hunting.Identifier);
            if(tmpHunting!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpHunting.Status=Status.Finish;
                    context.SaveChanges();
                    tx.Commit();
                    return new RepositoryResult<Hunting>(true, tmpHunting, TAG+"15");
                }
                catch(Exception ex){
                    log.Error($"Zakoczenie polowania nie powiodło się {hunting}, {ex}");
                    return new RepositoryResult<Hunting>(true, null, TAG+"16");
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                return new RepositoryResult<Hunting>(true, null, TAG+"17");;

            }
        }

        public RepositoryResult<IEnumerable<Hunting>> Query(Func<Hunting, bool> query)
        {
            //var result = new RepositoryResult<IEnumerable<Hunting>>(false, new List<Hunting>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Huntings.Include(l=>l.Leader).Include(q=>q.Quarries).Include(ph=>ph.PartialHuntings).Include(u=>u.Users).Where(query);
                return new RepositoryResult<IEnumerable<Hunting>>(true, resultQuery.AsEnumerable(), TAG+"09");
            }
            catch(Exception ex){
                log.Error($"Zapytanie nie powiodło się {query}, {ex}");
                return new RepositoryResult<IEnumerable<Hunting>>(true, null, TAG+"10");;
            }
            finally{
                tx?.Dispose();
            }

        }

        public RepositoryResult<Hunting> Update(Hunting hunting)
        {
            //var result = new RepositoryResult<Hunting>(false, null,TAG);
            var tmpHunting = context.Huntings.Find(hunting.Identifier);
            if(tmpHunting!=null && tmpHunting.Status!=Status.Finish)
            {
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpHunting.Leader = hunting.Leader;
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Zaktualizowano polowanie {hunting}");
                    return new RepositoryResult<Hunting>(true,tmpHunting,TAG+"03");
                }
                catch(Exception ex){
                    log.Error($"Nie udało się zaktuaizować polowania {hunting}, {ex}");
                    return new RepositoryResult<Hunting>(false,null,TAG+"04");
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                return new RepositoryResult<Hunting>(false,null, TAG+"12");
            }
        }

        public RepositoryResult<Hunting> Start(Guid identifier)
        {
            //var result = new RepositoryResult<Hunting>(false, new Hunting());
            var selectedHunting = context.Huntings.Find(identifier);
            IDbContextTransaction tx = null;
            try {
                tx = context.Database.BeginTransaction();
                selectedHunting.Status = Status.Activate;
                context.SaveChanges();
                tx.Commit();
                return new RepositoryResult<Hunting>(true, selectedHunting, TAG+"13");
            }
            catch (Exception ex) {
                return new RepositoryResult<Hunting>(true, selectedHunting, TAG+"14");;
            }
            finally {
                tx?.Dispose();
            }
        }

        private List<T> ShuffleList<T>(List<T> list)
        {
            Random rd = new Random();
            int count = list.Count;
            while (count > 1) {
                count--;
                int k = (rd.Next(0,count)%count);
                T value = list[k];
                list[k] = list[count];
                list[count] = value;
            }
            return list;
        }

        private List<int> GenerateList(int count)
        {
            var list = new List<int>();
            for (int i = 0; i < count; i++) {
                list.Add(i);
            }
            list = ShuffleList<int>(list);
            return list;
        }


    }
}