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
    public class QuarryRepository : IQuarryRepository
    {
        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(QuarryRepository));
        public QuarryRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
        }
        public Result<Quarry> Add(Quarry quarry)
        {
            var result = new Result<Quarry>(false, new Quarry());
            IDbContextTransaction tx = null;
            try
            {
                tx = context.Database.BeginTransaction();
                quarry.Identifier = Guid.NewGuid();
                quarry.Animal = context.Animals.FirstOrDefault(i=>i.Identifier == quarry.Animal.Identifier);
                context.Quarries.Add(quarry);
                context.SaveChanges();
                result = new Result<Quarry>(true, quarry);
                log.Info("Dodana quarry");
                return result;
                
            }
            catch (Exception ex) {
                log.Error($"Nie dodano quarry{ex}");
                return result;
            }
            finally {
                tx?.Dispose();
            }
        }

        public void Delete(Guid identifier)
        {
            var existQuarry = context.Quarries.Find(identifier);
            IDbContextTransaction tx = null;
            try{
                tx = context.Database.BeginTransaction();
                context.Quarries.Remove(existQuarry);
                context.SaveChanges();
                tx.Commit();
                log.Info($"Usunięto{identifier}");
            }
            catch(Exception ex){
                log.Error(ex);
            }
            finally{
                tx?.Dispose();
            }
        }

        public Result<Quarry> Find(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Quarry>> Query(Func<Quarry, bool> query)
        {
            var result = new Result<IEnumerable<Quarry>>(false, new List<Quarry>());
            IDbContextTransaction tx = null;
            try
            {
                tx = context.Database.BeginTransaction();
                var resultQuery = context.Quarries.Where(x => query.Invoke(x));
                return new Result<IEnumerable<Quarry>>(true, resultQuery.AsEnumerable());
            }
            catch (Exception ex)
            {
                return result;
            }
            finally {
                tx?.Dispose();
            }
        }

        public void Update(Quarry quarry)
        {
            var existQuarry = context.Quarries.FirstOrDefault(q=>q.Identifier == quarry.Identifier);
            IDbContextTransaction tx = null;
            try {
                tx = context.Database.BeginTransaction();
                existQuarry.Animal = context.Animals.FirstOrDefault(x => x.Identifier == quarry.Animal.Identifier) ?? existQuarry.Animal;
                existQuarry.Amount = existQuarry.Amount-quarry.Amount;
                context.SaveChanges();
                tx.Commit();
            }
            catch (Exception ex) { 
                log.Error(ex);
            }
            finally {
                tx?.Dispose();
            }
        }
    }
}