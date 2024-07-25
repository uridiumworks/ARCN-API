namespace ARCN.Application.DataModels.UserProfile
{
    public class NewUserDataModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? roleName { get; set; }

    }
  
}
