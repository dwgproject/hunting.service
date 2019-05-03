using System;

namespace GravityZero.HuntingSupport.Service.Session{
    public interface IUserSession
    {
        SessionUnit Get(Guid identifier);
        void AddOrUpdate(SessionUnit session);
        void Close(Guid identifier);
    }
}