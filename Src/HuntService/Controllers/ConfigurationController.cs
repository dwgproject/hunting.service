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

        [HttpGet]
        public JsonResult GetRole(string id)
        {
            //string input = JsonConvert.DeserializeObject<string>(id);
            var queryResult = configuration.GetRole(id);
            return new JsonResult(ServiceResponse<IEnumerable<RoleServiceModel>>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code)); 
        }

        [HttpPut]
        public JsonResult UpdateRole([FromBody] RoleServiceModel role)
        {
            var queryResult = configuration.UpdateRole(role);
            return new JsonResult(ServiceResponse<string>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }

        [HttpPost]
        public JsonResult AddAnimal([FromBody] AnimalServiceModel animal)
        {
            var addResult = configuration.AddAnimal(animal);
            return new JsonResult(ServiceResponse<string>.Create(addResult.IsSuccess,addResult.Payload,addResult.Code));
        }

        [HttpGet]
        public JsonResult GetAllAnimals()
        {
            var queryResult = configuration.GetAnimals();
            return new JsonResult(ServiceResponse<IEnumerable<AnimalServiceModel>>.Create(queryResult.IsSuccess,queryResult.Payload,queryResult.Code));
        }

        [HttpGet]
        public JsonResult GetAnimal(string id)
        {
            var queryResult = configuration.GetAnimal(id);
            return new JsonResult(ServiceResponse<IEnumerable<AnimalServiceModel>>.Create(queryResult.IsSuccess,queryResult.Payload,queryResult.Code));
        }

        [HttpPut]
        public JsonResult UpdateAnimal([FromBody] AnimalServiceModel animal)
        {
            var updateResult = configuration.UpdateAnimal(animal);
            return new JsonResult(ServiceResponse<string>.Create(updateResult.IsSuccess,updateResult.Payload,updateResult.Code));
        }

        [HttpDelete]
        public JsonResult DeleteAnimal(Guid id)
        {
            var deleteResult = configuration.DeleteAnimal(id);
            return new JsonResult(ServiceResponse<string>.Create(deleteResult.IsSuccess,deleteResult.Payload,deleteResult.Code));
        }
    }
}
