using System;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Service.Context.Domain;
using GravityZero.HuntingSupport.Service.Context.Exceptions;
using GravityZero.HuntingSupport.Service.Session;
using GravityZero.HuntingSupport.Repository.Extension;

namespace GravityZero.HuntingSupport.Service.Context
{
    public class ServiceContext : IServiceContext
    {
        private static readonly string session_closed_message = "Session has been closed.";
        private static readonly string user_added_message = "User has been added.";

        private IUserRepository repository;
        private IUserSession session;
        public ServiceContext(IRepository repository)
        {
            this.repository = repository.UserRepository;
            this.session = new UserSession();
        }

        public bool CheckSession(Guid identifier)
        {
            return session.Get(identifier) != null;
        }

        public ServiceResult<Guid> SignIn(Authentication authentication)
        {
            var getUserByAutheticationResult = repository.GetUserByAuthetication(authentication.Login, authentication.Password);   
            if (getUserByAutheticationResult.IsSuccess){
                var newSession = new SessionUnit(getUserByAutheticationResult.Payload.Identifier, DateTime.Now);
                session.AddOrUpdate(newSession);
            }
            return getUserByAutheticationResult.IsSuccess ? 
                    ServiceResult<Guid>.Success(getUserByAutheticationResult.Payload.Identifier, "code") :
                        ServiceResult<Guid>.Failed(Guid.Empty,"code");
        }

        public ServiceResult<string> SignOut(Guid identifier)
        {
            try{
                session.Close(identifier);
            }catch (SessionCloseException ex){
                return ServiceResult<string>.Failed(ex.GetBaseException().Message,"code");
            }
            return ServiceResult<string>.Success(ServiceContext.session_closed_message, "code");
        }
    }
}