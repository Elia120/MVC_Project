using System;
using System.ComponentModel.DataAnnotations;

namespace Compuskills.Projects.TotalTimesheetPro.Domain.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        [Display(Name ="Project")]
        public string ProjectName { get; set; }
        [Display(Name = "Client")]
        public int ClientID { get; set; }
        [Display(Name = "Client")]
        public virtual Client Client { get; set; }
        [DataType(DataType.Currency)]
        public decimal BillRate { get; set; }
        public bool IsActive { get; set; }
    }
}
