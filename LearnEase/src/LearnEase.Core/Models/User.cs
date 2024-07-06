using Microsoft.AspNetCore.Identity;

namespace LearnEase.Core.Models;

public class User : IdentityUser
{
    public bool IsMuted { get; set; }

    public bool IsActive { get; set; }

    public string? AvatarPath { get; set; }
}
