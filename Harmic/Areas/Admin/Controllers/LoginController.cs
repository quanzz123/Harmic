using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly HarmicContext _context;
        public LoginController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TbAccount acc)
        {
            if (acc == null)
            {
                return NotFound();
            }
            //mã hoá mật khẩ trước khi kiểm tra
            string pasw = Function.MD5Password(acc.Password);
            //kiểm tra sự tồn tại của email trong DB
            var check = _context.TbAccounts.Where(m => (m.Email == acc.Email) && (m.Password == pasw)).FirstOrDefault();
            if (check == null)
            {
                // hiển thị thông báo
                Function._Message = "mật khẩu hoặc email sai!";
                return RedirectToAction("Index", "Login");
            }
            //vaof trang admin nếu đúng fullname và pass
            Function._Message = string.Empty;
            Function._AccountId = check.AccountId;
            Function._FullName = string.IsNullOrEmpty(check.FullName) ? string.Empty : check.FullName;
            Function._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
            return RedirectToAction("Index", "Home");
        }
    }
}
