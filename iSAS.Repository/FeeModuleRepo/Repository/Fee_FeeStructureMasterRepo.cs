using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_FeeStructureMasterRepo : IFee_FeeStructureMasterRepo
    {
        public List<Fee_FeeStructureMasterModels> GetFeeStructureList(string StructId,string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeStructureMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StructId", StructId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@QueryFor", "GetStructureList");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Fee_FeeStructureMasterModels
                {
                    Default = r.Field<bool>("Default"),
                    DueSetup = r.Field<bool>("DueSetup"),
                    FineSetup = r.Field<bool>("FineSetup"),
                    Installment = r.Field<string>("Installment"),
                    InstallmentSetup = r.Field<bool>("InstallmentSetup"),
                    IsDeletetable = r.Field<bool>("IsDeletetable"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    StructId = r.Field<string>("StructId"),
                    StructName = r.Field<string>("StructName"),
                    PrintOrder = r.Field<Int16>("PrintOrder"),
                    IsActive = r.Field<bool>("IsActive"),
                }).ToList();
            }
        }

        public Fee_FeeStructureMasterModels GetFeeStructureByStructId(string StructId,string sessionId)
        {
            return GetFeeStructureList(StructId, sessionId).FirstOrDefault();
        }

        public Tuple<int,string> Fee_FeeStructureMaster_CRUD(Fee_FeeStructureMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeStructureMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StructId", model.StructId);
                cmd.Parameters.AddWithValue("@StructName", model.StructName);
                cmd.Parameters.AddWithValue("@Default", model.Default);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
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

        public Tuple<int, string> Fee_FeeStructureMaster_CRUD(string StructId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeStructureMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StructId", StructId);
                cmd.Parameters.AddWithValue("@StructName", "");
                cmd.Parameters.AddWithValue("@Default", "");
                cmd.Parameters.AddWithValue("@PrintOrder", "");
                cmd.Parameters.AddWithValue("@IsActive", "");

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }



        public List<SelectListItem> Fee_StrectureDropDown_ByClassSectionId(string ClassSectionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeBill_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                cmd.Parameters.AddWithValue("@Mode", "GetStructList");
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StructName"),
                    Value = r.Field<string>("StructID"),
                }).ToList();
            }
        }
    }
}
