namespace RecipeSharingAPI.Helpers.DTOs.RecipeDTOs
{
    public class UpdateRecipeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        //public string ChefId { get; set; }
    }
}
