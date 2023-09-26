using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SOTI.CureWell.WebAPI.Providers;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(SOTI.CureWell.WebAPI.Startup))]

namespace SOTI.CureWell.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);//Middleware
            var provider = new AuthProvider();
            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                TokenEndpointPath = new PathString("/token"),
                Provider = provider
            };

            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());//Generate Token
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
