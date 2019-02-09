using System;

namespace Hunt.ServiceContext.Exceptions{
    public class SessionAddException : Exception{
        public SessionAddException(string message): base(message)
        {
            
        }
    }
}
