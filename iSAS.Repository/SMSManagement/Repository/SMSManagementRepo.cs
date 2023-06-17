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
    public class SMSManagementRepo : ISMSManagementRepo
    {
        public DropDownListFor_SMSManagement GetSMS_ManagementDropDownList()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DropDownListFor_SMSManagement dropDownMaster = new DropDownListFor_SMSManagement();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_LandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                dropDownMaster.ClassList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassNameWithSection"),
                    Value = r.Field<string>("ClassSectionId"),
                }).ToList();

                dropDownMaster.DepartmentList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DeptName"),
                    Value = r.Field<string>("DeptID"),
                }).ToList();

                dropDownMaster.StudentGroupList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("GroupName"),
                    Value = r.Field<int>("GroupID").ToString(),
                }).ToList();

                dropDownMaster.TeacherGroupList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("GroupName"),
                    Value = r.Field<int>("GroupID").ToString(),
                }).ToList();

                var client = new WebClient();
                string key = ConfigurationManager.AppSettings["SMSReportKeyBasedOnIP"];
                try
                {
                    var content = client.DownloadString("https://api-alerts.solutionsinfini.com/v3/?method=account.credits&api_key=" + key + "&format=xml");
                    dropDownMaster.CreditBalance = content.Between("<credits>", "</credits>");
                }
                catch { }
                return dropDownMaster;
            }
        }

        public List<SMS_StudentModel> GetStudentDetails(string ClassIds, string StudentGroupId, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_SMSMessagener_SearchStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                string functionVal = "";
                if (!string.IsNullOrEmpty(ClassIds))
                    functionVal = "ClassWise";
                else if (!string.IsNullOrEmpty(StudentGroupId))
                    functionVal = "GroupWise";

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@Function", functionVal);
                cmd.Parameters.AddWithValue("@ClassId", ClassIds);
                cmd.Parameters.AddWithValue("@GrpId", StudentGroupId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SMS_StudentModel
                {
                    ERP = r.Field<string>("ERP"),
                    AdmNo = r.Field<string>("AdmNo"),
                    Student = r.Field<string>("Student"),
                    Class = r.Field<string>("Class"),
                    SMSNo = r.Field<string>("SMSNo"),
                    Father = r.Field<string>("Father"),
                }).ToList();
            }
        }

        public List<SMS_AdminAndStaffModel> GetStaffDetails(string DeptIds, string StaffGroupId, string IsForAdminStaff)
        {
            if (IsForAdminStaff == "YES")
                return GetStaffList();

            return GetStaffList(DeptIds, StaffGroupId);
        }
        private List<SMS_AdminAndStaffModel> GetStaffList(string DeptIds, string StaffGroupId)
        {
            List<SMS_AdminAndStaffModel> staffdetailsList = new List<SMS_AdminAndStaffModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_SMSMessagener_SearchDeptStaff", con);
                cmd.CommandType = CommandType.StoredProcedure;

                string functionVal = "";
                if (!string.IsNullOrEmpty(DeptIds))
                    functionVal = "DeptWise";

                else if (!string.IsNullOrEmpty(StaffGroupId))
                    functionVal = "GroupWise";

                cmd.Parameters.AddWithValue("@Function", functionVal);
                cmd.Parameters.AddWithValue("@DeptId", DeptIds);
                cmd.Parameters.AddWithValue("@GrpId", StaffGroupId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                staffdetailsList = ds.Tables[0].AsEnumerable().Select(r => new SMS_AdminAndStaffModel
                {
                    Department = r.Field<string>("Department"),
                    Mobile = r.Field<string>("Mobile"),
                    StaffCode = r.Field<string>("StaffCode"),
                    StaffID = r.Field<string>("StaffID"),
                    StaffName = r.Field<string>("StaffName"),
                }).ToList();
            }
            return staffdetailsList;
        }
        private List<SMS_AdminAndStaffModel> GetStaffList()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_SMSMessagener_SearchAdmin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SMS_AdminAndStaffModel
                {
                    Department = r.Field<string>("Department"),
                    Mobile = r.Field<string>("Mobile"),
                    StaffCode = r.Field<string>("StaffCode"),
                    StaffID = r.Field<string>("StaffID"),
                    StaffName = r.Field<string>("StaffName"),
                }).ToList();
            }
        }

        public Tuple<int, string> StudentSMSGroup_CRUD(string GroupName, string SMSGroupType, List<SMS_StudentModel> studentList)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("select");
                dt.Columns.Add("UID");
                dt.Columns.Add("AdmNo");
                dt.Columns.Add("Student");
                dt.Columns.Add("Cls");
                dt.Columns.Add("Sec");
                dt.Columns.Add("Mob");
                dt.Columns.Add("Father");
                int rowcount = studentList.Count;
                for (int x = 0; x < rowcount; x++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = true;
                    row[1] = studentList[x].ERP;
                    row[2] = studentList[x].AdmNo;
                    row[3] = studentList[x].Student;
                    row[4] = studentList[x].Class;
                    row[5] = "";
                    row[6] = studentList[x].SMSNo;
                    row[7] = studentList[x].Father;
                    dt.Rows.Add(row);
                }

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_AddSMSGroup_NEW", con);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Student Info
                cmd.Parameters.AddWithValue("@GroupName", GroupName);
                cmd.Parameters.AddWithValue("@GCategory", "S");
                cmd.Parameters.AddWithValue("@SMS_S_ERPNo", dt);

                #endregion

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> StaffSMSGroup_CRUD(string GroupName, string SMSGroupType, List<SMS_AdminAndStaffModel> staffList)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("StaffID");
                int rowcount = staffList.Count;
                for (int x = 0; x < rowcount; x++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = staffList[x].StaffID;
                    dt.Rows.Add(row);
                }

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_AddSMSGroupForTeacher_NEW", con);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Student Info
                cmd.Parameters.AddWithValue("@GroupName", GroupName);
                cmd.Parameters.AddWithValue("@GCategory", "T");
                cmd.Parameters.AddWithValue("@SMS_T_StaffID", dt);

                #endregion

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }


        public List<SMS_OutboxModel> GetSMSOutboxDetails()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_Transaction_OutBox", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SMS_OutboxModel
                {
                    MobileNo = r.Field<string>("MobileNo"),
                    ReciverName = r.Field<string>("ReciverName"),
                    RecordSendBy = r.Field<string>("RecordSendBy"),
                    RecordSendDate = r.Field<DateTime>("RecordSendDate"),
                    SendDate = r.Field<DateTime>("SendDate"),
                    SMSText = r.Field<string>("SMSText"),
                }).ToList();
            }
        }
        public Tuple<int, string> Delete_OutBoxSMS(DataTable dt)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SMS_OutBox_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type_OutBox", dt);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(1, "SMS Deleted Successfully..!");
            }
        }

    }
}
