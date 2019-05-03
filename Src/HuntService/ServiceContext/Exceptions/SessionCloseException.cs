using System;

namespace GravityZero.HuntingSupport.Service.Context.Exceptions{
    public class SessionCloseException : Exception{
        public SessionCloseException(string message): base(message)
        {
            
        }
    }
}