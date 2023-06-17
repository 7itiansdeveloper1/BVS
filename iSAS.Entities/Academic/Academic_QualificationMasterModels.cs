using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_QualificationMasterModels
    {
        public string QualifId { get; set; }

        [StringLength(50, ErrorMessage ="Max 50 Char is Allowed..!")]
        [Required(ErrorMessage ="Name is Req..")]
        [Display(Name ="Qualification")]
        public string QualifName { get; set; }

        public int FatherStrength { get; set; }
        public int MotherStrength { get; set; }
        public int StaffStrength { get; set; }
        public bool IsDeletable { get; set; }

        //Extra Feild 
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
