using Compuskills.Projects.TotalTimesheetPro.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Compuskills.Projects.TotalTimesheetPro.Domain.DataSource
{
    public class TotalTimesheetProContext : IdentityDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimesheetEntry> TimesheetEntries { get; set; }

        public static TotalTimesheetProContext Create()
        {
            return new TotalTimesheetProContext();
        }

        public System.Data.Entity.DbSet<Compuskills.Projects.TotalTimesheetPro.Domain.Models.TtpUser> IdentityUsers { get; set; }
    }
}
