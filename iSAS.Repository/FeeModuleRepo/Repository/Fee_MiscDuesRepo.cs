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
    public class Fee_MiscDuesRepo : IFee_MiscDuesRepo
    {
        public Fee_MiscDueModel GetMist_FromLoadDropDownList(string SessionId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_MiscDueModel model = new Fee_MiscDueModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscDueClassWise_Tranaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ClassId", "");
                cmd.Parameters.AddWithValue("@SectionId", "");
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StructId", "");
                cmd.Parameters.AddWithValue("@DueDate", "");
                cmd.Parameters.AddWithValue("@FeeHeadId", "");
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.HeadList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("HeadName"),
                    Value = r.Field<string>("HeadID"),
                    Selected= r.Field<bool>("IsSelected"),
                }).ToList();

                model.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID"),
                }).ToList();

                model.Amount = Convert.ToInt32(ds.Tables[2].Rows[0][0]);
                return model;
            }
        }
        public List<SelectListItem> getSectionList(string SessionId, string UserId, string ClassId, string FeeHeadId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_MiscDueModel model = new Fee_MiscDueModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscDueClassWise_Tranaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", "");
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StructId", "");
                cmd.Parameters.AddWithValue("@DueDate", "");
                cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                cmd.Parameters.AddWithValue("@Mode", "GetSectionList");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SecName"),
                    Value = r.Field<string>("SecID"),
                }).ToList();
            }
        }
        public List<SelectListItem> getStructureList(string SessionId, string UserId, string ClassId, string SectionId, string FeeHeadId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_MiscDueModel model = new Fee_MiscDueModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscDueClassWise_Tranaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StructId", "");
                cmd.Parameters.AddWithValue("@DueDate", "");
                cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                cmd.Parameters.AddWithValue("@Mode", "StructureList");


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
        public List<SelectListItem> getInstallmentList(string SessionId, string UserId, string ClassId, string SectionId, string StrectureId, string FeeHeadId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_MiscDueModel model = new Fee_MiscDueModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscDueClassWise_Tranaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StructId", StrectureId);
                cmd.Parameters.AddWithValue("@DueDate", "");
                cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                cmd.Parameters.AddWithValue("@Mode", "GetInstallmentList");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("InstallDueDate"),
                    Value = r.Field<string>("InstallDueDate"), //InstallID
                }).ToList();
            }
        }
        public List<Fee_MiscDuesStudentDetailsModel> getStudentDetailsListList(string SessionId, string UserId, string ClassId, string SectionId, string StrectureId, string DueDate, string FeeHeadId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_MiscDueModel model = new Fee_MiscDueModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscDueClassWise_Tranaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StructId", StrectureId);

                if (!string.IsNullOrEmpty(DueDate))
                    cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate).Date);
                cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                cmd.Parameters.AddWithValue("@Mode", "GetStudentList");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Fee_MiscDuesStudentDetailsModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    Amount = r.Field<string>("Amount"),
                    ERPNo = r.Field<string>("ERPNo"),
                    Father = r.Field<string>("Father"),
                    Selected = r.Field<bool>("Selected"),
                    StudentName = r.Field<string>("StudentName"),
                    TransRefNo = r.Field<string>("TransRefNo"),
                    Iscancellable = r.Field<bool>("IsCancellable"),
                }).ToList();
            }
        }


        public List<SelectListItem> GetMistHeadList()
        {
            List<SelectListItem> miscHeadList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscllaneousDueTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", "");
                cmd.Parameters.AddWithValue("@ERPNo", "");
                cmd.Parameters.AddWithValue("@QueryMode", "MiscHeadList");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                miscHeadList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("HeadName"),
                    Value = r.Field<string>("HeadID"),
                }).ToList();
            }
            return miscHeadList;
        }

        public List<SelectListItem> GetMiscInstallmentList(string SessionId, string ERPNo)
        {
            List<SelectListItem> installmentList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscllaneousDueTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@QueryMode", "StudentDueDateList");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                installmentList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DisplayDueDate"),
                    Value = r.Field<string>("DisplayDueDate"),
                }).ToList();
            }
            return installmentList;
        }

        public List<Fee_MiscDuesDetails> GetMiscDueList(string SessionId, string ERPNo)
        {
            List<Fee_MiscDuesDetails> misDueList = new List<Fee_MiscDuesDetails>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscllaneousDueTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@QueryMode", "StudentMiscHeadList");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                misDueList = ds.Tables[0].AsEnumerable().Select(r => new Fee_MiscDuesDetails
                {
                    Amount = r.Field<int>("Amount"),
                    Balance = r.Field<int>("Balance"),
                    DisplayDueDate = r.Field<string>("DisplayDueDate"),
                    HeadId = r.Field<string>("HeadId"),
                    HeadName = r.Field<string>("HeadName"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    PaidAmount = r.Field<int>("PaidAmount"),
                    TransRefNo = r.Field<string>("TransRefNo"),
                }).ToList();
            }
            return misDueList;
        }

        public Tuple<int, string> Fee_MiscDues_CRUD(Fee_MiscDueModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscDueClassWiseTransaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ERPNo", model.ERPNos);
                cmd.Parameters.AddWithValue("@CRUDFor", model.CRUDFor);
                cmd.Parameters.AddWithValue("@FeeHeadId", model.HeadId);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);

                if (!string.IsNullOrEmpty(model.InstallmentId))
                    cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(model.InstallmentId).Date);

                cmd.Parameters.AddWithValue("@MiscDueAmount", model.Amount);
                cmd.Parameters.AddWithValue("@TransRefNo", model.Selected_TransRefNo);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }



        public List<SelectListItem> getStudentDueHeadList(string sessionId, string erpNo)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_MiscDueModel model = new Fee_MiscDueModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscellanouesHeadStudentWise_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@QueryFor", "StudentConcessionHeadList");
                cmd.Parameters.AddWithValue("@FeeHeadId", "");
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("FeeHeadName"),
                    Value = r.Field<string>("FeeHeadId"),
                }).ToList();
            }
        }
        public List<Fee_StudentHeadWiseDueDetailsModel> getStudentDuesHeadWise(string sessionId, string erpNo, string headId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_MiscDueModel model = new Fee_MiscDueModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscellanouesHeadStudentWise_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@QueryFor", "StudentFeeHeadDueList");
                cmd.Parameters.AddWithValue("@FeeHeadId", headId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new Fee_StudentHeadWiseDueDetailsModel
                {
                    Amount = r.Field<int>("Amount"),
                    Cancelable = r.Field<bool>("Cancelable"),
                    DueDate = r.Field<string>("DueDate"),
                    Editable = r.Field<bool>("Editable"),
                    ERPNo = r.Field<string>("ERPNo"),
                    HeadName = r.Field<string>("HeadName"),
                    Paid = r.Field<int>("Paid"),
                    TransRefNo = r.Field<string>("TransRefNo"),
                    IsChargesAvailed = r.Field<string>("IsChargesAvailed"),
                }).ToList();
            }
        }
        public Tuple<int, string> Fee_MiscDues_StudentWise_CRUD(Fee_MiscDueModel model)
        {
            
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable paramDt = new DataTable();
                paramDt.Columns.Add("InvoiceRefNo");
                paramDt.Columns.Add("DueDate");
                paramDt.Columns.Add("MiscAmount");
                for (int x = 0; x < model.StudentDuesList.Count; x++)
                {
                    DataRow row = paramDt.NewRow();
                    row[0] = model.StudentDuesList[x].TransRefNo.Trim();
                    row[1] = model.StudentDuesList[x].DueDate.Trim();
                    row[2] = model.StudentDuesList[x].Amount;
                    paramDt.Rows.Add(row);
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscellanouesHeadStudentWise_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@ConcCategory", "Fee");
                cmd.Parameters.AddWithValue("@ERPNo", model.ERPNos);
                cmd.Parameters.AddWithValue("@CRUDFor", model.CRUDFor);
                cmd.Parameters.AddWithValue("@FeeHeadId", model.HeadId);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@Dt", paramDt);
                cmd.Parameters.AddWithValue("@DueDate", "");
                cmd.Parameters.AddWithValue("@InvoiceRefNo", "");
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Fee_MiscDues_StudentWise_CRUD(string erpNo, string headId, string sessionId, string dueDate, string invRefNo,string userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable paramDt = new DataTable();
                paramDt.Columns.Add("InvoiceRefNo");
                paramDt.Columns.Add("DueDate");
                paramDt.Columns.Add("MiscAmount");
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_MiscellanouesHeadStudentWise_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@ConcCategory", "Fee");
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@CRUDFor", "CANCEL");
                cmd.Parameters.AddWithValue("@FeeHeadId", headId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@Dt", paramDt);
                if (!string.IsNullOrWhiteSpace(dueDate))
                    cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(dueDate).Date);
                cmd.Parameters.AddWithValue("@InvoiceRefNo", invRefNo);
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}