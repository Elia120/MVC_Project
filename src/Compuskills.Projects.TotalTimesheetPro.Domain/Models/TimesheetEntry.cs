using System;
using System.ComponentModel.DataAnnotations;

namespace Compuskills.Projects.TotalTimesheetPro.Domain.Models
{
    public class TimesheetEntry
    {
        public int TimesheetEntryID { get; set; }
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]

        public DateTime? EndTime { get; set; }
    }
}
