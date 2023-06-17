using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities
{
    public class Student_TC
    {

        public Student_TC()
        {
            DropDownList = new DropDownListFor_StudentTC();
        }

        public int PendingDue { get; set; }

        public int IssueBook { get; set; }

        public string SubjectErrorMsg { get; set; }
        public string Function { get; set; }

        public DropDownListFor_StudentTC DropDownList { get; set; }


        public class DropDownListFor_StudentTC
        {
            public DropDownListFor_StudentTC()
            {
                SessionList = new List<SelectListItem>();
                ClassList = new List<SelectListItem>();
                SectionList = new List<SelectListItem>();
                StudentList = new List<SelectListItem>();
                HigherClassOptionList = new List<SelectListItem>();
                _tcSubjectList = new List<TCSubjectList>();
            }
            public List<SelectListItem> SessionList { get; set; }
            public List<SelectListItem> ClassList { get; set; }
            public List<SelectListItem> SectionList { get; set; }
            public List<SelectListItem> StudentList { get; set; }
            public List<SelectListItem> HigherClassOptionList { get; set; }
            public List<TCSubjectList> _tcSubjectList { get; set; }



        }

        public string LastTCNo { get; set; }

        public string TCCount { get; set; }

        public int RollNo { get; set; }

        [Required(ErrorMessage = "Session is Req..")]
        [Display(Name = "Session")]
        public string Session { get; set; }

        //[Required(ErrorMessage = "TC No. is Req..")]
        [Display(Name = "TC  No.")]
        public string TCNo { get; set; }

        [Required(ErrorMessage = "Creation Date is Req..")]
        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }

        [Display(Name = "ERP No")]
        public string ERPNo { get; set; }

        [Display(Name = "Adm No.")]
        public string AdmNo { get; set; }

        [Required(ErrorMessage = "Class is Req..")]
        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Display(Name = "Class")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Section is Req..")]
        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Display(Name = "Section")]
        public string SectionName { get; set; }

        [Required(ErrorMessage = "Name is Req..")]
        [Display(Name = "1. Name of the pupil")]
        public string Student { get; set; }

        [Display(Name = "2. Mother's Name")]
        public string MotherName { get; set; }

        [Display(Name = "3. Father's Name")]
        public string FatherName { get; set; }



        [Display(Name = "4. DOB according to admission register")]
        public string DOB { get; set; }



        [Display(Name = "5. DOB Proof")]
        public string DOBProof { get; set; }




        [Display(Name = "6. Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "7. Belong to SC/ST")]
        public string BelongtoSCST { get; set; }

        [Display(Name = "8. Date of admission in school")]
        public string DOA { get; set; }

        [Display(Name = "Admission Class")]
        public string AdmissionClass { get; set; }

        [Display(Name = "9. Class in which pupil last studied(in figure)")]
        public string LastClassinFigure { get; set; }

        [Display(Name = "Class (in words)")]
        public string LastClassinWord { get; set; }


        [Display(Name = "10. School/Board Annual Examination last taken with result")]
        public string LastSchoolWithExamResult { get; set; }


        [Display(Name = "11. Weither Failed, if so once/twice in then same class")]
        public string FailedinSameClass { get; set; }


        [Display(Name = "13. Weither Promoted to higher class")]
        public bool IsQualifiedForHigherClass { get; set; }

        [Display(Name = "Class(in figure)")]
        public string HigherClassinFigure { get; set; }


        [Display(Name = "Class (in words)")]
        public string HigherClassinWord { get; set; }


        [Display(Name = "14. Month upto which pupil has paid school dues")]
        public string MonthForDues { get; set; }


        [Display(Name = "15. Any fee concession availed : if so, the nature of such concession")]
        public string NatureOFConcession { get; set; }

        [Display(Name = "16. Total no of Working Days")]
        public string TotalWorkingDays { get; set; }


        [Display(Name = "17. Total no of working days presents")]
        public string WorkingDaysPresents { get; set; }

        [Display(Name = "18. Weither NCC Cadet/Boy Scout/Girl Guide (Details may be given)")]
        public string WeitherNCCCadit { get; set; }


        [Display(Name = "19. Whether school is under Govt./Minority/Independent Category")]
        public string isSchoolUnderGovt { get; set; }

        [Display(Name = "20. Games played or Extra Curricular Activities in which the pupil usually took part (mention Achievement level their in)")]
        public string ExtraCurricularActivities { get; set; }


        [Display(Name = "21. General Conduct")]
        public string GeneralConduct { get; set; }

        //[Range(0,int.MaxValue,ErrorMessage = "Please enter valid number.")]
        //[Display(Name = "Refund Fee")]
        //public string RefundFee { get; set; }

        //[Required(ErrorMessage = "Date is Req..")]
        [Display(Name = "22. Date of Application for Certificate")]
        public string AppliedDate { get; set; }

        //[Required(ErrorMessage = "Date is Req..")]
        [Display(Name = "23. Date on which pupil's name was struck off the rolls of the school")]
        public string struckOffDate { get; set; }

        //[Required(ErrorMessage = "Date is Req..")]
        [Display(Name = "24. Date of Issue of Certificate")]
        public string IssueDate { get; set; }

        [Display(Name = "25. Reason for leaving the School")]
        public string ReasonForTC { get; set; }

        [Display(Name = "26. Any other Remark Description")]
        public string Description { get; set; }

        [Display(Name = "Is TC Cancelled")]
        public bool IsTCCancelled { get; set; }

        public string fileKey { get; set; }
        public string filePath { get; set; }
        public string fileName { get; set; }

    }
    public class TCSubjectList
    {
        public bool IsSelected { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Nullable<int> PrintOrder { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CBy { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
        public Nullable<int> MBy { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
    }
}
