
namespace ARCN.Repository.Repositories
{
    public class FCARepository : GenericRepository<FCA>, IFCARepository
    {
        public FCARepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
