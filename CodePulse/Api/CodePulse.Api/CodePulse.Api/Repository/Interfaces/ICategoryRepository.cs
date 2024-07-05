﻿
using CodePulse.Api.Models.Domain;
using CodePulse.Api.Models.DTO;

namespace CodePulse.Api.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateAsync(Category category);
        IEnumerable<Category> GetCategories();
        Task<Category> GetCategoryById(string id);
        Task<Category> UpdateCategory(CategoryRequestDto request);
    }
}
