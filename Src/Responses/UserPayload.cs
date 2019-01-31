using Hunt.Model;

namespace Hunt.Responses{

    public class UserPayload{
        public User User { get; private set; }

        private UserPayload(User user){
            this.User = user;
        }

        public static UserPayload Create(User user){
            return new UserPayload(user);
        }
    }
}