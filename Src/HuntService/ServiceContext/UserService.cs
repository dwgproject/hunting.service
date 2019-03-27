using System;
using System.Linq;
using System.Collections.Generic;
using Hunt.ServiceContext.Domain;
using Hunt.ServiceContext.Result;
using HuntRepository.Infrastructure;
using Hunt.ServiceContext.Extensions;

namespace Hunt.ServiceContext{
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
            RepositoryResult<IEnumerable<Model.User>> queryResult = userRepository.Query(ux => ux.Login == user.Login);
            if (!queryResult.IsSuccess)
                return ServiceResult<string>.Failed(string.Empty, check_user_error);//tutaj error powinien przyjsc z repo
            if (queryResult.Payload.Any())
                return ServiceResult<string>.Failed(string.Empty, check_user_failed);

            RepositoryResult<Model.User> result = userRepository.Add(user.ConverToUserRepository());
            if (!result.IsSuccess)
                return ServiceResult<string>.Failed(string.Empty, string.Empty);//kod od repo
                
            return ServiceResult<string>.Success(string.Empty, string.Empty);//kod z repo o dodaniu użytkownika
        }

        public ServiceResult<IEnumerable<User>> All()
        {
            RepositoryResult<IEnumerable<Model.User>> queryResult = userRepository.Query(qx => { return true; });
            if (queryResult.IsSuccess){
                IList<User> users = new List<User>();
                foreach (Model.User user in  queryResult.Payload)
                    users.Add(user.ConverToUserService());
                return ServiceResult<IEnumerable<User>>.Failed(users, queryResult.Code); 
            }
            return ServiceResult<IEnumerable<User>>.Failed(Enumerable.Empty<User>(), queryResult.Code);
        }

        public ServiceResult<string> Delete(Guid identifier)
        {
            RepositoryResult<string> deleteResult = userRepository.Delete(identifier);
            return deleteResult.IsSuccess ? 
                        ServiceResult<string>.Success(deleteResult.Payload, deleteResult.Code) : 
                            ServiceResult<string>.Failed(deleteResult.Payload, deleteResult.Code);
        }

        public ServiceResult<User> Get(Guid identifer)
        {
            RepositoryResult<Model.User> findResult = userRepository.Find(identifer);
            return findResult.IsSuccess ? 
                        ServiceResult<User>.Success(findResult.Payload.ConverToUserService() ,findResult.Code) : 
                            ServiceResult<User>.Failed(findResult.Payload.ConverToUserService(),findResult.Code);
        }

        public ServiceResult<User> Update(FullUser user)
        {
            throw new ApplicationException();
            // RepositoryResult<string> updateResult = userRepository.Update(user.ConverToUserRepository());
            // return updateResult.IsSuccess ? 
            //             ServiceResult<User>.Success(updateResult.Payload, updateResult.Code) : 
            //                 ServiceResult<string>.Failed(updateResult.Payload, updateResult.Code);
        }
    }
}