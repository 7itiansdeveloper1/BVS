using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_ConcessionPolicyModels
    {
        public string ConcId { get; set; }

        [StringLength(50, ErrorMessage ="Max Len 50..!")]
        [Required(ErrorMessage = "Policy Name is Req..!")]
        [Display(Name ="Policy Name")]
        public string ConcName { get; set; }

        public string ConcCategory { get; set; }

        [Display(Name = "Default")]
        public bool IsDefault { get; set; }

        [Range(1, 100, ErrorMessage = "Percentage Can not be more than 100..!")]
        [Display(Name = "Percentage")]
        public int DefaultPer { get; set; }

        public int PrintOrder { get; set; }
        public bool Active { get; set; }

        [Display(Name = "Work Process")]
        public bool WorkProcess { get; set; }

        [Display(Name = "Work Process By")]
        public int WorkProcessBy { get; set; }


        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        public List<SelectListItem> UserRoleList { get; set; }
    }
}
