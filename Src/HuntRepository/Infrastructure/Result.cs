namespace HuntRepository.Infrastructure{
    public class RepositoryResult<TData> {

        public bool IsSuccess { get; private set; }
        public TData Payload { get; private set; }
        public string Code { get; private set; }

        public RepositoryResult(bool isSuccess, TData payload)
        {
            this.IsSuccess = isSuccess;
            this.Payload = payload;
        }

        public RepositoryResult(bool isSuccess, TData payload, string code)
        {
            this.Code = code;
            this.IsSuccess = isSuccess;
            this.Payload = payload;
        }
    }
}