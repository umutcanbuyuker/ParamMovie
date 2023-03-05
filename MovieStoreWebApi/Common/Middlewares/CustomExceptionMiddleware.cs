using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MovieStoreWebApi.Services.LogService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Common.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;

        public CustomExceptionMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            _logService = logService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var requestBody = ReadRequestBody(context);
                var message = $"Request Method:{context.Request.Method} Request Path:{context.Request.Path} \nRequest Body:{requestBody}";
                _logService.WriteConsole(message);

                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            var message = $"{context.Response.StatusCode} {context.Request.Method} {ex.Message}";
            _logService.WriteConsole(message);
            var jsonMessage = JsonConvert.SerializeObject(new { error = message }, Formatting.Indented);
            return context.Response.WriteAsync(jsonMessage);
        }

        private string ReadRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestBody = Encoding.UTF8.GetString(buffer);
            context.Request.Body.Position = 0;
            return requestBody;
        }
    }

    public static class CustomException
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
