
using System.Collections.Generic;

namespace NewsAppNative.Core.DTO
{
    public class ResponseDTO<T>
    {
        public ResponseStatus Status { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }        
        public List<T> Content { get; set; }
        public bool IsSuccess => Status != ResponseStatus.Error;
    }
}
