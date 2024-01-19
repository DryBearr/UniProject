namespace Contollers;
using Microsoft.AspNetCore.Mvc;

public class PostController : Controller
{
    [Route("Posts")]
    public ActionResult Index()
    {
        return View();
    }
}