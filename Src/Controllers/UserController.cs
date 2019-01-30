using Hunt.Data;
using Hunt.Eventing;
using Hunt.Model;
using Hunt.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace Hunt.Controllers
{
    public class UserController : ControllerBase
    {
        private IHubContext<EventingHub> hub;
        private HuntContext huntContext;

        public UserController(IHubContext<EventingHub> hub, HuntContext huntContext)
        {
            this.huntContext = huntContext;
            this.hub = hub;
        }

        [HttpGet]
        public JsonResult Get(string identifier)
        {
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
            if (user == null) return new JsonResult(new Response(false, "User is null."));            
            if (user.Identifier != Guid.Empty){
                User found = huntContext.Find<User>(user.Identifier);
                return new JsonResult(new Response(false, $"User {found.Name} already exist."));
            } 
        
            try
            {
                user.Identifier = Guid.NewGuid();
                huntContext.Add<User>(user);
                huntContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response(true, $"Problem z dodaniem user {user.Name}, {user.Identifier}, {ex.Message}"));
            }

            return new JsonResult(new Response(true, $"Dodano user {user.Name}, {user.Identifier}"));
        }

        // api/User/SignIn
        [HttpPost]
        public JsonResult SignIn(string identifier){


            //huntContext.Users.Add();
            return new JsonResult("Ok");
        }
        
        
        // api/User/SignOut
        public JsonResult SignOut(){
            return new JsonResult("Ok");
        }

        [HttpDelete]
        public JsonResult Delete(){
            return new JsonResult("Ok");
        }
    }
}

//add // api/User/SignUp
//log 
//update 
//delete

//public ActionResult<IEnumerable<string>> 

// [Route("api/[controller]")]
// [ApiController] //reprezentuje api controllera