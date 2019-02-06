using HuntRepository.Infrastructure;

namespace Hunt.Data{
    public class Repository : IRepository
    {
        public IUserRepository UserRepository { get; private set; }
        public IAnimalRepository AnimalRepository {get; private set;}

        public Repository(IUserRepository _repository)
        {
            //UserRepository = new UserRepository();
            UserRepository = _repository;
        }


    }
}