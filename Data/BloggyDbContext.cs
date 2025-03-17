using Bloggy_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy_MVC.Data
{
    public class BloggyDbContext : DbContext
    {
        public BloggyDbContext(DbContextOptions<BloggyDbContext> options) : base(options)
        {
        }
        //add relatationship between Blog and Tags
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<BlogPostLike> BlogPostLikes { get; set; }

        public DbSet<BlogPostComment> BlogPostComments { get; set; }

        
    }
}
