using Microsoft.Identity.Client;
using System.Windows;

namespace UI_Servicios
{
    public partial class App : Application
    {
        static App()
        {
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{TenantId}")
                .WithDefaultRedirectUri()
                .Build();
            TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }
        
        private static string ClientId = "8e2bcda2-badd-44f0-bcf9-7ded4b755afc";
        private static string TenantId = "3acc4246-bf5b-4548-85d6-b38715d56c79";
        private static string Instance = "https://login.microsoftonline.com/";
        public static IPublicClientApplication _clientApp;
        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
    }
}
