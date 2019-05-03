using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Response
{
    public class UserPayload{
        public UserServiceModel User { get; private set; }

        private UserPayload(UserServiceModel user){
            this.User = user;
        }

        public static UserPayload Create(UserServiceModel user){
            return new UserPayload(user);
        }
    }
}