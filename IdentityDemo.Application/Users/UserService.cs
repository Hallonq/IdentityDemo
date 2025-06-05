using IdentityDemo.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityDemo.Application.Users;
public class UserService : IUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password) =>
        await Task.FromResult(new UserResultDto("Creating users is not yet implemented"));

    public async Task<UserResultDto> SignInAsync(string email, string password) =>
        await Task.FromResult(new UserResultDto("Signing in is not yet implemented"));
}