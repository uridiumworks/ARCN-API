
namespace ARCN.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? MiddleName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? DateOfBirth { get; set; }
        public int? StateId { get; set; }
        public string? PermanentAddress { get; set; }

        public void UpdateUser(ApplicationUser applicationUser)
        {
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            MiddleName = applicationUser.MiddleName;
            DateOfBirth = applicationUser.DateOfBirth;
            PermanentAddress = applicationUser.PermanentAddress;
            ProfileImageUrl = applicationUser.ProfileImageUrl ?? "";
            PhoneNumber = applicationUser.PhoneNumber;
            StateId = applicationUser.StateId;

        }
    }
}
