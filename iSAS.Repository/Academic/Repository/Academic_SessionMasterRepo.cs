using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_SessionMasterRepo : IAcademic_SessionMasterRepo
    {
        public List<Academic_SessionMasterModel> getSessionMasterList(string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<Academic_DesignationMasterModels> designationList = new List<Academic_DesignationMasterModels>();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SesssionMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SesssionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new Academic_SessionMasterModel
                {
                    Active = r.Field<bool>("Active"),
                    DemotioinSessionId = r.Field<string>("DemotioinSessionId"),
                   // DSesssion = r.Field<string>("DSesssion"),
                    EAdmNo = r.Field<string>("EAdmNo"),
                    EDate = r.Field<string>("EDate"),
                    IsDefault = r.Field<bool>("IsDefault"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                    PromotionSessionId = r.Field<string>("PromotionSessionId"),
                    //PSesssion = r.Field<string>("PSesssion"),
                    SAdmNo = r.Field<string>("SAdmNo"),
                    SDate = r.Field<string>("SDate"),
                    SessId = r.Field<Int16>("SessId").ToString(),
                    Session = r.Field<string>("Session"),
                    SessionDisplayName = r.Field<string>("SessionDisplayName"),
                    UID = r.Field<string>("UID"),
                }).ToList();
            }
        }
        public Academic_SessionMasterModel sessionMasterDetailsById(string sessionId)
        {
            return getSessionMasterList(sessionId).FirstOrDefault();
        }
        public Tuple<int, string> Academic_SessionMaster_CRUD(Academic_SessionMasterModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SesssionMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessId", model.SessId);
                cmd.Parameters.AddWithValue("@SessName", model.SessionDisplayName);
                cmd.Parameters.AddWithValue("@Session", model.Session);

                if (!string.IsNullOrEmpty(model.SDate))
                    cmd.Parameters.AddWithValue("@SDate", Convert.ToDateTime(model.SDate).Date);

                if (!string.IsNullOrEmpty(model.EDate))
                    cmd.Parameters.AddWithValue("@EDate", Convert.ToDateTime(model.EDate).Date);

                cmd.Parameters.AddWithValue("@SAdmNo", model.SAdmNo);
                cmd.Parameters.AddWithValue("@EAdmNo", model.EAdmNo);
                cmd.Parameters.AddWithValue("@UID", model.UID);
                cmd.Parameters.AddWithValue("@Default", model.IsDefault);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@Active", model.Active);
                cmd.Parameters.AddWithValue("@PromotionSessionId", model.PromotionSessionId);
                cmd.Parameters.AddWithValue("@DemotioinSessionId", model.DemotioinSessionId);
                cmd.Parameters.AddWithValue("@USerId", model.UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public List<SelectListItem> getAllSessionList()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<Academic_DesignationMasterModels> designationList = new List<Academic_DesignationMasterModels>();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("select SessID,SessName from GetSession()", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<Int16>("SessID").ToString(),
                    Text = r.Field<string>("SessName"),
                }).ToList();
            }
        }
        public List<SelectListItem> getAllSessionWithDefaultSelected()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAllSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("SessID"),
                    Text = r.Field<string>("SessionDisplayName"),
                    Selected = r.Field<bool>("Default"),
                }).ToList();
            }
        }
    }
}