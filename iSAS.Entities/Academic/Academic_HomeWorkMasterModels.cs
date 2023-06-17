using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Academic
{
    public class Academic_HomeWorkMasterModels
    {
        public Academic_HomeWorkMasterModels()
        {
            SubjectList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            UploadedByList = new List<SelectListItem>();
            StudentList = new List<SelectListItem>();
        }

        public string HomeWorkId { get; set; }

        [Required(ErrorMessage = "Title is Req..")]
        public string Title { get; set; }

        [Display(Name = "Subject")]
        public string SubjectId { get; set; }

        [Display(Name = "Class")]
        public string[] ClassSectionId { get; set; }

        [Display(Name = "All Student")]
        public bool AllStudent { get; set; }

        [Display(Name = "Student")]
        public string[] StudentId { get; set; }
        public string StudentId_Str { get; set; } //Used for getting student Ids

        public string AttachmentReference { get; set; }

        [Required(ErrorMessage = "Uploaded by is Req..")]
        [Display(Name = "Uploaded By")]
        public string UploadedBy { get; set; }

        
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }


        [Display(Name = "Date")]
        public string UploadDate { get; set; }

        [Display(Name = "Submission Date")]
        public string SubmissionDate { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public string Discription { get; set; }
        public string textEditorPDFFilePath { get; set; }
        public string ReturnViewName { get; set; }

        //[Display(Name = "Browse File")]
        //public HttpPostedFileBase[] files { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> UploadedByList { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public string responseListName { get; set; }
        public List<response> responseList { get; set; }
    }
    public class response
    {
        public Int64 SNo { get; set; }
        public string HomeWorkId { get; set; }
        public string ERP { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string SubmitDate { get; set; }
        public string havingAttachment { get; set; }
        public string havingLink { get; set; }
        public bool isReviewed { get; set; }
    }
    public class answerSheet
    {
        public string HomeWorkId { get; set; }
        public string ERP { get; set; }
        public string Student { get; set; }
        public string AttachFiles { get; set; }
        public string RevertAttachFiles { get; set; }
        public string VedioLink1 { get; set; }
        public string VedioLink2 { get; set; }
        public string homeworkname { get; set; }
        public string Remark { get; set; }
        public bool isReviewed { get; set; }
        public bool isSubmited { get; set; }
        public string textEditorPDFFilePath { get; set; }
        public string RevertAttachFilesPath { get; set; }
    }
}
