using AraucariasBookStore.DataAccess.Repository.IRepository;
using AraucariasBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraucariasBookStore.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _dbContext;

        public CoverTypeRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public void Update(CoverType coverType)
        {
            _dbContext.CoverTypes.Update(coverType);
        }
    }
}
