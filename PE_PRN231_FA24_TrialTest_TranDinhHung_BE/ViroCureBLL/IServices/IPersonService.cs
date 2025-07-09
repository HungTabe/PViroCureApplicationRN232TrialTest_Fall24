using ViroCureBLL.DTOs;

namespace ViroCureBLL.IServices
{
    public interface IPersonService
    {
        Task<AddPersonResponseDto> AddPersonAsync(AddPersonRequestDto request);
        //Function get person by ID
        Task<PersonResponseDto?> GetPersonAsync(int personId);
        //Function get all person
        Task<List<PersonResponseDto>> GetAllPersonsAsync();
        //Function update person by id
        Task<ApiResponseDto> UpdatePersonAsync(int personId, UpdatePersonRequestDto request);
        //Function delete person by id
        Task<bool> DeletePersonAsync(int personId);




    }
} 