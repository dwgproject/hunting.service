using System;
using System.Linq;
using System.Collections.Generic;
using GravityZero.HuntingSupport.Repository.Infrastructure;
using GravityZero.HuntingSupport.Repository.Model;

namespace HuntRepository.Extensions
{
    public static class QueryExtensions{

        public static RepositoryResult<IEnumerable<User>> GetUsersByDate(this IUserRepository repository, DateTime from, DateTime to){
            
            RepositoryResult<IEnumerable<User>> result = repository.Query((ux) => ux.Issued > from && ux.Issued < to);
            return result;
        }

        public static RepositoryResult<IEnumerable<User>> GetUsersByAuthentication(this IUserRepository repository, string login, string password){
            return repository.Query((ux) => ux.Login == login && ux.Password == password);
        }

        public static RepositoryResult<User> GetUserByAuthetication(this IUserRepository repository, string login, string password){
            var resultQuery = repository.Query((ux) => ux.Login == login && ux.Password == password);
            if (resultQuery.IsSuccess && resultQuery.Payload.Count() ==1)
                return new RepositoryResult<User>(true, resultQuery.Payload.Single());
            return new RepositoryResult<User>(false, null);
        }
    }
}