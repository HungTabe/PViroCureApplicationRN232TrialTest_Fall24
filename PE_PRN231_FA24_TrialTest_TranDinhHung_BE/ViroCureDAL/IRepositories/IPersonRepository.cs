using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViroCureDAL.Entities;

namespace ViroCureDAL.IRepositories
{
    public interface IPersonRepository
    {
        // login
        Task<ViroCureUser> LoginAsync(string email, string password);
    }
}
