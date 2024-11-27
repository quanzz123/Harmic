using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {

        [Area("Admin")]
        public IActionResult Index()
        {
            //kiểm tra trạng thái đăng nhập
            if (!Function.IsLogin())
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [Area("Admin")]
        [Route("Admin/Logout")]
        public IActionResult Logout()
        {
            Function._AccountId = 0;
            Function._FullName = string.Empty;
            Function._Email = string.Empty;
            Function._Message = string.Empty;
            Function._MessageEmail = string.Empty;
            return RedirectToAction("Index", "Home");
        }
    }
}
