using Harmic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Controllers
{
    public class ContactController : Controller
    {
        private readonly HarmicContext _context;
        public ContactController(HarmicContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Create(string name, string phone, string email, string message)
        {
            try
            {
                TbContact contact = new TbContact();
                contact.Name = name;
                contact.Phone = phone;
                contact.Email = email;
                contact.Message = message;
                contact.CreatedDate = DateTime.Now;
                _context.Add(contact);
                _context.SaveChanges();
                return Json(new { status = true });
            } catch
            {
                return Json(new { status = false });
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
