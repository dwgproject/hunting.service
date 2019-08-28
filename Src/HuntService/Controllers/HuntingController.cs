using System;
using System.Collections.Generic;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context;
using GravityZero.HuntingSupport.Service.Context.Domain;
using GravityZero.HuntingSupport.Service.Response;
using Microsoft.AspNetCore.Mvc;

namespace GravityZero.Hunting.Service.Controllers
{
    public class HuntingController: ControllerBase
    {
        private readonly IHuntingService hunting;

        public HuntingController(IHuntingService hunting)
        {
            this.hunting = hunting;
        }

        [HttpPost]
        public JsonResult AddQuarry([FromBody] QuarryServiceModel quarry)
        {
            var addResult = hunting.AddQuarry(quarry);
            return new JsonResult(ServiceResponse<string>.Create(addResult.IsSuccess, addResult.Payload, addResult.Code));
        }

        [HttpGet]
        public JsonResult GetQuarries()
        {
            var queryResult = hunting.GetQuarries();
            return new JsonResult(ServiceResponse<IEnumerable<QuarryServiceModel>>.Create(queryResult.IsSuccess,queryResult.Payload,queryResult.Code));
        }

        [HttpGet]
        public JsonResult GetQuarry(Guid id)
        {
            var queryResult = hunting.GetQuarry(id);
            return new JsonResult(ServiceResponse<IEnumerable<QuarryServiceModel>>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }

        [HttpDelete]
        public JsonResult DeleteQuarry(Guid id)
        {
            var deleteResult = hunting.DeleteQuarry(id);
            return new JsonResult(ServiceResponse<string>.Create(deleteResult.IsSuccess, deleteResult.Payload, deleteResult.Code)); 
        }

        [HttpPut]
        public JsonResult UpdateQuarry([FromBody] QuarryServiceModel quarry)
        {
            var updateResult = hunting.UpdateQuarry(quarry);
            return new JsonResult(ServiceResponse<string>.Create(updateResult.IsSuccess,updateResult.Payload,updateResult.Code));
        }

        [HttpPost]
        public JsonResult AddHunting([FromBody] HuntingServiceModel hunt)
        {
            var addResult = hunting.AddHunting(hunt);
            return new JsonResult(ServiceResponse<string>.Create(addResult.IsSuccess, addResult.Payload, addResult.Code));
        }

        [HttpGet]
        public JsonResult GetHuntings()
        {
            var queryResult = hunting.GetHuntings();
            return new JsonResult(ServiceResponse<IEnumerable<HuntingServiceModel>>.Create(queryResult.IsSuccess,queryResult.Payload,queryResult.Code));
        }

        [HttpGet]
        public JsonResult GetHunting(Guid id)
        {
            var queryResult = hunting.GetHunting(id);
            return new JsonResult(ServiceResponse<IEnumerable<HuntingServiceModel>>.Create(queryResult.IsSuccess, queryResult.Payload, queryResult.Code));
        }

        [HttpPut]
        public JsonResult UpdateHunting([FromBody] HuntingServiceModel hunt)
        {
            var updateResult = hunting.UpdateHunting(hunt);
            return new JsonResult(String.Empty);
        }
    }
}