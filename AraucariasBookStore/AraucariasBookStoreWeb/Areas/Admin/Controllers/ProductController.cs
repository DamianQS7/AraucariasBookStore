using AraucariasBookStore.DataAccess;
using AraucariasBookStore.DataAccess.Repository;
using AraucariasBookStore.DataAccess.Repository.IRepository;
using AraucariasBookStore.Models;
using AraucariasBookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AraucariasBookStoreWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult ProductList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductViewModel viewModel = new()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverTypeRepo.GetAll().Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                // Create Model
                return View(viewModel);
            }
            else
            {
                // Update Product
                viewModel.Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
                return View(viewModel);
            }

            
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel viewModel, IFormFile? file)
        {
            
            if (ModelState.IsValid)
            {
                string wwwRootFolderPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    // Generate a new and unique identifier for the file, to avoid redundancy.
                    string fileName = Guid.NewGuid().ToString();

                    // We get the path for the folder containing the images
                    string uploads = Path.Combine(wwwRootFolderPath, @"images\products");

                    // We get the extension from the filename
                    string extension = Path.GetExtension(file.FileName);

                    // Delete the image if previously existed, for when we update.
                    if(viewModel.Product.ImageUrl != null)
                    {
                        var oldImgPath = Path.Combine(wwwRootFolderPath, viewModel.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImgPath))
                            System.IO.File.Delete(oldImgPath);
                    }

                    // We copy the file into our root folder contaning the images.
                    using (FileStream fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    viewModel.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                
                if(viewModel.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(viewModel.Product);
                    TempData["success"] = "The product has been successfully created";
                }
                else
                {
                    _unitOfWork.Product.Update(viewModel.Product);
                    TempData["success"] = "The product has been successfully updated";
                }
                    
                _unitOfWork.Save();
                
                return RedirectToAction("ProductList");
            }

            return View(viewModel);
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            // We will delete the image as well from the project.
            var oldImgPath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImgPath))
                System.IO.File.Delete(oldImgPath);

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
