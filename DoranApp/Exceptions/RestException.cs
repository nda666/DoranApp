using System;

namespace DoranApp.Exceptions
{
    public class RestException : Exception
    {
        public int ErrorCode { get; }
        public string StatusText { get; }

        public RestException(string paramName, int errorCode)
            : base(paramName)
        {
            ErrorCode = errorCode;
        }

        public RestException(string paramName, int errorCode, string statusText) : base(paramName)
        {
            ErrorCode = errorCode;
            StatusText = statusText;
        }

    }
}
