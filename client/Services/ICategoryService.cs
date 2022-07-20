using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client.Services
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public interface ICategoryService
    {
        public Task<List<Category>> GetCategories();
    }
}