using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViroCure.DAL.Entities;

namespace ViroCure.DAL.IRepositories
{
    public interface IUserRepo
    {
        //Login
        Task<ViroCureUser> LoginAsync(string email, string password);
    }
}
