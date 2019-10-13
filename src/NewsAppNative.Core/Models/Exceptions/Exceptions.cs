using System;

namespace NewsAppNative.Core.Models
{
    public class NetworkErrorException : Exception
    {
        public NetworkErrorException()
        {
        }

        public NetworkErrorException(string msg) : base(msg)
        {
        }
    }

    public class UrlNotFoundException : Exception
    {
        public UrlNotFoundException()
        {
        }

        public UrlNotFoundException(string msg) : base(msg)
        {
        }
    }
}
