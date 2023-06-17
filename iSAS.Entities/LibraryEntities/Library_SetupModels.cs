using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.LibraryEntities
{
    public class Library_SetupModels
    {
        public string LibId { get; set; }

        [Display(Name ="Library Name")]
        [Required(ErrorMessage ="Library name is Req..!")]
        [StringLength(50, ErrorMessage ="Max 50 char is Allowed..!")]
        public string LibName { get; set; }



        [StringLength(10, ErrorMessage = "Max 10 char is Allowed..!")]
        [Display(Name = "Library Prefix")]
        public string LibPrefix { get; set; }

        [Display(Name = "Accession No")]
        [Required(ErrorMessage = "Accession No is Req..!")]
        public string StartAccNo { get; set; }


        public string LibCat { get; set; }

        [Display(Name = "Accession Length")]
        public int AccLen { get; set; }

        public bool UsePrefix { get; set; }

        [Display(Name = "First Accession No")]
        public string FirstAccNo { get; set; }

        [Display(Name = "Last Accession No")]
        public string LastAccNo { get; set; }
        public int BookStrength { get; set; }
        public bool IsDeleteable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
