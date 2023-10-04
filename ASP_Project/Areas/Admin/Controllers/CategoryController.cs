using ASP_Project.Areas.Identity.Data.DbContexts;
using ASP_Project.Areas.Identity.Data.Extensions;
using ASP_Project.Areas.Identity.Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ASP_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private IValidator<Category> _categoryValidator { get; set; }

        private readonly UserContext _dbContext;

        public CategoryController(UserContext context, IValidator<Category> validator)
        {
            _dbContext = context;
            _categoryValidator = validator;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            if (categories != null)
            {
                return View(categories);
            }
            throw new ArgumentException("Category is not valid...");
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            var result = await _categoryValidator.ValidateAsync(category);

            if (result.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category created succsessfully";
                return RedirectToAction("Index", "Category");
            }
            result.AddToModelState(this.ModelState);

            return View(category);
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
		public async Task<IActionResult> Edit(Category category)
		{
			#region
			//ModelState.Clear();
			//var result = await _categoryValidator.ValidateAsync(category);

			//if (result.IsValid)
			//{
			//    _dbContext.Categories.Update(category);
			//    _dbContext.SaveChanges();
			//    TempData["success"] = "Category updated succsessfully";

			//    return RedirectToAction("Index", "Category");
			//}
			//result.AddToModelState(this.ModelState);

			//return View(category); 
			#endregion
			var existingCategory = _dbContext.Categories.AsNoTracking().FirstOrDefault(c => c.Id == category.Id);
			if (existingCategory.Name != category.Name)
			{
				ModelState.Clear();
				var result = await _categoryValidator.ValidateAsync(category);

				if (result.IsValid)
				{

					_dbContext.Categories.Update(category);
					_dbContext.SaveChanges();
					TempData["success"] = "Category updated succsessfully";

					return RedirectToAction("Index", "Category");
				}
				result.AddToModelState(this.ModelState);
			}
			else
			{
				if (ModelState.IsValid)
				{

					_dbContext.Categories.Update(category);
					_dbContext.SaveChanges();
					TempData["success"] = "Category updated succsessfully";

					return RedirectToAction("Index", "Category");
				}
				else
				{
					ModelState.AddModelError("category.Description", "Category description must be between 1 and 100 characters");
				}
			}
			return View(category);
		}


		public IActionResult Delete(int id)
        {
            var category = _dbContext!.Categories!.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var category = _dbContext!.Categories!.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category was deleted succsessfully";

            return RedirectToAction("Index");
        }
    }
}
