using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.SignalrServices
{
    public class SearchRecipeHub : Hub
    {
        private readonly AppDbContext _appDbContext;
        public SearchRecipeHub(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task SearchRecipe(string part)
        {
            if (string.IsNullOrEmpty(part)) { await Clients.Caller.SendAsync("SearchRecipeResponse", new List<object> { }); return; }
            var recipes = await _appDbContext.Recipes.Where(r => r.Name.StartsWith(part)).Select(r => new { r.Name, r.ChefName }).Take(10).ToListAsync();
            await Clients.Caller.SendAsync("SearchRecipeResponse", recipes);
        }
    }
}
