
using Azure;
using Bloggy_MVC.Data;
using Bloggy_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy_MVC.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggyDbContext bloggyDbContext;

        public TagRepository(BloggyDbContext bloggyDbContext)
        {
            this.bloggyDbContext = bloggyDbContext;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
        {
            var query = bloggyDbContext.Tags.AsQueryable();
            

            // Filtering
            if (string.IsNullOrWhiteSpace(searchQuery) == false) 
            {
                query = query.Where(x => x.Name.Contains(searchQuery) || x.DisplayName.Contains(searchQuery));
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase) )
                {
                    query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                }

                if (string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName);
                }

            }

            


            //Pagination
            var skipResult = (pageNumber - 1) * pageSize; 
            query = query.Skip(skipResult).Take(pageSize);

            return await query.ToListAsync();
           // return await bloggyDbContext.Tags.ToListAsync();


        }

        public async Task<Tag?> AddAsync(Tag tag)
        {
            await bloggyDbContext.Tags.AddAsync(tag);
            await bloggyDbContext.SaveChangesAsync();
            return tag;
        }


        public Task<Tag?> GetAsync(Guid id)
        {
            return bloggyDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag =  await bloggyDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await bloggyDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await bloggyDbContext.Tags.FindAsync(id);
            
            if (existingTag != null)
            {
                bloggyDbContext.Tags.Remove(existingTag);
                await bloggyDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<int> CountAsync()
        {
            return await bloggyDbContext.Tags.CountAsync();
        }
    }
}
