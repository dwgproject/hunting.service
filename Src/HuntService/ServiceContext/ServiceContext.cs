
using Hunt.ServiceContext;
using Hunt.ServiceContext.Domain;
using Hunt.ServiceContext.Exceptions;
using Hunt.ServiceContext.Extensions;
using Hunt.ServiceContext.Result;
using Hunt.ServiceContext.ServiceSession;
using System;
using System.Collections.Generic;
using HuntRepository.Extensions;
using System.Linq;
using HuntRepository.Infrastructure;

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

        public ServiceResult<User> SignIn(Authentication authentication)
        {
            Result<Model.User> getUserByAutheticationResult = repository.GetUserByAuthetication(authentication.Login, authentication.Password);            
            return getUserByAutheticationResult.IsSuccess ? 
                    ServiceResult<User>.Success(new User().ConverToUserService(getUserByAutheticationResult.Payload),"code") :
                        ServiceResult<User>.Failed(null,"code");//może jakiś Dummy Object
        }

        public ServiceResult<string> SignOut(Guid identifier)
        {
            try{
                session.Close(identifier);
            }catch (SessionCloseException ex){
                return ServiceResult<string>.Failed(ex.GetBaseException().Message,"code");
            }
            return ServiceResult<string>.Success(Context.session_closed_message, "code");
        }
    }
}



            // if (!result.IsSuccess)
            //     return new ServiceResult<User>(false, null);
            // var user = new User().ConverToUserService(result.Payload);
            // return new ServiceResult<User>(true, user);

            
        // public ServiceResult<string> SignUp(FullUser user)
        // {
        //     var result = repository.Add(user.ConverToUserRepository());

        //     return new ServiceResult<string>(result.IsSuccess, result.IsSuccess ? user_added_message : "Problem.");
        // }

        //return new ServiceResult<string>(false, ex.GetBaseException().Message);