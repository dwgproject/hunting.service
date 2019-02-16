using System;
using System.Collections.Generic;
using System.Linq;
using Hunt.Configuration;
using Hunt.Data;
using Hunt.Model;
using HuntRepository.Infrastructure;
using log4net;
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
                    log.Info($"Usunięto usera: {tmp}");
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunać usera: {tmp},{ex} ");
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

        public Result<IEnumerable<Hunter>> Query(Func<User, bool> query)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}