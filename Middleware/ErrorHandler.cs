using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Middleware
{

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    namespace ListsApi.Middleware
    {
        public class ErrorHandler
        {
            private readonly RequestDelegate _next;
            private readonly IHttpContextAccessor _contextAccessor;

            private HttpContext _context { get { return _contextAccessor.HttpContext; } }

            private readonly ILogger<ErrorHandler> _logger;
            private readonly IWebHostEnvironment _env;

            private List<string> _environmentsThatWriteMessageToClien = new List<string> { "development", "vcloud" };

            public ErrorHandler(RequestDelegate next,
                                ILoggerFactory loggerFactory,
                                IWebHostEnvironment env, IHttpContextAccessor contextAccessor)
            {
                this._next = next;
                this._logger = loggerFactory.CreateLogger<ErrorHandler>();
                this._env = env;
                this._contextAccessor = contextAccessor;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next.Invoke(context);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex);
                }
            }

            private Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
               
                string sResult = "";
                int responseStatusCode = (int)getHttpStatusCode(exception);

                sResult = ErrorResponse(exception, responseStatusCode);
                context.Response.Headers["Content-Encoding"] = "UTF-8";
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = responseStatusCode;

                return context.Response.WriteAsync(sResult);
            }


            private string ErrorResponse(Exception ex, int responseStatusCode)
            {
                var text = ex.Message;
                var result = new Response<string>(text);
                return (Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }

            private HttpStatusCode getHttpStatusCode(Exception exception)
            {
                if (exception is ArgumentNullException || exception is HttpRequestException || exception is JsonException || exception is NotSupportedException)
                    return HttpStatusCode.BadRequest;
                if (exception is UnauthorizedAccessException)
                    return HttpStatusCode.Unauthorized;
                return HttpStatusCode.InternalServerError;

            }
        }
    }

}
