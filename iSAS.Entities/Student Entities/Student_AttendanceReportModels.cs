using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Entities
{
    public class Student_AttendanceReportModels
    {
        public Student_AttendanceReportModels()
        {
            ClassSectionList = new List<ClassSectionModel>();
            ReportNameList = new List<SelectListItem>();
            //AttnReportList = new List<Stud_AttnReportModel>();
            //AttnDetailReportList = new List<Stud_AttnReportDetailModel>();
            HeaderNameList = new List<string>();
            ValueList = new List<List<string>>();
        }

        public string Print { get; set; }
        public string ReportType { get; set; }
        public string OrderBy { get; set; }

        [Display(Name = "Report Name")]
        public string ReportName { get; set; }
        public string WingId { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Class & Section")]
        public string ClassSectionIDs { get; set; }

        [Display(Name = "From Date")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        public string ToDate { get; set; }

        public string SelectedClassNames { get; set; }
        public string SelectedReportName { get; set; }

        public List<ClassSectionModel> ClassSectionList { get; set; }
        public List<SelectListItem> ReportNameList { get; set; }
        public List<string> HeaderNameList { get; set; }
        public List<List<string>> ValueList { get; set; }
        //public List<Stud_AttnReportModel> AttnReportList { get; set; }
        //public List<Stud_AttnReportDetailModel> AttnDetailReportList { get; set; }
    }
    public class AttendanceRegisterViewModel
    {
        private List<StudentPTMAttendance> _studentPTMAttendanceList;
        private IEnumerable<SelectListItem> _sessionList;
        private IEnumerable<SelectListItem> _classList;
        private IEnumerable<SelectListItem> _sectionList;
        private List<ClassAttendance> _classAttendanceList;
        private IEnumerable<SelectListItem> _attRegDateList;
        private DataTable attendanceDataTable;


        public AttendanceRegisterViewModel()
        {
            this._studentPTMAttendanceList = new List<StudentPTMAttendance>();
            this._sessionList = Enumerable.Empty<SelectListItem>();
            this._classList = Enumerable.Empty<SelectListItem>();
            this._sectionList = Enumerable.Empty<SelectListItem>();
            this._attRegDateList = Enumerable.Empty<SelectListItem>();
            this.attendanceDataTable = new DataTable();
            AttenDetails_New = new List<StudentAttendanceDetailsModel>();
        }
        public int DefaulterPercentage { get; set; }
        public string ClassTeacherName { get; set; }
        public string AttendanceMessage { get; set; }
        public IEnumerable<SelectListItem> SessionList
        {
            get
            {
                return this._sessionList;
            }
            set
            {
                this._sessionList = value;
            }
        }

        public string SelectedSessionId { get; set; }

        public IEnumerable<SelectListItem> ClassList
        {
            get
            {
                return this._classList;
            }
            set
            {
                this._classList = value;
            }
        }

        public string SelectedClassId { get; set; }

        public IEnumerable<SelectListItem> SectionList
        {
            get
            {
                return this._sectionList;
            }
            set
            {
                this._sectionList = value;
            }
        }

        public string SelectedSectionId { get; set; }
        public IEnumerable<SelectListItem> AttRegDateList
        {
            get
            {
                return this._attRegDateList;
            }
            set
            {
                this._attRegDateList = value;
            }
        }
        public string SelectedAttRegDate { get; set; }

        public List<StudentPTMAttendance> StudentPTMAttendanceList
        {
            get
            {
                return this._studentPTMAttendanceList;
            }
            set
            {
                this._studentPTMAttendanceList = value;
            }

        }

        public DataTable AttendanceDataTable
        {
            get
            {
                return this.attendanceDataTable;
            }
            set
            {
                this.attendanceDataTable = value;
            }
        }
        public List<ClassAttendance> ClassAttendanceList
        {
            get
            {
                return this._classAttendanceList;
            }
            set
            {
                this._classAttendanceList = value;
            }

        }

        public List<StudentAttendanceDetailsModel> AttenDetails_New { get; set; }

    }
    public class StudentAttendanceDetailsModel
    {
        public int sno { get; set; }
        public string ERPNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
        public string AdmNo { get; set; }
        public int CurrentMonthOpenDay { get; set; }
        public decimal CurrentMonthAttendance { get; set; }
        public int PreviousMonthOpenDay { get; set; }
        public decimal PrviousMonthAttendance { get; set; }
        public int TotalNoofOpenDay { get; set; }
        public decimal TotalAttendance { get; set; }
        public decimal AttendancePercentage { get; set; }
        public string ClassName { get; set; }
        public string AttenDate { get; set; }
    }
}
