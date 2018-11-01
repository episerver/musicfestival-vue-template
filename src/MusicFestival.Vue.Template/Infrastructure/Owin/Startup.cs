using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.ContentApi.Cms.Helpers.Internal;
using EPiServer.ContentApi.OAuth;
using EPiServer.Core;
using EPiServer.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartup(typeof(MusicFestival.Template.Infrastructure.Owin.Startup))]
namespace MusicFestival.Template.Infrastructure.Owin
{
    public class Startup
    {
        public Startup()
        {
            // Parameterless constructor required by OWIN.
        }

        public void Configuration(IAppBuilder app)
        {
            app.AddCmsAspNetIdentity<ApplicationUser>(new ApplicationOptions() { ConnectionStringName = "EPiServerDB" });

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider.
            // Configure the sign in cookie.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/util/login.aspx"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager<ApplicationUser>, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => manager.GenerateUserIdentityAsync(user)),
                    OnApplyRedirect = (context =>
                    {
                        if (!ContentApiHelper.IsContentApiRequest(context.Request.Uri))
                        {
                            context.Response.Redirect(context.RedirectUri);
                        }
                    }),
                    OnResponseSignOut = (context => context.Response.Redirect(UrlResolver.Current.GetUrl(ContentReference.StartPage))),
                },
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseContentApiIdentityOAuthAuthorization<ApplicationUserManager<ApplicationUser>, ApplicationUser>(new ContentApiOAuthOptions()
            {
                RequireSsl = false,
            });
        }
    }
}