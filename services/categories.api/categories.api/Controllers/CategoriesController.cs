using CodePulse.Api.Data;
using CodePulse.Api.Models.Domain;
using CodePulse.Api.Models.DTO;
using CodePulse.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _iCategoryRepository;

        public CategoriesController(ICategoryRepository iCategoryRepository)
        {
            _iCategoryRepository = iCategoryRepository;
        }
        [HttpPost("CreateCategory")] 
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            }; 
            var categoryNew = await _iCategoryRepository.CreateAsync(category); 
            return Ok(new CategoryRequestDto 
            { 
                Id = categoryNew .Id,
                Name=categoryNew.Name,
                UrlHandle=categoryNew.UrlHandle
            });
        }
        [HttpGet("GetAllCategories")]
        public   IActionResult  GetAllCategory()
        { 
            var categories =   _iCategoryRepository.GetCategories();
            var categoriesDto = categories.Select(category => new CategoryRequestDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            }).ToList();

             return Ok(categoriesDto);
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var category =await _iCategoryRepository.GetCategoryById(id);
            var categoriesDto = new CategoryRequestDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            } ;

            return Ok(categoriesDto);
        }
        [HttpPut("updateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryRequestDto request)
        {
            var category = await _iCategoryRepository.UpdateCategory(request);
            var categoriesDto = new CategoryRequestDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(categoriesDto);
        }
        [HttpDelete("deleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
             await _iCategoryRepository.DeleteCategory(id);  
            return Ok();
        }
    }
}
