using System.Collections.Generic;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Domain;

namespace GravityZero.HuntingSupport.Service.Context.Extensions
{
    public static class UserHuntingConverter
    {
        public static ICollection<UserHunting> ConvertCollectionToModel(this ICollection<UserHuntingServiceModel> model)
        {
            if(model is null)
                return new List<UserHunting>();
            IList<UserHunting> users = new List<UserHunting>();
            foreach (var item in model)
            {
                users.Add(item.ConvertToModel());
            }
            return users;
        }

        public static UserHunting ConvertToModel(this UserHuntingServiceModel model)
        {
            if(model is null)
                return new UserHunting();
            return new UserHunting(){
                HuntingId = model.HuntingId,
                UserId = model.UserId,
                //User = model.User.ConvertToUserModel(),
            };
        }
    }
}