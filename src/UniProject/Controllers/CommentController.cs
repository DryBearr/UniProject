namespace Contollers;
using Microsoft.AspNetCore.Mvc;

public class CommentController : Controller
{
    [Route("Comments")]
    public ActionResult Index()
    {
        return View();
    }
}