
namespace ARCN.Repository.Repositories
{
    public class SupervisorReportRepository : GenericRepository<SupervisionReport>, ISupervisionReportRepository
    {
        public SupervisorReportRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
