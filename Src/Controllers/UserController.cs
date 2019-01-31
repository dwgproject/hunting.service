using Hunt.Data;
using Hunt.Eventing;
using Hunt.Model;
using Hunt.Responses;
using Hunt.ServiceContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace Hunt.Controllers
{
    public class UserController : ControllerBase
    {
        private IServiceContext serviceContext;
        private IHubContext<EventingHub> hub;
        private HuntContext huntContext;
        
        public UserController(IHubContext<EventingHub> hub, HuntContext huntContext, IServiceContext serviceContext)
        {
            this.serviceContext = serviceContext;
            this.huntContext = huntContext;
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
            return new JsonResult("ok");
        }

        [HttpPost]
        public JsonResult SignUp([FromBody]User user)
        {
            if (user == null) 
                return MessagePayloadResponse.Failure("User is Null.");

            if (user.Identifier != Guid.Empty){
                User found = huntContext.Find<User>(user.Identifier);
                    return MessagePayloadResponse.Failure($"User {found.Name} already exist.");
            } 
        
            try
            {
                user.Identifier = Guid.NewGuid();
                huntContext.Add<User>(user);
                huntContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return MessagePayloadResponse.Failure($"Problem z dodaniem user {user.Name}, {user.Identifier}, {ex.Message}");
            }

            return MessagePayloadResponse.Success($"User {user.Identifier} has been added.");
        }
        
        [HttpPost]
        public JsonResult SignIn([FromBody]User user){
            if (user == null) 
                return MessagePayloadResponse.Failure("User is Null.");
            
            if (user.Identifier == Guid.Empty)
                return MessagePayloadResponse.Failure($"User don't exist.");
                
            User found = huntContext.Find<User>(user.Identifier);
            serviceContext.CreateSession(found);
            
            return new JsonResult("Ok");
        }
        
        [HttpPost]
        public JsonResult SignOut(Guid identifier){
            if (identifier == Guid.Empty)
                return MessagePayloadResponse.Failure($"User don't exist.");
                
            User found = huntContext.Find<User>(identifier);
            serviceContext.DestoySession(found);
            return new JsonResult("Ok");
        }

        [HttpDelete]
        public JsonResult Delete(Guid identifier){
            if (identifier == Guid.Empty)
                return MessagePayloadResponse.Failure($"User don't exist.");
            try{
                User found = huntContext.Find<User>(identifier);
                huntContext.Remove<User>(found);
            }catch(Exception ex){
                return MessagePayloadResponse.Failure($"Problem z usunięciem user: {ex.Message}");
            }
            return MessagePayloadResponse.Success("User has been deleted");
        }
    }
}