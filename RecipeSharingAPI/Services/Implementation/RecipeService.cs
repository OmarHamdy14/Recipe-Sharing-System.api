using AutoMapper;
using RecipeSharingAPI.Base;
using RecipeSharingAPI.CloudinaryConfgs;
using RecipeSharingAPI.Helpers.DTOs.RecipeDTOs;
using RecipeSharingAPI.Helpers.DTOs.Responses;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Services.Interface;

namespace RecipeSharingAPI.Services.Implementation
{
    public class RecipeService : IRecipeService
    {
        private readonly IEntityBaseRepository<Recipe> _base;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        public RecipeService(IEntityBaseRepository<Recipe> @base, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _base = @base;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<Recipe> GetById(Guid Id)
        {
            return await _base.Get(r => r.Id == Id,"RecipeImages");
        }
        public async Task<List<Recipe>> GetAllByCategoryId(Guid CategoryId)
        {
            return await _base.GetAll(r => r.CategoryId == CategoryId, "RecipeImages");
        }
        //get last recipes as facebook home page ???
        public async Task<SimpleResponseDTO> Create(CreateRecipeDTO model, List<IFormFile> files)
        {
            var recipe = _mapper.Map<Recipe>(model);
            await _base.Create(recipe);
            var res = await _cloudinaryService.UploadListOfImagesAsync(recipe.Id, files);
            if (res.IsSuccess) return new SimpleResponseDTO() { IsSuccess = true, Message = "Creation is done" };
            return new SimpleResponseDTO() { IsSuccess = false, Message = "Creation failed" };
        }
        public async Task<SimpleResponseDTO> UploadImage(Guid RecipeId, IFormFile files)
        {
            var res = await _cloudinaryService.UploadImageAsync(RecipeId, files);
            if (res.IsSuccess) return new SimpleResponseDTO() { IsSuccess = true, Message = "Creation is done" };
            return new SimpleResponseDTO() { IsSuccess = false, Message = "Creation failed" };
        }
        public async Task<SimpleResponseDTO> DeleteImage(string PublicId)
        {
            var res = await _cloudinaryService.DeleteImageAsync(PublicId);
            if (res.IsDeleted) return new SimpleResponseDTO() { IsSuccess = true, Message = "Creation is done" };
            return new SimpleResponseDTO() { IsSuccess = false, Message = "Deletion failed" };
        }
    }
}
