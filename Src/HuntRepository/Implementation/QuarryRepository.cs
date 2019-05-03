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
    public class QuarryRepository : IQuarryRepository
    {
        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(QuarryRepository));
        private string TAG = "RQ";
        public QuarryRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
        }
        public RepositoryResult<Quarry> Add(Quarry quarry)
        {
            var result = new RepositoryResult<Quarry>(false, new Quarry());
            IDbContextTransaction tx = null;
            try
            {
                tx = context.Database.BeginTransaction();
                quarry.Identifier = Guid.NewGuid();
                quarry.Animal = context.Animals.FirstOrDefault(i=>i.Identifier == quarry.Animal.Identifier);
                context.Quarries.Add(quarry);
                context.SaveChanges();
                result = new RepositoryResult<Quarry>(true, quarry);
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

        public RepositoryResult<string> Delete(Guid identifier)
        {
            var result = new RepositoryResult<string>(false, "",TAG);
            var existQuarry = context.Quarries.Find(identifier);
            IDbContextTransaction tx = null;
            try{
                tx = context.Database.BeginTransaction();
                context.Quarries.Remove(existQuarry);
                context.SaveChanges();
                tx.Commit();
                log.Info($"Usunięto{identifier}");
                result = new RepositoryResult<string>(true,"",TAG);
                return result;
            }
            catch(Exception ex){
                log.Error(ex);
                result = new RepositoryResult<string>(false,ex.Message.ToString(),TAG+"03");
                return result;
            }
            finally{
                tx?.Dispose();
            }
        }

        public RepositoryResult<Quarry> Find(Guid identifier)
        {
            try{
                var found = context.Quarries.Include(a=>a.Animal).FirstOrDefault(i=>i.Identifier == identifier);
                return found != null ?
                                new RepositoryResult<Quarry>(true, found):
                                new RepositoryResult<Quarry>(false, null);
                            
            }
            catch(Exception ex){
                log.Error($"{ex}");
                return new RepositoryResult<Quarry>(false, null);
            }
            finally{}
        }

        public RepositoryResult<IEnumerable<Quarry>> Query(Func<Quarry, bool> query)
        {
            var result = new RepositoryResult<IEnumerable<Quarry>>(false, new List<Quarry>());
            IDbContextTransaction tx = null;
            try
            {
                tx = context.Database.BeginTransaction();
                var resultQuery = context.Quarries.Where(x => query.Invoke(x));
                return new RepositoryResult<IEnumerable<Quarry>>(true, resultQuery.AsEnumerable());
            }
            catch (Exception ex)
            {
                return result;
            }
            finally {
                tx?.Dispose();
            }
        }

        public RepositoryResult<Quarry> Update(Quarry quarry)
        {
            var result = new RepositoryResult<Quarry>(false, null,TAG);
            var existQuarry = context.Quarries.FirstOrDefault(q=>q.Identifier == quarry.Identifier);
            if(existQuarry!=null){
                IDbContextTransaction tx = null;
                try {
                    tx = context.Database.BeginTransaction();
                    existQuarry.Animal = context.Animals.FirstOrDefault(x => x.Identifier == quarry.Animal.Identifier) ?? existQuarry.Animal;
                    existQuarry.Amount = existQuarry.Amount-quarry.Amount;
                    context.SaveChanges();
                    tx.Commit();
                    result = new RepositoryResult<Quarry>(true,existQuarry,TAG);
                    return result;
                }
                catch (Exception ex) { 
                    log.Error(ex);
                    result = new RepositoryResult<Quarry>(false,null,TAG+"01");
                    return result;
                }
                finally {
                    tx?.Dispose();
                }
            }
            else{
                result = new RepositoryResult<Quarry>(false,null, TAG+"02");
                return result;
            }

        }
    }
}