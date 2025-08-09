using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Base;
using RecipeSharingAPI.CloudinaryConfgs;
using RecipeSharingAPI.Helpers.DTOs.PaginationDTOs;
using RecipeSharingAPI.Helpers.DTOs.RecipeDTOs;
using RecipeSharingAPI.Helpers.DTOs.Responses;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Services.Interface;

namespace RecipeSharingAPI.Services.Implementation
{
    public class RecipeService : IRecipeService
    {
        private readonly IEntityBaseRepository<Recipe> _base;
        private readonly UserManager<ApplicationUser> _userManeger;
        private readonly AppDbContext _appDbContext;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        public RecipeService(IEntityBaseRepository<Recipe> @base, IMapper mapper, ICloudinaryService cloudinaryService, UserManager<ApplicationUser> userManeger, AppDbContext appDbContext)
        {
            _base = @base;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _userManeger = userManeger;
            _appDbContext = appDbContext;
        }
        public async Task<Recipe> GetById(Guid Id)
        {
            return await _base.Get(r => r.Id == Id,"RecipeImages");
        }
        public async Task<List<Recipe>> GetAllByCategoryId(Guid CategoryId, PaginationConfgDTO pagDTO)
        {
            var recipes = await _base.GetAll(r => r.CategoryId == CategoryId, "RecipeImages");
            return recipes.OrderBy(r => r.Name).Skip(pagDTO.PageNum - 1 * pagDTO.PageSize).Take(pagDTO.PageSize).ToList();

        }
        public async Task<List<Recipe>> GetAllRecipesStartWith(string part, PaginationConfgDTO pagDTO)
        {
            var recipes = await _appDbContext.Recipes.Where(r => r.ChefName.StartsWith(part)).ToListAsync();
            return recipes.OrderBy(r => r.Name).Skip(pagDTO.PageNum - 1 * pagDTO.PageSize).Take(pagDTO.PageSize).ToList();
        }
        public async Task<List<Recipe>> GetAllRecipesBypublicationDate(DateTime publicationDate, PaginationConfgDTO pagDTO)
        {
            var recipes = await _base.GetAll(r => r.CreatedDate.Date == publicationDate.Date);
            return recipes.OrderByDescending(r => r.CreatedDate).Skip(pagDTO.PageNum - 1 * pagDTO.PageSize).Take(pagDTO.PageSize).ToList();
        }
        //get last recipes as facebook home page ???
        public async Task<SimpleResponseDTO> Create(CreateRecipeDTO model, List<IFormFile> files)
        {
            var recipe = _mapper.Map<Recipe>(model);
            var chef = await _userManeger.FindByIdAsync(model.ChefId);
            recipe.ChefName = chef.FirstName + ' ' + chef.LastName;
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
