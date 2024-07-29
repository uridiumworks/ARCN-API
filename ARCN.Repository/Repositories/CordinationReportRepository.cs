
namespace ARCN.Repository.Repositories
{
    public class CordinationReportRepository : GenericRepository<CordinationReport>, ICordinationReportRepository
    {
        public CordinationReportRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
