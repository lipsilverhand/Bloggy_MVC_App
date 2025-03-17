using Bloggy_MVC.Data;
using Bloggy_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace Bloggy_MVC.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggyDbContext bloggyDbContext;

        public BlogPostRepository(BloggyDbContext bloggyDbContext)
        {
            this.bloggyDbContext = bloggyDbContext;
        } 

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggyDbContext.AddAsync(blogPost);
            await bloggyDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var existingBlog = await bloggyDbContext.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                bloggyDbContext.BlogPosts.Remove(existingBlog);
                await bloggyDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggyDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggyDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await bloggyDbContext.BlogPosts.FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await bloggyDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                //save 
                await bloggyDbContext.SaveChangesAsync();
                return existingBlog;
            };

            return null;
        }

        public async Task<int> CountAsync()
        {
            return await bloggyDbContext.BlogPosts.CountAsync();
        }
    }
}
