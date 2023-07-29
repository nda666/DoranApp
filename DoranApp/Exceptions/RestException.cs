using System;

namespace DoranApp.Exceptions
{
    public class RestException : Exception
    {
        public int ErrorCode { get; }
        public string StatusText { get; }


        public RestException( int errorCode, string statusText) : base(statusText)
        {
            ErrorCode = errorCode;
            StatusText = statusText;
        }

    }
}
