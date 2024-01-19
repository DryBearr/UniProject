namespace CustomFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Console.WriteLine("CustomExceptionFilter__________________________________Start");
        Console.WriteLine(context.Exception);
        Console.WriteLine("CustomExceptionFilter__________________________________End");


        context.Result = new RedirectToRouteResult(
            new RouteValueDictionary 
            { 
                { "controller", "Home" }, 
                { "action", "Error" } 
            });
        context.ExceptionHandled = true;
    }
}
