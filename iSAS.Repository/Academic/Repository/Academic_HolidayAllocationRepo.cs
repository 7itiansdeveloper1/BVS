using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_HolidayAllocationRepo : IAcademic_HolidayAllocationRepo
    {
        //public List<SelectListItem> GetDepartmentList()
        //{
        //    List<SelectListItem> deptList = new List<SelectListItem>();
        //    DataSet ds = new DataSet();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_Academic_HolidayAllocation_Transaction", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@QueryFor", "Dept");
        //        cmd.Parameters.AddWithValue("@HolidayId", "");

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        con.Close();

        //        deptList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
        //        {
        //            Text = r.Field<string>("DeptName"),
        //            Value = r.Field<string>("DeptID"),
        //        }).ToList();
        //    }
        //    return deptList;
        //}

        public List<Academic_HolidayAllocationModel> GetHolidayAllocationList()
        {
            List<Academic_HolidayAllocationModel> allocationList = new List<Academic_HolidayAllocationModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Academic_HolidayAllocation_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetHolidayAllocationList");
                cmd.Parameters.AddWithValue("@HolidayId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                allocationList = ds.Tables[0].AsEnumerable().Select(r => new Academic_HolidayAllocationModel
                {
                    Department = r.Field<string>("Department"),
                    FDate = r.Field<string>("FDate"),
                    HolidayId = r.Field<string>("HolidayId"),
                    HolidayName = r.Field<string>("HolidayName"),
                    NoofDays = r.Field<int>("NoofDays"),

                    SNo = r.Field<long>("SNo"),
                    IsDeletable = r.Field<bool>("IsDeletable"),
                    TDate = r.Field<string>("TDate"),
                    Wings = r.Field<string>("Wings"),

                }).ToList();
            }
            return allocationList;
        }

        public Academic_HolidayAllocationModel GetStaffList(Academic_HolidayAllocationModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Academic_HolidayAllocation_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "EditHolidayAllocation");
                cmd.Parameters.AddWithValue("@HolidayId", model.HolidayId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.DepartmentList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DeptName"),
                    Value = r.Field<string>("DeptID"),
                }).ToList();


                model.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID"),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();

                model.StaffList = ds.Tables[2].AsEnumerable().Select(r => new StaffDetailModel
                {
                    DeptId = r.Field<string>("DeptId"),
                    DeptName = r.Field<string>("DeptName"),
                    StaffID = r.Field<string>("StaffID"),
                    StaffName = r.Field<string>("Staff"),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();
                return model;
            }
        }

        public string Academic_HolidayAllocation_CRUD(string HolidayId, string ReferenceIds, string CRUDFor)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Academic_HolidayAllocation_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HolidayId", HolidayId);
                cmd.Parameters.AddWithValue("@ReferenceId", ReferenceIds);
                cmd.Parameters.AddWithValue("@CRUDFor", CRUDFor);
                cmd.Parameters.AddWithValue("@CRUDMode", "SAVE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                message = dt.Rows[0][1].ToString();
                return message;
            }
        }
    }
}
