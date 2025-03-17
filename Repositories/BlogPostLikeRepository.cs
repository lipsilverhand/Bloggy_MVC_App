
using Bloggy_MVC.Data;
using Bloggy_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy_MVC.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggyDbContext bloggyDbContext;

        public BlogPostLikeRepository(BloggyDbContext bloggyDbContext)
        {
            this.bloggyDbContext = bloggyDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await bloggyDbContext.BlogPostLikes.AddAsync(blogPostLike);
            await bloggyDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await bloggyDbContext.BlogPostLikes.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggyDbContext.BlogPostLikes
                .CountAsync(x => x.BlogPostId == blogPostId);
        }

        

    }

       
    
}
