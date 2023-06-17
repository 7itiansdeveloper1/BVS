using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_DesignationMasterModels
    {
        public string DesigID { get; set; }

        [StringLength(50, ErrorMessage ="Max 50 char is Allowed..!")]
        [Required(ErrorMessage ="Designation Name is Req..!")]
        [Display(Name ="Designation")]
        public string DesigName { get; set; }

        public int StaffStrength { get; set; }
        public bool IsDeletable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
