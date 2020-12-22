using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Entity;

namespace Introduction.IService
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<List<Category>> GetAllIncludeBook();
        Task<Category> GetById(int id);
        //Interface (ICategoryRepository)
        Task<Category> Insert(Category obj);
        Task<Category> Update(Category obj);
        Task Delete(int id);
        Task InsertMultiple(IEnumerable<Category> list);
    }
}
