using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Academic
{
    public class Academic_DynamicReportWizardModels
    {
        public Academic_DynamicReportWizardModels()
        {
            ModuleList = new List<SelectListItem>();
            ReportFeildList = new List<SelectListItem>();
        }

        public string ReportId { get; set; }

        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Required(ErrorMessage = "Report Name is Req..!")]
        [Display(Name = "Report Name")]
        public string ReportName { get; set; }

        [Required(ErrorMessage = "Display Name is Req..!")]
        [Display(Name = "Display Name")]
        public string ReportDisplayName { get; set; }

        [Required(ErrorMessage = "Module is Req..!")]
        [Display(Name ="Module")]
        public Int16 ModuleId { get; set; }
        public string ModuleName { get; set; }

        [Display(Name = "Report Type")]
        public string ReportType { get; set; }
        public bool IsActive { get; set; }
        public string ReportStatus { get; set; }

        [Required(ErrorMessage = "Report Caption is Req..!")]
        [Display(Name = "Report Caption")]
        public string ReportCaption { get; set; }

        public string UserId { get; set; }

        public List<SelectListItem> ModuleList { get; set; }
        public List<SelectListItem> ReportFeildList { get; set; }
    }
}
