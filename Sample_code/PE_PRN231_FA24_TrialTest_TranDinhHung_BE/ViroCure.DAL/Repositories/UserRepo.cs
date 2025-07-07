using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViroCure.DAL.Entities;
using ViroCure.DAL.IRepositories;
using ViroCureDAL.basic;

namespace ViroCure.DAL.Repositories
{
    public class UserRepo : GenericRepository<ViroCureUser>, IUserRepo
    {

        public UserRepo(ViroCureFal2024dbContext context) : base(context)
        {
        }

        public async Task<ViroCureUser> LoginAsync(string email, string password)
        {
            return await _context.ViroCureUsers.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
        }
    }
}
