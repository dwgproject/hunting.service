namespace Hunt.Responses{
    public class Response{

        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Response(bool isSuccess, string message){
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
    }
}