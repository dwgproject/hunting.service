using System;


namespace Hunt.Model
{
    public class HunterHunting
    {
        public Guid HunterId { get; set; }
        public Hunter Hunter { get; set; }
        public Guid HuntingId { get; set; }
        public Hunting Hunting { get; set; }
    }
}