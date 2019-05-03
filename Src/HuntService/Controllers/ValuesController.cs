using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GravityZero.HuntingSupport.Repository;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Session;
using Hunt.Eventing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GravityZero.Hunting.Service.Controllers
{
    public class ValuesController : ControllerBase
    {
        private IServiceContext context;
        private IHubContext<EventingHub> hubContext;
        private HuntContext huntContext;

        public ValuesController(IServiceContext context, IHubContext<EventingHub> hubContext, HuntContext huntContext)
        {
            this.context = context;
            this.hubContext = hubContext;
            this.huntContext = huntContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            
            await hubContext.Clients.All.SendAsync("test", $"Home page loaded at: {DateTime.Now}");
            //await hubContext.Clients.User("s").SendAsync("test", $"Home page loaded at: {DateTime.Now}"); //konkretny user
            //EventingHub.SendMessage("Invoke controller method");
            var user = new User();
            user.Identifier = Guid.NewGuid();
            user.Name = "Wiechu";

            huntContext.Users.Add(user);
            huntContext.SaveChanges();

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
//[Route("api/[controller]")]
//[ApiController]