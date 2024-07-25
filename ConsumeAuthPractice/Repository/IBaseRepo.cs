using ConsumeAuthPractice.DTO;

namespace ConsumeAuthPractice.Repository
{
    public interface IBaseRepo
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
