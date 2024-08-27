
using FunnyMaps.Server.Exceptions;
using FunnyMaps.Server.Response;
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

                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
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

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(details);
            }

            catch (Exception e)
            {
                var details = new ApiError
                {
                    Title = "Api Error",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = e.Message,
                    
                };

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(details);
            }
        }
    }
}
