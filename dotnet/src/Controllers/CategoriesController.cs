using dotnet.Db;
using dotnet.Models;
using dotnet.Dto;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult index(string? search, int? page = 1, int? pageSize = 5)
        {
            if (page == null || pageSize == null || page <= 0 || pageSize <= 0)
            {
                page = 1;
                pageSize = 5;
            }
            var totalCategories = _context.Categories
                .Where(c => string.IsNullOrEmpty(search) ||
                    (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(search)) ||
                    (!string.IsNullOrEmpty(c.Description) && c.Description.Contains(search)))
                .Count();
            var categories = _context.Categories
                .Where(c => string.IsNullOrEmpty(search) ||
                    (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(search)) ||
                    (!string.IsNullOrEmpty(c.Description) && c.Description.Contains(search)))
                .OrderBy(c => c.CreatedAt)
                .Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description
                }).ToList();

            return Ok(new
            {
                Categories = categories,
                Total = totalCategories,
            });
        }
        // {
        //     var categories = _context.Categories
        //     .Where(c => string.IsNullOrEmpty(search) || 
        //         (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(search)) || 
        //         (!string.IsNullOrEmpty(c.Description) && c.Description.Contains(search)))
        //     .OrderBy(c => c.CreatedAt)
        //     .Select(c => new
        //     {
        //         c.Id,
        //         c.Name,
        //         c.Description
        //     }).ToList();
        //     return Ok(new { Categories = categories });
        // }

        [HttpPost]
        public IActionResult Create(Categories categories)
        {
            var name = categories.Name;
            var description = categories.Description;
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new { Message = "Name and description are required" });
            }

            _context.Categories.Add(new Categories
            {
                Name = name,
                Description = description
            });
            _context.SaveChanges();

            return Ok(new { Message = "Category created successfully" });
        }

        [HttpDelete]
        public async Task<IActionResult> DestroyAsync([FromBody] int[] id )
        {

            foreach (var item in id)
            {
                var category = await _context.Categories.FindAsync(item);  // ค้นหา record

                if (category == null)
                {
                    return NotFound(); // ถ้าไม่เจอ
                }
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();    // ลบจริงใน DBxs
            }
            return Ok(
                new
                {
                    Message = "delete successfully"
                }
            );
        }

    }
}