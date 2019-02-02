using System;
using System.Collections.Generic;
using System.Net;
using CopyDirectoryLib.Exception;

namespace CopyDirectory.Internal.Exceptions
{
    public class ExceptionToHttpCodeConverter
    {
        private readonly Dictionary<Type, HttpExceptionResponseInfo> _exceptionInfo = new Dictionary<Type, HttpExceptionResponseInfo>();

        protected void AddValues(Type type, HttpStatusCode code, string msg)
        {
            _exceptionInfo.Add(type, new HttpExceptionResponseInfo(code, msg));
        }

        private HttpExceptionResponseInfo GetValue(Type type)
        {
            if (!_exceptionInfo.TryGetValue(type, out HttpExceptionResponseInfo info))
            {
                info = new HttpExceptionResponseInfo(HttpStatusCode.InternalServerError, null);
            }

            return info;
        }

        public ExceptionToHttpCodeConverter()
        {
            AddValues(typeof(UnauthorizedAccessException), HttpStatusCode.Unauthorized, "UnauthorizedAccessException");
            AddValues(typeof(InternalException), HttpStatusCode.InternalServerError, "InternalServerErrorException");
            AddValues(typeof(CopyingException), HttpStatusCode.ServiceUnavailable, "Copyingexception");
        }

        public HttpExceptionResponseInfo GetMessageAndHttpCode(Exception exception)
        {
            var values = GetValue(exception.GetType());

            if (values.Message == null)
            {
                values.Message = exception.ToString();
            }

            return values;
        }
    }

    public class HttpExceptionResponseInfo
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }

        public HttpExceptionResponseInfo(HttpStatusCode status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
