
namespace ARCN.Repository.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
