
namespace ARCN.Repository.Repositories
{
    public class NarisRepository : GenericRepository<Naris>, INarisRepository
    {
        public NarisRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
