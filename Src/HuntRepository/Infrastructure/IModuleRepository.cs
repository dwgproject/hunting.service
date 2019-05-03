using System;
using System.Collections.Generic;

namespace GravityZero.HuntingSupport.Repository.Infrastructure
{
    public interface IModuleRepository<TResult, TModelParam, TIdentifierParam>{

        RepositoryResult<TResult> Add(TModelParam user);
        RepositoryResult<string> Delete(TIdentifierParam identifier);
        RepositoryResult<TResult> Update(TModelParam user);
        RepositoryResult<TResult> Find(TIdentifierParam identifier);
        RepositoryResult<IEnumerable<TResult>> Query(Func<TModelParam, bool> query);
    }
}