using Hunt.Model;

namespace Hunt.ServiceContext{
    public interface IServiceContext
    {
        void CreateSession(User user);
        void DestoySession(User user);
    }
}