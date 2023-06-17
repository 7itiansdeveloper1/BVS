using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_ProfessionMasterModels
    {
        public string ProfId { get; set; }

        [StringLength(50, ErrorMessage ="Maximum 50 Char is allowed..!")]
        [Required(ErrorMessage ="Profession Name is Req..!")]
        [Display(Name ="Profession")]
        public string ProfName { get; set; }


        public int FatherStrength { get; set; }
        public int MotherStrength { get; set; }
        public bool IsDeletable { get; set; }
        
        //Extra Feild 
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
