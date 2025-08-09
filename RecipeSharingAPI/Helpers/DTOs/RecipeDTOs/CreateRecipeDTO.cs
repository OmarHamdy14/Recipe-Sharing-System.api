namespace RecipeSharingAPI.Helpers.DTOs.RecipeDTOs
{
    public class CreateRecipeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CategoryId { get; set; }
        public string ChefId { get; set; }
    }
}
