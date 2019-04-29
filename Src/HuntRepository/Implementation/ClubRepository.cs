using System;
using System.Collections.Generic;
using System.Linq;
using GravityZero.HuntingSupport.Repository.Configuration;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Repository.Model;
using log4net;
using Microsoft.EntityFrameworkCore.Storage;

namespace GravityZero.HuntingSupport.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(ClubRepository));
        private string TAG = "RC";

        public ClubRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
        }
        public RepositoryResult<Club> Add(Club club)
        {
            var result = new RepositoryResult<Club>(false, new Club());
            IDbContextTransaction tx = null;
            try{
                tx = context.Database.BeginTransaction();
                club.Identifier = Guid.NewGuid();
                context.Clubs.Add(club);
                context.SaveChanges();
                tx.Commit();
                result = new RepositoryResult<Club>(true, club);
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

        public RepositoryResult<string> Delete(Guid identifier)
        {
            var result = new RepositoryResult<string>(false, "",TAG);
            var tmpClub = context.Clubs.Find(identifier);
            if(tmpClub!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Clubs.Remove(tmpClub);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Usunięto koło łowieckie {identifier}");
                    result = new RepositoryResult<string>(true,"",TAG);
                    return result;
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunac koła łowieckiego {identifier}, {ex}");
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

        public RepositoryResult<Club> Find(Guid identifier)
        {
                try{
                var found = context.Clubs.Find(identifier);
                return found != null ?
                                new RepositoryResult<Club>(true, found):
                                new RepositoryResult<Club>(false, null);
                            
            }
            catch(Exception ex){
                log.Error($"{ex}");
                return new RepositoryResult<Club>(false, null);
            }
            finally{}
        }

        public RepositoryResult<IEnumerable<Club>> Query(Func<Club, bool> query)
        {
            RepositoryResult<IEnumerable<Club>> result = new RepositoryResult<IEnumerable<Club>>(false, new List<Club>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Clubs.Where(ux=>query.Invoke(ux));
                return new RepositoryResult<IEnumerable<Club>>(true, resultQuery.AsEnumerable());
            }
            catch(Exception ex){
                log.Error($"Zapytanie nie powiodło sie {query}, {ex}");
                return result;
            }
            finally{              
                tx?.Dispose();
            }
        }

        public RepositoryResult<Club> Update(Club club)
        {
            var result = new RepositoryResult<Club>(false, null,TAG);
            var tmpClub = context.Clubs.Find(club.Identifier);
            if(tmpClub!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpClub.Name = club.Name;
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Zaktualizowano koło łowieckie {club}");
                    result = new RepositoryResult<Club>(true,tmpClub,TAG);
                    return result;

                }
                catch(Exception ex){
                    log.Error($"Nie udało się zaktualizować koła łowieckiego {club}, {ex}");
                    result = new RepositoryResult<Club>(false,null,TAG+"01");
                    return result;
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                result = new RepositoryResult<Club>(false,null, TAG+"02");
                return result;
            }
        }
    }
}