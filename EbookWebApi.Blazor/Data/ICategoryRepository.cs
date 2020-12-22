using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbookWebApi.Blazor.Models;

namespace EbookWebApi.Blazor.Data
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int categoryId);
    }

}
