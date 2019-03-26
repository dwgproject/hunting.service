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

namespace HuntRepository.Model
{
    public class PartialHuntingRepository : IPartialHuntingRepository
    {

        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(PartialHuntingRepository));
        private string TAG = "RP";

        public PartialHuntingRepository(HuntContext context)
        {
            this.context = context;  
            LoggerConfig.ReadConfiguration();          
        }

        public RepositoryResult<PartialHunting> Add(PartialHunting user)
        {
            throw new NotImplementedException();
        }

        public RepositoryResult<string> Delete(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public RepositoryResult<PartialHunting> Find(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public RepositoryResult<PartialHunting> Finish(Guid identifier)
        {
            var result = new RepositoryResult<PartialHunting>(false, new PartialHunting());
            var selectedPartialHunting = context.PartialHuntings.Find(identifier);
            IDbContextTransaction tx = null;
            try
            {
                tx = context.Database.BeginTransaction();
                selectedPartialHunting.Status = Status.Finish;
                context.SaveChanges();
                tx.Commit();
                return new RepositoryResult<PartialHunting>(true, selectedPartialHunting);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return result;
            }
            finally
            {
                tx?.Dispose();
            }
        }

        public RepositoryResult<IEnumerable<PartialHunting>> Query(Func<PartialHunting, bool> query)
        {
            var result = new RepositoryResult<IEnumerable<PartialHunting>>(false, new List<PartialHunting>());
            IDbContextTransaction tx = null;
            try {
                tx = context.Database.BeginTransaction();
                var resultQuary = context.PartialHuntings.Include(x=>x.Hunting).Where(ux => query.Invoke(ux));
                return result = new RepositoryResult<IEnumerable<PartialHunting>>(true, resultQuary.AsEnumerable());
            }
            catch (Exception ex) {
                log.Error(ex);
                return result;
            }
            finally {
                tx?.Dispose();
            }
        }

        public RepositoryResult<PartialHunting> Start(Guid identifier)
        {
            var result = new RepositoryResult<PartialHunting>(false, new PartialHunting());
            var selectedPartialHunting = context.PartialHuntings.Find(identifier);
            IDbContextTransaction tx = null;
            try
            {
                tx = context.Database.BeginTransaction();
                selectedPartialHunting.Status = Status.Activate;
                context.SaveChanges();
                tx.Commit();
                return new RepositoryResult<PartialHunting>(true, selectedPartialHunting);
            }
            catch (Exception ex)
            {

                return result;
            }
            finally {
                tx?.Dispose();
            }
        }

        public RepositoryResult<string> Update(PartialHunting user)
        {
            throw new NotImplementedException();
        }
    }
}