using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.HR_Payroll_Entities
{
    public class HR_Payroll_CTC_SetupModels
    {
        public HR_Payroll_CTC_SetupModels()
        {
            HeadList = new List<SelectListItem>();
            SlabList = new List<SlabDetailsModels>();
        }

        [Required]
        public string PayBandId { get; set; }
        public string PayBandName { get; set; }

        [Display(Name ="Head")]
        [Required(ErrorMessage ="Head")]
        public string HeadId { get; set; }


        public string HeadName { get; set; }


        [Display(Name = "Head Type")]
        [Required(ErrorMessage = "Head Type")]
        public string HeadType { get; set; }

        [Required]
        public string Value { get; set; }

        public string Formula { get; set; }

        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        public string SlabValidationMsg { get; set; }

        public List<SelectListItem> HeadList { get; set; }
        public List<SlabDetailsModels> SlabList { get; set; }
    }

    public class SlabDetailsModels
    {
        public string Minimum { get; set; }
        public string Maximum { get; set; }
        public string Result { get; set; }
    }
}
