


namespace ARCN.Application.Interfaces.Repositories
{
    public interface IStateRepository : IGenericRepository<State>
    {
        ValueTask<List<State>> GetStates();
    }
}
