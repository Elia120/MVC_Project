using System;

namespace Compuskills.Projects.TotalTimesheetPro.Domain.Models
{
    public class TimesheetEntry
    {
        public int TimesheetEntryID { get; set; }
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
