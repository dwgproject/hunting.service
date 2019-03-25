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
    public class ScoreRepository: IScoreRepository
    {
         private readonly HuntContext context;
         private readonly ILog log = LogManager.GetLogger(typeof(HuntingRepository));
         private string TAG = "RS";

        public ScoreRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
        }

        public RepositoryResult<Score> Add(Score score)
        {
            var result = new RepositoryResult<Score>(false, new Score());
            IDbContextTransaction tx = null;
            try{
                score.Identifier = Guid.NewGuid();
                context.Scores.Add(score);
                context.SaveChanges();
                tx.Commit();
                result = new RepositoryResult<Score>(true, score);
                log.Info($"Dodano nowy rezultar {score}");
                return result;
            }
            catch(Exception ex){
                log.Error($"Nie udało się dodać nowego rezultatu {score}, {ex}");
                return result;
            }
            finally{
                tx?.Dispose();
            }
        }

        public RepositoryResult<string> Delete(Guid identifier)
        {
            var result = new RepositoryResult<string>(false, "",TAG);
            var tmpScore = context.Scores.Find(identifier);
            if(tmpScore!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Scores.Remove(tmpScore);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Usunięto rezultat {identifier}");
                    result = new RepositoryResult<string>(true,"",TAG);
                    return result;
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunac rezultatu {identifier}, {ex}");
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

        public RepositoryResult<Score> Find(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public RepositoryResult<IEnumerable<Score>> Query(Func<Score, bool> query)
        {
            var result = new RepositoryResult<IEnumerable<Score>>(false, new List<Score>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Scores.Where(ux=>query.Invoke(ux));
                return new RepositoryResult<IEnumerable<Score>>(true, resultQuery.AsEnumerable());
            }
            catch(Exception ex){
                log.Error($"Zapytanie nie powiodło się {query}, {ex}");
                return result;
            }
            finally{              
                tx?.Dispose();
            }

        }

        public RepositoryResult<string> Update(Score score)
        {
            var result = new RepositoryResult<string>(false, "",TAG);
            var tmpScore = context.Scores.Find(score.Identifier);
            if(tmpScore!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpScore.Animal = score.Animal;
                    tmpScore.Quantity = score.Quantity;
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Zaktualizowano rezultat {score}");
                    result = new RepositoryResult<string>(true,"",TAG);
                    return result;
                }
                catch(Exception ex){
                    log.Error($"Nie udało sie zaktualizować rezultatu {score}, {ex}");
                    result = new RepositoryResult<string>(false,ex.Message.ToString(),TAG+"01");
                    return result;
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                result = new RepositoryResult<string>(false,"Object not exist", TAG+"02");
                return result;
            }
        }
    }
}