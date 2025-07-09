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
        Task<Person> CreatePersonAsync(Person person);
        Task<bool> PersonExistsAsync(int personId);

        // Func : Get person data by id : Get person data + Get virus data
        Task<Person?> GetPersonWithVirusesAsync(int personId);
        // Func : Get all person data : Get person data + Get virus data
        Task<List<Person>> GetAllPersonsWithVirusesAsync();
        // Func : Update person data
        Task<Person> UpdatePersonAsync(Person person);



    }
}
