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
    public class Fee_ConcessionRepo : IFee_ConcessionRepo
    {
        public List<SelectListItem> GetConcessionCategoryList(string ConcessionCategroy, string QueryFor)
        {
            List<SelectListItem> concessionCategoryList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ConcCategory", ConcessionCategroy);
                cmd.Parameters.AddWithValue("@ERPNo", "");
                cmd.Parameters.AddWithValue("@QueryFor", QueryFor);
                cmd.Parameters.AddWithValue("@FeeHeadId", "");
                cmd.Parameters.AddWithValue("@SessionId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                concessionCategoryList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ConcName"),
                    Value = r.Field<string>("ConcId"),
                }).ToList();
            }
            return concessionCategoryList;
        }


        public int GetConcessionCategoryPercent(string ConcessionCategoryId)
        {
            int concessionCategoryPercent = 0;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ConcCategory", "Fee");
                cmd.Parameters.AddWithValue("@ERPNo", "");
                cmd.Parameters.AddWithValue("@QueryFor", "ConcessionList");
                cmd.Parameters.AddWithValue("@FeeHeadId", "");
                cmd.Parameters.AddWithValue("@SessionId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                List<SelectListItem> concessionListWithPercent = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = Convert.ToString(r.Field<int>("DefaultPer")),
                    Value = r.Field<string>("ConcId"),
                }).ToList();

                SelectListItem SelectedConcession = concessionListWithPercent.Where(r => r.Value == ConcessionCategoryId).FirstOrDefault();
                if (SelectedConcession != null && !string.IsNullOrEmpty(SelectedConcession.Text))
                    Int32.TryParse(SelectedConcession.Text, out concessionCategoryPercent);
            }
            return concessionCategoryPercent;
        }


        public List<SelectListItem> GetHeadList(string ConcessionCategroy, string ERPNo, string SessionId, string QueryFor)
        {
            List<SelectListItem> headList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ConcCategory", ConcessionCategroy);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@QueryFor", QueryFor);
                cmd.Parameters.AddWithValue("@FeeHeadId", "");
                cmd.Parameters.AddWithValue("@SessionId", SessionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                headList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("FeeHeadName"),
                    Value = r.Field<string>("FeeHeadId"),
                }).ToList();
            }
            return headList;
        }


        public List<SelectListItem> GetInstallmentList(string ERPNo, string SessionId, string QueryFor)
        {
            List<SelectListItem> headList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConcCategory", "");
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@QueryFor", QueryFor);
                cmd.Parameters.AddWithValue("@FeeHeadId", "");
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                headList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("InstallmentName"),
                    Value = r.Field<string>("InstallmentId"),
                }).ToList();
            }
            return headList;
        }

        public List<ConsessioinDetailsModel> GetConcessionDetailsList(string ConcessionCategroy, string ERPNo, string QueryFor, string FeeHeadId, string SessionId, string InstallmentId)
        {
            List<ConsessioinDetailsModel> concessionDetailsList = new List<ConsessioinDetailsModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ConcCategory", ConcessionCategroy);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@QueryFor", QueryFor);
                cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@InstallmentId", InstallmentId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                concessionDetailsList = ds.Tables[0].AsEnumerable().Select(r => new ConsessioinDetailsModel
                {
                    Amount = r.Field<int>("Amount"),
                    Balance = r.Field<int>("Balance"),
                    ConcAmount = r.Field<int>("ConcAmount"),
                    DiscAmount = r.Field<int>("DiscAmount"),
                    CreditNoteRefNo = r.Field<string>("CreditNoteRefNo"),
                    DisplayDueDate = r.Field<string>("DisplayDueDate"),
                    HeadId = r.Field<string>("HeadId"),
                    HeadName = r.Field<string>("HeadName"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    PaidAmount = r.Field<int>("PaidAmount"),
                    TransRefNo = r.Field<string>("TransRefNo"),
                    ERPNo = r.Field<string>("ERPNo"),
                    IsSelected = r.Field<bool>("IsSelected")
                }).ToList();
            }
            return concessionDetailsList;
        }

        public Tuple<int, string> Fee_Concession_CRUD(Fee_ConcessionModels model, DataTable paramdt, string userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                if (paramdt.Rows.Count > 0)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionTransaction_CRUD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ConcCategory", "Fee");
                    cmd.Parameters.AddWithValue("@ERPNo", model.StudentDetails.StudentId);
                    cmd.Parameters.AddWithValue("@CRUDFor", model.CRUDFor);
                    cmd.Parameters.AddWithValue("@FeeHeadId", model.HeadId);
                    cmd.Parameters.AddWithValue("@SessionId", model.StudentDetails.SessionId);
                    cmd.Parameters.AddWithValue("@TransMode", model.FeeModeId);
                    cmd.Parameters.AddWithValue("@TransBank", model.SelectedBankId);
                    cmd.Parameters.AddWithValue("@TransBranch", model.BranchName);
                    cmd.Parameters.AddWithValue("@TransRefNo", model.TransactionNo);
                    cmd.Parameters.AddWithValue("@ConcessionId", model.ConcessionCategoryId);
                    cmd.Parameters.AddWithValue("@ConcessionFor", model.ConcessionFor);
                    cmd.Parameters.AddWithValue("@CreditNoteId", model.Selected_CreditNoteId);
                    if (!string.IsNullOrWhiteSpace(model.Selected_DueDate))
                        cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(model.Selected_DueDate).Date);

                    if (!string.IsNullOrEmpty(model.Selected_ConcessionAmount))
                        cmd.Parameters.AddWithValue("@ConcessionAmount", Convert.ToInt32(model.Selected_ConcessionAmount));

                    cmd.Parameters.AddWithValue("@Dt", paramdt);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
                }
                else
                    return new Tuple<int, string>(0, "No Record Selected...!");

            }
        }

        public Tuple<int, string> Fee_Concession_CRUD(string SessionId, string ERPNo, string DueDate, string FeeHeadId, string CreditNoteId, string ConcessionFor, string userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionTransaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConcCategory", "Fee");
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@CRUDFor", "CANCEL");
                cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);

                cmd.Parameters.AddWithValue("@TransMode", "");
                cmd.Parameters.AddWithValue("@TransBank", "");
                cmd.Parameters.AddWithValue("@TransBranch", "");
                cmd.Parameters.AddWithValue("@TransRefNo", "");
                cmd.Parameters.AddWithValue("@ConcessionId", "");
                cmd.Parameters.AddWithValue("@ConcessionFor", ConcessionFor);
                cmd.Parameters.AddWithValue("@CreditNoteId", CreditNoteId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate).Date);


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
