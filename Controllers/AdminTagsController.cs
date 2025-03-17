using Bloggy_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Bloggy_MVC.Data;
using System.ComponentModel;
using Bloggy_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Bloggy_MVC.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Bloggy_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            ValidteAddTagRequest(addTagRequest);
            if (ModelState.IsValid == false)
            {
                return View();
            }
            //Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);


            return RedirectToAction("List"); //Automatic return to List page after added new tag
        }

        
        [HttpGet]
        [ActionName("List")] 
        public async Task<IActionResult> List(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 3, int pageNumber = 1)
        {
            var totalRecords = await tagRepository.CountAsync();
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber--;
            }

            if (pageNumber < 1)
            {
                pageNumber++;
            }

            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            var tags = await tagRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

            return View(tags);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View();
        }

        
        [HttpPost]
        public  async Task<IActionResult> Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                
            } else
            {

            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id});
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            var deleteTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deleteTag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }


        private void ValidteAddTagRequest(AddTagRequest addTagRequest)
        {
            if (addTagRequest.Name != null && addTagRequest.DisplayName != null)
            {
                if (addTagRequest.Name == addTagRequest.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }
    }
}
