using System;

namespace GravityZero.HuntingSupport.Service.Context.Domain
{
    public class UserHuntingServiceModel
    {       
        public Guid UserId { get; set; }
        public UserServiceModel User { get; set; }
        public Guid HuntingId { get; set; }
        public HuntingServiceModel Hunting { get; set; }
    }
}