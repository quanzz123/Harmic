using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Harmic.Utilities;
namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegisterController : Controller
    {
        private readonly HarmicContext _context;
        public RegisterController(HarmicContext context)
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
            if(acc == null)
            {
                return NotFound();
            }
            //kiêm tra sự tồn tại Email trong DB
            var check = _context.TbAccounts.Where(m => m.Email == acc.Email).FirstOrDefault();
            if(check != null)
            {
                //hiên thị thông báo nếu
                Function._MessageEmail = "Duplicate Email!";
                return RedirectToAction("Index", "Register");
            }

            // Tách tên từ Fullname để tạo Username
            /* if (!string.IsNullOrWhiteSpace(acc.FullName))
             {

                 var nameParts= Function._FullName.Trim().Split(' ');
                 acc.Username = nameParts.Length > 0 ? nameParts[^1] : Function._FullName; // Lấy từ cuối hoặc toàn bộ Fullname nếu không tách được
             }*/
           /* if(!string.IsNullOrWhiteSpace(acc.Username))
            {
                acc.Username = Function._FullName;

            }*/
            //Nếu khong có thì thêm vô DB
            Function._MessageEmail = string.Empty;
            acc.Password = Function.MD5Password(acc.Password);
            acc.Username = Function.GetUsername1(acc.FullName);
            _context.Add(acc);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
