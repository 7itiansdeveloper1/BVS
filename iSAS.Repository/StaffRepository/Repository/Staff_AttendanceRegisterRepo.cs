using ISas.Entities.StaffEntities;
using ISas.Repository.StaffRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.StaffRepository.Repository
{
    public class Staff_AttendanceRegisterRepo : IStaff_AttendanceRegisterRepo
    {
        public List<StaffAttendanceDetailsModel> GetStaffAttenDetails(string AttenDate, string DeptIDs)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StaffAttendance_Attendance_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(AttenDate))
                    cmd.Parameters.AddWithValue("@AttDate", Convert.ToDateTime(AttenDate).Date);
                cmd.Parameters.AddWithValue("@DeptId", DeptIDs);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new StaffAttendanceDetailsModel
                {
                    AttStatus = r.Field<bool>("AttStatus"),
                    Department = r.Field<string>("Department"),
                    InTime = r.Field<string>("InTime"),
                    Mobile = r.Field<string>("Mobile"),
                    OutTime = r.Field<string>("OutTime"),
                    StaffCode = r.Field<string>("StaffCode"),
                    StaffID = r.Field<string>("StaffID"),
                    StaffName = r.Field<string>("StaffName"),
                    AttStatusStr = r.Field<string>("AttStatusStr"),
                }).ToList();
            }
        }
        public Tuple<int, string> Staff_AttendanceRegister_CRUD(Staff_AttendanceRegisterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable paramDt = new DataTable();
                paramDt.Columns.Add("StaffId");
                paramDt.Columns.Add("InTime");
                paramDt.Columns.Add("OutTime");
                paramDt.Columns.Add("AttStatus");
                for (int i=0; i < model.StaffAttendanceDetails.Count; i++)
                {
                    DataRow row = paramDt.NewRow();
                    row[0] = model.StaffAttendanceDetails[i].StaffID;
                    row[1] = model.StaffAttendanceDetails[i].InTime;
                    row[2] = model.StaffAttendanceDetails[i].OutTime;
                    row[3] = model.StaffAttendanceDetails[i].AttStatusStr == "on" ? "P" : model.StaffAttendanceDetails[i].AttStatusStr;
                    //row[3] = model.StaffAttendanceDetails[i].AttStatusStr;
                    paramDt.Rows.Add(row);
                }

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StaffAttendance_Attendance_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);

                if (!string.IsNullOrEmpty(model.AttenDate))
                    cmd.Parameters.AddWithValue("@AttDate", Convert.ToDateTime(model.AttenDate).Date);
                cmd.Parameters.AddWithValue("@StaffAttendanceDT", paramDt);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
