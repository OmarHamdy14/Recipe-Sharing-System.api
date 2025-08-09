using System.ComponentModel;

namespace RecipeSharingAPI.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ChefName { get; set; } /*= Chef.FirstName + ' ' + Chef.LastName;*/
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string ChefId { get; set; }
        public ApplicationUser Chef { get; set; }
        public ICollection<RecipeImage> RecipeImages { get; set; }
    }
}
