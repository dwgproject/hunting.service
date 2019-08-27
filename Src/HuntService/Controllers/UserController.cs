using GravityZero.HuntingSupport.Service.Context;
using GravityZero.HuntingSupport.Service.Context.Domain;
using GravityZero.HuntingSupport.Service.Response;
using GravityZero.HuntingSupport.Service.Session;
using Hunt.Eventing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace GravityZero.Hunting.Service.Controllers
{
    public class UserController : ControllerBase
    {
        private IServiceContext serviceContext;
        private IHubContext<EventingHub> hub;
        private  IUserService userService;
        public UserController(IHubContext<EventingHub> hub, IServiceContext serviceContext, IUserService userService)
        {
            this.serviceContext = serviceContext;
            this.userService = userService;
            this.hub = hub;
        }

        [HttpGet]
        public JsonResult Get(Guid identifier)
        {
            ServiceResult<UserServiceModel> getResult = userService.Get(identifier);
            return new JsonResult(ServiceResponse<UserServiceModel>.Create(getResult.IsSuccess, getResult.Payload, getResult.Code));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            ServiceResult<IEnumerable<UserServiceModel>> allResult =  userService.All();
            return new JsonResult(ServiceResponse<IEnumerable<UserServiceModel>>.Create(allResult.IsSuccess, allResult.Payload, allResult.Code));
        }

        [HttpPost]
        public JsonResult SignUp([FromBody]FullUser user)
        {
            ServiceResult<string> addResult = userService.Add(user);
            return new JsonResult(ServiceResponse<string>.Create(addResult.IsSuccess, addResult.Payload, addResult.Code));
        }
        
        [HttpPost]
        public JsonResult SignIn([FromBody]Authentication authentication)
        {
            ServiceResult<Guid> authenticationResult = serviceContext.SignIn(authentication);
            return new JsonResult(ServiceResponse<Guid>.Create(authenticationResult.IsSuccess, authenticationResult.Payload, authenticationResult.Code));
        }
        
        [HttpPost]
        public JsonResult SignOut(Guid identifier)
        {
            ServiceResult<string> signOutResult = serviceContext.SignOut(identifier);
            return new JsonResult(ServiceResponse<string>.Create(signOutResult.IsSuccess, signOutResult.Payload, signOutResult.Code));
        }

        [HttpDelete]
        public JsonResult Delete(Guid id)
        {
            ServiceResult<string> deleteResult = userService.Delete(id);
            return new JsonResult(ServiceResponse<string>.Create(deleteResult.IsSuccess, deleteResult.Payload, deleteResult.Code));
        }

        [HttpGet]
        public JsonResult GetByLogin(string id)
        {
            var getByLoginResult = userService.GetByLogin(id);
            return new JsonResult(ServiceResponse<IEnumerable<UserServiceModel>>.Create(getByLoginResult.IsSuccess, getByLoginResult.Payload, getByLoginResult.Code));
        }

        [HttpPut]
        public JsonResult Update([FromBody] FullUser user)
        {
            var updateResult = userService.Update(user);
            return new JsonResult(ServiceResponse<UserServiceModel>.Create(updateResult.IsSuccess, updateResult.Payload, updateResult.Code));
        }

        [HttpGet]
        public JsonResult KeepAlive(Guid identifier)
        {
            bool checkSessionResult = serviceContext.CheckSession(identifier);
            return new JsonResult(ServiceResponse<Guid>.Create(checkSessionResult, identifier, "US01"));
        }
    }
}