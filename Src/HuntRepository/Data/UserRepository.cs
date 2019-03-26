using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hunt.Model;
using HuntRepository.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using log4net;
using Hunt.Configuration;
using HuntRepository.Data;
using Microsoft.EntityFrameworkCore;

namespace Hunt.Data{

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
            var result = new RepositoryResult<User>(false, new User());
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
                    result = new RepositoryResult<User>(true, user);
                    log.Info($"Dodano usera: {user}");
                    return result;
                }catch(Exception ex){
                    log.Error($"Nie udało się dodać usera:{user}, {ex}");
                    return result;
                }finally{
                    tx?.Dispose();
                }
            }
            else{
                return result;
            }
            
        }

        public RepositoryResult<string> Delete(Guid identidier)
        {
            var result = new RepositoryResult<string>(false, "",TAG);
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
                    result = new RepositoryResult<string>(true,"",TAG);
                    return result;
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunać usera: {identidier},{ex} ");
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

        public RepositoryResult<User> Find(Guid identifier)
        {
            HuntContext context = null;
            try{
                //context = new HuntContext();
                //var found = context.Users.Find(identifier);
                var found = context.Users.Include(h=>h.Huntings).FirstOrDefault(i=>i.Identifier == identifier);
                return found != null ? 
                                new RepositoryResult<User>(true, found) : 
                                    new RepositoryResult<User>(false, null);

            }catch(Exception ex){
                return new RepositoryResult<User>(false, null);    
            }finally{
            }
        }

        public RepositoryResult<IEnumerable<User>> Query(Func<User, bool> query)
        {
            RepositoryResult<IEnumerable<User>> result = new RepositoryResult<IEnumerable<User>>(false, new List<User>());
            //HuntContext context = null;
            IDbContextTransaction tx = null;
            try{
                //context = new HuntContext();
                var resultQuery = context.Users.Include("Role").Where(ux => query.Invoke(ux));                
                return new RepositoryResult<IEnumerable<User>>(true, resultQuery.AsEnumerable());
            }catch(Exception ex){
                log.Error($"Zapytanie nie powiodło sie {query}, {ex}");
                return result;
            }finally{
                tx?.Dispose();
            }
        }

        public RepositoryResult<string> Update(User user)
        {
            var result = new RepositoryResult<string>(false, "",TAG);
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
                    result = new RepositoryResult<string>(true,"",TAG);
                    return result;
                }
                catch(Exception ex){
                    log.Error($"Nie udało update usera:{user}, {ex}");
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