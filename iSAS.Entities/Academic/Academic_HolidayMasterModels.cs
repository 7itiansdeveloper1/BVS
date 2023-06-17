using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_HolidayMasterModel
    {
        public string  HolidayId { get; set; }


        [StringLength(50, ErrorMessage ="Max 50 char is allowed..!")]
        [Required(ErrorMessage ="Holiday Name is Req..!")]
        [Display(Name ="Holiday Name")]
        public string HolidayName { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "For Student")]
        public bool HolidayForStudent { get; set; }

        [Display(Name = "For Staff")]
        public bool HolidayForStaff { get; set; }


        [Display(Name = "Holiday Count")]
        public int HolidayCount { get; set; }
        public bool IsDeleteable { get; set; }

        // Extra Field
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
