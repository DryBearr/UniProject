namespace Controllers;
using AutoMapper;
using Contollers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Services;
using DTOs;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            throw new Exception("Something Bad happend :( !", e);   
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
            throw new Exception($"Could not Get Details about user with Id {id}", e);
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
            throw new Exception($"Could Not Delete User With Id:{id}", e);
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
            throw new Exception($"Could Not Delete User With Id:{id}", e);
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
        try
        {
            var user = _mapper.Map<UserDto>(requestUserDto);
            await _userService.CreateUser(user);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            throw new Exception("Could Not Create New User ", e);
        }
        
    }


    [HttpGet("Post/{id}")]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var user = await _userService.GetUserById(id); 

            var model = new RequestUserDto
            {
                Username = user.Username,
                UnhashedPassword = "",
            };

            return View(model);
        }
        catch(Exception e)
        {
            throw new Exception($"Could Not Update User With Id: {id}", e);
        }
    }

    [HttpPost("Post/{id}")]
    public async Task<IActionResult> Update(int id, RequestUserDto user)
    {
        try
        {
            var updatedUser = _mapper.Map<UpdateUserDto>(user);
            updatedUser.UserId = id; 
            await _userService.UpdateUser(updatedUser);

            return RedirectToAction("Index"); 
        }
        catch(Exception e)
        {
            throw new Exception($"Could Not Update User With Id: {id}", e);
        }
    }
}