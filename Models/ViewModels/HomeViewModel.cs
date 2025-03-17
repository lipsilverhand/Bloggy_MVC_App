using Bloggy_MVC.Models.Domain;

namespace Bloggy_MVC.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; } 

        public IEnumerable<Tag> Tags { get; set; } 

    }
}
