using System.Collections.Generic;
using Hunt.ServiceContext.Domain;

namespace Hunt.Responses{

    public class RolesPayload{
        public IEnumerable<Role> Roles { get; private set; }

        private RolesPayload(IEnumerable<Role> roles){
            this.Roles = roles;
        }

        public static RolesPayload Create(IEnumerable<Role> roles){
            return new RolesPayload(roles);
        }
    }
}
