using ISas.Entities.CommonEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.TimeTable_Entities
{
    public class TimeTable_Adjustment_FormLoadModel
    {
        public TimeTable_Adjustment_FormLoadModel()
        {
            AbsentTeacherList = new List<Adjustment_TeacherDetailModel>();
            AdjustmentTeacherList = new List<Adjustment_TeacherDetailModel>();
            DayAdjustmentList = new List<DayAdjustment>();
            ReportHeader = new ReportHeaderEntities();
            wingList = new List<SelectListItem>();
        }
        [Required(ErrorMessage = "Date is required..!")]
        [Display(Name = "Date")]
        public string AdjustmentDate { get; set; }
        [Display(Name = "Wing")]
        public string wingId { get; set; }
        public List<DayAdjustment> DayAdjustmentList { get; set; }
        public List<Adjustment_TeacherDetailModel> AbsentTeacherList { get; set; }
        public List<Adjustment_TeacherDetailModel> AdjustmentTeacherList { get; set; }
        public List<SelectListItem> wingList { get; set; }
        public ReportHeaderEntities ReportHeader { get; set; }
    }

    public class Adjustment_TeacherDetailModel
    {
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public int NoofPeriod { get; set; }
        public int NoofAdjustPeriod { get; set; }
        public string AdjustPeriod { get; set; }
    }

    public class TimeTable_Adjustment_TransactionModel
    {
        public TimeTable_Adjustment_TransactionModel()
        {
            EffectedPeriodList = new List<Adjustment_PeriodDetailModel>();
            AvailableTeacherList = new List<Adjustment_PeriodDetailModel>();
            EffectedClassList = new List<SelectListItem>();
        }

        public string AbsentStaffName { get; set; }
        public string AbsentStaffId { get; set; }
        public string EffectedClassId { get; set; }
        public string AbsentDate { get; set; }
        public string UserId { get; set; }

        public List<SelectListItem> EffectedClassList { get; set; }

        public List<Adjustment_PeriodDetailModel> EffectedPeriodList { get; set; }
        public List<Adjustment_PeriodDetailModel> AvailableTeacherList { get; set; }
    }

    public class Adjustment_PeriodDetailModel
    {
        public string PeriodName { get; set; }
        public string SubjectName { get; set; }
        public string PeriodWithTime { get; set; }
        public string TeacherName { get; set; }
        public string TeacherId { get; set; }
    }

    public class DayAdjustment
    {
        public int Sno { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string SubjectName { get; set; }
        public string PeriodDay { get; set; }
        public string PeriodName { get; set; }
        public string ClassName { get; set; }
        public string PossibleAdjustment { get; set; }
    }
}
