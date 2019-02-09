using System;
using System.Linq;
using System.Collections.Generic;
using Hunt.Model;
using HuntRepository.Infrastructure;

namespace HuntRepository.Extensions{
    public static class QueryExtensions{

        public static Result<IEnumerable<User>> GetUsersByDate(this IUserRepository repository, DateTime from, DateTime to){
            
            Result<IEnumerable<User>> result = repository.Query((ux) => ux.Issued > from && ux.Issued < to);
            return result;
        }

        public static Result<IEnumerable<User>> GetUsersByAuthentication(this IUserRepository repository, string login, string password){
            return repository.Query((ux) => ux.Login == login && ux.Password == password);
        }

        public static Result<User> GetUserByAuthetication(this IUserRepository repository, string login, string password){
            var resultQuery = repository.Query((ux) => ux.Login == login && ux.Password == password);
            if (resultQuery.IsSuccess && resultQuery.Payload.Count() ==1)
                return new Result<User>(true, resultQuery.Payload.Single());
            return new Result<User>(false, null);
        }
    }
}