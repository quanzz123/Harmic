using Harmic.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Areas.Admin.Controllers
{
    
    public class FileManagerController : Controller
    {
        [Area("Admin")]
        [Route("/Admin/file-manager")]
        public IActionResult Index()
        {
            //kiểm tra trạng thái đăng nhập
            if (!Function.IsLogin())
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
