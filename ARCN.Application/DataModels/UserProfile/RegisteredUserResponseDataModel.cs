namespace ARCN.Application.DataModels.UserProfile
{
    public class RegisteredUserResponseDataModel
    {
        public ApplicationUser? ApplicationUser { get; set; } = new ApplicationUser();
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
    }
}
