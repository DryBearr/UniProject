namespace Contollers;
using Microsoft.AspNetCore.Mvc;
using Services;
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("")]
    public async Task<ActionResult> Index()
    {
        try
        {
            var users = await _userService.GetAllUsers();
            return View(users);
        }
        catch(Exception e)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    // [HttpGet("details/{id}")]
    // public async Task<ActionResult> Details(int id)
    // {  
    //     try
    //     {
    //         var user = await _userService.GetUserById(id);
    //     }
    //     catch (System.Exception)
    //     {
            
    //     }
    // } 
    
}