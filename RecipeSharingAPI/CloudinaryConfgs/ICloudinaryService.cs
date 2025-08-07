using CloudinaryDotNet.Actions;
using RecipeSharingAPI.Helpers.DTOs.Responses;

namespace RecipeSharingAPI.CloudinaryConfgs
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResponseDTO> UploadImageAsync(Guid RecipeId, IFormFile file);
        Task<ImageUploadResponseDTO> UploadListOfImagesAsync(Guid RecipeId, List<IFormFile> files);
        Task<ImageDeleteResponseDTO> DeleteImageAsync(string publicId);
    }
}