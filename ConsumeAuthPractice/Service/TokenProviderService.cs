using ConsumeAuthPractice.DTO;
using ConsumeAuthPractice.Repository;
using ConsumeAuthPractice.Utility;

namespace ConsumeAuthPractice.Service
{
    public class TokenProviderService : ITokenProvider
    {
        private readonly IHttpContextAccessor httpContext;
        public TokenProviderService(IHttpContextAccessor _httpContext)
        {
            httpContext = _httpContext;
        }
        public void ClearToken()
        {
            httpContext.HttpContext?.Response.Cookies.Delete(StaticData.TokenValue);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken=httpContext.HttpContext?.Request.Cookies.TryGetValue(StaticData.TokenValue, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string? token)
        {
            httpContext.HttpContext?.Response.Cookies.Append(StaticData.TokenValue, token);
        }
    }
}
