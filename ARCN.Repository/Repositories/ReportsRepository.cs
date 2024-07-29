
namespace ARCN.Repository.Repositories
{
    public class ReportsRepository : GenericRepository<Reports>, IReportsRepository
    {
        public ReportsRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }

    }
}
