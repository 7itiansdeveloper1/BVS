using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.LibraryEntities
{
    public class Library_BookTitleMasterModels
    {
        public string BooktitleId { get; set; }

        [Display(Name ="Name")]
        [Required(ErrorMessage ="Title name is Req..!")]
        [StringLength(100, ErrorMessage ="Max. 100 char is allowed..!")]
        public string BookTitleName { get; set; }


        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Max. 500 char is allowed..!")]
        public string Description { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }


        public int BookStrength { get; set; }
        public bool IsDeleteable { get; set; }

        [Display(Name = "Title Type")]
        public string TitleType { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }

    }
}
