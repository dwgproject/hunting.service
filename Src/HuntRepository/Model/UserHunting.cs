using System;

namespace Hunt.Model
{
    public class UserHunting
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid HuntingId { get; set; }
        public Hunting Hunting { get; set; }
    }
}