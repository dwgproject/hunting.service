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
            Result<IEnumerable<Model.User>> queryResult = userRepository.Query(ux => ux.Login == user.Login);
            if (!queryResult.IsSuccess)
                return ServiceResult<string>.Failed(string.Empty, check_user_error);//tutaj error powinien przyjsc z repo
            if (queryResult.Payload.Any())
                return ServiceResult<string>.Failed(string.Empty, check_user_failed);

            Result<Model.User> result = userRepository.Add(user.ConverToUserRepository());
            if (!result.IsSuccess)
                return ServiceResult<string>.Failed(string.Empty, string.Empty);//kod od repo
                
            return ServiceResult<string>.Success(string.Empty, string.Empty);//kod z repo o dodaniu użytkownika
        }

        public ServiceResult<IEnumerable<User>> All()
        {
            throw new NotImplementedException();
        }

        public ServiceResult<string> Delete(Guid identifier)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<User> Get(Guid identifer)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<User> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}