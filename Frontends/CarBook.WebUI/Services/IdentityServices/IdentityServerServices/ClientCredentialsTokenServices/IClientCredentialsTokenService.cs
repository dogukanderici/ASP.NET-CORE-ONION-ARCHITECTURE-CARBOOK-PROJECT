namespace CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialsTokenServices
{
    public interface IClientCredentialsTokenService
    {
        string GetClientCredentialsToken();
        Task<string> SetClientCredentialsToken();
    }
}
