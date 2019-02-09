using System;

namespace Hunt.ServiceContext.ServiceSession{
    public class Session{
        public Guid Identifier { get; private set; }
        public bool IsActive { get; set;}
        public long LastTick { get; set; }
        public DateTime StartTime { get; private set; }
        public DateTime StopTime { get; private set; }
    }
}