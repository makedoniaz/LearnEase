namespace LearnEase.Core.Services;

public interface IAdminPageService
{
    Task ToggleMuteUser(string userId);

    Task TogglePromoteUserToAdmin(string userId);

    Task TogglePromoteUserToAuthor(string userId);

    Task ToggleBanUser(string userId);
}
