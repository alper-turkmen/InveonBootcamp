using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated) 
        {
            return RedirectToAction("Index", "Books"); 
        }
        else
        {
            return RedirectToAction("Login", "Account"); 
        }
    }
}