using Microsoft.AspNetCore.Identity;

namespace RecipeSharingAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Recipe> recipes { get; set; }
    }
}
