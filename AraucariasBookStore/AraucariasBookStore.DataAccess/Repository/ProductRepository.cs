using AraucariasBookStore.DataAccess.Repository.IRepository;
using AraucariasBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraucariasBookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This update will compare the new Product object that we are working with, versus the info stored in the database
        /// and update only what we need (in this case was everything except for the image)
        /// </summary>
        /// <param name="product">The current Product object we are editing. </param>
        public void Update(Product product)
        {

            Product? objFromDb = _dbContext.Products.FirstOrDefault(obj => obj.Id == product.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = product.Name;
                objFromDb.Description = product.Description;
                objFromDb.ISBN= product.ISBN;
                objFromDb.Price = product.Price;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.PricePer50 = product.PricePer50;
                objFromDb.PricePer100 = product.PricePer100;
                objFromDb.Author = product.Author;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.CoverTypeId = product.CoverTypeId;
                if(product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
