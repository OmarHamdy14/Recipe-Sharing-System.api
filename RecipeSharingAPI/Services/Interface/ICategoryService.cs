using RecipeSharingAPI.Helpers.DTOs.CategoryDTOs;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Services.Interface
{
    public interface ICategoryService
    {
        Task<Category> GetById(Guid categoryId);
        Task<List<Category>> GetAll();
        Task<Category> Create(CreateCategoryDTO model);
        Task<Category> Update(Category category, UpdateCategoryDTO model);
        Task Delete(Category category);
    }
}
