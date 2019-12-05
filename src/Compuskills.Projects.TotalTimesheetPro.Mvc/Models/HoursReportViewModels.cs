using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc.Models
{
    public class HoursReportGetHoursViewModels
    {
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan? Hours { get; set; }
        [Display(Name = "Days Worked")]
        public int DaysWorked { get; set; }
    }
}