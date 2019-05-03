using System;
using System.Linq;
using System.Collections.Generic;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Service.Context.Domain;
using GravityZero.HuntingSupport.Repository.Model;
using GravityZero.HuntingSupport.Service.Context.Extensions;

namespace GravityZero.HuntingSupport.Service.Context
{
    public class UserService : IUserService
    {
        private static readonly string check_user_error = "us1";//Wystąpił błąd podczas sprawdzania 
        private static readonly string check_user_failed = "us2";//Uzytkoenik o tym identyfikatorze już istnieje
        private static readonly string check_user_empty_data = "us3";//Puste dane

        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ServiceResult<string> Add(FullUser user)
        {   if (user == null || user.Login == null)
                return ServiceResult<string>.Failed(string.Empty, check_user_empty_data);
            RepositoryResult<IEnumerable<User>> queryResult = userRepository.Query(ux => ux.Login == user.Login);
            if (!queryResult.IsSuccess)
                return ServiceResult<string>.Failed(string.Empty, check_user_error);//tutaj error powinien przyjsc z repo
            if (queryResult.Payload.Any())
                return ServiceResult<string>.Failed(string.Empty, check_user_failed);

            RepositoryResult<User> result = userRepository.Add(user.ConverToUserRepository());
            if (!result.IsSuccess)
                return ServiceResult<string>.Failed(string.Empty, string.Empty);//kod od repo
                
            return ServiceResult<string>.Success(string.Empty, string.Empty);//kod z repo o dodaniu użytkownika
        }

        public ServiceResult<IEnumerable<UserServiceModel>> All()
        {
            RepositoryResult<IEnumerable<User>> queryResult = userRepository.Query(qx => { return true; });
            if (queryResult.IsSuccess){
                IList<UserServiceModel> users = new List<UserServiceModel>();
                foreach (User user in  queryResult.Payload)
                    users.Add(user.ConverToUserService());
                return ServiceResult<IEnumerable<UserServiceModel>>.Failed(users, queryResult.Code); 
            }
            return ServiceResult<IEnumerable<UserServiceModel>>.Failed(Enumerable.Empty<UserServiceModel>(), queryResult.Code);
        }

        public ServiceResult<string> Delete(Guid identifier)
        {
            RepositoryResult<string> deleteResult = userRepository.Delete(identifier);
            return deleteResult.IsSuccess ? 
                        ServiceResult<string>.Success(deleteResult.Payload, deleteResult.Code) : 
                            ServiceResult<string>.Failed(deleteResult.Payload, deleteResult.Code);
        }

        public ServiceResult<UserServiceModel> Get(Guid identifer)
        {
            RepositoryResult<User> findResult = userRepository.Find(identifer);
            return findResult.IsSuccess ? 
                        ServiceResult<UserServiceModel>.Success(findResult.Payload.ConverToUserService() ,findResult.Code) : 
                            ServiceResult<UserServiceModel>.Failed(new UserServiceModel() ,findResult.Code);
        }

        public ServiceResult<FullUser> Update(FullUser user)
        {
            RepositoryResult<User> updateResult = userRepository.Update(user.ConverToUserRepository());
            return updateResult.IsSuccess ? 
                        ServiceResult<FullUser>.Success(updateResult.Payload.ConverToFullUser(), updateResult.Code) : 
                            ServiceResult<FullUser>.Failed(null, updateResult.Code);
        }
    }
}