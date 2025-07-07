using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViroCureDAL.basic;
using ViroCureDAL.Entities;

namespace ViroCureDAL.Repositories
{
    public class ViroCureUserRepository : GenericRepository<ViroCureUser>
    {
        public ViroCureUserRepository(ViroCureFal2024dbContext context) : base(context)
        {
        }
    }
}
