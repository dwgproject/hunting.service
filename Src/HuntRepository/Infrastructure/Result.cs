namespace HuntRepository.Infrastructure{
    public class Result<TData> {

        public bool IsSuccess { get; private set; }
        public TData Payload { get; private set; }

        public Result(bool isSuccess, TData payload)
        {
            this.IsSuccess = isSuccess;
            this.Payload = payload;
        }
    }
}