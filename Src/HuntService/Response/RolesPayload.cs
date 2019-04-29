using System.Collections.Generic;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Response
{
    public class RolesPayload{
        public IEnumerable<RoleServiceModel> Roles { get; private set; }

        private RolesPayload(IEnumerable<RoleServiceModel> roles){
            this.Roles = roles;
        }

        public static RolesPayload Create(IEnumerable<RoleServiceModel> roles){
            return new RolesPayload(roles);
        }
    }
}
