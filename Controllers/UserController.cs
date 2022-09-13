using Microsoft.AspNetCore.Mvc;
using MyProject.ViewModels;

namespace MyProject.Controllers;

public class UserController : Controller
{

  public static List<User> users = new();
  private readonly ILogger<UserController> _logger;

  public UserController(ILogger<UserController> logger)
  {
    _logger = logger;
  }

  public IActionResult AddUser() => View();

  [HttpPost]
  public IActionResult AddUser(User model)
  {
    if (!ModelState.IsValid) return View(model);
    users.Add(model);
    return RedirectToAction("AllUsers");
  }

  public IActionResult AddUsers() => View(users);
}