using FunnyMaps.Server.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FunnyMaps.Server
{
    public class FunnyMapsActionFilter : IExceptionFilter
    {
        //private readonly string _name;

        //public FunnyMapsActionFilterAttribute(string name)
        //{
        //    _name = name;
        //}
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    Console.WriteLine($"OnActionExecuted - {_name}");
        //}

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    Console.WriteLine($"OnActionExecuting - {_name}");
        //}
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UserExistsException)
            {
                context.HttpContext.Response.StatusCode = 500;
                context.Result = new JsonResult(context.Exception.Message);
            }
        }
    }
}
