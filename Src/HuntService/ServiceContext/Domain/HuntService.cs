using System;
using System.Collections.Generic;

namespace GravityZero.HuntingSupport.Service.Context.Domain
{
    public class HuntingServiceModel
    {
        public Guid Identifier { get; set; }
        public DateTime Issued { get; set; }
        public UserServiceModel Leader { get; set; }
        public StatusServiceModel Status {get;set;}
        public string Description { get; set; }
        public virtual ICollection<UserHuntingServiceModel> Users { get; set; }
        public ICollection<QuarryServiceModel> Quarries { get; set; }
        //wszystkie mioty na polowanie
        public ICollection<PartialHuntingServiceModel> PartialHuntings {get; set;}
    }
}