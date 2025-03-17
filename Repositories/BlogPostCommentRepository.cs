using Bloggy_MVC.Data;
using Bloggy_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy_MVC.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggyDbContext bloggyDbContext;

        public BlogPostCommentRepository(BloggyDbContext bloggyDbContext)
        {
            this.bloggyDbContext = bloggyDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await bloggyDbContext.BlogPostComments.AddAsync(blogPostComment);
            await bloggyDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentByBlogIdAsync(Guid blogPostId)
        {
            return await bloggyDbContext.BlogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();

        }
    }
}
