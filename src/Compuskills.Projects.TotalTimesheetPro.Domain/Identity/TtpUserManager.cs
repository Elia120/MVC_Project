using Compuskills.Projects.TotalTimesheetPro.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Compuskills.Projects.TotalTimesheetPro.Domain.Identity
{
    public class TtpUserManager : UserManager<TtpUser>
    {
        public TtpUserManager(IUserStore<TtpUser> store) : base(store)
        {
        }

        public static TtpUserManager Create(UserStore<TtpUser> userStore)
        {
            var manager = new TtpUserManager(userStore);
            
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<TtpUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
                        
            return manager;
        }
    }
}
