using ISas.Entities.TimeTable_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.DashboardEntities
{
    #region  Admission Details
    public class StudentAdmissionDetailsMainModels ////this Mode Contain Total
    {
        public StudentAdmissionDetailsMainModels()
        {
            StudentAdmissionDetails = new List<StudentAdmissionDetailsSubModel>();
        }

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

        public List<StudentAdmissionDetailsSubModel> StudentAdmissionDetails { get; set; }
    }
    public class StudentAdmissionDetailsSubModel
    {
        public string ClassId { get; set; }
        public string SecId { get; set; }
        public string FullClassName { get; set; }
        public int ClassPO { get; set; }
        public int SectionPO { get; set; }
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
    }
    #endregion

    #region Fee Collection Details
    public class FeeCollectionDetailsModel
    {
        public FeeCollectionDetailsModel()
        {
            FeeCollectionDetails = new List<FeeCollectionDetailsSubModel>();
        }
        public int Strength { get; set; }
        public int PaidStudent { get; set; }
        public int DefaulterStudent { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public int Due { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public int Received { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public int Balance { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public int AnnualDue { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public int AnnualReceived { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public int AnnualBalance { get; set; }

        public decimal ReceivedPercent { get; set; }
        public decimal AReceivedPercent { get; set; }
        public decimal PaidCountPercent { get; set; }

        public List<FeeCollectionDetailsSubModel> FeeCollectionDetails { get; set; }
    }
    public class FeeCollectionDetailsSubModel
    {
        public string ClassSectionId { get; set; }
        public string Fullclass { get; set; }
        public int Strength { get; set; }
        public int PaidStudent { get; set; }
        public int DefaulterStudent { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#.##}")]
        [DataType(DataType.Currency)]
        public int Due { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#.##}")]
        [DataType(DataType.Currency)]
        public int Received { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#.##}")]
        [DataType(DataType.Currency)]
        public int Balance { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#.##}")]
        [DataType(DataType.Currency)]
        public int AnnualDue { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#.##}")]
        [DataType(DataType.Currency)]
        public int AnnualReceived { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#.##}")]
        [DataType(DataType.Currency)]
        public int AnnualBalance { get; set; }

        public decimal ReceivedPercent { get; set; } //{ get { return Due > 0 ? (Received / Due) * 100 : 0; } set { this.ReceivedPercent = value; } }
        public decimal AReceivedPercent { get; set; } // { get {return  AnnualDue > 0 ? (AnnualReceived / AnnualDue) * 100 : 0; } set { this.AReceivedPercent = value; } }
        public decimal PaidCountPercent { get; set; } //{ get { return Strength > 0 ? (PaidStudent / Strength) * 100 : 0; } set { this.PaidCountPercent = value; } }
    }
    #endregion

    public class _StudentDetails
    {
        public Int64 Sno { get; set; }
        public string AdmNo { get; set; }
        public string DOA { get; set; }
        public string StudentName { get; set; }
        public string Father { get; set; }
        public string SMSNo { get; set; }
        public string ERP { get; set; }

        public string Duration { get; set; }
        public int Due { get; set; }
        public int Paid { get; set; }
        public int Balance { get; set; }
        public int Excess { get; set; }

        public string RecNo { get; set; }
        public string Mode { get; set; }
        public string Class { get; set; }

        public int RollNo { get; set; }
        public string DOB { get; set; }
        public string BG { get; set; }
        public string Religion { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public string FMobile { get; set; }
        public string FEmail { get; set; }
        public string Mother { get; set; }
        public string MMobile { get; set; }
        public string Address { get; set; }
        public string SAddhar { get; set; }
        public string FAddhar { get; set; }
        public string MAddhar { get; set; }
        public string FProf { get; set; }
        public string MProf { get; set; }
        public string FQuli { get; set; }
        public string MQuli { get; set; }
        public decimal Income { get; set; }
        public string S { get; set; }
        public string F { get; set; }
        public string M { get; set; }


    }

    public class StudentDetailsModel //For Teacher View in Teacher DashBoard
    {
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public string FullClassName { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string SMSNo { get; set; }
        public bool NewAdm { get; set; }
        public bool OldAdm { get; set; }
        public bool TC { get; set; }
        public bool NSO { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Category { get; set; }
    }

    public class StudentDozearModel
    {
        public StudentDozearModel()
        {
            StudentDetails = new _StudentDetails();
            DaysList = new List<string>();
            PeriodDetailsList = new List<TimeTable_PeriodModel>();
            FeeDetails = new List<FeeDetailsModel>();
            SubjectTeachers = new List<SubjectTeacherModel>();
            AttenDetails = new List<AttendanceDetailModel>();
            WeekForMonth = new WeekForMonth();
            SMSDetails = new List<SMSDetailsModel>();
            BookHistory = new List<BookHistoryModel>();
            TransportDetails = new TransportDetailsModel();
            SiblingNames = new List<StudentSiblingDetailsModel>();
        }
        public _StudentDetails StudentDetails { get; set; }
        public TransportDetailsModel TransportDetails { get; set; }
        public List<string> DaysList { get; set; }
        public WeekForMonth WeekForMonth { get; set; }
        public List<TimeTable_PeriodModel> PeriodDetailsList { get; set; }
        public List<FeeDetailsModel> FeeDetails { get; set; }
        public List<SubjectTeacherModel> SubjectTeachers { get; set; }
        public List<AttendanceDetailModel> AttenDetails { get; set; }

        public List<SMSDetailsModel> SMSDetails { get; set; }
        public List<BookHistoryModel> BookHistory { get; set; }
        public List<StudentSiblingDetailsModel> SiblingNames { get; set; }
    }

    public class StudentSiblingDetailsModel
    {
        public string ERPNo { get; set; }
        public string SiblingName { get; set; }
    }

    public class TransportDetailsModel
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

    public class SMSDetailsModel
    {
        public long Sno { get; set; }
        public string SMSTExt { get; set; }
        public string SMSDate { get; set; }
        public string SMSStatus { get; set; }
        public string SMSDeliveryDate { get; set; }
    }

    public class BookHistoryModel
    {
        public string Sno { get; set; }
        public string AccessionNo { get; set; }
        public string BookTitle { get; set; }
        public string IssueDate { get; set; }
        public string ReturnDate { get; set; }
        public int Fine { get; set; }
    }

    public class FeeDetailsModel
    {
        public long sno { get; set; }
        public string DueDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Duration { get; set; }
        public int Due { get; set; }
        public int Paid { get; set; }
        public string ReceiptNo { get; set; }
        public string Bill { get; set; }
        public string Status { get; set; }
        public int Balance { get; set; }
        public int Excess { get; set; }
    }
    public class SubjectTeacherModel
    {
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
        public string ImageURL { get; set; }
    }

    public class AttendanceDetailModel
    {
        public string MnthName { get; set; }
        public string OD { get; set; }
        public string P { get; set; }
        public string HalfDay { get; set; }
        public string A { get; set; }
        public string L { get; set; }
        public string FinalAttendance { get; set; }
        public decimal Percentage { get; set; }
        public string MinPercentTextColor { get; set; }
        

    }
}
