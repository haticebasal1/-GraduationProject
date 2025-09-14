using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoneCase.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}