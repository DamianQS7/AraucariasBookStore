using AraucariasBookStore.DataAccess;
using AraucariasBookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AraucariasBookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult CategoryList()
        {
            List<Category> categoryList = _dbContext.Categories.ToList();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            // Added a custom validation
            if(category.Name.Length < 2)
            {
                ModelState.AddModelError("Name", "The name must be at least 2 characters long");
            }
            if(ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["success"] = "The category has been successfully added";
                return RedirectToAction("CategoryList");
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
                return NotFound();

            Category? category = _dbContext.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            // Added a custom validation
            if (category.Name.Length < 2)
            {
                ModelState.AddModelError("Name", "The name must be at least 2 characters long");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                TempData["success"] = "The category has been successfully updated";
                return RedirectToAction("CategoryList");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Category? category = _dbContext.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "The category has been successfully deleted";
            return RedirectToAction("CategoryList");
        }
    }
}
