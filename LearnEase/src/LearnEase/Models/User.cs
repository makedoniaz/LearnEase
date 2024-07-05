using Microsoft.AspNetCore.Identity;

namespace LearnEase.Models;

public class User : IdentityUser
{
    public bool IsMuted { get; set; }

    public bool IsActive { get; set; }

    public string? AvatarPath { get; set; }
}
