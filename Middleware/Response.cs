using Newtonsoft.Json;

namespace Weather.Middleware.ListsApi.Middleware
{
   
    public class Response<T>
    {
        [JsonConstructor]
        public Response(T data, string message = null)
        {
            this.Data = data;
            this.Message = message;
        }

        public Response(string message) : this(default(T), message) { }
               
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
       
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}