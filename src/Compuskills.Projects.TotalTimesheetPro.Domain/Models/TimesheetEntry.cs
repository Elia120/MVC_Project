using System;
using System.ComponentModel.DataAnnotations;

namespace Compuskills.Projects.TotalTimesheetPro.Domain.Models
{
    public class TimesheetEntry
    {
        public int TimesheetEntryID { get; set; }
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Display(Name = "Project")]
        public virtual Project Project { get; set; }
        [Display(Name = "Start Time"),DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yy H:mm:ss tt}"), DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yy H:mm:ss tt}"), DataType(DataType.DateTime)]

        public DateTime? EndTime { get; set; }
    }
}
