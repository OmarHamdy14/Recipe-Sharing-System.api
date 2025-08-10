using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingAPI.Helpers.DTOs.PaginationDTOs;
using RecipeSharingAPI.Helpers.DTOs.RecipeDTOs;
using RecipeSharingAPI.Helpers.DTOs.Responses;
using RecipeSharingAPI.Models;
using RecipeSharingAPI.Services.Interface;

namespace RecipeSharingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            if(Guid.Empty == Id) return BadRequest();
            try
            {
                var recipe = await _recipeService.GetById(Id);
                if (recipe == null) return NotFound();
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [HttpGet("GetAllByCategoryId/{CategoryId}")]
        public async Task<IActionResult> GetAllByCategoryId(Guid CategoryId, [FromBody]PaginationConfgDTO pagDTO)
        {
            if (Guid.Empty == CategoryId) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var recipes = await _recipeService.GetAllByCategoryId(CategoryId,pagDTO);
                if (!recipes.Any()) return NotFound();
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [HttpGet("GetAllRecipesStartWith/{part}")]
        public async Task<IActionResult> GetAllRecipesStartWith(string part, [FromBody]PaginationConfgDTO pagDTO)
        {
            if (string.IsNullOrEmpty(part)) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var recipes = await _recipeService.GetAllRecipesStartWith(part, pagDTO);
                if (!recipes.Any()) return NotFound();
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [HttpGet("GetAllRecipesBypublicationDate/{publicationDate}")]
        public async Task<IActionResult> GetAllRecipesBypublicationDate(DateTime publicationDate, [FromBody] PaginationConfgDTO pagDTO)
        {
            // chech datatime is null or not
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var recipes = await _recipeService.GetAllRecipesBypublicationDate(publicationDate, pagDTO);
                if (!recipes.Any()) return NotFound();
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateRecipeDTO model, [FromForm]List<IFormFile> files)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var res = await _recipeService.Create(model, files);
                if (res.IsSuccess) return Ok(new { Message = res.Message });
                return BadRequest(new { Message = res.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [HttpPost("UploadImage/{RecipeId}")]
        public async Task<IActionResult> UploadImage(Guid RecipeId, [FromBody]IFormFile file)
        {
            if (Guid.Empty == RecipeId) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var res = await _recipeService.UploadImage(RecipeId, file);
                if (res.IsSuccess) return Ok(new { Message = res.Message });
                return BadRequest(new { Message = res.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [HttpDelete("DeleteImage/{PublicId}")]
        public async Task<IActionResult> DeleteImage(string PublicId)
        {
            if (string.IsNullOrEmpty(PublicId)) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var res = await _recipeService.DeleteImage(PublicId);
                if (res.IsSuccess) return Ok(new { Message = res.Message });
                return BadRequest(new { Message = res.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
