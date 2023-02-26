using AraucariasBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraucariasBookStore.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        void Save();
        // Sometimes, we want to perform 2 or 3 operations before saving the changes to the db.
        // That is why it is not recommended to call the SaveChanges method every time we finish an action
        // because we will have too many database calls.
        // To solve that, we will implement this method, and will call it as we need.
    }
}
