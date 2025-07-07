using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViroCureDAL.Entities;

namespace ViroCureDAL.IRepositories
{
    public interface IVirusRepository
    {
        Task<Virus?> GetVirusByNameAsync(string virusName);
        Task<Virus> CreateVirusAsync(Virus virus);
    }
}
