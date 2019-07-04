using System;
using System.Collections.Generic;
using GravityZero.HuntingSupport.Service.Context;
using GravityZero.HuntingSupport.Service.Context.Domain;
using GravityZero.HuntingSupport.Service.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public JsonResult DeleteRole(Guid id)
        {
            var queryResult = configuration.DeleteRole(id);
            return new JsonResult(ServiceResponse<string>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }

        [HttpPost]
        public JsonResult GetRole([FromBody] string name)
        {
            string input = JsonConvert.DeserializeObject<string>(name);
            var queryResult = configuration.GetRole(input);
            return new JsonResult(ServiceResponse<IEnumerable<RoleServiceModel>>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code)); 
        }

        [HttpPut]
        public JsonResult UpdateRole([FromBody] RoleServiceModel role)
        {
            var queryResult = configuration.UpdateRole(role);
            return new JsonResult(ServiceResponse<string>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }
    }
}
