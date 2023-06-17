using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ISas.Repository.Academic.Repository
{
    public class Academic_DepartmentMasterRepo : IAcademic_DepartmentMasterRepo
    {
        public List<Academic_DepartmentMasterModels> GetDepartmentList()
        {
            List<Academic_DepartmentMasterModels> departmentList = new List<Academic_DepartmentMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Staff_DeptMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetDepartmentList");
                cmd.Parameters.AddWithValue("@DeptId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                departmentList = ds.Tables[0].AsEnumerable().Select(r => new Academic_DepartmentMasterModels
                {
                    DeptID = r.Field<string>("DeptID"),
                    DeptName = r.Field<string>("DeptName"),

                    StaffStrength = r.Field<int>("StaffStrength"),
                    IsDeletable = r.Field<bool>("IsDeletable"),

                }).ToList();
            }
            return departmentList;
        }

        public Tuple<int, string> Academic_DepartmentMaster_CRUD(Academic_DepartmentMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Staff_DeptMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DeptID", model.DeptID);
                cmd.Parameters.AddWithValue("@DeptName", model.DeptName);
                cmd.Parameters.AddWithValue("@Default", 0);
                cmd.Parameters.AddWithValue("@PrintOrder", 0);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Academic_DepartmentMaster_CRUD(string DeptId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Staff_DeptMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DeptID", DeptId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

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
