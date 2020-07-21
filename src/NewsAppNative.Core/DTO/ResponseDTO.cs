
using System.Collections.Generic;
using System.Net;

namespace NewsAppNative.Core.DTO
{
    public class ResponseDTO<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }        
        public List<T> Content { get; set; }
        public bool IsSuccess { get; set; }
    }
}
