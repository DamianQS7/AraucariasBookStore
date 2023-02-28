using AraucariasBookStore.DataAccess;
using AraucariasBookStore.DataAccess.Repository;
using AraucariasBookStore.DataAccess.Repository.IRepository;
using AraucariasBookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AraucariasBookStoreWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork dbContext)
        {
            _unitOfWork = dbContext;
        }

        public IActionResult CategoryList()
        {
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
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
            if (category.Name.Length < 2)
            {
                ModelState.AddModelError("Name", "The name must be at least 2 characters long");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "The category has been successfully added";
                return RedirectToAction("CategoryList");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Category? category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

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
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
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

            Category? category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "The category has been successfully deleted";
            return RedirectToAction("CategoryList");
        }
    }
}
