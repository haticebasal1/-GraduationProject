using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.MVC.Models;

namespace PhoneCase.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
