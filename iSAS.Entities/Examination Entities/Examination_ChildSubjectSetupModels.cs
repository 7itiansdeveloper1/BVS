using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.Examination_Entities
{
    public class Examination_ChildSubjectSetupModels
    {
        public int SubjectPropertyId { get; set; }

        [StringLength(10)]
        public string ExamTemplateId { get; set; }
        public string Exam_TemplateName { get; set; }

        [StringLength(10)]
        public string ChildSubjectId { get; set; }

        [StringLength(150)]
        public string ChildSubjectName { get; set; }

        public int PrintOrder { get; set; }

        [StringLength(150)]
        public string SubjectDisplayName { get; set; }
        public string Assessment { get; set; }
        public bool IsDeleteable { get; set; }
        public bool HavingChildSubject { get; set; }


        public string SubjectId { get; set; }
        public string SubjectName { get; set; }

        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public string TransactionMode { get; set; }
    }
}
