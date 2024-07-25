namespace ConsumeAuthPractice.DTO
{
    public class ResponseDto
    {
        public Object? Result { get; set; }

        public Boolean Success { get; set; } = true;

        public string Message { get; set; } = "";
    }
}
