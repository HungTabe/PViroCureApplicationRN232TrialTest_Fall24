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
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(ViroCureFal2024dbContext context) : base(context)
        {
        }

        public async Task<ViroCureUser> LoginAsync(string email, string password)
        {
            return await _context.ViroCureUsers.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
        }
        public async Task<Person> CreatePersonAsync(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> PersonExistsAsync(int personId)
        {
            return await _context.People.AnyAsync(p => p.PersonId == personId);
        }

        public async Task<Person?> GetPersonWithVirusesAsync(int personId)
        {
            return await _context.People
                .Include(p => p.PersonViruses)
                .ThenInclude(pv => pv.Virus)
                .FirstOrDefaultAsync(p => p.PersonId == personId);
        }

        public async Task<List<Person>> GetAllPersonsWithVirusesAsync()
        {
            return await _context.People
                .Include(p => p.PersonViruses)
                .ThenInclude(pv => pv.Virus)
                .ToListAsync();
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            var existingPerson = await _context.People
                .Include(p => p.PersonViruses)
                .FirstOrDefaultAsync(p => p.PersonId == person.PersonId);

            if (existingPerson == null)
                throw new InvalidOperationException("Person not found");

            existingPerson.Fullname = person.Fullname;
            existingPerson.BirthDay = person.BirthDay;
            existingPerson.Phone = person.Phone;
            existingPerson.PersonViruses = null;

            await _context.SaveChangesAsync();

            var existingPersonA = await _context.People
                .Include(p => p.PersonViruses)
                .FirstOrDefaultAsync(p => p.PersonId == person.PersonId);

            existingPersonA.PersonViruses = person.PersonViruses;

            await _context.SaveChangesAsync();
            return existingPerson;
        }
    }
}
