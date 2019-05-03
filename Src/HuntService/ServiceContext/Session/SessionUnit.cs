using System;

namespace GravityZero.HuntingSupport.Service.Session
{
    public class SessionUnit
    {
        public Guid Identifier { get; private set; }
        public bool IsActive { get; set;}
        public DateTime LastTick { get; set;}
        public DateTime StartTime { get; private set; }
        public DateTime StopTime { get; private set; }

        public SessionUnit(Guid identifier, DateTime startTime)
        {
            this.Identifier = identifier;
            this.StartTime = startTime;
            this.LastTick = startTime;
        }
    }
}