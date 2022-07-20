using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions options;


        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Category>> GetCategories()
        {
            var response = await httpClient.GetAsync("/api/category");

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var categories = JsonSerializer.Deserialize<List<Category>>(content, options);
            return categories;
        }
    }
}