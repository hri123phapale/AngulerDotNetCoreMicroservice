using Azure.Core;
using CodePulse.Api.Data;
using CodePulse.Api.Models.Domain;
using CodePulse.Api.Models.DTO;
using CodePulse.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CodePulse.Api.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
        public IEnumerable<Category> GetCategories()
        {
            var categories = dbContext.Categories.ToList();
            return categories;
        }

        public async Task<Category> GetCategoryById(string id)
        { 
            return await dbContext.Categories.FirstAsync(a => a.Id == Guid.Parse(id));
        }
        public async Task<Category> UpdateCategory(CategoryRequestDto request)
        {
            var category = await dbContext.Categories.FirstAsync(a => a.Id == request.Id);
            category.Name = request.Name;
            category.UrlHandle = request.UrlHandle;

            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync();

            return category;
        } 
        public async Task DeleteCategory(string id)
        {
            var category = await dbContext.Categories.FirstAsync(a => a.Id == Guid.Parse(id));
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();  
        }
    }
}
