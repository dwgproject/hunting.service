using System;
using System.Collections.Generic;
using Hunt.Configuration;
using Hunt.Data;
using Hunt.Model;
using HuntRepository.Infrastructure;
using log4net;
using Microsoft.EntityFrameworkCore.Storage;

namespace HuntRepository.Model
{
    public class PartialHuntingRepository : IPartialHuntingRepository
    {

        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(PartialHuntingRepository));

        public PartialHuntingRepository(HuntContext context)
        {
            this.context = context;  
            LoggerConfig.ReadConfiguration();          
        }

        public Result<PartialHunting> Add(PartialHunting user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public Result<PartialHunting> Find(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public Result<PartialHunting> Finish(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<PartialHunting>> Query(Func<PartialHunting, bool> query)
        {
            throw new NotImplementedException();
        }

        public Result<PartialHunting> Start(Guid identifier)
        {
            var result = new Result<PartialHunting>(false, new PartialHunting());
            var selectedPartialHunting = context.PartialHuntings.Find(identifier);
            IDbContextTransaction tx = null;
            try
            {
                tx = context.Database.BeginTransaction();
                selectedPartialHunting.Status = Status.Activate;
                context.SaveChanges();
                tx.Commit();
                return new Result<PartialHunting>(true, selectedPartialHunting);
            }
            catch (Exception ex)
            {

                return result;
            }
            finally {
                tx?.Dispose();
            }
        }

        public void Update(PartialHunting user)
        {
            throw new NotImplementedException();
        }
    }
}