using System;
using System.Collections.Generic;

namespace ISas.Entities.SMSManagement
{
    public class SMS_ReportMainModel
    {
        public long SNo { get; set; }
        public string GID { get; set; }
        public DateTime SendTime { get; set; }
        public string MessegeSend { get; set; }

        public int Count { get; set; }
        public string Classes { get; set; }
    }

    public class SMS_ReportDetailFilterModel
    {
        public SMS_ReportDetailFilterModel()
        {
            referenceList = new List<SMS_ReportDetailModel>();
        }
        public string GID { get; set; }
        public string DStatus { get; set; }
        public List<SMS_ReportDetailModel> referenceList { get; set; }

    }

    public class SMS_ReportDetailModel
    {
        //public SMS_ReportDetailModel()
        //{
        //    referenceList = new List<SMS_ReferenceList>();
        //}
        public bool IsSelected { get; set; }
        public string GID { get; set; }
        public string Sub_GID { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string MobileNo { get; set; }
        public string Status { get; set; }
        public DateTime? SendTime { get; set; }
        public DateTime? DeliverTime { get; set; }
        public string ERPNo { get; set; }
        public bool IsStatusFromAPI { get; set; }

        public string status_desc { get; set; }

        public DateTime? DisplaySendTime { get; set; }
        public DateTime? DisplayDeliverTime { get; set; }
        //public List<SMS_ReferenceList> referenceList { get; set; }

    }
    //public class SMS_ReferenceList
    //{
    //    public bool IsSelected { get; set; }
    //    public string ReferenceNo { get; set; }
    //    public string SubGuidId { get; set; }
    //    public string MobileNo { get; set; }

    //}



}
