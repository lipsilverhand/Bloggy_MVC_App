using Bloggy_MVC.Models.Domain;

namespace Bloggy_MVC.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentByBlogIdAsync(Guid blog);
    
    }
}
