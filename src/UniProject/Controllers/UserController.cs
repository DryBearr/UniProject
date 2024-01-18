namespace Controllers;
using AutoMapper;
using Contollers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Services;
using DTOs;
using System.Diagnostics;

[Route("Users")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var tempUsers = await _userService.GetAllUsers(); 
            var users = new List<ResponseUserDto>();
            foreach(var user in tempUsers)
                users.Add(_mapper.Map<ResponseUserDto>(user));
            return View(users);
        }
        catch(Exception e)
        {
            return RedirectToAction("Error", "Home");   
        }
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {  
        try
        {
            var user = _mapper.Map<ResponseUserDto>(await _userService.GetUserById(id));
            return View(user);
        }
        catch (Exception e)
        {
            return RedirectToAction("Error", "Home");   
        }
    } 

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var user = _mapper.Map<ResponseUserDto>(await _userService.GetUserById(id));
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        catch (Exception e)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpPost("Delete/{id}")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _userService.DeleteUserById(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(RequestUserDto requestUserDto)
    {
        Console.WriteLine("IM IN MAZAFAKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        try
        {
            Console.WriteLine("__________________________________");
            Console.WriteLine($"USERNAME: {requestUserDto.Username}");
            Console.WriteLine($"PASSWORD: {requestUserDto.UnhashedPassword}");
            Console.WriteLine("__________________________________");
            var user = _mapper.Map<UserDto>(requestUserDto);
            await _userService.CreateUser(user);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine("__________________________________");
            Console.WriteLine(e.Message);
            Console.WriteLine(e.ToString());
            Console.WriteLine("__________________________________");
            return RedirectToAction("Error", "Home");
        }
        
    }
}