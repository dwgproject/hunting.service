using System;
using Hunt.Model;
using HuntRepository.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hunt.Data{

    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            
        }

        public Result<User> Add(User user)
        {
            var result = new Result<User>(false, new User());
            HuntContext context = null;
            IDbContextTransaction tx = null;
            try{
                context = new HuntContext();
                tx = context.Database.BeginTransaction();
                user.Identifier = Guid.NewGuid();
                context.Users.Add(user);
                context.SaveChanges();
                tx.Commit();
                result = new Result<User>(true, user);
                return result;
            }catch(Exception ex){
                return result;
            }finally{
                tx?.Dispose();
                context?.Dispose();
            }
        }

        public void Delete(User user)
        {
            
        }

        public Result<User> Find(User user)
        {
            return new Result<User>(false, new User());
        }

        public void Update(User user)
        {
            
        }
    }
}