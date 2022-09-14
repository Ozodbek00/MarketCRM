namespace Market.Service.Interfaces.IAuthServices
{
    public interface IAuthService
    {
        Task<string> GenerateTokenAsync(string login, string password);
    }
}
