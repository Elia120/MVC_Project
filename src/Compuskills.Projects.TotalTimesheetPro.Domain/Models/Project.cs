using System;

namespace Compuskills.Projects.TotalTimesheetPro.Domain.Models
{
    public class Project
    {
        public int ProjectID { get; set; }

        public int ClientID { get; set; }
        public virtual Client Client { get; set; }

        public decimal BillRate { get; set; }
        public bool IsActive { get; set; }
    }
}
