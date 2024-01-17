namespace Contollers;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [Route("/")]
    public ActionResult Index()
    {
        return View();
    }
}