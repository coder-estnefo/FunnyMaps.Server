
using FunnyMaps.Server.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FunnyMaps.Server
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }

            catch (UserExistsException e)
            {
                var details = new ProblemDetails
                {
                    Title = "Api Error",
                    Status = (int)HttpStatusCode.Conflict,
                    Detail = e.Message,
                };
                
                await context.Response.WriteAsJsonAsync(details);
            }

            catch (InvalidLoginDetailsException e)
            {
                var details = new ProblemDetails
                {
                    Title = "Api Error",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = e.Message,
                };

                await context.Response.WriteAsJsonAsync(details);
            }

            catch (Exception e)
            {
                var details = new ProblemDetails
                {
                    Title = "Api Error",
                    Status = (int)context.Response.StatusCode,
                    Detail = e.Message,
                    
                };

                await context.Response.WriteAsJsonAsync(details);
            }
        }
    }
}
