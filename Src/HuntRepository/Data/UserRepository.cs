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
        private readonly IHunterRepository hunterRepository;
 
        public UserRepository(HuntContext context)
        {
            this.context = context;
            LoggerConfig.ReadConfiguration();
            hunterRepository = new HunterRepository(context);
        }

        public Result<User> Add(User user)
        {
            var result = new Result<User>(false, new User());
            //HuntContext context = null;
            IDbContextTransaction tx = null;
            if(!context.Users.Any(i=>i.Email == user.Email)){
                try{
                //context = new HuntContext();
                    tx = context.Database.BeginTransaction();
                    user.Identifier = Guid.NewGuid();
                    user.Issued = DateTime.Now;
                    context.Users.Add(user);
                    context.SaveChanges();
                    tx.Commit();
                    result = new Result<User>(true, user);
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

        public void Delete(Guid identidier)
        {
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
                }
                catch(Exception ex){
                    log.Error($"Nie udało się usunać usera: {identidier},{ex} ");
                }
                finally{
                    tx?.Dispose();
                }
            }
        }

        public Result<User> Find(Guid identidier)
        {
            HuntContext context = null;
            try{
                //context = new HuntContext();
                var found = context.Users.Find(identidier);
                return found != null ? 
                                new Result<User>(true, found) : 
                                    new Result<User>(false, null);

            }catch(Exception ex){
                return new Result<User>(false, null);    
            }finally{
            }
        }

        public Result<IEnumerable<User>> Query(Func<User, bool> query)
        {
            Result<IEnumerable<User>> result = new Result<IEnumerable<User>>(false, new List<User>());
            //HuntContext context = null;
            IDbContextTransaction tx = null;
            try{
                //context = new HuntContext();
                var resultQuery = context.Users.Include("Role").Where(ux => query.Invoke(ux));                
                return new Result<IEnumerable<User>>(true, resultQuery.AsEnumerable());
            }catch(Exception ex){
                log.Error($"Zapytanie nie powiodło sie {query}, {ex}");
                return result;
            }finally{
                tx?.Dispose();
            }
        }

        public void Update(User user)
        {
            var tmpUser = context.Users.Find(user.Identifier);
            if(tmpUser!=null){
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    tmpUser.Name=user.Name;
                    tmpUser.Surname = user.Surname;
                    tmpUser.Password = user.Password;
                    tmpUser.Email = user.Email;
                    context.SaveChanges();
                    tx.Commit();
                    log.Info($"Update usera:{user} ");
                }
                catch(Exception ex){
                    log.Error($"Nie udało update usera:{user}, {ex}");
                }
                finally{
                    tx?.Dispose();
                }
            }
        }
    }
}