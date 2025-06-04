using dotnet.Db;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly MyDbContext _context;

    public CategoriesController(MyDbContext context)
    {
        _context = context;
    }

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
    
}