using System;
using System.Collections.Generic;
using System.Linq;
using Hunt.Responses;
using Hunt.ServiceContext;
using Hunt.ServiceContext.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Hunt.Controllers
{
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService configuration;
        public ConfigurationController(IConfigurationService configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetAllRoles()
        {
            var queryResult = configuration.GetRoles();
            return new JsonResult(Response<IEnumerable<Role>>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }

        [HttpPost]
        public JsonResult AddRole([FromBody]Role role)
        {
            var queryResult = configuration.AddRole(role);
            return new JsonResult(Response<string>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }

        [HttpDelete]
        public JsonResult DeleteRole([FromBody]Guid identifier)
        {
            var queryResult = configuration.DeleteRole(identifier);
            return new JsonResult(Response<string>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }
    }
}
