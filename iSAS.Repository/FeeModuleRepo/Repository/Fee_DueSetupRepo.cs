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
    public class Fee_DueSetupRepo : IFee_DueSetupRepo
    {
        public Tuple<List<SelectListItem>, List<SelectListItem>> GetHeadWithInstallmentList(string StructId,string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<SelectListItem> headList = new List<SelectListItem>();
                List<SelectListItem> installmentList = new List<SelectListItem>();

                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_DueSetup_Tranasaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@StructId", StructId);
                cmd.Parameters.AddWithValue("@QueryFor", "GetStructureInsallmentList");
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                installmentList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("InstallName"),
                    Value = r.Field<string>("InstallId"),
                }).ToList();

                headList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("HeadName"),
                    Value = r.Field<string>("HeadId"),
                }).ToList();
                return new Tuple<List<SelectListItem>, List<SelectListItem>>(headList, installmentList);
            }
        }

        public List<Fee_DueSummery> GetClassDueSummeryList(string StructId, string StructName, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<Fee_DueSummery> dueSummeryList = new List<Fee_DueSummery>();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_DueSetup_Tranasaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@StructId", StructId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@QueryFor", "ClassDueArchitectureList");
                

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                dueSummeryList = ds.Tables[0].AsEnumerable().Select(r => new Fee_DueSummery
                {
                    ClassId = r.Field<string>("ClassId"),
                    SectionId = r.Field<string>("SectionId"),
                    Class = r.Field<string>("Class"),
                    Amount = r.Field<int>("Amount"),
                    Strength = r.Field<int>("Strength"),
                    AppliedStuent = r.Field<int>("AppliedStuent"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    CreateInvoice = r.Field<bool>("CreateInvoice"),

                    StructId = StructId,
                    StructName = StructName,

                }).ToList();
                return dueSummeryList;
            }
        }

        public List<Fee_DueSetupModels> GetClassDueList(string StructId, string StructName, string ClassId, string SectionId, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<Fee_DueSetupModels> classDueList = new List<Fee_DueSetupModels>();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_DueSetup_Tranasaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetClassDueList");
                cmd.Parameters.AddWithValue("@StructId", StructId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                classDueList = ds.Tables[0].AsEnumerable().Select(r => new Fee_DueSetupModels
                {
                    RecordId = r.Field<int>("RecordId"),
                    InstallName = r.Field<string>("InstallName"),
                    HeadName = r.Field<string>("HeadName"),
                    Amount = r.Field<int>("Amount"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    StructId = StructId,
                    StructName = StructName
                }).ToList();
                return classDueList;
            }
        }

        public Tuple<int, string> Fee_DueSetup_CRUD(Fee_DueSetupModels model,string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_DueSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RecordId", model.RecordId);
                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", model.SectionId);
                cmd.Parameters.AddWithValue("@HeadId", model.HeadName);
                cmd.Parameters.AddWithValue("@Amount", model.Amount);

                cmd.Parameters.AddWithValue("@StructId", model.StructId);
                cmd.Parameters.AddWithValue("@InstallId", model.InstallName);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<int, string> Fee_DueSetup_CRUD(int RecordId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_DueSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RecordId", RecordId);
                cmd.Parameters.AddWithValue("@ClassId", "");
                cmd.Parameters.AddWithValue("@SectionId", "");
                cmd.Parameters.AddWithValue("@HeadId", "");
                cmd.Parameters.AddWithValue("@Amount", "");

                cmd.Parameters.AddWithValue("@StructId", "");
                cmd.Parameters.AddWithValue("@InstallId", "");
                cmd.Parameters.AddWithValue("@UserId", "");
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<int, string> Fee_DueSetup_CRUD(string StructId, string FromClassId, string FromSectionId, string ToClass, string UserId,string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_CopyDue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StructId", StructId);
                cmd.Parameters.AddWithValue("@FromClassId", FromClassId);
                cmd.Parameters.AddWithValue("@FromSectionId", FromSectionId);
                cmd.Parameters.AddWithValue("@ToClass", ToClass);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
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
