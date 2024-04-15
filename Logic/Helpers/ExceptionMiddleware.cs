using API.Core.Context;
using API.Models;
using Newtonsoft.Json;
using System.Net;

namespace API.Logic.Helpers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //var method = httpContext.Request.Method;

                //var tenantName = httpContext.Request.Headers["Client"];

                //if (string.IsNullOrEmpty(tenantName))
                //    tenantName = "";

                //if (method != "OPTIONS")
                //{
                //    ApplicationDbContext.SetConnection(tenantName);
                //}

                await _next(httpContext);

            }
            catch (Exception ex)
            {
                ApplicationDbContext.SetConnection("");
                await HandleGlobalExceptioAsync(httpContext, ex);
            }

        }

        private static Task HandleGlobalExceptioAsync(HttpContext Context, Exception ex)
        {
            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Context.Response.WriteAsync(JsonConvert.SerializeObject(new Reply()
            {
                Status = StatusCodes.Status500InternalServerError,
                Message = ex.Message,
                trace = ex.StackTrace
            }));
        }
    }
}
