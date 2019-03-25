using Hunt.Data;
using Hunt.Eventing;
using Hunt.Model;
using Hunt.Responses;
using Hunt.ServiceContext;
using HuntRepository.Extensions;
using HuntRepository.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace Hunt.Controllers
{
    public class UserController : ControllerBase
    {
        private IServiceContext serviceContext;
        private IHubContext<EventingHub> hub;
        private IUserRepository repository;
        
        public UserController(IHubContext<EventingHub> hub, IServiceContext serviceContext, IRepository repository)
        {
            this.serviceContext = serviceContext;
            this.repository = repository.UserRepository;
            this.hub = hub;
        }

        [HttpGet]
        public JsonResult Get(Guid identifier)
        {
            if (identifier == Guid.Empty) 
                return MessagePayloadResponse.Failure("User don't exists.");
                       
            return new JsonResult("ok");
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            //example query
            var queryResult = repository.GetUsersByDate(DateTime.Now, DateTime.Now);
            return new JsonResult("ok");
        }

        [HttpPost]
        public JsonResult SignUp([FromBody]User user)
        {
            if (user == null) 
                return MessagePayloadResponse.Failure("User is Null.");

            if (user.Identifier != Guid.Empty){
                RepositoryResult<User> result = repository.Find(user.Identifier);
                
                    return MessagePayloadResponse.Failure($"User {result.Payload.Name} already exist.");
            }
            
            user.Identifier = Guid.NewGuid();
            RepositoryResult<User> result1 = repository.Add(user);
            
            return MessagePayloadResponse.Success($"User {user.Identifier} has been added.");
        }
        
        [HttpPost]
        public JsonResult SignIn([FromBody]User user){
            if (user == null) 
                return MessagePayloadResponse.Failure("User is Null.");
            
            if (user.Identifier == Guid.Empty)
                return MessagePayloadResponse.Failure($"User don't exist.");
                       
            
            return new JsonResult("Ok");
        }
        
        [HttpPost]
        public JsonResult SignOut(Guid identifier){
            if (identifier == Guid.Empty)
                return MessagePayloadResponse.Failure($"User don't exist.");
                
            
            return new JsonResult("Ok");
        }

        [HttpDelete]
        public JsonResult Delete(Guid identifier){
            if (identifier == Guid.Empty)
                return MessagePayloadResponse.Failure($"User don't exist.");
            
            return MessagePayloadResponse.Success("User has been deleted");
        }
    }
}