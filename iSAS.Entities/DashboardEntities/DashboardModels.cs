using ISas.Entities.Academic;
using ISas.Entities.TimeTable_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.DashboardEntities
{
    public class Dashboard_StudentModel
    {
        public Dashboard_StudentModel()
        {
            NewsList = new List<Common_NECN_LandingModel>();
            EventsList = new List<Common_NECN_LandingModel>();
            NoticeList = new List<Common_NECN_LandingModel>();
            CircularList = new List<Common_NECN_LandingModel>();


            RouteDetails = new Dash_Stud_RouteDetailsModel();
            SubjectTeacherList = new List<Dash_Stud_SubjectTeacherModel>();
            // TimeTableDetails = new List<Dash_Stud_TimeTblModel>();
            ResultList = new List<Dash_Stud_ResultModel>();

            DaysList = new List<string>();
            PeriodDetailsList = new List<TimeTable_PeriodModel>();
            AttenDetails = new AttendanceSummaryModel();
            // FeeDetails = new List<FeeDetailsModel>();
            SMSDetails = new List<SMSDetailsModel>();
            BookHistory = new List<BookHistoryModel>();

            HomeWorkList = new List<Academic_HomeWorkMasterModels>();
            AssignmentList = new List<Academic_HomeWorkMasterModels>();
            DirectoryList = new List<Academic_DirectoryMasterModels>();
            SyllabusList = new List<Dash_SyllabusDetailsModels>();
            FeeSummary = new FeeSummaryModel();
        }
        public string foodForThought { get; set; }
        public string ERPNo { get; set; }
        public string DOB { get; set; }
        public string InvitationType { get; set; }
        public AttendanceSummaryModel AttenDetails { get; set; }
        //public List<FeeDetailsModel> FeeDetails { get; set; }
        public FeeSummaryModel FeeSummary { get; set; }
        public List<SMSDetailsModel> SMSDetails { get; set; }
        public List<BookHistoryModel> BookHistory { get; set; }

        public List<Common_NECN_LandingModel> NewsList { get; set; }
        public List<Common_NECN_LandingModel> EventsList { get; set; }
        public List<Common_NECN_LandingModel> NoticeList { get; set; }
        public List<Common_NECN_LandingModel> CircularList { get; set; }

        public string ClassTeacherName { get; set; }

        public List<Academic_HomeWorkMasterModels> HomeWorkList { get; set; }
        public List<Academic_HomeWorkMasterModels> AssignmentList { get; set; }
        public List<Academic_DirectoryMasterModels> DirectoryList { get; set; }
        public List<Dash_SyllabusDetailsModels> SyllabusList { get; set; }

        public Dash_Stud_RouteDetailsModel RouteDetails { get; set; }
        public List<Dash_Stud_SubjectTeacherModel> SubjectTeacherList { get; set; }
        //   public List<Dash_Stud_TimeTblModel> TimeTableDetails { get; set; }
        public List<Dash_Stud_ResultModel> ResultList { get; set; }

        public List<string> DaysList { get; set; }
        public List<TimeTable_PeriodModel> PeriodDetailsList { get; set; }
    }

    public class AttendanceSummaryModel
    {
        public string MnthName { get; set; }
        public int OD { get; set; }
        public decimal P { get; set; }
        public decimal HalfDay { get; set; }
        public decimal A { get; set; }
        public decimal L { get; set; }
        public string FinalAttendance { get; set; }
        public decimal Percentage { get; set; }
        public string AttDefaulterMessage { get; set; }

    }

    public class FeeSummaryModel
    {
        public string Flag { get; set; }
        public string DueDate { get; set; }
        public string PayableAmount { get; set; }

        //public int Due { get; set; }
        //public int Paid { get; set; }
        //public int Balance { get; set; }
        //public int Excess { get; set; }
    }

    public class Dash_SyllabusDetailsModels
    {
        public string SyllabusId { get; set; }
        public string Title { get; set; }
        public string AttachmentReference { get; set; }
        public string DescriptionReferenctURL { get; set; }
        public string UploadBy { get; set; }
        public string UploadDate { get; set; }
    }

    public class Dash_Stud_SubjectTeacherModel
    {
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
        public string ImageUrl { get; set; }
    }

    public class Dash_Stud_RouteDetailsModel
    {
        public string AvailedTransport { get; set; }
        public string Route { get; set; }
        public string Stop { get; set; }
        public string Facility { get; set; }
        public string Charges { get; set; }
        public string PickupTime { get; set; }
        public string DropTime { get; set; }
        public string Driver { get; set; }
        public string Helper { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
    }

    //public class Dash_Stud_TimeTblModel
    //{
    //    public string Day { get; set; }
    //    public string P1 { get; set; }
    //    public string P2 { get; set; }
    //    public string P3 { get; set; }
    //    public string P4 { get; set; }
    //    public string P5 { get; set; }
    //    public string P6 { get; set; }
    //    public string P7 { get; set; }
    //    public string P8 { get; set; }
    //}

    public class Dash_Stud_ResultModel
    {
        public string ExamId { get; set; }
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public string ExamName { get; set; }
        public string Duration { get; set; }
        public string ExamStatus { get; set; }
        public string ResultAnounceDate { get; set; }
        public string Result { get; set; }
        public bool IsDownload { get; set; }
    }

    public class AttnDetails_ParentModel
    {
        public AttnDetails_ParentModel()
        {
            ApplyNewLeave = new DashBoard_ParentRequestMasterModel();
            AppliedLeaveDetails = new List<DashBoard_ParentRequestMasterModel>();
            SessionList = new List<SelectListItem>();
            AttenDetails = new List<AttendanceDetailModel>();
        }
        public int SessionID { get; set; }
        public DashBoard_ParentRequestMasterModel ApplyNewLeave { get; set; }
        public List<DashBoard_ParentRequestMasterModel> AppliedLeaveDetails { get; set; }
        public List<SelectListItem> SessionList { get; set; }
        public List<AttendanceDetailModel> AttenDetails { get; set; }
        public string AttDefaulterMessage { get; set; }
    }

    public class AttnDetailsModel
    {
        public string MonthName { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int HolidayCount { get; set; }
        public int WeekOffCount { get; set; }
        public int LeaveCount { get; set; }
        public int HalfDaysCount { get; set; }
    }

    public class WeekForMonth
    {
        public List<Day> Week1 { get; set; } //days for week1
        public List<Day> Week2 { get; set; } //days for week2
        public List<Day> Week3 { get; set; } //days for week3
        public List<Day> Week4 { get; set; } //days for week4
        public List<Day> Week5 { get; set; } //days for week5
        public List<Day> Week6 { get; set; } //days for week6
        public string nextMonth { get; set; }
        public string prevMonth { get; set; }
        public string MonthNameWithYear { get; set; }
    }

    public class Day
    {
        public DateTime Date { get; set; }
        public string _Date { get; set; }
        public string dateStr { get; set; }
        public int dtDay { get; set; }
        public int? daycolumn { get; set; }

        public string AttendenceStatus { get; set; } //Present, Absent,Holiday
    }

    //Staff Dashboard Models
    #region Staff DashBoard Models
    public class Dashboard_StaffModel
    {
        public Dashboard_StaffModel()
        {
            NewsList = new List<Common_NECN_LandingModel>();
            EventsList = new List<Common_NECN_LandingModel>();
            NoticeList = new List<Common_NECN_LandingModel>();
            CircularList = new List<Common_NECN_LandingModel>();
            AttendanceSummary = new Staff_AttendanceSummaryModel();
            ToDoDetails = new List<ToDo_TaskEntitiesModel>();
            Student_BrithdayList = new List<Student_BirthdayDetailsModel>();
            Staff_BrithdayList = new List<Staff_BirthdayDetailsModel>();
            Staff_TimeTableDetails = new List<SelectListItem>();
            Staff_AdjustmentDetails = new List<SelectListItem>();
            Class_AttnSummary = new Class_AttendanceSummaryModel();
        }

        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string StaffId { get; set; }
        public string foodForThought { get; set; }
        public List<Common_NECN_LandingModel> NewsList { get; set; }
        public List<Common_NECN_LandingModel> EventsList { get; set; }
        public List<Common_NECN_LandingModel> NoticeList { get; set; }
        public List<Common_NECN_LandingModel> CircularList { get; set; }
        public Staff_AttendanceSummaryModel AttendanceSummary { get; set; }
        public List<ToDo_TaskEntitiesModel> ToDoDetails { get; set; }
        public List<Student_BirthdayDetailsModel> Student_BrithdayList { get; set; }
        public List<Staff_BirthdayDetailsModel> Staff_BrithdayList { get; set; }
        public List<SelectListItem> Staff_TimeTableDetails { get; set; }
        public List<SelectListItem> Staff_AdjustmentDetails { get; set; }
        public Class_AttendanceSummaryModel Class_AttnSummary { get; set; }
    }
    public class Staff_AttendanceSummaryModel
    {
        public int P { get; set; }
        public int A { get; set; }
        public int L { get; set; }
        public int HD { get; set; }
        public int SL { get; set; }
        public string TotalAttendance { get; set; }
    }
    public class Student_BirthdayDetailsModel
    {
        public string stud_uid { get; set; }
        public string Student { get; set; }
        public int Age { get; set; }
        public string DOB { get; set; }
        public string DOB1 { get; set; }
        public string FullClassName { get; set; }
        public string ImageURL { get; set; }
    }
    public class Staff_BirthdayDetailsModel
    {
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public int Age { get; set; }
        public string DOB { get; set; }
        public string DOB1 { get; set; }
        public string ImageURL { get; set; }
    }
    public class Class_AttendanceSummaryModel
    {
        public int ClassStrength { get; set; }
        public string AttStatus { get; set; }
        public int P { get; set; }
        public int A { get; set; }
        public int L { get; set; }
    }

    public class SalaryDetailsModel
    {
        public string StaffId { get; set; }
        public string SalMonth { get; set; }
        public string Salyear { get; set; }
        public string SalMonthName { get; set; }
        public decimal InHand { get; set; }
    }
    public class MyClassInfoModel
    {
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public string FullClassName { get; set; }
        public int ClassPO { get; set; }
        public int Strength { get; set; }
        public int NewAdm { get; set; }
        public int OldAdm { get; set; }
        public int TC { get; set; }
        public int NSO { get; set; }
        public int BOY { get; set; }
        public int GIRL { get; set; }
        public int GEN { get; set; }
        public int SC { get; set; }
        public int ST { get; set; }
        public int OBC { get; set; }
        public int EWS { get; set; }

        public int FeeDefaulter { get; set; }
        public int LibraryDefaulter { get; set; }
    }
    #endregion
    //Admin Dashboard Models
    public class DashboardModel_Admin
    {
        public DashboardModel_Admin()
        {
            FeeCollection = new CollectionDetailModel();
            TransportCollection = new CollectionDetailModel();
            AttnStatusList = new List<AttendanceStatusModel>();
            StudentStrengthList = new List<StudentStrenghtModel>();
            SectionList = new List<string>();
            noticeCircularList = new List<NoticeCircularList>();
            homeWorkList = new List<HomeWorkList>();
            classNotesList = new List<ClassNotesList>();
            studentBirthdayList = new List<StudentBirthdayList>();
            staffBirthdayList = new List<StaffBirthdayList>();
            sMSList = new List<SMSList>();
            AdmissionDetails = new AdmissionDetailsModel();

        }


        public AdmissionDetailsModel AdmissionDetails { get; set; }
        public CollectionDetailModel FeeCollection { get; set; }
        public CollectionDetailModel TransportCollection { get; set; }

        public List<AttendanceStatusModel> AttnStatusList { get; set; }
        public List<StudentStrenghtModel> StudentStrengthList { get; set; }
        public List<NoticeCircularList> noticeCircularList { get; set; }
        public List<HomeWorkList> homeWorkList { get; set; }
        public List<ClassNotesList> classNotesList { get; set; }
        public List<StudentBirthdayList> studentBirthdayList { get; set; }
        public List<StaffBirthdayList> staffBirthdayList { get; set; }
        public List<SMSList> sMSList { get; set; }

        public List<string> SectionList { get; set; }
        public string foodForThought { get; set; }
        // public List<TempStudentAttnStatus> TempStudentAttnStatus { get; set; }


    }

    public class AdmissionDetailsModel
    {
        [Display(Name = "REG.")]
        public int Registration { get; set; }

        [Display(Name = "ADM.")]
        public int Admission { get; set; }

        [Display(Name = "TC")]
        public int TC { get; set; }

        [Display(Name = "NSO")]
        public int NSO { get; set; }
    }

    //public class TempStudentAttnStatus
    //{
    //    public TempStudentAttnStatus()
    //    {
    //        ValueList = new List<Tuple<string, int>>();
    //    }
    //    public string ClassName { get; set; }
    //    public List<Tuple<string, int>>  ValueList { get; set; }
    //}

    public class AttendanceStatusModel
    {
        public string AttenStatus { get; set; }
        public int NoofCount { get; set; }
    }

    public class CollectionDetailModel
    {
        public int CASH { get; set; }
        public int CHEQUE { get; set; }
        public int ONLINE { get; set; }
        public int TOTAL { get; set; }
    }

    public class StudentStrenghtModel
    {
        public StudentStrenghtModel()
        {
            SectionList = new List<Tuple<string, string>>();
        }

        public string ClassName { get; set; }
        public int Sec1 { get; set; }
        public int Sec2 { get; set; }
        public int Sec3 { get; set; }
        public int Sec4 { get; set; }
        public int Sec5 { get; set; }
        public int Total { get; set; }
        public List<Tuple<string, string>> SectionList { get; set; }
    }

    public class AllClassSectionValueModel
    {
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string NoOfStudents { get; set; }
    }
    public class NoticeCircularList
    {
        public string UploadID { get; set; }
        public string UploadTitle { get; set; }
        public string UploadDescription { get; set; }
        public string UploadStartDate { get; set; }
        public string UploadEndDate { get; set; }
        public bool IsExpiry { get; set; }
        public string CreationDate { get; set; }
        public string AttachPath { get; set; }
        public bool HavingAttachment { get; set; }
        public bool IsNew { get; set; }
    }
    public class HomeWorkList
    {
        public string HomeWorkId { get; set; }
        public string Title { get; set; }
        public string HomeWorkCategory { get; set; }
        public string UploadDate { get; set; }
        public string SubmissionDate { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public string DiscriptionReference { get; set; }
    }
    public class ClassNotesList
    {
        public string HomeWorkId { get; set; }
        public string Title { get; set; }
        public string HomeWorkCategory { get; set; }
        public string UploadDate { get; set; }
        public string SubmissionDate { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public string DiscriptionReference { get; set; }
    }
    public class StudentBirthdayList
    {
        public string Student { get; set; }
        public int Age { get; set; }
        public string DOB { get; set; }
        public string DOB1 { get; set; }
        public string FullClassName { get; set; }
        public string ImageURL { get; set; }
    }
    public class StaffBirthdayList
    {
        public string StaffName { get; set; }
        public int Age { get; set; }
        public string DOB { get; set; }
        public string DOB1 { get; set; }
        public string ImageURL { get; set; }
    }
    public class SMSList
    {
        public string MessageSend { get; set; }
        public string SendTime { get; set; }
    }
}
