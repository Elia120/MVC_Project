using Compuskills.Projects.TotalTimesheetPro.Domain.Identity;
using Compuskills.Projects.TotalTimesheetPro.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc.Identity
{
    public class TtpSignInManager : SignInManager<TtpUser, string>
    {
        public TtpSignInManager(TtpUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(TtpUser user)
        {
            return ((TtpUserManager)UserManager).CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static TtpSignInManager Create(IdentityFactoryOptions<TtpSignInManager> options, IOwinContext context)
        {
            return new TtpSignInManager(context.GetUserManager<TtpUserManager>(), context.Authentication);
        }
    }
}