using System;
using System.Collections.Generic;
using System.Linq;
using Hunt.Data;
using Hunt.Model;
using HuntRepository.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace HuntRepository.Data
{
    public class ScoreRepository: IScoreRepository
    {
         private readonly HuntContext context;

        public ScoreRepository(HuntContext _context)
        {
            context = _context;
        }

        public Result<Score> Add(Score score)
        {
            var result = new Result<Score>(false, new Score());
            IDbContextTransaction tx = null;
            try{
                score.Identifier = Guid.NewGuid();
                context.Scores.Add(score);
                context.SaveChanges();
                tx.Commit();
                result = new Result<Score>(true, score);
                //TODO logger info
                return result;
            }
            catch(Exception ex){
                //TODA logger error
                return result;
            }
            finally{
                tx?.Dispose();
            }
        }

        public void Delete(Score score)
        {
            var tmpScore = context.Scores.Find(score.Identifier);
            if(tmpScore!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Scores.Remove(tmpScore);
                }
                catch(Exception ex){
                    //TODO logger error
                }
                finally{
                    tx?.Dispose();
                }
            }

        }

        public Result<Score> Find(Score user)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Score>> Query(Func<Score, bool> query)
        {
            var result = new Result<IEnumerable<Score>>(false, new List<Score>());
            IDbContextTransaction tx = null;
            try{
                var resultQuery = context.Scores.Where(ux=>query.Invoke(ux));
                return new Result<IEnumerable<Score>>(true, resultQuery.AsEnumerable());
            }
            catch(Exception ex){
                //TODO logger error
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