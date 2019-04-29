using System;
using GravityZero.HuntingSupport.Repository.Model;

namespace GravityZero.HuntingSupport.Repository.Infrastructure
{
    public interface IUserRepository : IModuleRepository<User, User, Guid>{
        
    }
}