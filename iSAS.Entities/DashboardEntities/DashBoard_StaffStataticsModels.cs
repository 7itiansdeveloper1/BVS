using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.DashboardEntities
{
    public class Staff_AttendanceDetailsModel
    {
        public Staff_AttendanceDetailsModel()
        {
            LeaveTypeList = new List<SelectListItem>();
            RequestToList = new List<RequestToModel>();
            LeaveBalanceDetails = new List<LeaveBalanceDetailsModel>();
            AvailedLeaveHistoryList = new List<AvailedLeaveHistoryModel>();
        }

        [Display(Name = "From Date")]
        public string FromDate { get; set; }

        [Display(Name ="To Date")]
        public string ToDate { get; set; }

        [Display(Name = "Leave Type")]
        public string LeaveTypeId { get; set; }

        public string Description { get; set; }
        public string UserId { get; set; }

        public List<SelectListItem> LeaveTypeList { get; set; }
        public List<RequestToModel> RequestToList { get; set; }
        public List<LeaveBalanceDetailsModel> LeaveBalanceDetails { get; set; }
        public List<AvailedLeaveHistoryModel> AvailedLeaveHistoryList { get; set; }
    }

    public class RequestToModel
    {
        public string Levels { get; set; }
        public bool IsSelected { get; set; }
        public bool IsRequestReadEnable { get; set; }
    }

    public class LeaveBalanceDetailsModel
    {
        public string LvID { get; set; }
        public string LvCode { get; set; }
        public int LeaveOpenBalance { get; set; }
        public int LeaveAvailed { get; set; }
        public int LeaveAvailable { get; set; }
    }

    public class AvailedLeaveHistoryModel
    {
        public string RequestId { get; set; }
        public string RReferenceCode { get; set; }
        public string RReferenceName { get; set; }
        public string RDiscription { get; set; }
        public string RSendTo { get; set; }
        public string RStatus { get; set; }
        public string RBy { get; set; }
        public string RDate { get; set; }
        public string MDate { get; set; }
        public string RCloseDate { get; set; }
        public bool IsEditable { get; set; }
    }
}
