using LibreriaMilo.Models;
using LibreriaMilo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibreriaMilo.Controllers;

public class UserController : Controller
{
    private readonly UserService _userService;
    
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        var response = _userService.GetAllUsers();
        return View(response.Data);
    }
    
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Create(User user)
    {
        var response = _userService.SaveUser(user);
        if (response.IsSuccess)
        {
            return RedirectToAction("Index");
        }
        return View(user);
    }
    
    public IActionResult Edit(int id)
    {
        var response = _userService.GetUserById(id);
        if (response.IsSuccess)
        {
            return View(response.Data);
        }
        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult Edit(int id, User user)
    {
        var response = _userService.UpdateUser(id, user);
        if (response.IsSuccess)
        {
            return RedirectToAction("Index");
        }
        return View(user);
    }
    
    public IActionResult Delete(int id)
    {
        _userService.DeleteUser(id);
        return RedirectToAction("Index");
    }
}