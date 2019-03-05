namespace Hunt.Responses{
    public class Response<TData>{

        public bool IsSuccess { get; private set; }
        public string PayloadType { get; private set; }
        public TData Payload { get; private set; }
        public string Code { get; private set; }

        public static Response<TData> Create(bool isSuccess, TData payload, string code){
            return new Response<TData>(isSuccess, payload, code);
        }
        private Response(bool isSuccess, TData payload, string code){
            this.IsSuccess = isSuccess;
            this.Payload = payload;
            this.PayloadType = payload != null ? payload.GetType().Name : "Unknown";
        }
    }
}