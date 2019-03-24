using Microsoft.AspNetCore.Mvc;

namespace Hunt.Responses{
    public class MessagePayloadResponse
    {
        public string Message { get; private set; }

        private MessagePayloadResponse(string message){
            this.Message = message;
        }

        // public static JsonResult Success(string message){
        //     return new JsonResult(Response<MessagePayloadResponse>
        //                             .Create(true, new MessagePayloadResponse(message)));
        // }

        // public static JsonResult Failure(string message){
        //     return new JsonResult(Response<MessagePayloadResponse>
        //                             .Create(false, new MessagePayloadResponse(message)));

        // }
    }
}