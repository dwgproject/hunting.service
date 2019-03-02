namespace Hunt.ServiceContext.Result{

    public class ServiceResult<TResult>{
        public bool IsSuccess {get; private set;}
        public TResult Payload { get; private set; }
        public string Code { get; private set; }

        public ServiceResult(bool isSuccess, TResult payload, string code)
        {
            this.IsSuccess = isSuccess;
            this.Payload = payload;
            this.Code = code;
        }

        public static ServiceResult<TResult> Success(TResult payload, string code){
            return new ServiceResult<TResult>(true, payload, code);
        }

        public static ServiceResult<TResult> Failed(TResult payload, string code){
            return new ServiceResult<TResult>(false, payload, code);
        }
    }

    // public class GetServiceDataResult<TPayload> : ServiceResult<TPayload>{

    // }
}