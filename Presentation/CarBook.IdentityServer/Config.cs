using CarBook.Configurations;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Client = Duende.IdentityServer.Models.Client;

namespace CarBook.IdentityServer
{
    public class Config
    {
        // Her API'ye erişim sağlayacak kaynak ve kaynağın kapsamları
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("CarBookFullPermission")
            {
                Scopes =
                {
                    "ReadPermission",
                    "FullPermission"
                }
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        // Kaynak kapsamlarının tanımı yapılır.
        public static IEnumerable<ApiScope> ApiScpoes => new ApiScope[]
        {
            new ApiScope("ReadPermission","Read Permission For Users",new[] {"scope"}),
            new ApiScope("FullPermission","Read-Write Permission For Admin Users",new[] {"scope"}),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        // Token'ı alınan kullanıcının hangi bilgilerine erişim sağlanacağı tanımlanır.
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        // Token alınacak kullanıcı tipleri tanımlanır.
        // Her kullanıcı tipi için ClientId ve ClientSecret tanımlanmak zorundadır.
        public static IEnumerable<Client> Clients => new Client[]
        {
            // Standart Üye
            new Client()
            {
                ClientId="CarBookMember",
                ClientName="CarBook Basic User",
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                ClientSecrets={new Secret("carbookmemberkeycarbookmemberkey".Sha256())},
                AllowedScopes=
                {
                    "ReadPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime=CacheKeys.TokenExpireInSeconds
            },

            new Client()
            {
                ClientId="CarBookAdmin",
                ClientName="CarBook Admin User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets= {new Secret("carbookadminkey1carbookadminkey1".Sha256()) },
                AllowedScopes=
                {
                    "ReadPermission",
                    "FullPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime=CacheKeys.TokenExpireInSeconds
            }
        };

    }
}
