using ConsumeAuthPractice.DTO;
using ConsumeAuthPractice.Repository;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Text;
using static ConsumeAuthPractice.Utility.StaticData;

namespace ConsumeAuthPractice.Service
{
    public class BaseService : IBaseRepo
    {
        private readonly IHttpClientFactory httpClient;
        public BaseService(IHttpClientFactory _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = httpClient.CreateClient("CrudApiClient");
                HttpRequestMessage message = new HttpRequestMessage();
                message.RequestUri = new Uri(requestDto.Url);
                if(requestDto.Data != null)
                {
                    message.Content= new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                switch(requestDto.apiType)
                {
                    case ApiType.Post: message.Method = HttpMethod.Post;break;
                    case ApiType.Put: message.Method = HttpMethod.Put;break;
                    case ApiType.Delete: message.Method = HttpMethod.Delete;break;
                    default: message.Method = HttpMethod.Get;break;
                }

                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                switch(apiResponse.StatusCode)
                {
                    case HttpStatusCode.Unauthorized: return new()
                    {
                        Success = false,
                        Message = "Not Found"
                    };
                        break;

                    case HttpStatusCode.Forbidden: return new()
                    {
                        Success = false,
                        Message = "Access Denied"
                    };
                        break;
                    case HttpStatusCode.NotFound: return new()
                    {
                        Success = false,
                        Message = "Not Found"
                    };
                        break;
                    case HttpStatusCode.InternalServerError: return new()
                    {
                        Success = false,
                        Message = "Internal Server Error"
                    };
                        break;

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto()
                {
                    Message = ex.Message,
                    Success = false,
                };
                return dto;
                
            }
        }
    }
}
