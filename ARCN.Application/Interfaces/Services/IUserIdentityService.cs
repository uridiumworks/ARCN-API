namespace ARCN.Application.Interfaces.Services
{
    public interface IUserIdentityService
    {
        string UserName { get; }
        string Email { get; }
        string? UserProfileId { get; }
        string? UserId { get; }
    }
}
