
namespace ARCN.Repository.Repositories
{
    public class ExtensionRepository : GenericRepository<Extension>, IExtensionRepository
    {
        public ExtensionRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
       
    }
}
