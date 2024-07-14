using CodePulse.Api.Data;
using CodePulse.Api.Models.Domain;
using CodePulse.Api.Models.DTO;
using CodePulse.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostsRepository _iBlogPostRepository;

        private readonly ICategoryRepository _iCategoryRepository;

        public BlogPostsController(IBlogPostsRepository iBlogPostRepository, ICategoryRepository iCategoryRepository)
        {
            _iBlogPostRepository = iBlogPostRepository;
            _iCategoryRepository = iCategoryRepository;
        }
        [HttpPost("CreateBlogPost")]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateBlogPost(BlogpostModel request)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                PublishDate = request.PublishDate,
                Auther = request.Auther,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };
            foreach (var categoryId in request.Categories)
            {
                var existingCategory = await _iCategoryRepository.GetCategoryById(categoryId.ToString());
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }
            var blogPostNew = await _iBlogPostRepository.CreateAsync(blogPost);
            return Ok(new BlogpostDto
            {
                Title = blogPostNew.Title,
                UrlHandle = blogPostNew.UrlHandle,
                ShortDescription = blogPostNew.ShortDescription,
                Content = blogPostNew.Content,
                FeaturedImageUrl = blogPostNew.FeaturedImageUrl,
                PublishDate = blogPostNew.PublishDate,
                Auther = blogPostNew.Auther,
                IsVisible = blogPostNew.IsVisible,
                Categories = blogPostNew.Categories.Select(a => new CategoryRequestDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    UrlHandle = a.UrlHandle
                }).ToList()
            });
        }
        [HttpGet("GetAllBlogsPosts")]
        public async Task<IActionResult> GetAllBlogPosts()
        {

            var blogposts = await _iBlogPostRepository.GetAllAsync();
            var model = blogposts.ToList().Select(blogPost => new BlogpostDto
            {
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishDate = blogPost.PublishDate,
                Auther = blogPost.Auther,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(a => new CategoryRequestDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    UrlHandle = a.UrlHandle
                }).ToList()
            }).ToList();

            return Ok(model);
        }

        [HttpGet("GetBlogPost/{id}")]
        public async Task<IActionResult> GetBlogPostById(string id)
        {
            var blogPost = await _iBlogPostRepository.GetBlogPostById(id);
            var model = new BlogpostDto
            {
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishDate = blogPost.PublishDate,
                Auther = blogPost.Auther,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(a => new CategoryRequestDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    UrlHandle = a.UrlHandle
                }).ToList()
            };

            return Ok(model);
        }
        [HttpGet("GetBlogPostByUrlHandle/{url}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle(string url)
        {
            var blogPost = await _iBlogPostRepository.GetBlogPostByUrlHandle(url);
            var model = new BlogpostDto
            {
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishDate = blogPost.PublishDate,
                Auther = blogPost.Auther,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(a => new CategoryRequestDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    UrlHandle = a.UrlHandle
                }).ToList()
            };

            return Ok(model);
        }
        [HttpPut("updateBlogPost")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBlogPost(BlogpostModel request)
        {
            var blogPost = await _iBlogPostRepository.UpdateBlogPost(request);
            var model = new BlogpostDto
            {
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishDate = blogPost.PublishDate,
                Auther = blogPost.Auther,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(a => new CategoryRequestDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    UrlHandle = a.UrlHandle
                }).ToList()
            };

            return Ok(model);
        }
        [HttpDelete("deleteBlogPost/{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPost(string id)
        {
            await _iBlogPostRepository.DeleteBlogPost(id);
            return Ok();
        }
    }
}
