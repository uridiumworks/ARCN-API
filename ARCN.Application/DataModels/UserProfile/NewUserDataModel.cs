namespace ARCN.Application.DataModels.UserProfile
{
    public class NewUserDataModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? MiddleName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string? ProfileImageUrl { get; set; }
        public string DateOfBirth { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int? StateId { get; set; }
        public string? PermanentAddress { get; set; }
    }
}
