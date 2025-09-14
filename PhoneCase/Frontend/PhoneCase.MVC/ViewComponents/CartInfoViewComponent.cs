using Microsoft.AspNetCore.Mvc;

namespace PhoneCase.MVC.ViewComponents;

public class CartInfoViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("Index");
    }
}
