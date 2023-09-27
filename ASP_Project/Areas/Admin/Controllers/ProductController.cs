using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Extensions;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Project.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private IValidator<Product> _productValidator { get; set; }

    private readonly UserContext _dbContext;

    public ProductController(UserContext context, IValidator<Product> validator)
    {
        _dbContext = context;
        _productValidator = validator;
    }

    public IActionResult Index()
    {
        var products = _dbContext.Products.ToList();
        if (products != null)
        {
            return View(products);
        }
        throw new ArgumentException("Product is not valid...");
    }


    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product? product)
    {
        var result = await _productValidator.ValidateAsync(product);

        if (result.IsValid)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            TempData["success"] = "Product created succsessfully";
            return RedirectToAction("Index", "Product");
        }
        result.AddToModelState(this.ModelState);

        return View(product);
    }



    public IActionResult Edit(int id)
    {
        var product = _dbContext.Categories.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Product product)
    {
        var result = await _productValidator.ValidateAsync(product);

        if (ModelState.IsValid)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            TempData["success"] = "Product updated succsessfully";

            return RedirectToAction("Index", "Product");
        }
        result.AddToModelState(this.ModelState);

        return View(product);
    }


    public IActionResult Delete(int id)
    {
        var product = _dbContext!.Products!.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int id)
    {
        var product = _dbContext!.Products!.Find(id);

        if (product == null)
        {
            return NotFound();
        }
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
        TempData["success"] = "Product was deleted succsessfully";

        return RedirectToAction("Index");
    }
}