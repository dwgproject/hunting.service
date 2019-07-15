using System;

namespace GravityZero.HuntingSupport.Service.Context.Domain
{
    public class PartialHuntersServiceModel
    {
        public Guid Identifier { get; set; }
        public UserServiceModel User { get;set; }
        // numer wylosowany na pojedynczy miot
        public int HunterNumber { get; set; }
    }
}