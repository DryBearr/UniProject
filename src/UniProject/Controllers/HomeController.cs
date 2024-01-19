namespace Contollers;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [Route("/")]
    public ActionResult Index()
    {
        return View();
    }

    [Route("/Error")]
    public IActionResult Error(string message)
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionFeature != null)
        {
            var exception = exceptionFeature.Error;

            Console.WriteLine(exception);

            ViewData["ErrorMessage"] = exception.Message;
        }
        else
        {
            ViewData["ErrorMessage"] = "An unknown error occurred.";
        }

        return View();
    }
}