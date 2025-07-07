using ViroCureBLL.DTOs;

namespace ViroCureBLL.IServices
{
    public interface IPersonService
    {
        Task<AddPersonResponseDto> AddPersonAsync(AddPersonRequestDto request);
    }
} 