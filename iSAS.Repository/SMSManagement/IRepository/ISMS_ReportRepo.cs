using ISas.Entities.SMSManagement;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.SMSManagement.IRepository
{
    public interface ISMS_ReportRepo
    {
        List<SMS_ReportMainModel> GetSMSDeliveryReport(string Mode, string Date, string SessionId);
        //SMS_ReportDetailFilterModel GetSMSDeliveryReportDetails(string Mode, string GID, string DStatus);
        List<SMS_ReportDetailModel> GetSMSDeliveryReportDetails(string Mode, string GID, string DStatus);
        Tuple<int, string> SMS_Resend(string GID);
        List<SelectListItem> GetSMSDeliveryCount(string GID, string SessionId);
    }
}
