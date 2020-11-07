using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(CourseApplication.App_Start.Startup))]

namespace CourseApplication.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            CookieAuthenticationOptions cookieAuthOptions = new CookieAuthenticationOptions();
            cookieAuthOptions.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            cookieAuthOptions.LoginPath = new PathString("/account/login");

            app.UseCookieAuthentication(cookieAuthOptions);
        }
    }
}
