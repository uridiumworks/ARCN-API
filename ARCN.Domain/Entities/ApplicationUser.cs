﻿
namespace ARCN.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
           Blogs=new HashSet<Blog>();
           Journals=new HashSet<Journals>();
           NewsLetters=new HashSet<NewsLetter>();
           Reports=new HashSet<Reports>();
            Naris=new HashSet<Naris>();
            CordinationReports=new HashSet<CordinationReport>();
        }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public bool IsAdmin { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Journals> Journals { get; set; }
        public virtual ICollection<NewsLetter> NewsLetters { get; set; }
        public virtual ICollection<Reports> Reports { get; set; }
        public virtual ICollection<Naris> Naris { get; set; }
        public virtual ICollection<CordinationReport> CordinationReports { get; set; }
        public void UpdateUser(ApplicationUser applicationUser)
        {
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            PhoneNumber = applicationUser.PhoneNumber;

        }
    }
}
