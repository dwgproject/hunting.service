using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using log4net;
using Microsoft.EntityFrameworkCore;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Repository.Configuration;

namespace GravityZero.HuntingSupport.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HuntContext context;
        private readonly ILog log = LogManager.GetLogger(typeof(UserRepository));
        private string TAG = "RU";

 
        public UserRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();

        }

        public RepositoryResult<User> Add(User user)
        {
            //var result = new RepositoryResult<User>(false, new User());
            //HuntContext context = null;
            IDbContextTransaction tx = null;
            if(!context.Users.Any(i=>i.Email == user.Email)){
                try{
                //context = new HuntContext();
                    tx = context.Database.BeginTransaction();
                    user.Identifier = Guid.NewGuid();
                    user.Issued = DateTime.Now;
                    user.Role = context.Roles.FirstOrDefault(r=>r.Identifier == user.Role.Identifier);
                    context.Users.Add(user);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Dodano usera: {user}");
                    return new RepositoryResult<User>(true, user, TAG+"01");
                }catch(Exception ex){
                    log.Error($"Nie udało się dodać usera:{user}, {ex}");
                    return new RepositoryResult<User>(false, null, TAG+"02");
                }finally{
                    tx?.Dispose();
                }
            }
            else{
                return new RepositoryResult<User>(false, null, TAG+"13");;
            }
            
        }

        public RepositoryResult<string> Delete(Guid identidier)
        {
            //var result = new RepositoryResult<string>(false, "",TAG);
            var tmpUser = context.Users.Find(identidier);
            if(tmpUser!=null)
            {
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Users.Remove(tmpUser);
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Usunięto usera: {identidier}");
                    return new RepositoryResult<string>(true,"",TAG+"05");
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunać usera: {identidier},{ex} ");
                    return new RepositoryResult<string>(false,ex.Message.ToString(),TAG+"06");
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                return new RepositoryResult<string>(false,"",TAG+"11");;
            }
        }

        public RepositoryResult<User> Find(Guid identifier)
        {
            HuntContext context = null;
            try{
                //context = new HuntContext();
                //var found = context.Users.Find(identifier);
                var found = context.Users.Include(h=>h.Huntings).FirstOrDefault(i=>i.Identifier == identifier);
                return found != null ? 
                                new RepositoryResult<User>(true, found, TAG+"07") : 
                                    new RepositoryResult<User>(false, null, TAG+"08");

            }catch(Exception ex){
                return new RepositoryResult<User>(false, null);    
            }finally{
            }
        }

        public RepositoryResult<IEnumerable<User>> Query(Func<User, bool> query)
        {
            //RepositoryResult<IEnumerable<User>> result = new RepositoryResult<IEnumerable<User>>(false, new List<User>(), string.Concat(TAG, 10));
            //HuntContext context = null;
            IDbContextTransaction tx = null;
            try{
                //context = new HuntContext();
                var resultQuery = context.Users.Include("Role").Where(ux => query.Invoke(ux));                
                return new RepositoryResult<IEnumerable<User>>(true, resultQuery.AsEnumerable(), TAG+"09");
            }catch(Exception ex){
                log.Error($"Zapytanie nie powiodło sie {query}, {ex}");
                return new RepositoryResult<IEnumerable<User>>(false, null, TAG+"10");;
            }finally{
                tx?.Dispose();
            }
        }

        public RepositoryResult<User> Update(User user)
        {
            //var result = new RepositoryResult<User>(false, null, TAG);
            var tmpUser = context.Users.Find(user.Identifier);
            if(tmpUser!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpUser.Login = user.Login ?? tmpUser.Login;
                    tmpUser.Name=user.Name ?? tmpUser.Name;
                    tmpUser.Surname = user.Surname ?? tmpUser.Surname;
                    tmpUser.Password = user.Password ?? tmpUser.Password;
                    tmpUser.Email = user.Email ?? tmpUser.Email;
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Update usera:{user} ");
                    return new RepositoryResult<User>(true,tmpUser, TAG+"03");
                }
                catch(Exception ex){
                    log.Error($"Nie udało update usera:{user}, {ex}");
                    return new RepositoryResult<User>(false,null,TAG+"04");
                }
                finally{
                    tx?.Dispose();
                }
            }
            else{
                return new RepositoryResult<User>(false,null, TAG+"12");
            }
        }
    }
}