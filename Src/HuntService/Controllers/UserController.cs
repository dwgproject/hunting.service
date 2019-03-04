using Hunt.Data;
using Hunt.Eventing;
using Hunt.Responses;
using Hunt.ServiceContext;
using Hunt.ServiceContext.Domain;
using Hunt.ServiceContext.Result;
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
        private  IUserService userService;
        public UserController(IHubContext<EventingHub> hub, IServiceContext serviceContext, IUserService userService)
        {
            this.serviceContext = serviceContext;
            this.userService = userService;
            this.hub = hub;
        }

        [HttpGet]
        public JsonResult Get(Guid identifier)
        {
            ServiceResult<User> getResult = userService.Get(identifier);
            return new JsonResult(Response<User>.Create(getResult.IsSuccess, getResult.Payload));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            //example query
            //var queryResult = repository.GetUsersByDate(DateTime.Now, DateTime.Now);
            return new JsonResult("ok");
        }

        [HttpPost]
        public JsonResult SignUp([FromBody]FullUser user)
        {
            
            
            
            
            return new JsonResult("Ok");
            
        }
        
        [HttpPost]
        public JsonResult SignIn([FromBody]Authentication authentication){ // zaloguj się
            ServiceResult<Guid> authenticationResult = serviceContext.SignIn(authentication);
            return new JsonResult(Response<Guid>.Create(authenticationResult.IsSuccess, authenticationResult.Payload));
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


            // user.Identifier = Guid.NewGuid();
            // Result<User> result = repository.Add(user);
            
            // return MessagePayloadResponse.Success($"User {user.Identifier} has been added.");