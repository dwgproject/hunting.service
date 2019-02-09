
using Hunt.ServiceContext;
using Hunt.ServiceContext.Domain;
using Hunt.ServiceContext.Exceptions;
using Hunt.ServiceContext.Extensions;
using Hunt.ServiceContext.Results;
using Hunt.ServiceContext.ServiceSession;
using System;
using System.Collections.Generic;
using HuntRepository.Extensions;
using System.Linq;

namespace Hunt.ServiceContext
{
    public class Context : IServiceContext
    {
        private static readonly string session_closed_message = "Session has been closed.";
        private static readonly string user_added_message = "User has been added.";

        private HuntRepository.Infrastructure.IUserRepository repository;
        private IUserSession session;
        public Context(HuntRepository.Infrastructure.IRepository repository)
        {
            this.repository = repository.UserRepository;
            this.session = new UserSession();
        }

        public bool CheckSession(Guid identifier)
        {
            return session.Get(identifier) != null;
        }

        public Result<User> SignIn(Authentication authentication)
        {
            var result = repository.GetUserByAuthetication(authentication.Login, authentication.Password);            
            if (!result.IsSuccess)
                return new Result<User>(false, null);
            var user = new User().ConverToUserService(result.Payload);
            return new Result<User>(true, user);
        }

        public Result<string> SignOut(Guid identifier)
        {
            try{
                session.Close(identifier);
            }catch (SessionCloseException ex){
                return new Result<string>(false, ex.GetBaseException().Message);
            }
            return new Result<string>(true, Context.session_closed_message);
        }

        public Result<string> SignUp(FullUser user)
        {
            var result = repository.Add(user.ConverToUserRepository());
            return new Result<string>(result.IsSuccess, result.IsSuccess ? user_added_message : "Problem.");
        }
    }
}
//GetUsersByAuthentication(authentication.Login, authentication.Password);