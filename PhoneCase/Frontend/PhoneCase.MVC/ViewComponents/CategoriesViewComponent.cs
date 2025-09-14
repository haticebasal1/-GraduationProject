using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneCase.Shared.Dtos.CategoryDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;
using PhoneCase.Shared.Enums;

namespace PhoneCase.MVC.ViewComponents;

public class CategoriesViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
            return View();
    }
    }
