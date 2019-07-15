using System;

namespace GravityZero.HuntingSupport.Service.Context.Domain
{
    public class QuarryServiceModel
    {
        public Guid Identifier { get; set; }
        public AnimalServiceModel Animal { get; set; }
        public int Amount { get; set; }
    }
}