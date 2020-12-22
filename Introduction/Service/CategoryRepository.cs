using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Entity;
using Introduction.IService;
using Microsoft.EntityFrameworkCore;

namespace Introduction.Service
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PustakaDbContext _dbContext;
        public CategoryRepository(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetAllIncludeBook()
        {
            return await _dbContext.Categories.Include(c => c.Books).ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<Category> Insert(Category obj)
        {
            _dbContext.Add(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }

        public async Task<Category> Update(Category obj)
        {
            _dbContext.Update(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(int id)
        {
            var book = await _dbContext.Categories.FindAsync(id);
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task InsertMultiple(IEnumerable<Category> list)
        {
            foreach (var itm in list)
            {
                _dbContext.Add(itm);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
