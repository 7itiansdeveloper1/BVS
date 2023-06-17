using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.LibraryEntities
{
    public class Library_AuthorMasterModels
    {
        public string AuthId { get; set; }

        [Required(ErrorMessage = "Name is Req..!")]
        [StringLength(100, ErrorMessage = "Max 100 Char is Allowed..!")]
        [Display(Name = "Author/Co-Author Name")]
        public string AuthName { get; set; }

        [StringLength(100, ErrorMessage = "Max 500 Char is Allowed..!")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public int TitleStrength { get; set; }
        public bool IsDeleteable { get; set; }

        [Display(Name = "Author Type")]
        public string AuthorType { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }

    public class Library_Author_IndividualReportModel
    {
        public Library_Author_IndividualReportModel()
        {
            BookDetails = new List<Library_AuthIndividualReportDetailModel>();
        }

        public string ReportTitle { get; set; }
        public string AuthorType { get; set; }
        public string AuthorId { get; set; }

        public List<Library_AuthIndividualReportDetailModel> BookDetails { get; set; }
    }
    public class Library_AuthIndividualReportDetailModel
    {
        public long Sno { get; set; }
        public string TitleId { get; set; }
        public string Title { get; set; }
        public int NoofBooks { get; set; }
    }

    public class Library_Author_BookTitleWiseReportModel
    {
        public long Sno { get; set; }
        public string AccNo { get; set; }
        public string Title { get; set; }
        public string AlmirahNo { get; set; }
        public string ShelfNo { get; set; }
        public string BookCall { get; set; }
    }
}
