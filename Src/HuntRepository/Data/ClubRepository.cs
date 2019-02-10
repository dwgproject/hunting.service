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
    public class ClubRepository : IClubRepository
    {
        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(ClubRepository));

        public ClubRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
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
                log.Info($"Utworzono nowe koło łowieckie {club}");
                return result;
               
            }
            catch(Exception ex){
                log.Error($"NIe udało się utworzyć koła łowieckiego {club}, {ex}");
                return result;
            }
            finally{
                tx?.Dispose();
            }           
        }

        public void Delete(Guid identifier)
        {
            var tmpClub = context.Clubs.Find(identifier);

            if(tmpClub!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Clubs.Remove(tmpClub);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Usunięto koło łowieckie {identifier}");
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunac koła łowieckiego {identifier}, {ex}");
                }
                finally{
                    tx?.Dispose();
                }    
            }
        }

        public Result<Club> Find(Guid identifier)
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
                log.Error($"Zapytanie nie powiodło sie {query}, {ex}");
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
                    log.Info($"Zaktualizowano koło łowieckie {club}");
                }
                catch(Exception ex){
                    log.Error($"Nie udało się zaktualizować koła łowieckiego {club}, {ex}");
                }
                finally{
                    tx?.Dispose();
                }
            }
        }
    }
}