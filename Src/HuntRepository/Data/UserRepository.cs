using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hunt.Model;
using HuntRepository.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace Hunt.Data{

    public class UserRepository : IUserRepository
    {
        private readonly HuntContext context;

        public UserRepository(HuntContext _context)
        {
            context = _context;
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
                    return result;
                }catch(Exception ex){
                    //TODO: dodać loggera celem logowania błędów
                    return result;
                }finally{
                    tx?.Dispose();
                    context?.Dispose();
                }
            }
            else{
                return result;
            }
            
        }

        public void Delete(User user)
        {
            var tmpUser = context.Users.Find(user.Identifier);
            if(tmpUser!=null)
            {
                IDbContextTransaction tx = null;
                try{
                    tx = context.Database.BeginTransaction();
                    context.Users.Remove(tmpUser);
                    context.SaveChanges();
                    tx.Commit();
                }
                catch(Exception ex){

                }
                finally{
                    tx?.Dispose();
                    context?.Dispose();
                }
            }
        }

        public Result<User> Find(User user)
        {
            return new Result<User>(false, new User());
        }

        public Result<IEnumerable<User>> Query(Func<User, bool> query)
        {
            Result<IEnumerable<User>> result = new Result<IEnumerable<User>>(false, new List<User>());
            //HuntContext context = null;
            IDbContextTransaction tx = null;
            try{
                //context = new HuntContext();
                var resultQuery = context.Users.Where(ux => query.Invoke(ux));                
                return new Result<IEnumerable<User>>(true, resultQuery.AsEnumerable());
            }catch(Exception ex){
                //TODO: dodać loggera celem logowania błędów
                return result;
            }finally{
                tx?.Dispose();
                context?.Dispose();
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
                }
                catch(Exception ex){

                }
                finally{
                    tx?.Dispose();
                    context?.Dispose();
                }
            }
        }
    }
}