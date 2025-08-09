namespace RecipeSharingAPI.Helpers.DTOs.AccountDTOs
{
    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
