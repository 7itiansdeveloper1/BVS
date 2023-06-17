using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class DashBoard_ParentRequestMasterRepo : IDashBoard_ParentRequestMasterRepo
    {
        public List<DashBoard_ParentRequestMasterModel> GetParentRequestMasterList(string UserID, string Category)
        {
            List<DashBoard_ParentRequestMasterModel> requestList = new List<DashBoard_ParentRequestMasterModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DahsBoard_RequestMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserID);
                cmd.Parameters.AddWithValue("@RCategory", Category);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                requestList = ds.Tables[0].AsEnumerable().Select(r => new DashBoard_ParentRequestMasterModel
                {
                    RequestId = r.Field<string>("RequestId"),
                    RequestSubject = r.Field<string>("RequestSubject"),
                    RequestDate = r.Field<string>("RequestDate"),
                    RequestStatus = r.Field<string>("RequestStatus"),
                    RepliedDate = r.Field<string>("RepliedDate"),

                    FromDate = r.Field<string>("FromDate"),
                    ToDate = r.Field<string>("ToDate"),
                    LeaveDays = r.Field<int>("LeaveDays"),

                    //RequestUserId = r.Field<string>("CreationDate"),
                    //RepliedByUserId = r.Field<string>("CreationDate"),
                    //RequestCategory = r.Field<string>("CreationDate"),
                    //DisplayName
                }).ToList();
            }
            return requestList;
        }

        public string ParentRequestMaster_CRUD(DashBoard_ParentRequestMasterModel model)
        {
            string message = "";
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DahsBoard_RequestMasterCRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RequestId", model.RequestId);
                cmd.Parameters.AddWithValue("@UserId", model.UserID);
                cmd.Parameters.AddWithValue("@RCategory", model.RequestCategory);
                cmd.Parameters.AddWithValue("@Function", model.Function);
                cmd.Parameters.AddWithValue("@RequestText", model.RequestSubject);
                cmd.Parameters.AddWithValue("@RequestStatus", model.RequestStatus);

                if (!string.IsNullOrEmpty(model.FromDate))
                    cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(model.FromDate).Date);

                if (!string.IsNullOrEmpty(model.ToDate))
                    cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(model.ToDate).Date);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            message = dt1.Rows[0][1].ToString();
            return message;
        }


        public List<RequestCommunicationDetailsModel> GetCommunicationDetails(string RequestID, string UserID)
        {
            List<RequestCommunicationDetailsModel> communicationList = new List<RequestCommunicationDetailsModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_GetRequestCommunication", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RequestId", RequestID);
                cmd.Parameters.AddWithValue("@UserId", UserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                communicationList = ds.Tables[0].AsEnumerable().Select(r => new RequestCommunicationDetailsModel
                {
                    Message = r.Field<string>("Message"),
                    SendBy = r.Field<string>("SendBy"),
                    SendDateTime = r.Field<string>("SendDateTime"),
                    IsUserSendMsg = r.Field<bool>("IsUserSendMsg"),
                }).ToList();
            }
            return communicationList;
        }
    }
}
