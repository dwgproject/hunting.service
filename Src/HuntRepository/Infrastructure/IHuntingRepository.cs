using Hunt.Model;

namespace HuntRepository.Infrastructure
{
    public interface IHuntingRepository: IModuleRepository<Hunting, Hunting>
    {
         Result<Hunting> Finish(Hunting hunting);
    }
}