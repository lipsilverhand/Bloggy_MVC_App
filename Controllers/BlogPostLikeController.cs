using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bloggy_MVC.Models.ViewModels;
using Bloggy_MVC.Repositories;
using Bloggy_MVC.Models.Domain;
using Bloggy_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace Bloggy_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly BloggyDbContext bloggyDbContext;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository, BloggyDbContext bloggyDbContext) 
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.bloggyDbContext = bloggyDbContext;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var model = new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId,
            };

            await blogPostLikeRepository.AddLikeForBlog(model);
            return Ok("Like added successfully.");
        }

        

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var totalLike = await blogPostLikeRepository.GetTotalLikes(blogPostId);
            return Ok(totalLike);
        }

        
    }
}
