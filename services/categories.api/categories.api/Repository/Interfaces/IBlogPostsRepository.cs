
using CodePulse.Api.Models.Domain;
using CodePulse.Api.Models.DTO;

namespace CodePulse.Api.Repository.Interfaces
{
    public interface IBlogPostsRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        IEnumerable<BlogPost> GetBlogPosts();
        Task<BlogPost> GetBlogPostById(string id);
        Task<BlogPost> UpdateBlogPost(BlogpostModel request);
        Task DeleteBlogPost(string id);
    }
}
