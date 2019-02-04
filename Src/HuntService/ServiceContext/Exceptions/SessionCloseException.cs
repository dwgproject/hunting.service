using System;

namespace Hunt.ServiceContext.Exceptions{
    public class SessionCloseException : Exception{
        public SessionCloseException(string message): base(message)
        {
            
        }
    }
}