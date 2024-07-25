using Microsoft.AspNetCore.Mvc;
using static ConsumeAuthPractice.Utility.StaticData;

namespace ConsumeAuthPractice.DTO
{
    public class RequestDto
    {
        public ApiType apiType { get; set; } = ApiType.get;

        public string Url { get; set; }

        public Object Data { get; set; }

        public string AccessToken { get; set; }
    }
}
