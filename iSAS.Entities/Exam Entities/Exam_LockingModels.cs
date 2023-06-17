using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Exam_Entities
{
    public class Exam_LockingModels
    {
        public  Exam_LockingModels()
        {
            classAssessmentList = new List<classAssessmentList>();
            classList = new List<SelectListItem>();
         }
        public string SelectedClassId { get; set; }
        public List<classAssessmentList> classAssessmentList { get; set; }

        [Display(Name = "Class")]
        public List<SelectListItem> classList { get; set; }
    }

    public class classAssessmentList
    {
        public string classId { get; set; }
        public string assessmentId { get; set; }
        public string assessmentName { get; set; }
        public bool isLocked { get; set; }
    }
}
