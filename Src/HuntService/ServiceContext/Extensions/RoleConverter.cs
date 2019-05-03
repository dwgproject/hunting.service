using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context.Extensions
{
    public static class RoleConverter{

        public static Role ConvertToModel(this RoleServiceModel role){
            return new Role(){
                Identifier = role.Identifier,
                Name = role.Name
            };
        }

        public static RoleServiceModel ConvertToServiceRole(this RoleServiceModel role, Role model){
            role.Identifier = model.Identifier;
            role.Name = model.Name;
            return role;
        }
    }
}
