
namespace ARCN.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public bool IsAdmin { get; set; }

        public void UpdateUser(ApplicationUser applicationUser)
        {
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            PhoneNumber = applicationUser.PhoneNumber;

        }
    }
}
