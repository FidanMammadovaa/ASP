using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Project.Areas.Admin.Controllers;

[Area("Admin")]

public class CategoryController : Controller
{
    private readonly UserContext _dbContext;

    public CategoryController(UserContext context)
    {
        _dbContext = context;
    }

    public IActionResult Index()
    {
        var categories = _dbContext.Categories.ToList();
        if(categories != null)
        {
            return View(categories);
        }
        throw new ArgumentException("Category is not valid...");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category? category)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category created succsessfully";
            return RedirectToAction("Index", "Category");
        }
        throw new ArgumentException("Category is not valid...");
    }
    public IActionResult Create()
    {
        return View();

    }
    
    public IActionResult Edit(int id)
    {
        var category = _dbContext.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category updated succsessfully";

            return RedirectToAction("Index", "Category");
        }
        throw new ArgumentException("Category is not valid...");
    }
}