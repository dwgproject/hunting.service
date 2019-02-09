using System;
using System.Collections.Generic;

namespace HuntRepository.Infrastructure{
    public interface IModuleRepository<TResult, TModelParam, TIdentifierParam>{

        Result<TResult> Add(TModelParam user);
        void Delete(TIdentifierParam identifier);
        void Update(TModelParam user);
        Result<TResult> Find(TIdentifierParam identifier);
        Result<IEnumerable<TResult>> Query(Func<TModelParam, bool> query);
    }
}