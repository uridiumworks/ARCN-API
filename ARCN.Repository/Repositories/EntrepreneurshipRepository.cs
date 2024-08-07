
namespace ARCN.Repository.Repositories
{
    public class EntrepreneurshipRepository : GenericRepository<Entrepreneurship>, IEntrepreneurshipRepository
    {
        public EntrepreneurshipRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
