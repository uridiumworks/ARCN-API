
namespace ARCN.Repository.Repositories
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
        public async ValueTask<List<State>> GetStates()
        {
            var res =  await this.FindAll().OrderBy(x=>x.StateName)
                .ToListAsync();

            return res;
        }
    }
}
