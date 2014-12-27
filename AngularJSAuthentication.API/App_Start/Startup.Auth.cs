using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngularJSAuthentication.Glass;
using AngularJSAuthentication.Glass.Provider;
using System.Threading.Tasks;
using Microsoft.Owin;
using AngularJSAuthentication.Data.Entities;
using AngularJSAuthentication.Data.Repository;
using System.Configuration;
using AngularJSAuthentication.API.App_Start;
using System.Security.Claims;

namespace AngularJSAuthentication.API
{
    /// <summary>
    /// Added by David Chan
    /// </summary>
    public partial class Startup
    {
        public void ConfigGlassAuth(IAppBuilder app)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            var glassProvider = new GlassAuthenticationProvider
            {
                OnAuthenticated = context =>
                {
                    context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));

                    var accessToken = context.AccessToken;
                    var refreshToken = context.RefreshToken;
                    var expiresIn = context.ExpiresIn;
                    var tokenType = context.TokenType;
                    var issued = context.Issued;

                    var userId = context.Id;

                    var container = UnityConfig.GetConfiguredContainer();
                    var resolver = new UnityResolver(container);
                    var credentialService = resolver.GetService<GlassCredentialRepository>();
                    var credential = credentialService.FindCredential(userId);
                    if (credential.Result == null)
                    {
                        var glassCredential = new GlassCredential()
                        {
                            UserId = userId,
                            AccessToken = accessToken,
                            RefreshToken = refreshToken,
                            ExpiresIn = expiresIn,
                            TokenType = tokenType,
                            Issued = issued,
                            Active = true
                        };

                        credentialService.SaveCredential(glassCredential);
                    }
                    return Task.FromResult<object>(null);
                }
            };

            //var config = DependencyResolver.Current.GetService<IConfigurationOptions>();
            app.UseGlassAuthentication(new GlassAuthenticationOptions
            {
                ClientId = clientId, //config.ClientId,
                ClientSecret = clientSecret, //config.ClientSecret,
                Provider = glassProvider,
                //CallbackPath = new PathString("/authcomplete.html"),
                Scope = new List<string>
                {
                    "https://www.googleapis.com/auth/glass.timeline",
                    "https://www.googleapis.com/auth/glass.location",
                    "https://www.googleapis.com/auth/userinfo.profile",
                    "https://www.googleapis.com/auth/userinfo.email"
                }
            });
        }
    }
}