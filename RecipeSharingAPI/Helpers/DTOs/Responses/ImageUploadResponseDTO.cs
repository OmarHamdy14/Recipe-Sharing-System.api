namespace RecipeSharingAPI.Helpers.DTOs.Responses
{
    public class ImageUploadResponseDTO
    {
        public string? Url { get; set; }
        public string? PublicId { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
