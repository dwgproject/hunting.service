using System;
using System.Collections.Generic;

namespace GravityZero.HuntingSupport.Service.Context.Domain
{
    public class PartialHuntingServiceModel
    {
        public Guid Identifier { get; set; }
        public int Number { get; set; }
        public HuntingServiceModel Hunting { get;set; }
        public StatusServiceModel Status { get;set; }
        public ICollection<PartialHuntersServiceModel> PartialHunters {get; set;}
    }
}