using System;
using Hunt.Model;

namespace HuntRepository.Infrastructure{

    public interface IUserRepository : IModuleRepository<User, User, Guid>{
        
    }
}