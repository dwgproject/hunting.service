using System;
using System.Collections.Generic;
using System.Linq;
using Hunt.Data;
using Hunt.Model;
using HuntRepository.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace HuntRepository.Data
{
    public class HuntingRepository : IHuntingRepository
    {
        private readonly HuntContext context;

        public HuntingRepository(HuntContext _context)
        {
            context = _context;
        }

        public Result<Hunting> Add(Hunting hunting)
        {

            var result = new Result<Hunting>(false, new Hunting());
            IDbContextTransaction tx = null;   
            try{
                tx = context.Database.BeginTransaction();
                hunting.Identifier = Guid.NewGuid();
                hunting.Issued = DateTime.Now;
                hunting.Status = true;
                context.Huntings.Add(hunting);
                context.SaveChanges();
                tx.Commit();
                result = new Result<Hunting>(true, hunting);
                //TODO logger info
                return result;
            }
            catch(Exception ex){
                //TODO logger error
                return result;
            }
            finally{
                tx?.Dispose();
            }
            
        }

        public void Delete(Hunting hunting)
        {
            var tmpHunting = context.Huntings.Find(hunting.Identifier);
            if(tmpHunting!=null){
                IDbContextTransaction tx=null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Remove(hunting);
                    context.SaveChanges();
                    tx.Commit();
                    //TODO logger info
                }
                catch(Exception ex){
                    //Logger error
                }
                finally{
                    tx?.Dispose();
                }
            }
        }

        public Result<Hunting> Find(Hunting user)
        {
            throw new NotImplementedException();
        }

        public Result<Hunting> Finish(Hunting hunting)
        {
            var result = new Result<Hunting>(false, new Hunting());
            var tmpHunting = context.Huntings.Find(hunting.Identifier);
            if(tmpHunting!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpHunting.Status=false;
                    context.SaveChanges();
                    tx.Commit();
                    result = new Result<Hunting>(true, tmpHunting);
                    //TODO logger info
                    return result;
                }
                catch(Exception ex){
                    //TODO logger error
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

        public Result<IEnumerable<Hunting>> Query(Func<Hunting, bool> query)
        {
            var result = new Result<IEnumerable<Hunting>>(false, new List<Hunting>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Huntings.Where(ux=>query.Invoke(ux));
                return new Result<IEnumerable<Hunting>>(true, resultQuery.AsEnumerable());
            }
            catch(Exception ex){
                //TODO logger error
                return result;
            }
            finally{
                tx?.Dispose();
            }

        }

        public void Update(Hunting hunting)
        {
            var tmpHunting = context.Huntings.Find(hunting.Identifier);
            if(tmpHunting!=null && tmpHunting.Status!=false)
            {
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpHunting.Leader = hunting.Leader;
                    tmpHunting.Users = hunting.Users;
                    tmpHunting.Animals = hunting.Animals;
                    context.SaveChanges();
                    tx.Commit();
                    //TODO logger info
                }
                catch(Exception ex){
                    //TODO logger error
                }
                finally{
                    tx?.Dispose();
                }
            }
        }
    }
}