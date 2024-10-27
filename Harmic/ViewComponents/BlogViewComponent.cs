using Harmic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly HarmicContext _context;

        public BlogViewComponent(HarmicContext context)
        {
            _context = context;
        }
        

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = _context.TbBlogs.Where(m => (bool)m.IsActive).ToList();
            return await Task.FromResult<IViewComponentResult>
                (View(item));
        }
    }
}
