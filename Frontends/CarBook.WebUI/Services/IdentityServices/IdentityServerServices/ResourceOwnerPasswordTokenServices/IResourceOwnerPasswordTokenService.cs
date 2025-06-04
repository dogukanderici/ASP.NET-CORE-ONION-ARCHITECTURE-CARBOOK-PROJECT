namespace CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialTokenServices
{
    public interface IResourceOwnerPasswordTokenService
    {
        Task<string> GetTokenAsync();
    }
}
