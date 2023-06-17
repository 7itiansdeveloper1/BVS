using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.DashboardEntities
{
    public class DashBoard_ParentRequestMasterModel
    {
        public DashBoard_ParentRequestMasterModel()
        {
            CommunicationDetailsList = new List<RequestCommunicationDetailsModel>();
        }

        public string RequestId { get; set; }
        
        [Display(Name = "Description")]
        [Required(ErrorMessage ="Description is Req..")]
        [StringLength(500, ErrorMessage = "Max Len 500..")]
        public string RequestSubject { get; set; }

        public string RequestDate { get; set; }
        public string RequestCategory { get; set; }
        public string RequestUserId { get; set; }
        public string RequestStatus { get; set; }
        public string RepliedByUserId { get; set; }
        public string RepliedDate { get; set; }

        [Display(Name = "From Date")]
        //[Required(ErrorMessage ="From Date is Req..")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        //[Required(ErrorMessage = "To Date is Req..")]
        public string ToDate { get; set; }

        // Extra Feilds
        public string UserID { get; set; }
        public string Function { get; set; }

        [Display(Name = "Leave Days")]
        public int LeaveDays { get; set; }

        public List<RequestCommunicationDetailsModel> CommunicationDetailsList { get; set; }
    }


    public class RequestCommunicationDetailsModel
    {
        public string Message { get; set; }
        public string SendBy { get; set; }
        public string SendDateTime { get; set; }
        public bool IsUserSendMsg { get; set; }
    }
}
