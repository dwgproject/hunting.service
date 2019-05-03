using System;
using System.Collections.Generic;
using GravityZero.HuntingSupport.Service.Context;
using GravityZero.HuntingSupport.Service.Context.Domain;
using GravityZero.HuntingSupport.Service.Response;
using Microsoft.AspNetCore.Mvc;

namespace GravityZero.Hunting.Service.Controllers
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
            return new JsonResult(ServiceResponse<IEnumerable<RoleServiceModel>>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }

        [HttpPost]
        public JsonResult AddRole([FromBody]RoleServiceModel role)
        {
            var queryResult = configuration.AddRole(role);
            return new JsonResult(ServiceResponse<string>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }

        [HttpDelete]
        public JsonResult DeleteRole([FromBody]Guid identifier)
        {
            var queryResult = configuration.DeleteRole(identifier);
            return new JsonResult(ServiceResponse<string>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }
    }
}
