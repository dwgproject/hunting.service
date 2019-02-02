using Hunt.Model;

namespace HuntRepository.Infrastructure{

    public interface IUserRepository : IModuleRepository<Result<User>, User>{
        
        //IEnumerable<User> Get(Query )
    }
}