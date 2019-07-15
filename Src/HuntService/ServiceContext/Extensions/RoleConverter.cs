using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context.Extensions
{
    public static class RoleConverter{

        public static Role ConvertToModel(this RoleServiceModel role){
            if(role is null)
                return new Role();
            return new Role(){
                Identifier = role.Identifier,
                Name = role.Name
            };
        }

        public static RoleServiceModel ConvertToServiceRole(this RoleServiceModel role, Role model){
            if(model is null){
                return new RoleServiceModel();
            }
            role.Identifier = model.Identifier;
            role.Name = model.Name;
            return role;
        }
    }
}
