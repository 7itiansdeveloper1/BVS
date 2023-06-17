using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace ISas.Entities.DashboardEntities
{
    public class StudentHomeworkModels
    {
        public StudentHomeworkModels()
        {

        }
        public List<SelectListItem> subjectList { get; set; }

        [Display(Name = "From")]
        [Required(ErrorMessage = "From Date is Req..!")]
        public string fromDate { get; set; }

        [Display(Name = "To")]
        [Required(ErrorMessage = "To Date is Req..!")]
        public string toDate { get; set; }

        [Display(Name = "Subject")]
        public string selectedSubjectId { get; set; }

        [Display(Name = "Status")]
        public string selectedStatus { get; set; }
    }

    public class homework
    {
        public string homeWorkId { get; set; }
        public string homeWorkTitle { get; set; }
        public string subject { get; set; }
        public string homeWorkDate { get; set; }
        public string submissionDate { get; set; }
        public string status { get; set; }
        public string isReviewed { get; set; }
        public string referedBy { get; set; }
        public string attachments { get; set; }
        public string descriptionAttachment { get; set; }
        public string AttachFiles { get; set; }
        public string VedioLink1 { get; set; }
        public string VedioLink2 { get; set; }
        public bool isSubmited { get; set; }
        public string ansDescription { get; set; }
        public string studentId { get; set; }
        public string textEditorPDFFilePath { get; set; }
        public string FeedbackattachmentsFilePath { get; set; }
        public string remark { get; set; }

    }
}
