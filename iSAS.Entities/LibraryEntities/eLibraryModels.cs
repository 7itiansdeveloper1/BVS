using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.LibraryEntities
{
    public class eLibraryModels
    {
        public eLibraryModels()
        {
            List<eLibraryBook> eLibraryBooks = new List<eLibraryBook>();
            List<SelectListItem> classList = new List<SelectListItem>();
            List<SelectListItem> subjectList = new List<SelectListItem>();
        }
        //[Required(ErrorMessage = "Class is required...!")]
        [Display(Name = "Class")]
        public string [] classIds { get; set; }
        public eLibraryBook eLibraryBook { get; set; }
        public List<eLibraryBook> eLibraryBooks { get; set; }
        public List<SelectListItem> classList { get; set; }
        public List<SelectListItem> subjectList { get; set; }
        public string Function { get; set; }
    }

    public class eLibraryBook
    {
        public int eBookNo { get; set; }
        [Required(ErrorMessage ="Book title is required...!")]
        [Display(Name ="Book Title")]
        public string eBookName { get; set; }
        [Display(Name = "Book Type")]
        [Required(ErrorMessage = "Book type is required...!")]
        public string eBookType { get; set; }
        [Display(Name = "Subject")]
        public string eBookSubjectId { get; set; }
        public string eBookSubjectName { get; set; }
        [Display(Name = "Class")]
        public string[] eBookClassId { get; set; }
        public string eBookClassName { get; set; }
        public string eBookattachementName { get; set; }
        public string eBookattachementPath { get; set; }
        public int totalDownloads { get; set; }
        public bool isActive { get; set; }
        public string remark { get; set; }
        public string UserId { get; set; }
        public string filePath { get; set; }
        public string fileKey { get; set; }
        
    }
}
