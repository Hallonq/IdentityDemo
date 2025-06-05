
using IdentityDemo.Application.Dtos;
using IdentityDemo.Application.Users;
using IdentityDemo.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityDemo.Infrastructure.Services;
public class IdentityUserService
    (
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> RoleManager
    ) : IIdentityUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password)
    {
        var result = await userManager.CreateAsync(new ApplicationUser()
        {
            UserName = user.Email,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        }, password);

        if (result.Succeeded)
        {
            await userManager.AddClaimAsync(await userManager.FindByEmailAsync(user.Email), new Claim("Lexicon", "Student"));
        }

        return new UserResultDto(result.Errors.FirstOrDefault()?.Description);
    }

    public async Task<UserResultDto> SignInAsync(string email, string password)
    {

        var result = await signInManager.PasswordSignInAsync(email, password, false, false);

        return new UserResultDto(result.Succeeded ? null : "Error :)");
    }

    public Task SignOutAsync()
    {
        return signInManager.SignOutAsync();
    }
}
