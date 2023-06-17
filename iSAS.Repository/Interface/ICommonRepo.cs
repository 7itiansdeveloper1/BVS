using ISas.Entities;
using ISas.Entities.CommonEntities;
using ISas.Entities.SMSManagement;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Mvc;
using System.Data;


namespace ISas.Repository.Interface
{
    public interface ICommonRepo
    {
        List<SelectListItem> GetStudentList(string sessionId, string classId, string sectionId);
        StudentSearchModel GetStudentSearchDetails(string SessionId, string ErpNo, string AdmNo);
        List<SelectListItem> GetBankList();
        bool SendEmail(string ToMailId,string Subject,string Body,bool IsBodyHtml,  List<Attachment> Attachments);
        void ExceptionLoggingToDataBase(ExceptionLogger model);
        DataSet Get_ReportConfiguration(string Module, string RefNo);

        /// <summary>
        /// Used For Send SMS Without Log
        /// </summary>
        /// <param name="SMSNos"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        Tuple<int, string> SendSMS(string SMSNos, string Msg);

        /// <summary>
        /// Used For Send SMS To Students (Multiple Students/Bulk SMS), With Log
        /// </summary>
        /// <param name="SMSNos"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        Tuple<int, string> SendSMS(string Msg, string MsgType, List<SMS_StudentModel> uniquefilteredStud, List<SMS_StudentModel> duplicatefilteredStud);

        /// <summary>
        /// Used For Send SMS To Staff, With Log
        /// </summary>
        /// <param name="SMSNos"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        Tuple<int, string> SendSMS(string Msg, string MsgType, List<SMS_AdminAndStaffModel> filteredStaff);

        /// <summary>
        /// Used For Send SMS To Student (Single Student), With Log
        /// </summary>
        /// <param name="SMSNos"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        Tuple<int, string> SendSMS(string Msg, string MsgType, string ERPNo, string SMSNo);

        bool CHECKTO_UserAuthorization(string ControllerName, string RequestFor, string UserID);
        ReportHeaderEntities ReportHeaderDetails(string ReportName);
        Tuple<int, string> AlertTransaction_CRUD(AlertTransactionModel model);
        List<AlertTransactionModel> GetNotificationList(string UserId);
        int GetNotificationCount(string UserId);
        bool CheckIsUniqueNo(string CheckFiledname, string ReferenceNo, string Id);

        List<Basic_StudentInfoModel> Basic_StudentInformation_ByMode(
           string Mode, string SessionId, string Param1, string Param2, string Param3, string Param4, string Param5);

        ClinetInfoEntities get_ClientInfo();
        Tuple<int, string> ReSendSMS(string GID, List<SMS_ReportDetailModel> filteredReference, List<SMS_ReportDetailModel> duplicatefilteredReference);
    }
}
