
using CodePulse.Api.Models.Domain;
using CodePulse.Api.Models.DTO;

namespace CodePulse.Api.Repository.Interfaces
{
    public interface IBlogPostsRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetBlogPostById(string id);
        Task<BlogPost> UpdateBlogPost(BlogpostModel request);
        Task DeleteBlogPost(string id);
    }
}
