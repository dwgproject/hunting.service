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

namespace HuntRepository.Data
{
    public class ScoreRepository: IScoreRepository
    {
         private readonly HuntContext context;
         private readonly ILog log = LogManager.GetLogger(typeof(HuntingRepository));

        public ScoreRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
        }

        public Result<Score> Add(Score score)
        {
            var result = new Result<Score>(false, new Score());
            IDbContextTransaction tx = null;
            try{
                tx = context.Database.BeginTransaction();
                score.Identifier = Guid.NewGuid();
                score.Issued = DateTime.Now;
                score.Animal = context.Animals.FirstOrDefault(a => a.Identifier == score.Animal.Identifier);
                score.Hunting = context.Huntings.FirstOrDefault(h => h.Identifier == score.Hunting.Identifier);
                score.User = context.Users.FirstOrDefault(u => u.Identifier == score.User.Identifier);
                context.Scores.Add(score);
                context.SaveChanges();
                tx.Commit();
                result = new Result<Score>(true, score);
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

        public void Delete(Guid identifier)
        {
            var tmpScore = context.Scores.Find(identifier);
            if(tmpScore!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Scores.Remove(tmpScore);
                    tx.Commit();
                    log.Info($"Usunięto rezultat {identifier}");
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunac rezultatu {identifier}, {ex}");
                }
                finally{
                    tx?.Dispose();
                }
            }

        }

        public Result<Score> Find(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Score>> Query(Func<Score, bool> query)
        {
            var result = new Result<IEnumerable<Score>>(false, new List<Score>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Scores.Include(u=>u.User).Include(a=>a.Animal).Include(h=>h.Hunting).Where(ux=>query.Invoke(ux));
                return new Result<IEnumerable<Score>>(true, resultQuery.AsEnumerable());
            }
            catch(Exception ex){
                log.Error($"Zapytanie nie powiodło się {query}, {ex}");
                return result;
            }
            finally{              
                tx?.Dispose();
            }

        }

        public void Update(Score score)
        {
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
                }
                catch(Exception ex){
                    log.Error($"Nie udało sie zaktualizować rezultatu {score}, {ex}");
                }
                finally{
                    tx?.Dispose();
                }
            }
        }
    }
}