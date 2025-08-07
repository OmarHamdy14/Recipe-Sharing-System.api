using RecipeSharingAPI.Helpers.DTOs.RecipeDTOs;
using RecipeSharingAPI.Helpers.DTOs.Responses;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Services.Interface
{
    public interface IRecipeService
    {
        Task<Recipe> GetById(Guid Id);
        Task<List<Recipe>> GetAllByCategoryId(Guid CategoryId);
        Task<SimpleResponseDTO> Create(CreateRecipeDTO model, List<IFormFile> files);
        Task<SimpleResponseDTO> UploadImage(Guid RecipeId, IFormFile files);
        Task<SimpleResponseDTO> DeleteImage(string PublicId);
    }
}
