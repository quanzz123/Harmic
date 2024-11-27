using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;

namespace Harmic.Controllers
{
    public class ProductController : Controller
    {
        private readonly HarmicContext _context;

        public ProductController(HarmicContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/product/{alias}-{id}.html")]

        public async Task<IActionResult> Details(int? id)
            {
            if(id == null || _context.TbProducts == null)
            {
                return NotFound();
            }
            TempData["ID"] = id;
            var product = await _context.TbProducts.Include(i => i.CategoryProduct)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null) {
                return NotFound();            
            }

            ViewBag.productReview = _context.TbProductReviews.
                Where(i => i.ProductId == id && i.IsActive).ToList();
            ViewBag.productRelated = _context.TbProducts.
                Where(i=>i.ProductId != id && i.CategoryProductId == product.CategoryProductId).Take(5).
                OrderByDescending( i => i.ProductId).ToList();
            return View(product);
        }
        [HttpPost]
        public IActionResult Reviews(string name, string phone, string email, string details, int productid)
        {
            try
            {
                int id = (int)TempData["ID"];
                TbProductReview productreview = new TbProductReview();
                productreview.Name = name;
                productreview.Phone = phone;
                productreview.Email = email;
                productreview.CreatedDate = DateTime.Now;

                productreview.Detail = details;
                productreview.ProductId = id;
                productreview.IsActive = true;// xử lí sau
                _context.Add(productreview);
                _context.SaveChangesAsync();
                return Json(new { status = true }); 
            } catch
            {
                return Json(new { status = false });

            }
            
        }

    }
}
