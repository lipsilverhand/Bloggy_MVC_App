using Microsoft.AspNetCore.Identity;

namespace Bloggy_MVC.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
