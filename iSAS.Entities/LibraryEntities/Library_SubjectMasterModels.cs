using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.LibraryEntities
{
    public class Library_SubjectMasterModels
    {
        public string SubjectId { get; set; }

        [Required(ErrorMessage = "Subject Name is Req..!")]
        [StringLength(100, ErrorMessage ="Max 100 Char is Allowed..!")]
        [Display(Name ="Subject Name")]
        public string SubjectName { get; set; }

        [StringLength(500, ErrorMessage = "Max 500 Char is Allowed..!")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public int SubjectStrength { get; set; }
        public bool IsDeleteable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
