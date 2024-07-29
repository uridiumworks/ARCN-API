
namespace ARCN.Repository.Repositories
{
    public class NewsLetterRepository : GenericRepository<NewsLetter>, INewsLetterRepository
    {
        public NewsLetterRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }

    }
}
