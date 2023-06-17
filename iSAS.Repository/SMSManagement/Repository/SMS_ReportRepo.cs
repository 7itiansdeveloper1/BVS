using ISas.Entities.SMSManagement;
using ISas.Repository.SMSManagement.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ISas.Repository.SMSManagement.Repository
{
    public class SMS_ReportRepo : ISMS_ReportRepo
    {
        public List<SMS_ReportMainModel> GetSMSDeliveryReport(string Mode, string Date, string SessionId)
        {
            List<SMS_ReportMainModel> deliveryReportList = new List<SMS_ReportMainModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_DeliveryReport", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", Mode);
                cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date).Date);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                deliveryReportList = ds.Tables[0].AsEnumerable().Select(r => new SMS_ReportMainModel
                {
                    GID = r.Field<string>("GID"),
                    MessegeSend = r.Field<string>("MessegeSend"),
                    SendTime = r.Field<DateTime>("SendTime"),
                    SNo = r.Field<long>("SNo"),
                    Classes = r.Field<string>("Classes"),
                    Count = r.Field<int>("Count"),
                }).ToList();
            }
            return deliveryReportList;
        }
        public List<SelectListItem> GetSMSDeliveryCount(string GID, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_DeliveryReport", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", "MessageDeliveryCount");
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@GID", GID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("STATUS"),
                    Value = r.Field<int>("Count").ToString(),
                }).ToList();
            }
        }

        #region Old Code to get SMS Report
        //public List<SMS_ReportDetailModel> GetSMSDeliveryReportDetails(string Mode, string GID, string DStatus)
        //{
        //    List<SMS_ReportDetailModel> deliveryReportDetailsList = new List<SMS_ReportDetailModel>();
        //    DataSet ds = new DataSet();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_SMS_DeliveryReport", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@Mode", Mode);
        //        cmd.Parameters.AddWithValue("@GID", GID);
        //        cmd.Parameters.AddWithValue("@DStatus", DStatus);

        //        //cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date).Date);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        con.Close();

        //        deliveryReportDetailsList = ds.Tables[0].AsEnumerable().Select(r => new SMS_ReportDetailModel
        //        {
        //            GID = r.Field<string>("GID"),
        //            Class = r.Field<string>("Class"),
        //            DisplaySendTime = r.Field<DateTime?>("SendTime"),
        //            DisplayDeliverTime = r.Field<DateTime?>("DeliverTime"),
        //            MobileNo = r.Field<string>("MobileNo"),
        //            Status = r.Field<string>("Status"),
        //            Student = r.Field<string>("Student"),
        //            Sub_GID = r.Field<string>("Sub_GID"),
        //            ERPNo = r.Field<string>("ERPNo"),
        //        }).ToList();

        //        var client = new WebClient();
        //        string key = ConfigurationManager.AppSettings["SMSReportKeyBasedOnIP"];
        //        try
        //        {
        //            for (int i = 0; i < deliveryReportDetailsList.Count; i++)
        //            {
        //                if (deliveryReportDetailsList[i].Status != "DELIVRD")
        //                {
        //                    var content = client.DownloadString("https://api-alerts.solutionsinfini.com/v3/?method=sms.status&api_key=" + key + "&format=xml&id=" + deliveryReportDetailsList[i].Sub_GID + "");
        //                    deliveryReportDetailsList[i].Status = content.Between("</mobile><status>", "</status><senttime>");
        //                    deliveryReportDetailsList[i].SendTime = content.Between("<senttime>", "</senttime>");
        //                    deliveryReportDetailsList[i].DeliverTime = content.Between("<dlrtime>", "</dlrtime>");
        //                    deliveryReportDetailsList[i].IsStatusFromAPI = true;
        //                }
        //            }

        //            if (deliveryReportDetailsList.Where(r => r.IsStatusFromAPI && !string.IsNullOrEmpty(r.Status)).Count() > 0)
        //            {
        //                using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //                {
        //                    string subGIDs = string.Join(",", deliveryReportDetailsList.Where(r => r.IsStatusFromAPI && !string.IsNullOrEmpty(r.Status)).Select(r => r.Sub_GID).ToList());
        //                    string MobileNos = string.Join(",", deliveryReportDetailsList.Where(r => r.IsStatusFromAPI && !string.IsNullOrEmpty(r.Status)).Select(r => r.MobileNo).ToList());
        //                    string ERPNos = string.Join(",", deliveryReportDetailsList.Where(r => r.IsStatusFromAPI && !string.IsNullOrEmpty(r.Status)).Select(r => r.ERPNo).ToList());
        //                    string status = string.Join(",", deliveryReportDetailsList.Where(r => r.IsStatusFromAPI && !string.IsNullOrEmpty(r.Status)).Select(r => r.Status).ToList());
        //                    string sentTime = string.Join(",", deliveryReportDetailsList.Where(r => r.IsStatusFromAPI && !string.IsNullOrEmpty(r.Status)).Select(r => r.SendTime).ToList());
        //                    string deliverTime = string.Join(",", deliveryReportDetailsList.Where(r => r.IsStatusFromAPI && !string.IsNullOrEmpty(r.Status)).Select(r => r.DeliverTime).ToList());

        //                    con1.Open();
        //                    SqlCommand cmd1 = new SqlCommand("sp_SMS_Transaction_CRUD", con1);
        //                    cmd1.CommandType = CommandType.StoredProcedure;

        //                    cmd1.Parameters.AddWithValue("@GID", GID);
        //                    cmd1.Parameters.AddWithValue("@Sub_GID", subGIDs);
        //                    cmd1.Parameters.AddWithValue("@MobileNo", MobileNos);
        //                    cmd1.Parameters.AddWithValue("@ERPNo", ERPNos);
        //                    cmd1.Parameters.AddWithValue("@DStatus", status);
        //                    cmd1.Parameters.AddWithValue("@SentTime", sentTime);
        //                    cmd1.Parameters.AddWithValue("@DeliverTime", deliverTime);
        //                    cmd1.Parameters.AddWithValue("@MessegeSend", "");
        //                    cmd1.Parameters.AddWithValue("@IsSendForStaff", false);
        //                    cmd1.Parameters.AddWithValue("@Mode", "STATUSUPDATE");

        //                    cmd1.ExecuteNonQuery();
        //                    con1.Close();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return deliveryReportDetailsList;
        //        }
        //    }
        //    return deliveryReportDetailsList;
        //}
        #endregion
        public List<SMS_ReportDetailModel> GetSMSDeliveryReportDetails(string Mode, string GID, string DStatus)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<SMS_ReportDetailModel> deliveryReportDetailsList = new List<SMS_ReportDetailModel>();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_DeliveryReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", Mode);
                cmd.Parameters.AddWithValue("@GID", GID);
                cmd.Parameters.AddWithValue("@DStatus", DStatus);
                //cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date).Date);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                deliveryReportDetailsList = ds.Tables[0].AsEnumerable().Select(r => new SMS_ReportDetailModel
                {
                    GID = r.Field<string>("GID"),
                    Class = r.Field<string>("Class"),
                    SendTime = r.Field<DateTime?>("SendTime"),
                    DeliverTime = r.Field<DateTime?>("DeliverTime"),
                    MobileNo = r.Field<string>("MobileNo"),
                    Status = r.Field<string>("Status"),
                    Student = r.Field<string>("Student"),
                    Sub_GID = r.Field<string>("Sub_GID"),
                    ERPNo = r.Field<string>("ERPNo"),
                    status_desc = r.Field<string>("status_desc"),
                }).ToList();
                try
                {
                    string undeliveredSubGuids = string.Join(",", deliveryReportDetailsList.Where(r => r.Status != "Delivered").Select(r => r.Sub_GID).ToList());
                    string url = "http://buzzify.in/V2/http-dlr.php?apikey=vYukNWvhUsRfc8eD&msgid=" + undeliveredSubGuids + "&format=json";
                    var client = new WebClient();
                    var content = client.DownloadString(url);
                    var tempRes = Newtonsoft.Json.JsonConvert.DeserializeObject<SMS_ResponseModel>(content);
                    deliveryReportDetailsList = deliveryReportDetailsList.Select(r => { r.IsStatusFromAPI = true; r.Status = tempRes.data.Where(x => x.id == r.Sub_GID).FirstOrDefault().status; r.status_desc = tempRes.data.Where(x => x.id == r.Sub_GID).FirstOrDefault().status_desc; r.DeliverTime = tempRes.data.Where(x => x.id == r.Sub_GID).FirstOrDefault().delivered_date; return r; }).ToList();



                    List<SMS_ReportDetailModel> newStatusList = deliveryReportDetailsList.Where(r => r.IsStatusFromAPI && !string.IsNullOrEmpty(r.Status)).ToList();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SubGUID");
                    dt.Columns.Add("MoblieNo");
                    dt.Columns.Add("ERPNo");

                    dt.Columns.Add("DStatus");
                    dt.Columns.Add("DeliverTime");
                    dt.Columns.Add("status_desc");
                    for (int x = 0; x < newStatusList.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = newStatusList[x].Sub_GID;
                        row[1] = newStatusList[x].MobileNo;
                        row[2] = newStatusList[x].ERPNo;

                        row[3] = newStatusList[x].Status;
                        row[4] = newStatusList[x].DeliverTime;
                        row[5] = newStatusList[x].status_desc;
                        dt.Rows.Add(row);
                    }
                    Implementation.CommonRepo.SaveSendSMSReport(GID, null, false, null, dt, "STATUSUPDATE");
                }
                catch (Exception ex)
                {
                    return deliveryReportDetailsList;
                }
                return deliveryReportDetailsList;
            }
        }

        //public SMS_ReportDetailFilterModel GetSMSDeliveryReportDetails(string Mode, string GID, string DStatus)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        List<SMS_ReportDetailModel> deliveryReportDetailsList = new List<SMS_ReportDetailModel>();
        //        SMS_ReportDetailFilterModel deliveryReportDetailsModel = new SMS_ReportDetailFilterModel();
        //        DataSet ds = new DataSet();
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_SMS_DeliveryReport", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Mode", Mode);
        //        cmd.Parameters.AddWithValue("@GID", GID);
        //        cmd.Parameters.AddWithValue("@DStatus", DStatus);
        //        //cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date).Date);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        con.Close();

        //        deliveryReportDetailsModel.referenceList = ds.Tables[0].AsEnumerable().Select(r => new SMS_ReportDetailModel
        //        {
        //            GID = r.Field<string>("GID"),
        //            Class = r.Field<string>("Class"),
        //            SendTime = r.Field<DateTime?>("SendTime"),
        //            DeliverTime = r.Field<DateTime?>("DeliverTime"),
        //            MobileNo = r.Field<string>("MobileNo"),
        //            Status = r.Field<string>("Status"),
        //            Student = r.Field<string>("Student"),
        //            Sub_GID = r.Field<string>("Sub_GID"),
        //            ERPNo = r.Field<string>("ERPNo"),
        //            status_desc = r.Field<string>("status_desc"),
        //        }).ToList();

        //        deliveryReportDetailsList = ds.Tables[0].AsEnumerable().Select(r => new SMS_ReportDetailModel
        //        {
        //            GID = r.Field<string>("GID"),
        //            Class = r.Field<string>("Class"),
        //            SendTime = r.Field<DateTime?>("SendTime"),
        //            DeliverTime = r.Field<DateTime?>("DeliverTime"),
        //            MobileNo = r.Field<string>("MobileNo"),
        //            Status = r.Field<string>("Status"),
        //            Student = r.Field<string>("Student"),
        //            Sub_GID = r.Field<string>("Sub_GID"),
        //            ERPNo = r.Field<string>("ERPNo"),
        //            status_desc = r.Field<string>("status_desc"),
        //        }).ToList();
        //        try
        //        {
        //            string undeliveredSubGuids = string.Join(",", deliveryReportDetailsList.Where(r => r.Status != "Delivered").Select(r => r.Sub_GID).ToList());
        //            string url = "http://buzzify.in/V2/http-dlr.php?apikey=vYukNWvhUsRfc8eD&msgid=" + undeliveredSubGuids + "&format=json";
        //            var client = new WebClient();
        //            var content = client.DownloadString(url);
        //            var tempRes = Newtonsoft.Json.JsonConvert.DeserializeObject<SMS_ResponseModel>(content);
        //            deliveryReportDetailsList = deliveryReportDetailsList.Select(r => { r.IsStatusFromAPI = true; r.Status = tempRes.data.Where(x => x.id == r.Sub_GID).FirstOrDefault().status; r.status_desc = tempRes.data.Where(x => x.id == r.Sub_GID).FirstOrDefault().status_desc; r.DeliverTime = tempRes.data.Where(x => x.id == r.Sub_GID).FirstOrDefault().delivered_date; return r; }).ToList();
        //            List<SMS_ReportDetailModel> newStatusList = deliveryReportDetailsList.Where(r => r.IsStatusFromAPI && !string.IsNullOrEmpty(r.Status)).ToList();
        //            DataTable dt = new DataTable();
        //            dt.Columns.Add("SubGUID");
        //            dt.Columns.Add("MoblieNo");
        //            dt.Columns.Add("ERPNo");

        //            dt.Columns.Add("DStatus");
        //            dt.Columns.Add("DeliverTime");
        //            dt.Columns.Add("status_desc");
        //            for (int x = 0; x < newStatusList.Count; x++)
        //            {
        //                DataRow row = dt.NewRow();
        //                row[0] = newStatusList[x].Sub_GID;
        //                row[1] = newStatusList[x].MobileNo;
        //                row[2] = newStatusList[x].ERPNo;

        //                row[3] = newStatusList[x].Status;
        //                row[4] = newStatusList[x].DeliverTime;
        //                row[5] = newStatusList[x].status_desc;
        //                dt.Rows.Add(row);
        //            }
        //            Implementation.CommonRepo.SaveSendSMSReport(GID, null, false, null, dt, "STATUSUPDATE");
        //        }
        //        catch (Exception ex)
        //        {
        //            return deliveryReportDetailsModel;
        //        }
        //        return deliveryReportDetailsModel;
        //    }
        //}
        public Tuple<int, string> SMS_Resend(string  GID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_SMS_Resend", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GID", GID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
