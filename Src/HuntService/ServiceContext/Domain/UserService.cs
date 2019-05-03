using System;

namespace GravityZero.HuntingSupport.Service.Context.Domain
{
    public class UserServiceModel
    {
        public Guid Identifier { get; set; }
        //public DateTime Issued { get; set; }
        //public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public RoleServiceModel Role { get; set; }
    }
}