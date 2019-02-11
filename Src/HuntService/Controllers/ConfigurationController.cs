using System;
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
            if (queryResult.IsSuccess)
                return new JsonResult(Response<RolesPayload>.Create(queryResult.IsSuccess, RolesPayload.Create(queryResult.Payload)));
            return new JsonResult(Response<RolesPayload>.Create(queryResult.IsSuccess, RolesPayload.Create(Enumerable.Empty<Role>())));
        }

        [HttpPost]
        public JsonResult AddRole([FromBody]Role role)
        {
            var queryResult = configuration.AddRole(role);
            return  queryResult.IsSuccess ? MessagePayloadResponse.Success(queryResult.Payload) : MessagePayloadResponse.Failure(queryResult.Payload);
        }

        [HttpDelete]
        public JsonResult DeleteRole([FromBody]Guid identifier)
        {
            var queryResult = configuration.DeleteRole(identifier);
            return queryResult.IsSuccess ? MessagePayloadResponse.Success(queryResult.Payload) : MessagePayloadResponse.Failure(queryResult.Payload);
        }
    }
}
