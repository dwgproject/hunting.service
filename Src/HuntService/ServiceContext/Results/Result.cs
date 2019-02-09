namespace Hunt.ServiceContext.Results{

    public class Result<TResult>{
        public bool IsSuccess {get; private set;}
        public TResult Payload { get; private set; }

        public Result(bool isSuccess, TResult payload)
        {
            this.IsSuccess = isSuccess;
            this.Payload = payload;
        }
    }
}