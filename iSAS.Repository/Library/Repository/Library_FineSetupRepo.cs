using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.Library.Repository
{
    public class Library_FineSetupRepo : ILibrary_FineSetupRepo
    {
        //public List<SelectListItem> Get_LibraryList(string UserId)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        DataSet ds = new DataSet();
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_Lib_FineSetup_Transaction", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@Mode", "FormLoad");


        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        con.Close();

        //        return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
        //        {
        //            Text = r.Field<string>("LibName"),
        //            Value = r.Field<string>("LibID"),
        //        }).ToList();
        //    }
        //}
        public List<Library_FineSetupModels> Get_FinSetupList(string LibraryId, string FineId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_FineSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LibraryId", LibraryId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@FineId", FineId);
                cmd.Parameters.AddWithValue("@Mode", "GetLibraryFine");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Library_FineSetupModels
                {
                    FineId = r.Field<string>("FineId"),
                    FineRateForStaff = r.Field<int>("FineRateForStaff"),
                    FineRateForStudent = r.Field<int>("FineRateForStudent"),
                    IsActive = r.Field<bool>("IsActive"),
                    LibraryId = r.Field<string>("LibraryId"),
                    LibraryName = r.Field<string>("LibraryName"),
                }).ToList();
            }
        }
        public Library_FineSetupModels Get_FinSetupById( string LibraryId, string FineId, string UserId)
        {
            return Get_FinSetupList(LibraryId, FineId, UserId).FirstOrDefault();
        }
        public Tuple<int, string> Library_FineSetup_CRUD(Library_FineSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_FineSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FineId", model.FineId);
                cmd.Parameters.AddWithValue("@LibraryId", model.LibraryId);
                cmd.Parameters.AddWithValue("@FineRateForStudent", model.FineRateForStudent);
                cmd.Parameters.AddWithValue("@FineRateForStaff", model.FineRateForStaff);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
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
        public Tuple<int, string> Library_FineSetup_CRUD(string FineId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_FineSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FineId", FineId);
                //cmd.Parameters.AddWithValue("@LibraryId", model.LibraryId);
                //cmd.Parameters.AddWithValue("@FineRateForStudent", model.FineRateForStudent);
                //cmd.Parameters.AddWithValue("@FineRateForStaff", model.FineRateForStaff);
                //cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                //cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
