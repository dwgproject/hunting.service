using System;
using System.Collections.Generic;

namespace HuntRepository.Infrastructure{
    public interface IModuleRepository<TResult, TParam>{

        Result<TResult> Add(TParam user);
        void Delete(TParam user);
        void Update(TParam user);
        Result<TResult> Find(TParam user);
        Result<IEnumerable<TResult>> Query(Func<TParam, bool> query);
    }
}