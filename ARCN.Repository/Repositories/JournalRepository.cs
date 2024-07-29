
namespace ARCN.Repository.Repositories
{
    public class JournalRepository : GenericRepository<Journals>, IJournalRepository
    {
        public JournalRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }

    }
}
