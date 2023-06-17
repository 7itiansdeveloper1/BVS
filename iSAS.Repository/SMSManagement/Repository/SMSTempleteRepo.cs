using ISas.Entities.SMSManagement;
using ISas.Repository.SMSManagement.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.SMSManagement.Repository
{
    public class SMSTempleteRepo : ISMSTempleteRepo
    {
        public List<SelectListItem> GetTempleteTypeList()
        {
            List<SelectListItem> dropDownMaster = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_MessageTemplateMaster_FormLoad", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                dropDownMaster = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("TemplateName"),
                    Value = r.Field<string>("TemplateId"),
                    Selected = r.Field<bool>("Default"),
                }).ToList();
            }
            return dropDownMaster;
        }
        public IEnumerable<SMSTempleteModel> GetAllSMSTempleteList(string SMSID)
        {
            List<SMSTempleteModel> ListOfStaffDetails = new List<SMSTempleteModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_MessageTemplateMaster_LandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SMSID", SMSID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                ListOfStaffDetails = ds.Tables[0].AsEnumerable().Select(r => new SMSTempleteModel
                {
                    Active = r.Field<bool>("Active"),
                    SMSID = r.Field<string>("SMSID"),
                    SmsText = r.Field<string>("SmsText"),
                    TemplateID = r.Field<string>("TemplateID"),
                    TemplateName = r.Field<string>("TemplateName"),
                }).ToList();
            }
            return ListOfStaffDetails;
        }
        public SMSTempleteModel GetSMSTempleteBySMSID(string SMSID)
        {
            return GetAllSMSTempleteList(SMSID).FirstOrDefault();
        }
        public Tuple<int, string> SMSTemplete_CRUD(SMSTempleteModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_MessageTemplateMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SMSText", model.SmsText);
                cmd.Parameters.AddWithValue("@TemplateId", model.TemplateID);
                cmd.Parameters.AddWithValue("@Function", model.Function);
                cmd.Parameters.AddWithValue("@SMSID", model.SMSID);
                cmd.Parameters.AddWithValue("@UserId", model.CreatedBy);
                cmd.Parameters.AddWithValue("@IsActive", Convert.ToInt32(model.Active));


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }



        public List<UnDeliveredSMSModel> GetUnDeliveredSMSList(string UserId)
        {
            List<UnDeliveredSMSModel> ListOfUnDeliveredSMS = new List<UnDeliveredSMSModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_UndeliveredSMS_TeacherLandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                ListOfUnDeliveredSMS = ds.Tables[0].AsEnumerable().Select(r => new UnDeliveredSMSModel
                {
                    FullMessage = r.Field<string>("FullMessage"),
                    MessageId = r.Field<string>("MessageId"),
                    ReportName = r.Field<string>("ReportName"),
                    ShortSMS = r.Field<string>("ShortSMS"),

                }).ToList();
            }
            return ListOfUnDeliveredSMS;
        }



        public List<UnDeliveredSMSDetailsModel> GetUnDeliveredSMSDetailList_ForTeacher(string MessegeId, string UserId)
        {
            List<UnDeliveredSMSDetailsModel> ListOfUnDeliveredSMSDetails = new List<UnDeliveredSMSDetailsModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_UndeliveredSMS_TeacherLandingPage_GetReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@MessageId", MessegeId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                ListOfUnDeliveredSMSDetails = ds.Tables[0].AsEnumerable().Select(r => new UnDeliveredSMSDetailsModel
                {
                    Class = r.Field<string>("Class"),
                    ERPNo = r.Field<string>("ERPNo"),
                    Father = r.Field<string>("Father"),
                    Mobile = r.Field<string>("Mobile"),
                    Mother = r.Field<string>("Mother"),
                    Sno = r.Field<Int64>("Sno"),
                    Student = r.Field<string>("Student"),
                }).ToList();
            }
            return ListOfUnDeliveredSMSDetails;
        }

        public List<UnDeliveredSMSDetailsModel> GetUnDeliveredSMSDetailList_ForDropMessegePage(string ClassIds, string MobileNos)
        {
            List<UnDeliveredSMSDetailsModel> ListOfUnDeliveredSMSDetails = new List<UnDeliveredSMSDetailsModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                if (!string.IsNullOrEmpty(MobileNos))
                    MobileNos = MobileNos.Replace("\n", ",");

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_UndeliveredSMS_GetUndeliveredList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassIds);
                cmd.Parameters.AddWithValue("@MobileNo", MobileNos);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                ListOfUnDeliveredSMSDetails = ds.Tables[0].AsEnumerable().Select(r => new UnDeliveredSMSDetailsModel
                {
                    Class = r.Field<string>("Class"),
                    ERPNo = r.Field<string>("ERPNo"),
                    Father = r.Field<string>("Father"),
                    Mobile = r.Field<string>("Mobile"),
                    Mother = r.Field<string>("Mother"),
                    Sno = r.Field<Int64>("Sno"),
                    Student = r.Field<string>("Student"),
                }).ToList();
            }
            return ListOfUnDeliveredSMSDetails;
        }



        public string SMS_DropMessages_CRUD(SMS_DropMessages_CRUDModel model)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_UndeliveredSMS_CreateReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", model.ERPNos);
                cmd.Parameters.AddWithValue("@IsSharewithCoordinator", model.IsSharewithCoordinator);
                cmd.Parameters.AddWithValue("@IsSharewithClassTeacher", model.IsSharewithClassTeacher);
                cmd.Parameters.AddWithValue("@EmailToCoordinator", model.IsEmailToCoordinator);
                cmd.Parameters.AddWithValue("@EmailToClassTeacher", model.IsEmailToClassTeacher);
                cmd.Parameters.AddWithValue("@MessageId", model.MessageId);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                message = dt.Rows[0][1].ToString();
                return message;
            }
        }

        public Tuple<List<SelectListItem>, List<SelectListItem>> GetSMS_DropMessagesDropDowns()
        {
            List<SelectListItem> classList = new List<SelectListItem>();
            List<SelectListItem> templeteSMSList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_UndeliveredSMS_FormLoad", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                classList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID"),
                }).ToList();


                templeteSMSList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SMSText"),
                    Value = r.Field<string>("SMSID"),
                }).ToList();
            }
            return new Tuple<List<SelectListItem>, List<SelectListItem>>(classList, templeteSMSList);
        }

    }
}
