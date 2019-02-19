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
    public class HunterRepository : IHunterRepository
    {
        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(HunterRepository));

        public HunterRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
        }
        public Result<Hunter> Add(User user)
        {
            var result = new Result<Hunter>(false, new Hunter());
            var hunter = new Hunter();
            IDbContextTransaction tx = null;
            try{
                //context = new HuntContext();
                    tx = context.Database.BeginTransaction();
                    hunter.Identifier = Guid.NewGuid();
                    hunter.User = user;
                    context.Hunters.Add(hunter);
                    context.SaveChanges();
                    tx.Commit();
                    result = new Result<Hunter>(true, hunter);
                    log.Info($"Dodano usera/hunter: {hunter.User}");
                    return result;
                }catch(Exception ex){
                    log.Error($"Nie udało się dodać usera:{hunter.User}, {ex}");
                    return result;
                }finally{
                    tx?.Dispose();
                }
        }

        public void Delete(Guid identifier)
        {
            var tmp = context.Hunters.Where(u=>u.User.Identifier == identifier).Select(i=>i.Identifier).SingleOrDefault();
            if(tmp!=null){
                var tmpHunter = context.Hunters.Find(tmp);
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Hunters.Remove(tmpHunter);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Usunięto huntera: {tmp}");
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunać huntera: {tmp},{ex} ");
                }
                finally{
                    tx?.Dispose();
                }
            }
        }

        public Result<Hunter> Find(Guid identifier)
        {
            throw new NotImplementedException();
        }


        public Result<IEnumerable<Hunter>> QueryHunter(Func<Hunter, bool> query)
        {
            var result = new Result<IEnumerable<Hunter>>(false, new List<Hunter>());
            IDbContextTransaction tx = null;
            try{
                tx= context.Database.BeginTransaction();
                var resultQuery = context.Hunters.Include("User").Where(ux=>query.Invoke(ux));
                return new Result<IEnumerable<Hunter>>(true, resultQuery.AsEnumerable());
            }
            catch(Exception ex){
                log.Error($"Zapytanie nie powiodło się {ex}");
                return result;
            }
            finally{
                tx?.Dispose();
            }
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
        
        public Result<IEnumerable<Hunter>> Query(Func<User, bool> query)
        {
            throw new NotImplementedException();
        }
    }
}