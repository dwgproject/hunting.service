namespace GravityZero.HuntingSupport.Repository.Infrastructure
{
    public interface IRepository
    {
        IUserRepository UserRepository { get; }
        IAnimalRepository AnimalRepository {get;}
        IHuntingRepository HuntingRepository {get;}
        IScoreRepository ScoreRepository {get;}
    }
}