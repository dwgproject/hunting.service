using Hunt.Model;

namespace Hunt.ServiceContext.Extensions{

    public static class RoleConverter{

        public static Role ConvertToModel(this Hunt.ServiceContext.Domain.Role role){
            return new Role(){
                Identifier = role.Identifier,
                Name = role.Name
            };
        }

        public static Hunt.ServiceContext.Domain.Role ConvertToServiceRole(this Hunt.ServiceContext.Domain.Role role, Role model){
            role.Identifier = model.Identifier;
            role.Name = model.Name;
            return role;
        }
    }
}
