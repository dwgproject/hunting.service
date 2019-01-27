using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuntingHelperWebService.ApplicationContext;
using HuntingHelperWebService.Eventing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HuntingHelperWebService.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ValuesController : ControllerBase
    {
        private IApplicationContext context;
        private IHubContext<EventingHub> hubContext;

        public ValuesController(IApplicationContext context, IHubContext<EventingHub> hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            
            await hubContext.Clients.All.SendAsync("test", $"Home page loaded at: {DateTime.Now}");
            //await hubContext.Clients.User("s").SendAsync("test", $"Home page loaded at: {DateTime.Now}"); //konkretny user
            //EventingHub.SendMessage("Invoke controller method");
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
