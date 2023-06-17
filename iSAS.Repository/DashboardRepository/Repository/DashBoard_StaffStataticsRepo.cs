using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class DashBoard_StaffStataticsRepo : IDashBoard_StaffStataticsRepo
    {
        public MyClassInfoModel GetClassInfo(string SessionId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                MyClassInfoModel model = new MyClassInfoModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StaffLogin_Child", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "GetMyClassData");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.ClassId = ds.Tables[0].Rows[0][0].ToString();
                    model.SectionId = ds.Tables[0].Rows[0][1].ToString();
                    model.FullClassName = ds.Tables[0].Rows[0][2].ToString();
                    model.ClassPO = Convert.ToInt32(ds.Tables[0].Rows[0][3]);
                    model.Strength = Convert.ToInt32(ds.Tables[0].Rows[0][4]);
                    model.NewAdm = Convert.ToInt32(ds.Tables[0].Rows[0][5]);
                    model.OldAdm = Convert.ToInt32(ds.Tables[0].Rows[0][6]);
                    model.TC = Convert.ToInt32(ds.Tables[0].Rows[0][7]);
                    model.NSO = Convert.ToInt32(ds.Tables[0].Rows[0][8]);
                    model.BOY = Convert.ToInt32(ds.Tables[0].Rows[0][9]);
                    model.GIRL = Convert.ToInt32(ds.Tables[0].Rows[0][10]);
                    model.GEN = Convert.ToInt32(ds.Tables[0].Rows[0][11]);
                    model.SC = Convert.ToInt32(ds.Tables[0].Rows[0][12]);
                    model.ST = Convert.ToInt32(ds.Tables[0].Rows[0][13]);
                    model.OBC = Convert.ToInt32(ds.Tables[0].Rows[0][14]);
                    model.EWS = Convert.ToInt32(ds.Tables[0].Rows[0][15]);

                }
                if (ds.Tables[1].Rows.Count > 0)
                    model.FeeDefaulter = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                if (ds.Tables[2].Rows.Count > 0)
                    model.LibraryDefaulter = Convert.ToInt32(ds.Tables[2].Rows[0][0]);
                return model;
            }
        }
        public List<BookHistoryModel> GetBookHistory(string SessionId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StaffLogin_Child", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "GetMyBooks");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new BookHistoryModel
                {
                    AccessionNo = r.Field<string>("AccessionNo"),
                    BookTitle = r.Field<string>("BookTitle"),
                    Fine = r.Field<int>("Fine"),
                    IssueDate = r.Field<string>("IssueDate"),
                    ReturnDate = r.Field<string>("ReturnDate"),
                    Sno = r.Field<string>("Sno"),
                }).ToList();
            }
        }
        public List<SMSDetailsModel> GetSMSDetails(string SessionId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StaffLogin_Child", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "GetMySMS");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SMSDetailsModel
                {
                    SMSDate = r.Field<string>("SMSDate"),
                    SMSDeliveryDate = r.Field<string>("SMSDeliveryDate"),
                    SMSStatus = r.Field<string>("SMSStatus"),
                    SMSTExt = r.Field<string>("SMSTExt"),
                    Sno = r.Field<long>("Sno"),
                }).ToList();
            }
        }
        public List<SalaryDetailsModel> GetSalaryDetails(string SessionId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StaffLogin_Child", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "GetSalaryList");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SalaryDetailsModel
                {
                    InHand = r.Field<decimal>("InHand"),
                    SalMonth = r.Field<string>("SalMonth"),
                    SalMonthName = r.Field<string>("SalMonthName"),
                    Salyear = r.Field<string>("Salyear"),
                    StaffId = r.Field<string>("StaffId"),
                }).ToList();
            }
        }

        public Staff_AttendanceDetailsModel GetStaffAttendanceInfo_FormLoad(string UserId, int Month, int Year)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Staff_AttendanceDetailsModel model = new Staff_AttendanceDetailsModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StaffAttendance_LeaveApply_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");
                cmd.Parameters.AddWithValue("@LeaveId", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();


                model.LeaveBalanceDetails = ds.Tables[1].AsEnumerable().Select(r => new LeaveBalanceDetailsModel
                {
                    LvCode = r.Field<string>("LvCode"),
                    LvID = r.Field<string>("LvID"),
                    LeaveAvailable = r.Field<int>("LeaveAvailable"),
                    LeaveAvailed = r.Field<int>("LeaveAvailed"),
                    LeaveOpenBalance = r.Field<int>("LeaveOpenBalance"),
                }).ToList();

                model.AvailedLeaveHistoryList = ds.Tables[2].AsEnumerable().Select(r => new AvailedLeaveHistoryModel
                {
                    IsEditable = r.Field<bool>("IsEditable"),
                    MDate = r.Field<string>("MDate"),
                    RBy = r.Field<string>("RBy"),
                    RCloseDate = r.Field<string>("RCloseDate"),
                    RDate = r.Field<string>("RDate"),
                    RDiscription = r.Field<string>("RDiscription"),
                    RequestId = r.Field<string>("RequestId"),
                    RReferenceCode = r.Field<string>("RReferenceCode"),
                    RReferenceName = r.Field<string>("RReferenceName"),
                    RSendTo = r.Field<string>("RSendTo"),
                    RStatus = r.Field<string>("RStatus"),
                }).ToList();

                model.RequestToList = ds.Tables[3].AsEnumerable().Select(r => new RequestToModel
                {
                    IsRequestReadEnable = r.Field<bool>("IsRequestReadEnable"),
                    IsSelected = r.Field<bool>("IsSelected"),
                    Levels = r.Field<string>("Levels"),
                }).ToList();
                model.LeaveTypeList = ds.Tables[4].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("LeaveName"),
                    Value = r.Field<string>("LeaveId"),
                }).ToList();
                return model;
            }
        }

        public List<Tuple<string, string>> GetStaffAttenDetails(int Month, int Year, string UserID)
        {
            List<Tuple<string, string>> attenDetailList = new List<Tuple<string, string>>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StaffAttendance_LeaveApply_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserID);
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                attenDetailList = ds.Tables[0].AsEnumerable().Select(r => new Tuple<string, string>(r.Field<string>("AttDate"), r.Field<string>("AttStaus")) { }).ToList();
            }
            return attenDetailList;
        }

        public List<LeaveBalanceDetailsModel> GetStaffLeaveBalanceDetails(string UserId, int Month, int Year)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StaffAttendance_LeaveApply_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");
                cmd.Parameters.AddWithValue("@LeaveId", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();


                return ds.Tables[1].AsEnumerable().Select(r => new LeaveBalanceDetailsModel
                {
                    LvCode = r.Field<string>("LvCode"),
                    LvID = r.Field<string>("LvID"),
                    LeaveAvailable = r.Field<int>("LeaveAvailable"),
                    LeaveAvailed = r.Field<int>("LeaveAvailed"),
                    LeaveOpenBalance = r.Field<int>("LeaveOpenBalance"),
                }).ToList();
            }
        }


        public Tuple<int, string> DashBoard_StaffStatatics_CRUD(Staff_AttendanceDetailsModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_WorkFlow_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RequestId", null);
                cmd.Parameters.AddWithValue("@RCategory", "StaffLeaveApply");
                cmd.Parameters.AddWithValue("@RReferenceCode", model.LeaveTypeId);
                cmd.Parameters.AddWithValue("@RDiscription", model.Description);
                cmd.Parameters.AddWithValue("@RSendTo", string.Join(",", model.RequestToList.Where(r => r.IsSelected).Select(r => r.Levels).ToList()));
                cmd.Parameters.AddWithValue("@RStatus", "UNDER PROCESS");
                cmd.Parameters.AddWithValue("@RCloseDate", null);

                if (!string.IsNullOrEmpty(model.FromDate))
                    cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(model.FromDate).Date);

                if (!string.IsNullOrEmpty(model.ToDate))
                    cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(model.ToDate).Date);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
    }
}
