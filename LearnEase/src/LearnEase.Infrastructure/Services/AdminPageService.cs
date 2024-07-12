using System.Security.Claims;
using LearnEase.Core.Models;
using LearnEase.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace LearnEase.Infrastructure.Services;

public class AdminPageService : IAdminPageService
{
    private readonly UserManager<User> userManager;

    public AdminPageService(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    public async Task ToggleBanUser(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            throw new Exception("User not found!");

        user.IsActive = !user.IsActive;
        await userManager.UpdateAsync(user);
    }

    public async Task TogglePromoteUserToAdmin(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            throw new Exception("User not found!");

        var userIsAdmin = (await userManager.GetRolesAsync(user)).Contains("Admin");

        if (userIsAdmin) {
            await userManager.RemoveFromRoleAsync(user, "Admin");
            return;
        }

        await userManager.AddToRoleAsync(user, "Admin");
    }

    public async Task TogglePromoteUserToAuthor(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            throw new Exception("User not found!");

        var userIsAdmin = (await userManager.GetRolesAsync(user)).Contains("Author");

        if (userIsAdmin) {
            await userManager.RemoveFromRoleAsync(user, "Author");
            return;
        }

        await userManager.AddToRoleAsync(user, "Author");
    }

    public async Task ToggleMuteUser(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            throw new Exception("User not found!");

        user.IsMuted = !user.IsMuted;
        await userManager.UpdateAsync(user);

        var claims = await userManager.GetClaimsAsync(user);
        var isMutedClaim = claims.FirstOrDefault(c => c.Type == "IsMuted");

        if (isMutedClaim == null)
            throw new Exception("Muted claim doesn't exist!");
            
        await userManager.RemoveClaimAsync(user, isMutedClaim);
        await userManager.AddClaimAsync(user, new Claim("IsMuted", user.IsMuted.ToString()));
    }
}
