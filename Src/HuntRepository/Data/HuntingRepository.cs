using System;
using System.Collections.Generic;
using System.Linq;
using Hunt.Configuration;
using Hunt.Data;
using Hunt.Model;
using HuntRepository.Infrastructure;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HuntRepository.Data
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

            var result = new RepositoryResult<Hunting>(false, new Hunting());
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
                result = new RepositoryResult<Hunting>(true, hunting);
                log.Info($"Dodano nowe polowanie {hunting}");
                return result;
            }
            catch(Exception ex){
                log.Error($"Nie udało sie dodać nowego polowania {hunting}, {ex}");
                return result;
            }
            finally{
                tx?.Dispose();
            }
            
        }

        public RepositoryResult<string> Delete(Guid identifier)
        {
            var result = new RepositoryResult<string>(false, "",TAG);
            var tmpHunting = context.Huntings.Find(identifier);
            if(tmpHunting!=null){
                IDbContextTransaction tx=null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Remove(tmpHunting);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Usunięto polowanie {identifier}");
                    result = new RepositoryResult<string>(true,"",TAG);
                    return result;
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunąc polowania {identifier}, {ex}");
                    result = new RepositoryResult<string>(false,ex.Message.ToString(),TAG+"03");
                    return result;
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                result = new RepositoryResult<string>(false,"",TAG+"04");
                return result;
            }
        }

        public RepositoryResult<Hunting> Find(Guid identifier)
        {
                try{
                var found = context.Huntings.Include(l=>l.Leader).Include(s=>s.Status).Include(u=>u.Users).Include(q=>q.Quarries).Include(p=>p.PartialHuntings).FirstOrDefault(i=>i.Identifier==identifier);
                return found != null ?
                                new RepositoryResult<Hunting>(true, found):
                                new RepositoryResult<Hunting>(false, null);
                            
            }
            catch(Exception ex){
                log.Error($"{ex}");
                return new RepositoryResult<Hunting>(false, null);
            }
            finally{}
        }

        public RepositoryResult<Hunting> Finish(Hunting hunting)
        {
            var result = new RepositoryResult<Hunting>(false, new Hunting());
            var tmpHunting = context.Huntings.Find(hunting.Identifier);
            if(tmpHunting!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpHunting.Status=Status.Finish;
                    context.SaveChanges();
                    tx.Commit();
                    result = new RepositoryResult<Hunting>(true, tmpHunting);
                    return result;
                }
                catch(Exception ex){
                    log.Error($"Zakoczenie polowania nie powiodło się {hunting}, {ex}");
                    return result;
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                return result;

            }
        }

        public RepositoryResult<IEnumerable<Hunting>> Query(Func<Hunting, bool> query)
        {
            var result = new RepositoryResult<IEnumerable<Hunting>>(false, new List<Hunting>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Huntings.Include(l=>l.Leader).Include(q=>q.Quarries).Include(ph=>ph.PartialHuntings).Include(u=>u.Users).Where(query);
                return new RepositoryResult<IEnumerable<Hunting>>(true, resultQuery.AsEnumerable());
            }
            catch(Exception ex){
                log.Error($"Zapytanie nie powiodło się {query}, {ex}");
                return result;
            }
            finally{
                tx?.Dispose();
            }

        }

        public RepositoryResult<string> Update(Hunting hunting)
        {
            var result = new RepositoryResult<string>(false, "",TAG);
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
                    result = new RepositoryResult<string>(true,"",TAG);
                    return result;
                }
                catch(Exception ex){
                    log.Error($"Nie udało się zaktuaizować polowania {hunting}, {ex}");
                    result = new RepositoryResult<string>(false,ex.Message.ToString(),TAG+"01");
                    return result;
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                result = new RepositoryResult<string>(false,"Object not exist", TAG+"02");
                return result;
            }
        }

        public RepositoryResult<Hunting> Start(Guid identifier)
        {
            var result = new RepositoryResult<Hunting>(false, new Hunting());
            var selectedHunting = context.Huntings.Find(identifier);
            IDbContextTransaction tx = null;
            try {
                tx = context.Database.BeginTransaction();
                selectedHunting.Status = Status.Activate;
                context.SaveChanges();
                tx.Commit();
                return result = new RepositoryResult<Hunting>(true, selectedHunting);
            }
            catch (Exception ex) {
                return result;
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