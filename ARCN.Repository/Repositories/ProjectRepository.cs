
namespace ARCN.Repository.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
