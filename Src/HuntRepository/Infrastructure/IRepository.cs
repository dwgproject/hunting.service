namespace HuntRepository.Infrastructure{

    public interface IRepository{

        IUserRepository UserRepository { get; }
        IAnimalRepository AnimalRepository {get;}


    }
}