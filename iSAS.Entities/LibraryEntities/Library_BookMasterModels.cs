using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.LibraryEntities
{
    public class Library_BookMasterModels
    {
        public Library_BookMasterModels()
        {
            LibraryList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SubjectList = new List<SelectListItem>();
            SupplierList = new List<SelectListItem>();
            PublisherList = new List<SelectListItem>();
            AuthorList = new List<SelectListItem>();
            CoAuthorList = new List<SelectListItem>();
            TitleList = new List<SelectListItem>();
            SubTitleList = new List<SelectListItem>();
        }

        public string BookID { get; set; }

        [Display(Name ="Library")]
        public string LibID { get; set; }

        [Display(Name = "Date")]
        public string BookDate { get; set; }

        [Display(Name = "Acc No")]
        [StringLength(10)]
        public string AccNo { get; set; }

        [Required(ErrorMessage = "Title is Req..!")]
        [Display(Name = "Title")]
        public string TitleID { get; set; }

        [Display(Name = "Sub Title")]
        public string SubTitleID { get; set; }

        [Required(ErrorMessage = "Subject is Req..!")]
        [Display(Name = "Subject")]
        public string SubID { get; set; }

        [Display(Name = "Author")]
        public string AuthID { get; set; }

        [Display(Name = "Sub Author")]
        public string SubAuthID { get; set; }

        [Display(Name = "Edition")]
        [StringLength(20)]
        public string Edition { get; set; }

        [Display(Name = "Volume")]
        [StringLength(20)]
        public string Vol { get; set; }

        [Display(Name = "Year")]
        [StringLength(10)]
        public string Year { get; set; }

        [Display(Name = "Pages")]
        [StringLength(10)]
        public string Pages { get; set; }

        [Required(ErrorMessage = "ISBN No is Req..!")]
        [Display(Name = "ISBN No")]
        [StringLength(50)]
        public string ISBN { get; set; }

        [Display(Name = "Class Call No")]
        [StringLength(20)]
        public string ClassCall { get; set; }

        [Display(Name = "Book Call No")]
        [StringLength(20)]
        public string BookCall { get; set; }

        [Display(Name = "For Class")]
        public string ClassID { get; set; }

        [Display(Name = "Language")]
        public string Lang { get; set; }

        [Display(Name = "Book Type")]
        public string BookType { get; set; }

        [Display(Name = "Reference")]
        [StringLength(50)]
        public string Reference { get; set; }

        [Display(Name = "Remark")]
        [StringLength(50)]
        public string Remark { get; set; }

        [Display(Name = "No")]
        public string BillNo { get; set; }

        [Display(Name = "Bill Date")]
        public string BillDate { get; set; }

        [Display(Name = "Received From")]
        public string Recfrom { get; set; }

        [Display(Name = "Publisher")]
        public string PubID { get; set; }

        [Required(ErrorMessage = "Supplier is Req..!")]
        [Display(Name = "Supplier")]
        public string SupID { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Disc { get; set; }

        [Display(Name = "Net Price")]
        public decimal NetPrice { get; set; }

        [Display(Name = "Book Location")]
        public string BookLocation { get; set; }

        [Display(Name = "Almirah No")]
        public string AlmirahNo { get; set; }

        [Display(Name = "Shelf No")]
        public string ShelfNo { get; set; }

        [Display(Name = "No")]
        [StringLength(10)]
        public string WDNo { get; set; }

        [Display(Name = "With. Date")]
        public string WDDate { get; set; }

        [Display(Name = "With. Remark")]
        [StringLength(50)]
        public string WDRemark { get; set; }


        public bool IssueStatus { get; set; }
        public bool IsActive { get; set; }

        public List<SelectListItem> LibraryList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> SupplierList { get; set; }
        public List<SelectListItem> PublisherList { get; set; }
        public List<SelectListItem> AuthorList { get; set; }
        public List<SelectListItem> CoAuthorList { get; set; }
        public List<SelectListItem> TitleList { get; set; }
        public List<SelectListItem> SubTitleList { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
        
        public string BookEntry { get; set; }

        [Display(Name = "Total Book")]
        public int TotalBook { get; set; }

        [Display(Name = "Ref. Book")]
        public int RefBook { get; set; }

        [Display(Name = "Text Book")]
        public int TextBook { get; set; }
    }
}
