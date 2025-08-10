using RecipeSharingAPI.Helpers.DTOs.PaginationDTOs;
using RecipeSharingAPI.Helpers.DTOs.RecipeDTOs;
using RecipeSharingAPI.Helpers.DTOs.Responses;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Services.Interface
{
    public interface IRecipeService
    {
        Task<Recipe> GetById(Guid Id);
        Task<List<Recipe>> GetAllByCategoryId(Guid CategoryId, PaginationConfgDTO pagDTO);
        Task<List<Recipe>> GetAllRecipesStartWith(string part, PaginationConfgDTO pagDTO);
        Task<List<Recipe>> GetAllRecipesBypublicationDate(DateTime publicationDate, PaginationConfgDTO pagDTO);
        Task<SimpleResponseDTO> Create(CreateRecipeDTO model, List<IFormFile> files);
        Task<SimpleResponseDTO> UploadImage(Guid RecipeId, IFormFile files);
        Task<SimpleResponseDTO> DeleteImage(string PublicId);
    }
}
