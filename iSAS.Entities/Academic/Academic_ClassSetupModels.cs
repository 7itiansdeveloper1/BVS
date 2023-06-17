using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.Academic
{
    public class Academic_ClassSetupModels
    {
        public Academic_ClassSetupModels()
        {
            PromotedClassList = new List<SelectListItem>();
        }

        public string ClassId { get; set; }

        [Display(Name = "Class Name")]
        [Required(ErrorMessage = "Class Name is Req..!")]
        [StringLength(10, ErrorMessage = "Max Char len is 10..!")]
        public string ClassName { get; set; }

        [Display(Name = "Class Code")]
        public string Classcode { get; set; }

        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }

        [Range(1, 500, ErrorMessage ="Must be between 1 to 500..!")]
        [Required(ErrorMessage ="Must be grater than zero (0) ..!")]
        [Display(Name = "Max. Strength")]
        public int MaxStrength { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [Display(Name = "Is Alumni Class")]
        public bool IsAlumniClass { get; set; }

        [Display(Name = "Reg. Open")]
        public bool RegOpen { get; set; }

        [Display(Name = "Reg. Open Date")]
        public string RegOpenDate { get; set; }

        [Display(Name = "Reg. Close Date")]
        public string RegCloseDate { get; set; }

        [Display(Name = "Adm. Open")]
        public bool AdmOpen { get; set; }

        [Display(Name = "Adm. Open Date")]
        public string AdmOpenDate { get; set; }

        [Display(Name = "Adm. Close Date")]
        public string AdmCloseDate { get; set; }

        public bool IsDeletable { get; set; }

      //  [Required(ErrorMessage ="Promoted Class is Req..!")]
        [Display(Name ="Promoted Class")]
        public string PromotedClass { get; set; }

        //Extra Feild
        public string WingName { get; set; }
        public string WingId { get; set; }
        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        public string SchoolName { get; set; }
        public string SchoolId { get; set; }

        public List<SelectListItem> PromotedClassList { get; set; }
    }
}
