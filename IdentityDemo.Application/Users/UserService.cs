using IdentityDemo.Application.Dtos;

namespace IdentityDemo.Application.Users;
public class UserService
    (IIdentityUserService identityUserService) : IUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password) =>
        await identityUserService.CreateUserAsync(user, password);

    public async Task<UserResultDto> SignInAsync(string email, string password) =>
        await identityUserService.SignInAsync(email, password);
    public async Task SignOutAsync() =>
        await identityUserService.SignOutAsync();
}