using Microsoft.AspNetCore.Identity;
using RecipeSharingAPI.Helpers.DTOs.AccountDTOs;
using RecipeSharingAPI.Models;

namespace RecipeSharingAPI.Services.Interface
{
    public interface IAccountService
    {
        Task<ApplicationUser> FindById(string userId);
        Task<ApplicationUser> FindByEmail(string email);
        Task<ApplicationUser> FindByUserName(string name);
        Task<List<ApplicationUser>> GetAllUsers();
        Task<AuthModel> Register(RegistrationDTO model);
        Task<AuthModel> GetTokenAsync(LogInDTO model);
        Task<IdentityResult> Update(ApplicationUser user, UpdateUserDTO model);
        Task<bool> ChangePassword(ApplicationUser user, ChangePasswordDTO model);
    }
}
