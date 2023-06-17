using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_BankMasterModels
    {
        public string BankId { get; set; }

        [StringLength(50, ErrorMessage ="Only 50 Char. is Allowed..!")]
        [Required(ErrorMessage ="Bank Name is Req..!")]
        [Display(Name ="Bank Name")]
        public string BankName { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
        public bool IsDeletable { get; set; }
    }
}
