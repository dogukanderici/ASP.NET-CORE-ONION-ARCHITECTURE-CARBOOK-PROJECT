namespace CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialTokenServices
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetTokenAsync();
    }
}
