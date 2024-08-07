
namespace ARCN.Repository.Repositories
{
    public class ProgramRepository : GenericRepository<ARCNProgram>, IProgramRepository
    {
        public ProgramRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
