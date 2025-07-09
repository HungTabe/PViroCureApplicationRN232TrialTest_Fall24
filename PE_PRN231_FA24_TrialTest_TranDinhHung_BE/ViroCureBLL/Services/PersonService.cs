using ViroCureBLL.DTOs;
using ViroCureBLL.IServices;
using ViroCureDAL.Entities;
using ViroCureDAL.IRepositories;

namespace ViroCureBLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IVirusRepository _virusRepository;


        public PersonService(IPersonRepository personRepository, IVirusRepository virusRepository)
        {
            _personRepository = personRepository;
            _virusRepository = virusRepository;
        }

        public async Task<AddPersonResponseDto> AddPersonAsync(AddPersonRequestDto request)
        {
            // Validate birthday
            if (request.BirthDay >= new DateTime(2007, 1, 1))
            {
                throw new ArgumentException("Value for Birthday < 01-01-2007");
            }

            // Check if person already exists
            if (await _personRepository.PersonExistsAsync(request.PersonId))
            {
                throw new InvalidOperationException("Person with this ID already exists");
            }

            var person = new Person
            {
                PersonId = request.PersonId,
                Fullname = request.FullName,
                BirthDay = DateOnly.FromDateTime(request.BirthDay),
                Phone = request.Phone,
                PersonViruses = new List<PersonVirus>()
            };
            foreach (var virusDto in request.Viruses)
            {

                var virus = await _virusRepository.GetVirusByNameAsync(virusDto.VirusName);
                if (virus == null)
                {
                    virus = new Virus
                    {
                        VirusName = virusDto.VirusName
                    };
                    virus = await _virusRepository.CreateVirusAsync(virus);
                }

                // Create person-virus relationship
                var personVirus = new PersonVirus
                {
                    PersonId = person.PersonId,
                    VirusId = virus.VirusId,
                    ResistanceRate = virusDto.ResistanceRate
                };

                person.PersonViruses.Add(personVirus);
            }

            // Save person
            await _personRepository.CreatePersonAsync(person);

            return new AddPersonResponseDto
            {
                PersonId = person.PersonId,
                Message = "Person and viruses added successfully"
            };
        }

        public async Task<PersonResponseDto?> GetPersonAsync(int personId)
        {
            var person = await _personRepository.GetPersonWithVirusesAsync(personId);
            if (person == null)
                return null;

            return new PersonResponseDto
            {
                PersonId = person.PersonId,
                FullName = person.Fullname,
                BirthDay = person.BirthDay,
                Phone = person.Phone,
                Viruses = person.PersonViruses.Select(pv => new VirusResponseDto
                {
                    VirusName = pv.Virus.VirusName,
                    ResistanceRate = pv.ResistanceRate ?? 0
                }).ToList()
            };
        }

        public async Task<List<PersonResponseDto>> GetAllPersonsAsync()
        {
            var persons = await _personRepository.GetAllPersonsWithVirusesAsync();

            return persons.Select(person => new PersonResponseDto
            {
                PersonId = person.PersonId,
                FullName = person.Fullname,
                BirthDay = person.BirthDay,
                Phone = person.Phone,
                Viruses = person.PersonViruses.Select(pv => new VirusResponseDto
                {
                    VirusName = pv.Virus.VirusName,
                    ResistanceRate = pv.ResistanceRate ?? 0
                }).ToList()
            }).ToList();
        }

        public async Task<ApiResponseDto> UpdatePersonAsync(int personId, UpdatePersonRequestDto request)
        {
            // Validate birthday
            if (request.BirthDay >= new DateTime(2007, 1, 1))
            {
                throw new ArgumentException("Value for Birthday < 01-01-2007");
            }

            // Check if person exists
            var existingPerson = await _personRepository.GetPersonWithVirusesAsync(personId);
            if (existingPerson == null)
            {
                throw new InvalidOperationException("Person not found");
            }

            // Update person properties
            existingPerson.Fullname = request.FullName;
            existingPerson.BirthDay = DateOnly.FromDateTime(request.BirthDay);
            existingPerson.Phone = request.Phone;

            // Clear existing virus relationships
            existingPerson.PersonViruses.Clear();

            // Process viruses
            foreach (var virusDto in request.Viruses)
            {
                // Check if virus exists, if not create it
                var virus = await _virusRepository.GetVirusByNameAsync(virusDto.VirusName);
                if (virus == null)
                {
                    virus = new Virus
                    {
                        VirusName = virusDto.VirusName
                    };
                    virus = await _virusRepository.CreateVirusAsync(virus);
                }

                // Create person-virus relationship
                var personVirus = new PersonVirus
                {
                    PersonId = existingPerson.PersonId,
                    VirusId = virus.VirusId,
                    ResistanceRate = virusDto.ResistanceRate
                };

                existingPerson.PersonViruses.Add(personVirus);
            }

            // Update person
            await _personRepository.UpdatePersonAsync(existingPerson);

            return new ApiResponseDto
            {
                Message = "Person and viruses updated successfully"
            };
        }
    }
} 