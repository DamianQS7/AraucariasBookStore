using AraucariasBookStore.DataAccess;
using AraucariasBookStore.DataAccess.Repository;
using AraucariasBookStore.DataAccess.Repository.IRepository;
using AraucariasBookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AraucariasBookStoreWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork dbContext)
        {
            _unitOfWork = dbContext;
        }

        public IActionResult CoverTypeList()
        {
            IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverTypeRepo.GetAll();
            return View(coverTypeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CoverType coverType)
        {
            // Added a custom validation
            if (coverType.Name.Length < 2)
            {
                ModelState.AddModelError("Name", "The name must be at least 2 characters long");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypeRepo.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "The cover type has been successfully added";
                return RedirectToAction("CoverTypeList");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            CoverType? coverType = _unitOfWork.CoverTypeRepo.GetFirstOrDefault(x => x.Id == id);

            if (coverType == null)
                return NotFound();

            return View(coverType);
        }

        [HttpPost]
        public IActionResult Edit(CoverType coverType)
        {
            // Added a custom validation
            if (coverType.Name.Length < 2)
            {
                ModelState.AddModelError("Name", "The name must be at least 2 characters long");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypeRepo.Update(coverType);
                _unitOfWork.Save();
                TempData["success"] = "The covertype has been successfully updated";
                return RedirectToAction("CoverTypeList");
            }

            return View(coverType);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            CoverType? coverType = _unitOfWork.CoverTypeRepo.GetFirstOrDefault(x => x.Id == id);

            if (coverType == null)
                return NotFound();

            return View(coverType);
        }

        [HttpPost]
        public IActionResult Delete(CoverType coverType)
        {
            _unitOfWork.CoverTypeRepo.Remove(coverType);
            _unitOfWork.Save();
            TempData["success"] = "The cover type has been successfully deleted";
            return RedirectToAction("CoverTypeList");
        }
    }
}
