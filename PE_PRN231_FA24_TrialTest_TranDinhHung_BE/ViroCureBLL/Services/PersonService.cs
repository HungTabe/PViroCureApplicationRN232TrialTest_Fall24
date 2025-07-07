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

            // Create person entity
            var person = new Person
            {
                PersonId = request.PersonId,
                Fullname = request.FullName,
                BirthDay = DateOnly.FromDateTime(request.BirthDay),
                Phone = request.Phone,
                PersonViruses = new List<PersonVirus>()
            };

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
    }
} 