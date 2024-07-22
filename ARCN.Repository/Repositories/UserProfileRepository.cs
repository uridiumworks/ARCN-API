
namespace ARCN.Repository.Repositories
{
    public class UserProfileRepository : GenericRepository<ApplicationUser>, IUserProfileRepository
    {
        public UserProfileRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }

       
    }
}
