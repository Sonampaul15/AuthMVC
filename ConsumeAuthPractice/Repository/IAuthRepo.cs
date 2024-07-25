using ConsumeAuthPractice.DTO;

namespace ConsumeAuthPractice.Repository
{
    public interface IAuthRepo
    {
        Task<ResponseDto?> RegisterByNameAsync (RegiResponseDto regiResponseDto);
        Task<ResponseDto?> AssignRoleAsync (RegiResponseDto regiResponse);

        Task<ResponseDto?> LoginByNameAsync(LogineRequestDto logineRequestDto);
    }
}
