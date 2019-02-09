using System;

namespace Hunt.ServiceContext.ServiceSession{
    public interface IUserSession{
        Session Get(Guid identifier);
        void AddOrUpdate(Session session);
        void Close(Guid identifier);
    }
}