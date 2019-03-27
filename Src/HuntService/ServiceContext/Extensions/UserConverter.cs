using Hunt.ServiceContext.Domain;

namespace Hunt.ServiceContext.Extensions{
    public static class UserConverter{

        public static Hunt.Model.User ConverToUserRepository(this FullUser fullUser){
            return new Model.User(){
                Identifier = fullUser.Identifier,
                Name = fullUser.Name,
                Surname = fullUser.Surname,
                Email = fullUser.Email,
                Password = fullUser.Password,
                Login = fullUser.Login
            };
        }
        
        public static User ConverToUserService(this Hunt.Model.User model){
            return new User(){
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Identifier = model.Identifier
            };
        }

    }
}