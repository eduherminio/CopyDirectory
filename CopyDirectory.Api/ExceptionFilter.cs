using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace CopyDirectory.Internal.Exceptions
{
    public class ExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<ExceptionFilterAttribute> _logger;
        protected virtual ExceptionToHttpCodeConverter Converter => new ExceptionToHttpCodeConverter();

        public ExceptionFilterAttribute(IHostingEnvironment env, ILogger<ExceptionFilterAttribute> logger)
        {
            _env = env;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;

            HttpExceptionResponseInfo info = Converter.GetMessageAndHttpCode(ex);

            context.HttpContext.Response.StatusCode = (int)info.Status;
            if (info.Message != null)
            {
                context.Result = new JsonResult(info.Message);
            }
        }
    }
}
