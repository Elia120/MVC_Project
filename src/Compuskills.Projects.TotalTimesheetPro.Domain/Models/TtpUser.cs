using Microsoft.AspNet.Identity.EntityFramework;

namespace Compuskills.Projects.TotalTimesheetPro.Domain.Models
{
    public class TtpUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
