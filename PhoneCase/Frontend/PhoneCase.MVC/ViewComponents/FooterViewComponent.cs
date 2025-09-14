using Microsoft.AspNetCore.Mvc;

namespace PhoneCase.MVC.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<string> socialMediaLinks = ["facebook.com/PhoneCase", "x.com/PhoneCase", "instagram.com/PhoneCase"];
        return View(socialMediaLinks);
    }
}