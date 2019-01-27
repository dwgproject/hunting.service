using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using HuntingHelperWebService.Eventing;

namespace HuntingHelperWebService.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController] //reprezentuje api controllera
    public class UserController : ControllerBase
    {
        private IHubContext<EventingHub> hub;
        public UserController(IHubContext<EventingHub> hub)
        {
            this.hub = hub;
        }
        // api/User/SignIn
        [HttpPost]
        public void SignIn(){

        }
        // api/User/SignUp
        [HttpPost]
        public void SignUp(){

        }
        // api/User/SignOut
        public void SignOut(){

        }

        [HttpDelete]
        public void Delete(){

        }


    }
}

//add 
//log 
//update 
//delete

//public ActionResult<IEnumerable<string>> 