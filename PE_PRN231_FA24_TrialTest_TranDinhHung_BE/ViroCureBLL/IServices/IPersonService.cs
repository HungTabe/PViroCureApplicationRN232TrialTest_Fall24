using ViroCureBLL.DTOs;

namespace ViroCureBLL.IServices
{
    public interface IPersonService
    {
        Task<AddPersonResponseDto> AddPersonAsync(AddPersonRequestDto request);
        //Function get person by ID
        Task<PersonResponseDto?> GetPersonAsync(int personId);

    }
} 