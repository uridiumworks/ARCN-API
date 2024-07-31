using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Repository.Repositories
{
    public class LocalGovernmentRepository : GenericRepository<LocalGovernmentArea>, ILocalGovernmentAreaRepository
    {
        public LocalGovernmentRepository(ARCNDbContext dbContext) : base(dbContext)
        {
        }
    }
}
