using Hunt.Data;
using Hunt.Eventing;
using Hunt.Model;
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
        public IActionResult Get(string identifier)
        {
            return new JsonResult("ok");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new JsonResult("ok");
        }

        // api/User/SignIn
        [HttpPost]
        public IActionResult SignIn(string identifier){


            //huntContext.Users.Add();
            return new JsonResult("Ok");
        }
        
        [HttpPost]
        public IActionResult SignUp([FromBody]User user)
        {
            // User found = huntContext.Find<User>(user.Identifier);
            // if (found != null)
            //     return new JsonResult("Istnieje");

            // try
            // {
            //     user.Identifier = Guid.NewGuid();
            //     huntContext.Add<User>(user);
            // }
            // catch
            // {

            // }

            return new JsonResult("Ok");
        }
        // api/User/SignOut
        public IActionResult SignOut(){
            return new JsonResult("Ok");
        }

        [HttpDelete]
        public IActionResult Delete(){
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