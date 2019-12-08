using Compuskills.Projects.TotalTimesheetPro.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc.Models
{
    public class BillingReportViewModel
    {
        public Client Client { get; set; }
        public Project Project { get; set; }

        [Display(Name ="Total Hours")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan? TotalHours { get; set; }
        [Display(Name = "Billing Rate")]
        [DataType(DataType.Currency)]
        public decimal BillingRate { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
    }
}