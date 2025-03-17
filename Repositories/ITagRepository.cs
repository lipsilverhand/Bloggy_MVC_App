using Bloggy_MVC.Models.Domain;

namespace Bloggy_MVC.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery = null,
            string? sortBy = null,
            string? sortDirection = null,
            int pageNumber = 1,
            int pageSize = 100
            );

        Task<Tag?> GetAsync(Guid id);

        Task<Tag?> AddAsync(Tag id);

        Task<Tag?> UpdateAsync(Tag id);

        Task<Tag?> DeleteAsync(Guid id);

        Task<int> CountAsync();
    }
}
