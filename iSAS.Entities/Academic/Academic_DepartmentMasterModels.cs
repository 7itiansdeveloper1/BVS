using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_DepartmentMasterModels
    {
        public string DeptID { get; set; }

        [StringLength(50, ErrorMessage ="Maximum 50 Char is Allowed..!")]
        [Required(ErrorMessage ="Department Name is Req..!")]
        [Display(Name ="Department")]
        public string DeptName { get; set; }

        public int StaffStrength { get; set; }
        public bool IsDeletable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
