
namespace ARCN.Repository.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
