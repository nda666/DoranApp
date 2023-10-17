using System;

namespace DoranApp.Exceptions
{
    public class RestException : Exception
    {
        public RestException(int errorCode, string statusText, string errorType = "COMMON", dynamic data = null) :
            base(statusText)
        {
            ErrorCode = errorCode;
            StatusText = statusText;
            Data = data;
            ErrorType = errorType;
        }

        public int ErrorCode { get; }
        public string StatusText { get; }

        public dynamic Data { get; }

        public string ErrorType { get; }
    }
}