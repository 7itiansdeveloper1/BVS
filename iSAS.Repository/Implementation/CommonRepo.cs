using ISas.Entities;
using ISas.Entities.CommonEntities;
using ISas.Entities.SMSManagement;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace ISas.Repository.Implementation
{
    public class CommonRepo : ICommonRepo
    {
        public List<SelectListItem> GetStudentList(string sessionId, string classId, string sectionId)
        {
            List<SelectListItem> studentList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Student_GetStudentListByClassSection", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                studentList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StudentName"),
                    Value = r.Field<string>("ERPNo").ToString(),
                }).ToList();
            }
            return studentList;
        }

        public List<SelectListItem> GetBankList()
        {
            List<SelectListItem> bankList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_GetBankName", con);
                cmd.CommandType = CommandType.StoredProcedure;


                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                bankList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("BankName"),
                    Value = r.Field<string>("BankID").ToString(),
                }).ToList();
            }
            return bankList;
        }

        public StudentSearchModel GetStudentSearchDetails(string SessionId, string ErpNo, string AdmNo)
        {
            StudentSearchModel model = new StudentSearchModel();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_StudentSearchbyERPorAdmNo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ERPNo", ErpNo);
                cmd.Parameters.AddWithValue("@AdmNo", AdmNo);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                model = ds.Tables[2].AsEnumerable().Select(r => new StudentSearchModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ClassId = r.Field<string>("ClassId"),
                    ERPNo = r.Field<string>("ERPNo"),
                    FatherName = r.Field<string>("Father"),
                    FeeCategory = r.Field<string>("FeeCategoryName"),
                    FeeCategoryId = r.Field<string>("FeeCategoryId"),
                    MobileNo = r.Field<string>("SMSNo"),
                    MotherName = r.Field<string>("Mother"),
                    SectionId = r.Field<string>("SectionId"),
                    StudentId = r.Field<string>("ERPNo"),
                    SiblingName = r.Field<string>("SiblingName"),
                }).FirstOrDefault();

                if (model == null)
                    model = new StudentSearchModel();

                model.SectionList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SectionName"),
                    Value = r.Field<string>("SectionId").ToString(),
                    Selected = r.Field<bool>("Selected"),
                }).ToList();

                model.StudentList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Student"),
                    Value = r.Field<string>("ERPNo").ToString(),
                    Selected = r.Field<bool>("Selected"),
                }).ToList();

                model.ClassList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID").ToString(),
                    Selected = r.Field<bool>("Selected"),
                }).ToList();

                model.SiblingList = ds.Tables[4].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Student"),
                    Value = r.Field<string>("SiblingERPNo").ToString(),
                }).ToList();
            }
            return model;
        }

        public bool SendEmail(string ToMailId, string Subject, string Body, bool IsBodyHtml, List<Attachment> Attachments)
        {
            string PortName = System.Configuration.ConfigurationManager.AppSettings["PortName"];
            string FromMailId = System.Configuration.ConfigurationManager.AppSettings["FromMailId"];
            int PortNo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PortNo"]);
            string UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            string Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(PortName);
                mail.From = new MailAddress(FromMailId);
                mail.To.Add(ToMailId);
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = IsBodyHtml;

                //Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(Server.MapPath("~/Images/System/balVikasLog.jpg"));

                if (Attachments != null && Attachments.Count > 0)
                {
                    for (int i = 0; i < Attachments.Count; i++)
                    {
                        mail.Attachments.Add(Attachments[i]);
                    }
                }

                SmtpServer.Port = PortNo;
                SmtpServer.Credentials = new System.Net.NetworkCredential(UserName, Password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                return true; ;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        #region Alert Simfiny Code For Send SMS
        //public Tuple<int, string> SendSMS(string SMSNos, string Msg, string ErpNosOrStaffCodes, bool IsSendForStaff, string duplicateSMSNos, string duplicateSMSCorrespondERPNo, string MsgType)
        //{

        //try
        //{
        //    if (MsgType == "BOTH" || MsgType == "SMS") //If We Want to send sms or send sms with alert on app
        //    {
        //        string url = "";
        //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_Fee_Defaulter_Transaction", con);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            con.Open();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();
        //            da.Fill(ds);

        //            url = ds.Tables[1].Rows[0][0].ToString();
        //            url = url.Replace("[no]", SMSNos);
        //            url = url.Replace("[msg]", Msg);
        //        }

        //        var client = new WebClient();
        //        var content = client.DownloadString(url);//"Message GID=8456146407 ID=8456146407-1, 8456146407-2"; //
        //        string guidId = content.Between("Message GID=", "ID").Replace(" ", "");
        //        string subGuidIds = content.After("ID=").Replace(" ", "");

        //        int groupSize = 200;
        //        List<string> tempSubGuidIDList = subGuidIds.Split(',').ToList();
        //        if (tempSubGuidIDList.Count > groupSize)
        //        {
        //            int Quotient = tempSubGuidIDList.Count / groupSize;
        //            int Remainder = tempSubGuidIDList.Count % groupSize;

        //            List<string> tempSMSNoList = SMSNos.Split(',').ToList();
        //            List<string> tempERPNoList = ErpNosOrStaffCodes.Split(',').ToList();

        //            List<string> f_subGuidIds = new List<string>();
        //            List<string> f_smsNos = new List<string>();
        //            List<string> f_erpNos = new List<string>();

        //            var numberOfGroups = Quotient + (Remainder > 0 ? 1 : 0); // If there is any remainder then add it as another loop.
        //            for (int i = 0; i < numberOfGroups; i++)
        //            {
        //                f_subGuidIds.Add(string.Join(",", tempSubGuidIDList.Skip(i * groupSize).Take(groupSize).ToList()));
        //                f_smsNos.Add(string.Join(",", tempSMSNoList.Skip(i * groupSize).Take(groupSize).ToList()));
        //                f_erpNos.Add(string.Join(",", tempERPNoList.Skip(i * groupSize).Take(groupSize).ToList()));
        //            }

        //            for (int i = 0; i < numberOfGroups; i++)
        //                SaveSendSMSReport(guidId, f_subGuidIds[i], f_smsNos[i], f_erpNos[i], Msg, IsSendForStaff, "", "", MsgType);
        //        }
        //        else
        //        {
        //            SaveSendSMSReport(guidId, subGuidIds, SMSNos, ErpNosOrStaffCodes, Msg, IsSendForStaff, "", "", MsgType);
        //        }
        //        if (!string.IsNullOrEmpty(duplicateSMSNos) && !string.IsNullOrEmpty(duplicateSMSCorrespondERPNo))
        //        {
        //            List<string> tempDuplicateSMSNos = duplicateSMSNos.Split(',').ToList();
        //            if (tempDuplicateSMSNos.Count > groupSize)
        //            {
        //                int Quotient = tempDuplicateSMSNos.Count / groupSize;
        //                int Remainder = tempDuplicateSMSNos.Count % groupSize;

        //                List<string> duplicatetempERPNoList = duplicateSMSCorrespondERPNo.Split(',').ToList();

        //                List<string> f_smsNos = new List<string>();
        //                List<string> f_erpNos = new List<string>();

        //                var numberOfGroups = Quotient + (Remainder > 0 ? 1 : 0); // If there is any remainder then add it as another loop.
        //                for (int i = 0; i < numberOfGroups; i++)
        //                {
        //                    f_smsNos.Add(string.Join(",", tempDuplicateSMSNos.Skip(i * groupSize).Take(groupSize).ToList()));
        //                    f_erpNos.Add(string.Join(",", duplicatetempERPNoList.Skip(i * groupSize).Take(groupSize).ToList()));
        //                }

        //                for (int i = 0; i < numberOfGroups; i++)
        //                    SaveSendSMSReport(guidId, "", "", "", Msg, IsSendForStaff, f_smsNos[i], f_erpNos[i], MsgType);
        //            }
        //            else
        //            {
        //                SaveSendSMSReport(guidId, "", "", "", Msg, IsSendForStaff, duplicateSMSNos, duplicateSMSCorrespondERPNo, MsgType);
        //            }
        //        }
        //        return new Tuple<int, string>(1, "SMS Send Successfully..!");
        //    } 
        //    else //we alert type is app only
        //    {
        //        int groupSize = 200;
        //        List<string> tempSMSNoList = SMSNos.Split(',').ToList();
        //        List<string> tempERPNoList = ErpNosOrStaffCodes.Split(',').ToList();
        //        if (tempSMSNoList.Count > groupSize)
        //        {
        //            int Quotient = tempSMSNoList.Count / groupSize;
        //            int Remainder = tempSMSNoList.Count % groupSize;

        //           // List<string> f_subGuidIds = new List<string>();
        //            List<string> f_smsNos = new List<string>();
        //            List<string> f_erpNos = new List<string>();

        //            var numberOfGroups = Quotient + (Remainder > 0 ? 1 : 0); // If there is any remainder then add it as another loop.
        //            for (int i = 0; i < numberOfGroups; i++)
        //            {
        //                //f_subGuidIds.Add(string.Join(",", tempSubGuidIDList.Skip(i * groupSize).Take(groupSize).ToList()));
        //                f_smsNos.Add(string.Join(",", tempSMSNoList.Skip(i * groupSize).Take(groupSize).ToList()));
        //                f_erpNos.Add(string.Join(",", tempERPNoList.Skip(i * groupSize).Take(groupSize).ToList()));
        //            }

        //            for (int i = 0; i < numberOfGroups; i++)
        //                SaveSendSMSReport(null, null, f_smsNos[i], f_erpNos[i], Msg, IsSendForStaff, "", "", MsgType);
        //        }
        //        else
        //        {
        //            SaveSendSMSReport(null, null, SMSNos, ErpNosOrStaffCodes, Msg, IsSendForStaff, "", "", MsgType);
        //        }
        //        if (!string.IsNullOrEmpty(duplicateSMSNos) && !string.IsNullOrEmpty(duplicateSMSCorrespondERPNo))
        //        {
        //            List<string> tempDuplicateSMSNos = duplicateSMSNos.Split(',').ToList();
        //            if (tempDuplicateSMSNos.Count > groupSize)
        //            {
        //                int Quotient = tempDuplicateSMSNos.Count / groupSize;
        //                int Remainder = tempDuplicateSMSNos.Count % groupSize;

        //                List<string> duplicatetempERPNoList = duplicateSMSCorrespondERPNo.Split(',').ToList();

        //                List<string> f_smsNos = new List<string>();
        //                List<string> f_erpNos = new List<string>();

        //                var numberOfGroups = Quotient + (Remainder > 0 ? 1 : 0); // If there is any remainder then add it as another loop.
        //                for (int i = 0; i < numberOfGroups; i++)
        //                {
        //                    f_smsNos.Add(string.Join(",", tempDuplicateSMSNos.Skip(i * groupSize).Take(groupSize).ToList()));
        //                    f_erpNos.Add(string.Join(",", duplicatetempERPNoList.Skip(i * groupSize).Take(groupSize).ToList()));
        //                }

        //                for (int i = 0; i < numberOfGroups; i++)
        //                    SaveSendSMSReport(null, "", "", "", Msg, IsSendForStaff, f_smsNos[i], f_erpNos[i], MsgType);
        //            }
        //            else
        //            {
        //                SaveSendSMSReport(null, "", "", "", Msg, IsSendForStaff, duplicateSMSNos, duplicateSMSCorrespondERPNo, MsgType);
        //            }
        //        }
        //        return new Tuple<int, string>(1, "SMS Send Successfully..!");
        //    }
        //}
        //catch (Exception e)
        //{
        //    ExceptionLogger logger = new ExceptionLogger()
        //    {
        //        ExceptionMsg = e.Message,
        //        ExceptionStackTrace = e.StackTrace,
        //        ControllerName = "CommonRepo",
        //        ActionName = "SendSMS",
        //    };
        //    ExceptionLoggingToDataBase(logger);
        //    return new Tuple<int, string>(0, e.Message);
        //}
        // }
        #endregion


        public Tuple<int, string> SendSMS(string SMSNos, string Msg)
        {
            try
            {
                string url = "";
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("select * from SMS_API", con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        url = ds.Tables[0].Rows[0][0].ToString();
                        url = url.Replace("[no]", SMSNos);
                        url = url.Replace("[msg]", Msg);
                    }
                }

                var client = new WebClient();
                var content = client.DownloadString(url);

                string guidId = content.Between("Message GID=", "ID").Replace(" ", "");
                //string subGuidId = content.After("ID=").Replace(" ", "");
                if (!string.IsNullOrEmpty(guidId))
                    return new Tuple<int, string>(1, "SMS Send Successfully..!");

                return new Tuple<int, string>(0, "Failed to send sms..!");
            }
            catch (Exception e)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ExceptionMsg = e.Message,
                    ExceptionStackTrace = e.StackTrace,
                    ControllerName = "CommonRepo",
                    ActionName = "SendSMS",
                };
                ExceptionLoggingToDataBase(logger);
                return new Tuple<int, string>(0, e.Message);
            }
        }
        public Tuple<int, string> SendSMS(string Msg, string MsgType, List<SMS_StudentModel> uniquefilteredStud, List<SMS_StudentModel> duplicatefilteredStud)
        {
            try
            {
                if (MsgType == "BOTH" || MsgType == "SMS")
                {
                    string url = "";
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("sp_Fee_Defaulter_Transaction", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        url = ds.Tables[1].Rows[0][0].ToString();
                        url = url.Replace("[no]", string.Join(",", uniquefilteredStud.Select(r => r.SMSNo).ToList()));
                        url = url.Replace("[msg]", Msg);
                    }

                    var client = new WebClient();
                    var content = client.DownloadString(url);  //"{\"status\":\"OK\",\"data\":[{\"id\":\"17912298-1\",\"mobile\":\"919999306536\",\"status\":\"SUBMITTED\"},{\"id\":\"17912298-2\",\"mobile\":\"919927513645\",\"status\":\"SUBMITTED\"}],\"msgid\":\"818817912298937\",\"message\":\"message Submitted successfully\"}"; //client.DownloadString(url);//"Message GID=8456146407 ID=8456146407-1, 8456146407-2"; //
                    var tempRes = Newtonsoft.Json.JsonConvert.DeserializeObject<SMS_ResponseModel>(content);

                    for (int i = 0; i < uniquefilteredStud.Count; i++)
                        uniquefilteredStud = uniquefilteredStud.Select(c => { c.SubGuidId = tempRes.data.Where(r => r.mobile == "91" + c.SMSNo).FirstOrDefault() == null ? "" : tempRes.data.Where(r => r.mobile == "91" + c.SMSNo).FirstOrDefault().id; return c; }).ToList();

                    for (int i = 0; i < duplicatefilteredStud.Count; i++)
                        duplicatefilteredStud = duplicatefilteredStud.Select(c => { c.SubGuidId = tempRes.data.Where(r => r.mobile == "91" + c.SMSNo).FirstOrDefault() == null ? "" : tempRes.data.Where(r => r.mobile == "91" + c.SMSNo).FirstOrDefault().id; return c; }).ToList();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SubGUID");
                    dt.Columns.Add("MoblieNo");
                    dt.Columns.Add("ERPNo");
                    dt.Columns.Add("DStatus");
                    dt.Columns.Add("DeliverTime");
                    dt.Columns.Add("status_desc");

                    for (int x = 0; x < uniquefilteredStud.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = uniquefilteredStud[x].SubGuidId;
                        row[1] = uniquefilteredStud[x].SMSNo;
                        row[2] = uniquefilteredStud[x].ERP;
                        row[3] = null;
                        row[4] = null;
                        row[5] = null;
                        dt.Rows.Add(row);
                    }
                    for (int x = 0; x < duplicatefilteredStud.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = duplicatefilteredStud[x].SubGuidId;
                        row[1] = duplicatefilteredStud[x].SMSNo;
                        row[2] = duplicatefilteredStud[x].ERP;
                        row[3] = null;
                        row[4] = null;
                        row[5] = null;
                        dt.Rows.Add(row);
                    }

                    SaveSendSMSReport(tempRes.msgid, Msg, false, MsgType, dt, "CRUD");

                    return new Tuple<int, string>(1, "SMS Send Successfully..!");
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SubGUID");
                    dt.Columns.Add("MoblieNo");
                    dt.Columns.Add("ERPNo");

                    dt.Columns.Add("DStatus");
                    dt.Columns.Add("DeliverTime");
                    dt.Columns.Add("status_desc");

                    for (int x = 0; x < uniquefilteredStud.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = uniquefilteredStud[x].SubGuidId;
                        row[1] = uniquefilteredStud[x].SMSNo;
                        row[2] = uniquefilteredStud[x].ERP;

                        row[3] = null;
                        row[4] = null;
                        row[5] = null;
                        dt.Rows.Add(row);
                    }
                    for (int x = 0; x < duplicatefilteredStud.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = duplicatefilteredStud[x].SubGuidId;
                        row[1] = duplicatefilteredStud[x].SMSNo;
                        row[2] = duplicatefilteredStud[x].ERP;

                        row[3] = null;
                        row[4] = null;
                        row[5] = null;
                        dt.Rows.Add(row);
                    }
                    SaveSendSMSReport(null, Msg, false, MsgType, dt, "CRUD");

                    return new Tuple<int, string>(1, "SMS Send Successfully..!");
                }
            }
            catch
            {
                return new Tuple<int, string>(0, "Failed to send sms..!");
            }
        }
        public Tuple<int, string> SendSMS(string Msg, string MsgType, List<SMS_AdminAndStaffModel> filteredStaff)
        {
            try
            {
                if (MsgType == "BOTH" || MsgType == "SMS")
                {
                    string url = "";
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("sp_Fee_Defaulter_Transaction", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        url = ds.Tables[1].Rows[0][0].ToString();
                        url = url.Replace("[no]", string.Join(",", filteredStaff.Select(r => r.Mobile).ToList()));
                        url = url.Replace("[msg]", Msg);
                    }

                    var client = new WebClient();
                    var content = client.DownloadString(url);  //"{\"status\":\"OK\",\"data\":[{\"id\":\"17912298-1\",\"mobile\":\"919999306536\",\"status\":\"SUBMITTED\"},{\"id\":\"17912298-2\",\"mobile\":\"919927513645\",\"status\":\"SUBMITTED\"}],\"msgid\":\"818817912298937\",\"message\":\"message Submitted successfully\"}"; //client.DownloadString(url);//"Message GID=8456146407 ID=8456146407-1, 8456146407-2"; //
                    var tempRes = Newtonsoft.Json.JsonConvert.DeserializeObject<SMS_ResponseModel>(content);

                    for (int i = 0; i < filteredStaff.Count; i++)
                        filteredStaff = filteredStaff.Select(c => { c.SubGuidId = tempRes.data.Where(r => r.mobile == "91" + c.Mobile).FirstOrDefault() == null ? "" : tempRes.data.Where(r => r.mobile == "91" + c.Mobile).FirstOrDefault().id; return c; }).ToList();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SubGUID");
                    dt.Columns.Add("MoblieNo");
                    dt.Columns.Add("ERPNo");

                    dt.Columns.Add("DStatus");
                    dt.Columns.Add("DeliverTime");
                    dt.Columns.Add("status_desc");

                    for (int x = 0; x < filteredStaff.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = filteredStaff[x].SubGuidId;
                        row[1] = filteredStaff[x].Mobile;
                        row[2] = filteredStaff[x].StaffID;

                        row[3] = null;
                        row[4] = null;
                        row[5] = null;

                        dt.Rows.Add(row);
                    }

                    SaveSendSMSReport(tempRes.msgid, Msg, true, MsgType, dt, "CRUD");
                    return new Tuple<int, string>(1, "SMS Send Successfully..!");
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SubGUID");
                    dt.Columns.Add("MoblieNo");
                    dt.Columns.Add("ERPNo");

                    dt.Columns.Add("DStatus");
                    dt.Columns.Add("DeliverTime");
                    dt.Columns.Add("status_desc");

                    for (int x = 0; x < filteredStaff.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = filteredStaff[x].SubGuidId;
                        row[1] = filteredStaff[x].Mobile;
                        row[2] = filteredStaff[x].StaffID;


                        row[3] = null;
                        row[4] = null;
                        row[5] = null;

                        dt.Rows.Add(row);
                    }

                    SaveSendSMSReport(null, Msg, true, MsgType, dt, "CRUD");

                    return new Tuple<int, string>(1, "SMS Send Successfully..!");
                }
            }
            catch
            {
                return new Tuple<int, string>(0, "Failed to send sms..!");
            }
        }
        public Tuple<int, string> ReSendSMS(string GID, List<SMS_ReportDetailModel> filteredReference, List<SMS_ReportDetailModel> duplicatefilteredReference)
        {
            try
            {
                string Msg = "";
                string MsgType = "BOTH";
                bool IsSendForStaff = false;
                string url = "";
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("sp_Fee_Defaulter_Transaction", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GID", GID);
                    con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                    url = ds.Tables[1].Rows[0][0].ToString();
                    Msg = ds.Tables[1].Rows[0][1].ToString();
                    IsSendForStaff = Convert.ToBoolean(ds.Tables[1].Rows[0][2].ToString());
                    url = url.Replace("[no]", string.Join(",", filteredReference.Select(r => r.MobileNo).ToList()));
                    url = url.Replace("[msg]", Msg);
                    
                    }

                    var client = new WebClient();
                    var content = client.DownloadString(url);  //"{\"status\":\"OK\",\"data\":[{\"id\":\"17912298-1\",\"mobile\":\"919999306536\",\"status\":\"SUBMITTED\"},{\"id\":\"17912298-2\",\"mobile\":\"919927513645\",\"status\":\"SUBMITTED\"}],\"msgid\":\"818817912298937\",\"message\":\"message Submitted successfully\"}"; //client.DownloadString(url);//"Message GID=8456146407 ID=8456146407-1, 8456146407-2"; //
                    var tempRes = Newtonsoft.Json.JsonConvert.DeserializeObject<SMS_ResponseModel>(content);

                    for (int i = 0; i < filteredReference.Count; i++)
                    filteredReference = filteredReference.Select(c => { c.Sub_GID = tempRes.data.Where(r => r.mobile == "91" + c.MobileNo).FirstOrDefault() == null ? "" : tempRes.data.Where(r => r.mobile == "91" + c.MobileNo).FirstOrDefault().id; return c; }).ToList();

                    for (int i = 0; i < duplicatefilteredReference.Count; i++)
                    duplicatefilteredReference = duplicatefilteredReference.Select(c => { c.Sub_GID = tempRes.data.Where(r => r.mobile == "91" + c.MobileNo).FirstOrDefault() == null ? "" : tempRes.data.Where(r => r.mobile == "91" + c.MobileNo).FirstOrDefault().id; return c; }).ToList();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SubGUID");
                    dt.Columns.Add("MoblieNo");
                    dt.Columns.Add("ERPNo");
                    dt.Columns.Add("DStatus");
                    dt.Columns.Add("DeliverTime");
                    dt.Columns.Add("status_desc");

                    for (int x = 0; x < filteredReference.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = filteredReference[x].Sub_GID;
                        row[1] = filteredReference[x].MobileNo;
                        row[2] = filteredReference[x].ERPNo;
                        row[3] = null;
                        row[4] = null;
                        row[5] = null;
                        dt.Rows.Add(row);
                    }
                    for (int x = 0; x < duplicatefilteredReference.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = duplicatefilteredReference[x].Sub_GID;
                        row[1] = duplicatefilteredReference[x].MobileNo;
                        row[2] = duplicatefilteredReference[x].ERPNo;
                        row[3] = null;
                        row[4] = null;
                        row[5] = null;
                        dt.Rows.Add(row);
                    }

                    SaveSendSMSReport(tempRes.msgid, Msg, IsSendForStaff, MsgType, dt, "CRUD");

                    return new Tuple<int, string>(1, "SMS Send Successfully..!");
               
            }
            catch
            {
                return new Tuple<int, string>(0, "Failed to send sms..!");
            }
        }

        //public Tuple<int, string> ReSendSMS(string GID, List<SMS_ReportDetailModel> filteredReference)
        //{
        //    try
        //    {
        //        string Msg = "";
        //        string MsgType = "BOTH";
        //        string url = "";
        //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_Fee_Defaulter_Transaction", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@GID",GID);
        //            con.Open();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();
        //            da.Fill(ds);
        //            url = ds.Tables[1].Rows[0][0].ToString();
        //            Msg = ds.Tables[1].Rows[0][1].ToString();
        //            url = url.Replace("[no]", string.Join(",", filteredReference.Select(r => r.MobileNo).ToList()));
        //            url = url.Replace("[msg]", Msg);
        //        }

        //        var client = new WebClient();
        //        var content = client.DownloadString(url);  //"{\"status\":\"OK\",\"data\":[{\"id\":\"17912298-1\",\"mobile\":\"919999306536\",\"status\":\"SUBMITTED\"},{\"id\":\"17912298-2\",\"mobile\":\"919927513645\",\"status\":\"SUBMITTED\"}],\"msgid\":\"818817912298937\",\"message\":\"message Submitted successfully\"}"; //client.DownloadString(url);//"Message GID=8456146407 ID=8456146407-1, 8456146407-2"; //
        //        var tempRes = Newtonsoft.Json.JsonConvert.DeserializeObject<SMS_ResponseModel>(content);

        //        for (int i = 0; i < filteredReference.Count; i++)
        //            filteredReference = filteredReference.Select(c => { c.Sub_GID = tempRes.data.Where(r => r.mobile == "91" + c.MobileNo).FirstOrDefault() == null ? "" : tempRes.data.Where(r => r.mobile == "91" + c.MobileNo).FirstOrDefault().id; return c; }).ToList();

        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("SubGUID");
        //        dt.Columns.Add("MoblieNo");
        //        dt.Columns.Add("ReferenceNo");
        //        dt.Columns.Add("DStatus");
        //        dt.Columns.Add("DeliverTime");
        //        dt.Columns.Add("status_desc");

        //        for (int x = 0; x < filteredReference.Count; x++)
        //        {
        //            DataRow row = dt.NewRow();
        //            row[0] = filteredReference[x].Sub_GID;
        //            row[1] = filteredReference[x].MobileNo;
        //            row[2] = filteredReference[x].ERPNo;

        //            row[3] = null;
        //            row[4] = null;
        //            row[5] = null;

        //            dt.Rows.Add(row);



        //        }
        //        SaveSendSMSReport(tempRes.msgid, Msg, true, MsgType, dt, "CRUD");
        //        return new Tuple<int, string>(1, "SMS Send Successfully..!");
        //    }
        //    catch
        //    {
        //        return new Tuple<int, string>(0, "Failed to send sms..!");
        //    }
        //}

        public Tuple<int, string> SendSMS(string Msg, string MsgType, string ERPNo, string SMSNo)
        {
            try
            {
                if (MsgType == "BOTH" || MsgType == "SMS")
                {
                    string url = "";
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("sp_Fee_Defaulter_Transaction", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        url = ds.Tables[1].Rows[0][0].ToString();
                        url = url.Replace("[no]", SMSNo);
                        url = url.Replace("[msg]", Msg);
                    }

                    var client = new WebClient();
                    var content = client.DownloadString(url); //"{\"status\":\"OK\",\"data\":[{\"id\":\"17912298-1\",\"mobile\":\"919999306536\",\"status\":\"SUBMITTED\"},{\"id\":\"17912298-2\",\"mobile\":\"919927513645\",\"status\":\"SUBMITTED\"}],\"msgid\":\"818817912298937\",\"message\":\"message Submitted successfully\"}"; //client.DownloadString(url);//"Message GID=8456146407 ID=8456146407-1, 8456146407-2"; //
                    var tempRes = Newtonsoft.Json.JsonConvert.DeserializeObject<SMS_ResponseModel>(content);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SubGUID");
                    dt.Columns.Add("MoblieNo");
                    dt.Columns.Add("ERPNo");
                    dt.Columns.Add("DStatus");
                    dt.Columns.Add("DeliverTime");
                    dt.Columns.Add("status_desc");
                    DataRow row = dt.NewRow();
                    row[0] = tempRes.data.Count > 0 ? tempRes.data[0].id : "";
                    row[1] = SMSNo;
                    row[2] = ERPNo;
                    row[3] = null;
                    row[4] = null;
                    row[5] = null;
                    dt.Rows.Add(row);

                    SaveSendSMSReport(tempRes.msgid, Msg, false, MsgType, dt, "CRUD");

                    return new Tuple<int, string>(1, "SMS Send Successfully..!");
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SubGUID");
                    dt.Columns.Add("MoblieNo");
                    dt.Columns.Add("ERPNo");
                    dt.Columns.Add("DStatus");
                    dt.Columns.Add("DeliverTime");
                    dt.Columns.Add("status_desc");
                    DataRow row = dt.NewRow();
                    row[0] = "";
                    row[1] = SMSNo;
                    row[2] = ERPNo;
                    row[3] = null;
                    row[4] = null;
                    row[5] = null;
                    dt.Rows.Add(row);

                    SaveSendSMSReport(null, Msg, false, MsgType, dt, "CRUD");
                    return new Tuple<int, string>(1, "SMS Send Successfully..!");
                }
            }
            catch
            {
                return new Tuple<int, string>(0, "Failed to send sms..!");
            }
        }
        public static void SaveSendSMSReport(string guidId, string Msg, bool IsSendForStaff, string MsgType, DataTable dt, string mode) //string subGuidIds, string SMSNos, string ErpNosOrStaffCodes, // string duplicateSMSNos, string duplicateSMSCorrespondERPNo, 
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_Transaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GID", guidId);
                //cmd.Parameters.AddWithValue("@Sub_GID", subGuidIds);
                //cmd.Parameters.AddWithValue("@MobileNo", SMSNos);
                //cmd.Parameters.AddWithValue("@ERPNo", ErpNosOrStaffCodes);
                cmd.Parameters.AddWithValue("@MessegeSend", Msg);
                cmd.Parameters.AddWithValue("@IsSendForStaff", IsSendForStaff);
                //cmd.Parameters.AddWithValue("@DuplicateSMSNos", duplicateSMSNos);
                //cmd.Parameters.AddWithValue("@DuplicateERPNos", duplicateSMSCorrespondERPNo);
                cmd.Parameters.AddWithValue("@Mode", mode);
                cmd.Parameters.AddWithValue("@MessageType", MsgType);
                cmd.Parameters.AddWithValue("@DT", dt);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        public void ExceptionLoggingToDataBase(ExceptionLogger model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ExceptionLoggingToDataBase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExceptionMsg", model.ExceptionMsg);
                cmd.Parameters.AddWithValue("@ControllerName", model.ControllerName);
                cmd.Parameters.AddWithValue("@ActionName", model.ActionName);
                cmd.Parameters.AddWithValue("@ExceptionStackTrace", model.ExceptionStackTrace);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public bool CHECKTO_UserAuthorization(string ControllerName, string RequestFor, string UserID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_School_UserAuthorization", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UesrId", UserID);
                cmd.Parameters.AddWithValue("@Controller", ControllerName);
                cmd.Parameters.AddWithValue("@Action", RequestFor);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                try
                {
                    return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                }
                catch
                {
                    return false;
                }
            }
        }

        public ReportHeaderEntities ReportHeaderDetails(string ReportName)
        {
            ReportHeaderEntities model = new ReportHeaderEntities();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * from [dbo].[GetReportHeaders] ()", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                model.ReportName = ReportName;
                model.Header1 = ds.Tables[0].Rows[0][0].ToString();
                model.Header2 = ds.Tables[0].Rows[0][1].ToString();
                model.Header3 = ds.Tables[0].Rows[0][2].ToString();
                model.Header4 = ds.Tables[0].Rows[0][3].ToString();
                model.LogoURL = ds.Tables[0].Rows[0][4].ToString();
            }
            return model;
        }


        public Tuple<int, string> AlertTransaction_CRUD(AlertTransactionModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_AlertTransaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AlertNo", model.AlertNo);
                cmd.Parameters.AddWithValue("@AText", model.AText);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@AlertForUser", model.AlertForUser);
                cmd.Parameters.AddWithValue("@MarkRead", model.MarkRead);
                cmd.Parameters.AddWithValue("@IsCancelled", model.IsCancelled);
                cmd.Parameters.AddWithValue("@Mode", model.Mode);
                cmd.Parameters.AddWithValue("@AlertFor", model.AlertFor);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public List<AlertTransactionModel> GetNotificationList(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_School_Alert_GetUserAlertList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0].AsEnumerable().Select(r => new AlertTransactionModel
                {
                    AText = r.Field<string>("AText"),
                    AlertNo = r.Field<int>("AlertNo"),
                    Temp_HTMLClassName = r.Field<string>("HTMLClassName"),
                    ATitle = r.Field<string>("ATitle"),
                    MarkRead = r.Field<bool>("MarkRead"),
                }).ToList();
            }
        }
        public int GetNotificationCount(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * from sp_School_Alert_AlertCounter(" + UserId + ")", con);
                cmd.CommandType = CommandType.Text;

                // cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);

                return 0;
            }
        }


        public bool CheckIsUniqueNo(string CheckFiledname, string ReferenceNo, string Id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_CheckUniqueNo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CheckFiledname", CheckFiledname);
                cmd.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                cmd.Parameters.AddWithValue("@Id", Id);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                    return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);

                return false;
            }
        }

        public List<Basic_StudentInfoModel> Basic_StudentInformation_ByMode(
            string Mode, string SessionId, string Param1, string Param2, string Param3, string Param4, string Param5)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_CommonOjbect_GetClassStudentList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", Mode);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@Param1", Param1);
                cmd.Parameters.AddWithValue("@Param2", Param2);
                cmd.Parameters.AddWithValue("@Param3", Param3);
                cmd.Parameters.AddWithValue("@Param4", Param4);
                cmd.Parameters.AddWithValue("@Param5", Param5);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0].AsEnumerable().Select(r => new Basic_StudentInfoModel
                {
                    Address = r.Field<string>("Address"),
                    AdmNo = r.Field<string>("AdmNo"),
                    AlternateNumber = r.Field<string>("AlternateNumber"),
                    Class = r.Field<string>("Class"),
                    DOA = r.Field<string>("DOA"),
                    DOB = r.Field<string>("DOB"),
                    ERP = r.Field<string>("ERP"),
                    Father = r.Field<string>("Father"),
                    FMobileNo = r.Field<string>("FMobileNo"),
                    Gender = r.Field<string>("Gender"),
                    MMobileNo = r.Field<string>("MMobileNo"),
                    Mother = r.Field<string>("Mother"),
                    OnlyClassName = r.Field<string>("OnlyClassName"),
                    RollNo = r.Field<int>("RollNo"),
                    SMSNo = r.Field<string>("SMSNo"),
                    StructName = r.Field<string>("StructName"),
                    Student = r.Field<string>("Student"),
                }).ToList();
            }
        }

        //

        public DataSet Get_ReportConfiguration(string Module, string RefNo)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Report_GetCrystalReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Module", Module);
                cmd.Parameters.AddWithValue("@ReferenceNo", RefNo);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public ClinetInfoEntities get_ClientInfo()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * from GetClientInfo()", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0].AsEnumerable().Select(r => new ClinetInfoEntities
                {
                    Comp_Address = r.Field<string>("Comp_Address"),
                    Comp_EmailId = r.Field<string>("Comp_EmailId"),
                    Comp_Id = r.Field<int>("Comp_Id"),
                    Comp_MobileNo = r.Field<string>("Comp_MobileNo"),
                    Comp_Name = r.Field<string>("Comp_Name"),
                    Comp_SalesEmailId = r.Field<string>("Comp_SalesEmailId"),
                    Comp_SalesMobileNo = r.Field<string>("Comp_SalesMobileNo"),
                    Comp_Support = r.Field<string>("Comp_Support"),
                    CopyRightName = r.Field<string>("CopyRightName"),
                    CopyRightYear = r.Field<string>("CopyRightYear"),
                    Comp_WebLink = r.Field<string>("Comp_WebLink"),
                }).FirstOrDefault();
            }
        }
    }
}
