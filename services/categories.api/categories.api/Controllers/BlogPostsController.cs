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
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostsRepository _iBlogPostRepository;

        public BlogPostsController(IBlogPostsRepository iBlogPostRepository)
        {
            _iBlogPostRepository = iBlogPostRepository;
        }
        [HttpPost("CreateBlogPost")] 
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
                IsVisible = request.IsVisible
            }; 
            var blogPostNew = await _iBlogPostRepository.CreateAsync(blogPost); 
            return Ok(new BlogpostModel 
            {
                Title = blogPostNew.Title,
                UrlHandle = blogPostNew.UrlHandle,
                ShortDescription = blogPostNew.ShortDescription,
                Content = blogPostNew.Content,
                FeaturedImageUrl = blogPostNew.FeaturedImageUrl,
                PublishDate = blogPostNew.PublishDate,
                Auther = blogPostNew.Auther,
                IsVisible = blogPostNew.IsVisible
            });
        }
        [HttpGet("GetAllBlogsPosts")]
        public   IActionResult  GetAllBlogPost()
        {

            var blogposts =   _iBlogPostRepository.GetBlogPosts();
            var model = blogposts.Select(blogPost => new BlogpostModel
            {
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishDate = blogPost.PublishDate,
                Auther = blogPost.Auther,
                IsVisible = blogPost.IsVisible
            }).ToList();

             return Ok(model);
        }

        [HttpGet("GetBlogPost/{id}")]
        public async Task<IActionResult> GetBlogPostById(string id)
        {
            var blogPost =await _iBlogPostRepository.GetBlogPostById(id);
            var model = new BlogpostModel
            {
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishDate = blogPost.PublishDate,
                Auther = blogPost.Auther,
                IsVisible = blogPost.IsVisible
            } ;

            return Ok(model);
        }
        [HttpPut("updateBlogPost")]
        public async Task<IActionResult> UpdateBlogPost(BlogpostModel request)
        {
            var blogPost = await _iBlogPostRepository.UpdateBlogPost(request);
            var model = new BlogpostModel
            {
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishDate = blogPost.PublishDate,
                Auther = blogPost.Auther,
                IsVisible = blogPost.IsVisible
            };

            return Ok(model);
        }
        [HttpDelete("deleteBlogPost/{id}")]
        public async Task<IActionResult> DeleteBlogPost(string id)
        {
             await _iBlogPostRepository.DeleteBlogPost(id);  
            return Ok();
        }
    }
}
