namespace HuntRepository.Infrastructure{
    public interface IModuleRepository<TResult, TParam>{

        TResult Add(TParam user);
        void Delete(TParam user);
        void Update(TParam user);
        TResult Find(TParam user);
        //IEnumerable<User> Get(Query )


    }


}