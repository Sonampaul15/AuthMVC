namespace ConsumeAuthPractice.Repository
{
    public interface ITokenProvider
    {
        string? GetToken();
        void SetToken(string? token);
        void ClearToken();
    }
}
