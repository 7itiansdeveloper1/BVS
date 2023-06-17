using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Examination_Entities
{
    public class Examination_ReportCardModels
    {
        public string ExamId { get; set; }
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        
        public Examination_ReportCardModels()
        {
            ExamList = new List<SelectListItem>();
            StudentDetails = new List<ReportCard_StudentDetailsModel>();
        }

        public string UserId { get; set; }
        public string SessionId { get; set; }

        public List<SelectListItem> ExamList { get; set; }
        public List<ReportCard_StudentDetailsModel> StudentDetails { get; set; }
    }
    public class ReportCard_StudentDetailsModel
    {
        public bool Selected { get; set; }
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
    }


    #region Report Card Print Models
    public class Examination_ReportCard_HtmlPrintModel
    {
        public Examination_ReportCard_HtmlPrintModel()
        {
            HeaderDetails = new Examination_ReportCard_HeaderModel();
            StudentDetailsList = new List<Examination_ReportCard_StudentDetailsModel>();
            MarksOrGradeList = new List<Examination_ReportCard_MarksOrGradeDetailsModel>();
            CoScholasticAreasList = new List<Examination_ReportCard_CoScholasticAreasModel>();
            ResultDetailList = new List<Examination_ReportCard_ResultDetailsModel>();
            DistinctERPNos = new List<string>();
        }
        public List<string> DistinctERPNos { get; set; }
        public  Examination_ReportCard_HeaderModel HeaderDetails { get; set; }
        public List<Examination_ReportCard_StudentDetailsModel> StudentDetailsList { get; set; }
        public List<Examination_ReportCard_MarksOrGradeDetailsModel> MarksOrGradeList { get; set; }
        public List<Examination_ReportCard_CoScholasticAreasModel> CoScholasticAreasList { get; set; }
        public List<Examination_ReportCard_ResultDetailsModel> ResultDetailList { get; set; }
    }

    public class Examination_ReportCard_HeaderModel
    {
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public string Header4 { get; set; }
        public string Place { get; set; }
        public string LogoURL { get; set; }
        public string SessionName { get; set; }
        public string ReportcardDate { get; set; }
        public string CBSELogo { get; set; }
    }
    public class Examination_ReportCard_StudentDetailsModel
    {
        public string ERP { get; set; }
        public string DOA { get; set; }
        public string DOB { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string FAther { get; set; }
        public string Mother { get; set; }
        public string Address { get; set; }
        public string FMobileNo { get; set; }
        public string MMobileNo { get; set; }
        public string SMSNo { get; set; }
        public string Attendance { get; set; }
        public string PTMAttendance { get; set; }
        public string Hgt { get; set; }
        public string Wgt { get; set; }
        public string Remark { get; set; }
        public string StudentImage { get; set; }
    }
    public class Examination_ReportCard_MarksOrGradeDetailsModel
    {
        public string ERP { get; set; }
        public string SubjectDisplayName { get; set; }
        public decimal PT { get; set; }
        public decimal NoteBook { get; set; }
        public decimal SEA { get; set; }
        public decimal HalfYearly { get; set; }
        public decimal Total { get; set; }
        public string Grade { get; set; }
    }
    public class Examination_ReportCard_CoScholasticAreasModel
    {
        public string ERP { get; set; }
        public string SubjectDisplayName { get; set; }
        public string Term1 { get; set; }
        public bool IsDiscipline { get; set; }
    }
    public class Examination_ReportCard_ResultDetailsModel
    {
        public string ERPNo { get; set; }
        public string Result { get; set; }
    }
    #endregion
}
