using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Examination_Entities
{
    public class Examination_ProfileEntryModels
    {
        public string ExamId { get; set; }
        public string ClassId { get; set; }
        public string SectionId { get; set; }

        public Examination_ProfileEntryModels()
        {
            ExamList = new List<SelectListItem>();
            StudentDetails = new List<ProfileEntry_StudentDetailsModel>();
        }

        public string UserId { get; set; }
        public string SessionId { get; set; }

        public List<SelectListItem> ExamList { get; set; }
        public List<ProfileEntry_StudentDetailsModel> StudentDetails { get; set; }
    }
    public class ProfileEntry_StudentDetailsModel
    {
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
        public string Hgt { get; set; }
        public string Wgt { get; set; }
        public string Attendance { get; set; }
        public string PTMAttendance { get; set; }

        public string Attendance1 { get; set; }
        public string PTMAttendance1 { get; set; }
    }
}
