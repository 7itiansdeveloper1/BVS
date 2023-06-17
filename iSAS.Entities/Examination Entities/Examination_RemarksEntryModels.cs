using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Examination_Entities
{
    public class Examination_RemarksEntryModels
    {
        public string ExamId { get; set; }
        public string ClassId { get; set; }
        public string SectionId { get; set; }

        public Examination_RemarksEntryModels()
        {
            RemarksList = new List<SelectListItem>();
            ExamList = new List<SelectListItem>();
            StudentDetails = new List<RemarksEntry_StudentDetailsModel>();
        }

        public string UserId { get; set; }
        public string SessionId { get; set; }

        public List<SelectListItem> RemarksList { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public List<RemarksEntry_StudentDetailsModel> StudentDetails { get; set; }
    }
    public class RemarksEntry_StudentDetailsModel
    {
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
        public string RemarkId { get; set; }
    }
}
