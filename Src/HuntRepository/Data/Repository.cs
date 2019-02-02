using HuntRepository.Infrastructure;

namespace Hunt.Data{
    public class Repository : IRepository
    {
        public IUserRepository UserRepository { get; private set; }

        public Repository()
        {
            UserRepository = new UserRepository();
        }


    }
}