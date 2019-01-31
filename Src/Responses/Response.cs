namespace Hunt.Responses{
    public class Response<TData>{

        public bool IsSuccess { get; private set; }
        public string PayloadType { get; private set; }
        public TData Payload { get; private set; }

        public static Response<TData> Create(bool isSuccess, TData payload){
            return new Response<TData>(isSuccess, payload);
        }
        private Response(bool isSuccess, TData payload){
            this.IsSuccess = isSuccess;
            this.Payload = payload;
            this.PayloadType = payload != null ? payload.GetType().Name : "Unknown";
        }
    }
}