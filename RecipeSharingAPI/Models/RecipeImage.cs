namespace RecipeSharingAPI.Models
{
    public class RecipeImage
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
