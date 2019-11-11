namespace Compuskills.Projects.TotalTimesheetPro.Domain.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        public string Name { get; set; }
        public string TtpUserId { get; set; }
        public virtual TtpUser TtpUser { get; set; }
    }
}
