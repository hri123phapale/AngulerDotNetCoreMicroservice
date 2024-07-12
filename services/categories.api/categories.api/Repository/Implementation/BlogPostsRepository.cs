using Azure.Core;
using CodePulse.Api.Data;
using CodePulse.Api.Models.Domain;
using CodePulse.Api.Models.DTO;
using CodePulse.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CodePulse.Api.Repository.Implementation
{
    public class BlogPostsRepository : IBlogPostsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostsRepository(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost BlogPost)
        {
            await dbContext.AddAsync(BlogPost);
            await dbContext.SaveChangesAsync();
            return BlogPost;
        }
        public  async Task< IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts
                .Include(a=>a.Categories)
                .ToListAsync(); 
        }

        public async Task<BlogPost> GetBlogPostById(string id)
        { 
            return await dbContext.BlogPosts
                .Include(a => a.Categories)
                .FirstAsync(a => a.Id == Guid.Parse(id));
        }
        public async Task<BlogPost> UpdateBlogPost(BlogpostModel request)
        {
            var blogPost = await dbContext.BlogPosts.FirstAsync(a => a.Id == request.Id);
            blogPost.Title = request.Title;
            blogPost.UrlHandle = request.UrlHandle;
            blogPost.ShortDescription = request.ShortDescription;
            blogPost.Content = request.Content;
            blogPost.FeaturedImageUrl = request.FeaturedImageUrl;
            blogPost.PublishDate = request.PublishDate;
            blogPost.Auther = request.Auther;
            blogPost.IsVisible = request.IsVisible; 

            dbContext.BlogPosts.Update(blogPost);
            await dbContext.SaveChangesAsync();

            return blogPost;
        } 
        public async Task DeleteBlogPost(string id)
        {
            var blogPost = await dbContext.BlogPosts.FirstAsync(a => a.Id == Guid.Parse(id));
            dbContext.BlogPosts.Remove(blogPost);
            await dbContext.SaveChangesAsync(); 

        }
         
    }
}
