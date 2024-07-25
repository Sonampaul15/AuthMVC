using ConsumeAuthPractice.DTO;
using ConsumeAuthPractice.Repository;
using ConsumeAuthPractice.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace ConsumeAuthPractice.Service
{
    public class AuthService : IAuthRepo
    {
        private readonly IBaseRepo baseRepo;
        public AuthService(IBaseRepo _baseRepo)
        {
            baseRepo = _baseRepo;
        }
        public async Task<ResponseDto?> AssignRoleAsync(RegiResponseDto regiResponse)
        {
            return await baseRepo.SendAsync(new RequestDto
            {
                apiType = Utility.StaticData.ApiType.Post,
                Data = regiResponse,
                Url = StaticData.CrudApiUrl + "/api/Auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginByNameAsync(LogineRequestDto logineRequestDto)
        {
            return await baseRepo.SendAsync(new RequestDto
            {
                apiType = StaticData.ApiType.Post,
                Data = logineRequestDto,
                Url = StaticData.CrudApiUrl + "/api/Auth/Login"
            });
        }

        public async Task<ResponseDto?> RegisterByNameAsync(RegiResponseDto regiResponseDto)
        {
            return await baseRepo.SendAsync(new RequestDto
            {
                apiType = StaticData.ApiType.Post,
                Data = regiResponseDto,
                Url= StaticData.CrudApiUrl + "/api/Auth/register"
            });
        }
    }
}
