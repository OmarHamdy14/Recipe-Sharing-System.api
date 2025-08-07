using Microsoft.AspNetCore.Identity;

namespace RecipeSharingAPI.Models
{
    public class Chef : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; }
    }
}
