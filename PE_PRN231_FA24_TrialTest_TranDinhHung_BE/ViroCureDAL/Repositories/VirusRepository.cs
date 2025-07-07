using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViroCureDAL.basic;
using ViroCureDAL.Entities;
using ViroCureDAL.IRepositories;

namespace ViroCureDAL.Repositories
{
    public class VirusRepository : GenericRepository<Virus>, IVirusRepository
    {
        public VirusRepository(ViroCureFal2024dbContext context) : base(context)
        {
        }

        public async Task<Virus?> GetVirusByNameAsync(string virusName)
        {
            return await _context.Viruses.FirstOrDefaultAsync(v => v.VirusName == virusName);
        }

        public async Task<Virus> CreateVirusAsync(Virus virus)
        {
            _context.Viruses.Add(virus);
            await _context.SaveChangesAsync();
            return virus;
        }

    }
}   
