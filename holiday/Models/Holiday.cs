using System;
using System.ComponentModel.DataAnnotations;
namespace Holidays.Models

{
    public class Holiday
    {
        public int id { get; set; }

        [Display(Name = "Holiday Name")]
        public string HolidayName { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Finish Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Approval Status")]
        public int Approved { get; set; }

        [Display(Name = "Approver")]
        public string? Approver { get; set; }

        [Display(Name = "Holiday Type")]
        public string Type { get; set; }

        [Display(Name = "Holiday Cost")]
        public int Cost { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public Holiday()
        {

        }
    }

}
