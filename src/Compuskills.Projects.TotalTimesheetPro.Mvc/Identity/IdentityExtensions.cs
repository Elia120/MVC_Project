using Compuskills.Projects.TotalTimesheetPro.Domain.DataSource;
using Compuskills.Projects.TotalTimesheetPro.Domain.Identity;
using Compuskills.Projects.TotalTimesheetPro.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc.Identity
{
    public static class IdentityExtensions
    {
        public static TtpUserManager CreateUserManager(this IdentityFactoryOptions<TtpUserManager> options, IOwinContext context)
        {
            var userStore = new UserStore<TtpUser>(context.Get<TotalTimesheetProContext>());
            var manager = TtpUserManager.Create(userStore);

            // Register two factor authentication providers. 
            // The default Microsoft MVC template uses Phone and Emails as a step of receiving a code for verifying the user
            // Disabled in this sample project
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<TtpUser>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<TtpUser>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<TtpUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}