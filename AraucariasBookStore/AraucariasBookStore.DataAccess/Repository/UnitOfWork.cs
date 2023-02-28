using AraucariasBookStore.DataAccess.Repository.IRepository;
using AraucariasBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraucariasBookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Category = new CategoryRepository(_dbContext);
            CoverTypeRepo = new CoverTypeRepository(_dbContext);
        }

        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverTypeRepo { get; private set; }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
