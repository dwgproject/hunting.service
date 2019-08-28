using System.Collections.Generic;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context.Extensions
{
    public static class UserConverter
    {
        public static User ConverToUserRepository(this FullUser fullUser){
            if(fullUser is null)
                return new User();
            return new User(){
                Identifier = fullUser.Identifier,
                Name = fullUser.Name,
                Surname = fullUser.Surname,
                Email = fullUser.Email,
                Password = fullUser.Password,
                Login = fullUser.Login,
                Role = fullUser.Role.ConvertToModel()
            };
        }
        
        public static UserServiceModel ConverToUserService(this User model){
            if(model is null)
                return new UserServiceModel();
            return new UserServiceModel(){
                Name = model.Name,
                Surname = model.Surname,
                Login = model.Login,
                Email = model.Email,
                Role =  new RoleServiceModel().ConvertToServiceRole(model.Role),        
                Identifier = model.Identifier
            };
        }

        public static UserServiceModel ConvertToservice(this UserServiceModel user, User model)
        {
            if(model is null)
                return new UserServiceModel();
            return new UserServiceModel(){
                Identifier = model.Identifier,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Login = model.Login,
                Role = new RoleServiceModel().ConvertToServiceRole(model.Role)
            };
        }

        public static FullUser ConverToFullUser(this User model){
            if(model is null)
                return new FullUser();
            return new FullUser(){
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Identifier = model.Identifier,
                Login = model.Login,
                Password = model.Password,
            };
        }

        public static User ConvertToUserModel(this UserServiceModel model){
            if(model is null)
                return new User();
            return new User(){
                Login = model.Login,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Identifier = model.Identifier                
            };           
        }

        public static ICollection<UserServiceModel> ConvertCollectionToService(this ICollection<UserServiceModel> users, ICollection<User> model){
            if(model is null)
                return new List<UserServiceModel>();
            var list = new List<UserServiceModel>();
            foreach (var item in model)
            {
                list.Add(item.ConverToUserService());
            }
            return list;
        }       
    }
}