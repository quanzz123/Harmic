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
        [HttpPost]
        public async Task<IActionResult> Reviews(string name, string phone, string email, string details, int productid)
        {
            try
            {
                // Tạo đối tượng review mới
                TbProductReview productreview = new TbProductReview
                {
                    Name = name,
                    Phone = phone,
                    Email = email,
                    CreatedDate = DateTime.Now,
                    Detail = details,
                    ProductId = productid,
                    IsActive = true // xử lý kích hoạt trạng thái nếu cần
                };

                // Thêm vào DbSet và lưu vào cơ sở dữ liệu
                _context.TbProductReviews.Add(productreview);
                await _context.SaveChangesAsync(); // Sử dụng await để đảm bảo dữ liệu được lưu

                return Json(new { status = true }); // Trả về trạng thái thành công
            }
            catch (Exception ex)
            {
                // Ghi log lỗi (nếu cần) và trả về trạng thái thất bại
                return Json(new { status = false, message = ex.Message });
            }
        }


    }
}
