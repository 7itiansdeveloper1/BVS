using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Academic
{
    public class Academic_SyllabusMasterModels
    {
        public Academic_SyllabusMasterModels()
        {
            SubjectList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            UploadedByList = new List<SelectListItem>();
        }

        public string SyllabusId { get; set; }

        [Required(ErrorMessage = "Title is Req..!")]
        [Display(Name ="Title")]
        public string Title { get; set; }
        public string Discription { get; set; }

        [Display(Name = "Subject")]
        public string SubjectId { get; set; }


        public string AttachmentReference { get; set; }

        [Display(Name = "Uploaded By")]
        public string UploadedBy { get; set; }

        [Display(Name = "Uploaded Date")]
        public string UploadDate { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        public string textEditorPDFFilePath { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Class")]
        public string ClassId { get; set; }
        public string ReturnViewName { get; set; }

        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> UploadedByList { get; set; }
    }
}
