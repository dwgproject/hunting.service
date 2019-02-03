using System;
using System.Collections.Generic;
using Hunt.Model;
using HuntRepository.Infrastructure;

namespace HuntRepository.Extensions{
    public static class QueryExtensions{

        public static Result<IEnumerable<User>> GetUsersByDate(this IUserRepository repository, DateTime from, DateTime to){
            
            Result<IEnumerable<User>> result = repository.Query((ux) => ux.Issued > from && ux.Issued < to);
            return result;

        }
    }
}