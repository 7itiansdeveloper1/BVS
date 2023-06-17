using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Exam_Entities
{
    public class Exam_ClassSubjectSetup
    {

        public Exam_ClassSubjectSetup()
        {
            classList = new List<SelectListItem>();
            subjectList = new List<SelectListItem>();
            assesmentList = new List<SelectListItem>();
            classSubjectList = new List<ClassSubject>();
        }
        [Required(ErrorMessage = "Class is required mandatory..!")]
        [Display(Name ="Class")]
        public string classSectionId { get; set; }
        [Required(ErrorMessage = "Subject is required mandatory..!")]
        [Display(Name = "Subject")]
        public string subjectId { get; set; }
        [Required(ErrorMessage = "Assessment is required mandatory..!")]
        [Display(Name = "Assessment")]
        public string[] assessmentsIds { get; set; }
        public List<SelectListItem> classList { get; set; }
        public List<SelectListItem> subjectList { get; set; }
        public List<SelectListItem> assesmentList { get; set; }
        public List<ClassSubject> classSubjectList { get; set; }
    }
    public class ClassSubject
    {

        public string subjectId { get; set; }
        public string SubjectName { get; set; }
        public string AssessmentName { get; set; }
    }
}
