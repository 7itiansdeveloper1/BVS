using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.LibraryEntities
{
    public class Library_TransactionModels
    {
        public Library_TransactionModels()
        {
            BookIssueDetails = new Library_Transaction_BookIssueModel();
            BookReturnDetails = new List<Library_Transaction_BookDetailsModels>();
            StudentDetails = new StudentSearchModel();
            LibraryList = new List<SelectListItem>();
        }

        [Display(Name = "Trans. No.")]
        public string TransID { get; set; }


        [Display(Name = "Trans. Date")]
        public string TransDate { get; set; }

        [Display(Name = "Library")]
        public string LibraryId { get; set; }

        public string UserId { get; set; }
        public string TransactionType { get; set; }

        [Display(Name = "Return Acc. No.")]
        public string ReturnAccNo { get; set; }


        public List<SelectListItem> LibraryList { get; set; }
        public Library_Transaction_BookIssueModel BookIssueDetails { get; set; }
        public List<Library_Transaction_BookDetailsModels> BookReturnDetails { get; set; }
        public StudentSearchModel StudentDetails { get; set; }
    }

    public class Library_Transaction_BookIssueModel
    {
        public Library_Transaction_BookIssueModel()
        {
            BookDetails = new List<Library_Transaction_BookDetailsModels>();
        }

        [Display(Name ="Acc. No.")]
        public string AccNo { get; set; }

        public string IssueDate { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }

        [Display(Name = "Issue Days")]
        public int IssueDays { get; set; }

        public List<Library_Transaction_BookDetailsModels> BookDetails { get; set; }
    }

  

    public class Library_Transaction_BookDetailsModels
    {
        public string AccNo { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int TransID { get; set; }
        public string TransDate { get; set; }
        public int IssuedDays { get; set; }
        public bool IssueStatus { get; set; }

        public string IssueDate { get; set; }
        public string DueDate { get; set; }
        public int OverDueDays { get; set; }
        public int Fine { get; set; }
        public bool Selected { get; set; }
    }
}
