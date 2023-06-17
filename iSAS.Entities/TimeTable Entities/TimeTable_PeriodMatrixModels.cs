using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.TimeTable_Entities
{
    public class TimeTable_PeriodMatrixModels
    {
        [StringLength(10)]
        public string MatrixId { get; set; }

        [Required(ErrorMessage ="Matrix name is Req..!")]
        [Display(Name ="Matrix Name")]
        [StringLength(10)]
        public string MatrixName { get; set; }

        [Display(Name = "No of Period")]
        public int NoOfPeriod { get; set; }

        [Display(Name = "No of Days")]
        public int NoOfDays { get; set; }

        [Display(Name = "Saturday Day No")]
        public int saturdayDayNo { get; set; }

        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }

        public string PeriodMatrix { get; set; }
        public string Classes { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleteable { get; set; }

        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }

    public class PeriodMatrixClassSetupModels
    {
        public PeriodMatrixClassSetupModels()
        {
            ClassList = new List<SelectListItem>();
        }

        public string MatrixId { get; set; }
        //public string MatrixName { get; set; }
        public List<SelectListItem> ClassList { get; set; }

        public string UserId { get; set; }
    }
}
