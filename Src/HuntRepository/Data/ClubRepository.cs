using System;
using System.Collections.Generic;
using System.Linq;
using Hunt.Data;
using Hunt.Model;
using HuntRepository.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace HuntRepository.Data
{
    public class ClubRepository : IClubRepository
    {
        private readonly HuntContext context;

        public ClubRepository(HuntContext _context)
        {
            context = _context;
        }
        public Result<Club> Add(Club club)
        {
            var result = new Result<Club>(false, new Club());
            IDbContextTransaction tx = null;
            try{
                tx = context.Database.BeginTransaction();
                club.Identifier = Guid.NewGuid();
                context.Clubs.Add(club);
                context.SaveChanges();
                tx.Commit();
                result = new Result<Club>(true, club);
                return result;
                //TODO: logger info
            }
            catch(Exception ex){
                //TODO: logger error
                return result;
            }
            finally{
                tx?.Dispose();
            }           
        }

        public void Delete(Club club)
        {
            var tmpClub = context.Clubs.Find(club.Identifier);

            if(tmpClub!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Clubs.Remove(tmpClub);
                    context.SaveChanges();
                    tx.Commit();
                    //TODO logger info
                }
                catch(Exception ex){
                    // TODO logger error
                }
                finally{
                    tx?.Dispose();
                }    
            }
        }

        public Result<Club> Find(Club club)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Club>> Query(Func<Club, bool> query)
        {
            Result<IEnumerable<Club>> result = new Result<IEnumerable<Club>>(false, new List<Club>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Clubs.Where(ux=>query.Invoke(ux));
                return new Result<IEnumerable<Club>>(true, resultQuery.AsEnumerable());
            }
            catch(Exception ex){
                //TODO logger error
                return result;
            }
            finally{              
                tx?.Dispose();
            }
        }

        public void Update(Club club)
        {
            var tmpClub = context.Clubs.Find(club.Identifier);
            if(tmpClub!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpClub.Name = club.Name;
                    context.SaveChanges();
                    tx.Commit();
                    //TODO logger info
                }
                catch(Exception ex){
                    //TOOD logger error
                }
                finally{
                    tx?.Dispose();
                }
            }
        }
    }
}