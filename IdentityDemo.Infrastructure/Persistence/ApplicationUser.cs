﻿
using Microsoft.AspNetCore.Identity;

namespace IdentityDemo.Infrastructure.Persistence;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
