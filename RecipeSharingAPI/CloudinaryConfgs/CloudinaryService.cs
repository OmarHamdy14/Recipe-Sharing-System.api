using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Base;
using RecipeSharingAPI.Helpers.DTOs.Responses;

namespace RecipeSharingAPI.CloudinaryConfgs
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IEntityBaseRepository<RecipeImage> _baseImage;

        public CloudinaryService(IOptions<CloudinarySettings> config, IEntityBaseRepository<RecipeImage> baseImage)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
            _baseImage = baseImage;
        }
        public async Task<ImageUploadResponseDTO> UploadImageAsync(Guid RecipeId, IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file == null || file.Length == 0)
            {
                return new ImageUploadResponseDTO
                {
                    IsSuccess = false,
                    Message = "File is empty or null"
                };
            }

            try
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };

                var result = await _cloudinary.UploadAsync(uploadParams);

                if (result.Error != null)
                {
                    return new ImageUploadResponseDTO
                    {
                        IsSuccess = false,
                        Message = result.Error.Message
                    };
                }
                var recipeImage = new RecipeImage()
                {
                    Url = uploadResult.SecureUrl.AbsoluteUri,
                    PublicId = uploadResult.PublicId,
                    RecipeId = RecipeId
                };
                await _baseImage.Create(recipeImage);
                return new ImageUploadResponseDTO
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ImageUploadResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }
        }


        public async Task<ImageUploadResponseDTO> UploadListOfImagesAsync(Guid RecipeId, List<IFormFile> files)
        {
            var uploadResult = new ImageUploadResult();
            foreach(var file in files)
            {
                if (file == null || file.Length == 0)
                {
                    return new ImageUploadResponseDTO
                    {
                        IsSuccess = false,
                        Message = "File is empty or null"
                    };
                }

                try
                {
                    using var stream = file.OpenReadStream();

                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                    };

                    var result = await _cloudinary.UploadAsync(uploadParams);

                    if (result.Error != null)
                    {
                        return new ImageUploadResponseDTO
                        {
                            IsSuccess = false,
                            Message = result.Error.Message
                        };
                    }
                    var recipeImage = new RecipeImage()
                    {
                        Url = uploadResult.SecureUrl.AbsoluteUri,
                        PublicId = uploadResult.PublicId,
                        RecipeId = RecipeId
                    };
                    await _baseImage.Create(recipeImage);
                    return new ImageUploadResponseDTO
                    {
                        Url = result.SecureUrl.AbsoluteUri,
                        PublicId = result.PublicId,
                        IsSuccess = true
                    };
                }
                catch (Exception ex)
                {
                    return new ImageUploadResponseDTO
                    {
                        IsSuccess = false,
                        Message = $"Unexpected error: {ex.Message}"
                    };
                }
            }
            return new ImageUploadResponseDTO(){ IsSuccess=false };
        }

        public async Task<ImageDeleteResponseDTO> DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId))
            {
                return new ImageDeleteResponseDTO
                {
                    IsDeleted = false,
                    ErrorMessage = "Invalid publicId"
                };
            }

            try
            {
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.Result == "ok" || result.Result == "deleted")
                {
                    return new ImageDeleteResponseDTO
                    {
                        IsDeleted = true
                    };
                }

                return new ImageDeleteResponseDTO
                {
                    IsDeleted = false,
                    ErrorMessage = $"Failed to delete image. Reason: {result.Result}"
                };
            }
            catch (Exception ex)
            {
                return new ImageDeleteResponseDTO
                {
                    IsDeleted = false,
                    ErrorMessage = $"Unexpected error: {ex.Message}"
                };
            }
        }
    }
}
