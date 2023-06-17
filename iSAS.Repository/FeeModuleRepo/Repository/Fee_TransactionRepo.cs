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
    public class Fee_TransactionRepo : IFee_TransactionRepo
    {
        public Fee_Tran_LandingModel GetCollectionDetails(string fromDate, string toDate,string userId,string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_Tran_LandingModel landingDetails = new Fee_Tran_LandingModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("Fee_FeeTransactionLandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(fromDate).Date);
                cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(toDate).Date);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                landingDetails.ClassList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("ClassId"),
                    Text = r.Field<string>("ClassName"),
                }).ToList();

                landingDetails.FeeCollection = ds.Tables[1].AsEnumerable().Select(r => new FeeModesModel
                {
                    Cash = r.Field<int>("Cash"),
                    Cheque = r.Field<int>("Cheque"),
                    Online = r.Field<int>("Online"),
                    Total = r.Field<int>("Total"),
                }).FirstOrDefault();

                landingDetails.TransportCollection = ds.Tables[2].AsEnumerable().Select(r => new FeeModesModel
                {
                    Cash = r.Field<int>("Cash"),
                    Cheque = r.Field<int>("Cheque"),
                    Online = r.Field<int>("Online"),
                    Total = r.Field<int>("Total"),
                }).FirstOrDefault();
                return landingDetails;
            }
        }

        public List<ReceiptDetailModel> GetReceiptDetails(string fromDate, string toDate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<ReceiptDetailModel> receiptDeailsList = new List<ReceiptDetailModel>();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("Fee_FeeTransactionLandingPage_ReceiptDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(fromDate).Date);
                cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(toDate).Date);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                receiptDeailsList = ds.Tables[0].AsEnumerable().Select(r => new ReceiptDetailModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ClassName = r.Field<string>("ClassName"),
                    Date = r.Field<string>("Date"),
                    ERPNo = r.Field<string>("ERPNo"),
                    Mode = r.Field<string>("Mode"),
                    ReceiptNo = r.Field<string>("ReceiptNo"),
                    SNo = r.Field<Int64>("SNo"),
                    StudentName = r.Field<string>("StudentName"),
                    Status = r.Field<string>("Status"),
                    Amount = r.Field<int>("Amount"),
                }).ToList();
                return receiptDeailsList;
            }
        }

        public List<StudentDetailsModel> GetStudentDetails(string SearchType, string SearchText, string ClassID, string SessionID, string status)
        {
            List<StudentDetailsModel> studentDeailsList = new List<StudentDetailsModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Fee_FeeTransactionLandingPage_SearchStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchType", SearchType);
                cmd.Parameters.AddWithValue("@SearchText", SearchText);
                cmd.Parameters.AddWithValue("@SelectedClassID", ClassID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);
                cmd.Parameters.AddWithValue("@SearchActive", status);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                studentDeailsList = ds.Tables[0].AsEnumerable().Select(r => new StudentDetailsModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ClassName = r.Field<string>("ClassName"),
                    ERPNo = r.Field<string>("ERPNo"),
                    SNo = r.Field<Int64>("SNo"),
                    StudentName = r.Field<string>("StudentName"),
                    FatherName = r.Field<string>("FatherName"),
                    MotherName = r.Field<string>("MotherName"),
                    MobileNo = r.Field<string>("Mobile"),
                }).ToList();
            }
            return studentDeailsList;
        }
        public List<StudentDetailsModel> GetStudentDetails(string SearchType, string SearchText, string ClassID, string SessionID)
        {
            List<StudentDetailsModel> studentDeailsList = new List<StudentDetailsModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Fee_FeeTransactionLandingPage_SearchStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchType", SearchType);
                cmd.Parameters.AddWithValue("@SearchText", SearchText);
                cmd.Parameters.AddWithValue("@SelectedClassID", ClassID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                studentDeailsList = ds.Tables[0].AsEnumerable().Select(r => new StudentDetailsModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ClassName = r.Field<string>("ClassName"),
                    ERPNo = r.Field<string>("ERPNo"),
                    SNo = r.Field<Int64>("SNo"),
                    StudentName = r.Field<string>("StudentName"),
                    FatherName = r.Field<string>("FatherName"),
                    MotherName = r.Field<string>("MotherName"),
                    MobileNo = r.Field<string>("Mobile"),
                    AdmCategoryName = r.Field<string>("AdmCategoryName"),
                    AvailSnacks = r.Field<string>("AvailSnacks"),
                    AvailTransport = r.Field<string>("AvailTransport"),
                }).ToList();
            }
            return studentDeailsList;
        }

        public List<SelectListItem> GetUnpaidInstallmentList(string ErpNo, string SessionId, string FeeMode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<SelectListItem> unpaidInstallmentList = new List<SelectListItem>();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeTransaction_GetUnpaidInstallment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ErpNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@FeeMode", FeeMode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                unpaidInstallmentList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Duration"),
                    Value = r.Field<string>("DueDate"),
                }).ToList();
                return unpaidInstallmentList;
            }
        }

        public FeeReceiptModel GetFeeInstallmentDetails(string ErpNo, string SessionId, string DueDate, string FeeMode, string TransactionDate)
        {
            FeeReceiptModel unpaidInstallmentDetails = new FeeReceiptModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_GetInstallmentDueDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ErpNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);

                if (!string.IsNullOrEmpty(DueDate))
                    cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate).Date);

                if (!string.IsNullOrEmpty(TransactionDate))
                    cmd.Parameters.AddWithValue("@TransactionDate", Convert.ToDateTime(TransactionDate).Date);

                cmd.Parameters.AddWithValue("@FeeMode", FeeMode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                unpaidInstallmentDetails = ds.Tables[0].AsEnumerable().Select(r => new FeeReceiptModel
                {
                    Advance = r.Field<int>("Amount"),
                }).FirstOrDefault();

                if (unpaidInstallmentDetails == null)
                    unpaidInstallmentDetails = new FeeReceiptModel();

                unpaidInstallmentDetails.HeadList = ds.Tables[1].AsEnumerable().Select(r => new FeeHeadModel
                {
                    Amount = r.Field<int>("Due"),
                    //DueDate = r.Field<string>("DueDate"),
                    Duration = r.Field<string>("Duration"),
                    HeadName = r.Field<string>("HeadName"),
                }).ToList();
                if (ds.Tables[3].Rows.Count>0)
                unpaidInstallmentDetails.Discount = Convert.ToInt32(ds.Tables[3].Rows[0][0]);

                if (unpaidInstallmentDetails != null && unpaidInstallmentDetails.HeadList != null && unpaidInstallmentDetails.HeadList.Count > 0)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        unpaidInstallmentDetails.LateFee = Convert.ToInt32(ds.Tables[2].Rows[0][1]);
                        unpaidInstallmentDetails.MinFine = Convert.ToInt32(ds.Tables[2].Rows[0][3]);
                        unpaidInstallmentDetails.IsDiscountDisable = Convert.ToBoolean(ds.Tables[2].Rows[0][4]);
                        unpaidInstallmentDetails.MaxDiscountAmt = Convert.ToInt32(ds.Tables[2].Rows[0][5]);

                    }
                    else
                    {
                        unpaidInstallmentDetails.LateFee = 0;
                        unpaidInstallmentDetails.MinFine = 0;
                        unpaidInstallmentDetails.IsDiscountDisable = false ;
                        unpaidInstallmentDetails.MaxDiscountAmt = 100000;
                        
                    }
                        

                    unpaidInstallmentDetails.AmountToPay = unpaidInstallmentDetails.HeadList.Select(r => r.Amount).DefaultIfEmpty(0).Sum();
                    unpaidInstallmentDetails.NetPay = (unpaidInstallmentDetails.AmountToPay - unpaidInstallmentDetails.Advance) + unpaidInstallmentDetails.LateFee;
                    unpaidInstallmentDetails.PaidAmount = unpaidInstallmentDetails.NetPay- unpaidInstallmentDetails.Discount;
                }
            }
            return unpaidInstallmentDetails;

        }

        public List<ReceiptDetailModel> GetReceiptDetails(string erpNo)
        {
            List<ReceiptDetailModel> receiptDeailsList = new List<ReceiptDetailModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Fee_FeeTransactionLandingPage_ReceiptDetails_ByERPNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                receiptDeailsList = ds.Tables[0].AsEnumerable().Select(r => new ReceiptDetailModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ClassName = r.Field<string>("ClassName"),
                    Date = r.Field<string>("Date"),
                    ERPNo = r.Field<string>("ERPNo"),
                    Mode = r.Field<string>("Mode"),
                    ReceiptNo = r.Field<string>("ReceiptNo"),
                    SNo = r.Field<Int64>("SNo"),
                    StudentName = r.Field<string>("StudentName"),
                }).ToList();
            }
            return receiptDeailsList;
        }

        public string Fee_Transaction_CRUD(FeeReceiptModel model,string userid)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeTransaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", model.StudentDetails.SessionId);
                cmd.Parameters.AddWithValue("@ERPNo", model.StudentDetails.StudentId);
                cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(model.InstallmentId).Date);
                cmd.Parameters.AddWithValue("@ReceiptDate", Convert.ToDateTime(model.ReceiptDate).Date);
                cmd.Parameters.AddWithValue("@InHandAmount", model.TrnAmount);
                cmd.Parameters.AddWithValue("@Discount", model.Discount);
                cmd.Parameters.AddWithValue("@AdjustAmount", model.Advance);
                cmd.Parameters.AddWithValue("@FeeMode", model.FeeType);
                cmd.Parameters.AddWithValue("@PaymdentMode", model.FeeModeId);
                cmd.Parameters.AddWithValue("@Bank", model.SelectedBankId);
                cmd.Parameters.AddWithValue("@Branch", model.BranchName);
                cmd.Parameters.AddWithValue("@TransactionNo", model.TransactionNo);
                if (!string.IsNullOrEmpty(model.TransactionDate))
                    cmd.Parameters.AddWithValue("@TransactionDate", Convert.ToDateTime(model.TransactionDate).Date);
                cmd.Parameters.AddWithValue("@FineDue", model.LateFee);
                cmd.Parameters.AddWithValue("@UserId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                message = dt.Rows[0][0].ToString();
                return message;
            }
        }

        public string FeeReceipt_CancelOrDelete(string ERPNo, string SessionId, string ReceiptNo, string Mode,string userId)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_EditReceipt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@RecptNo", ReceiptNo);
                cmd.Parameters.AddWithValue("@Mode", Mode);
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                // message = dt.Rows[0][0].ToString();
                message = "Updated Successfully";
                return message;
            }
        }

        public string Get_LastReceiptNo()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from fn_Fee_GetLastReceiptNo()", con);
                cmd.CommandType = CommandType.Text;
            
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();

                return "";
            }
        }

        public Fee_StudentLedgerModel Get_Fee_StudentLedgerModel(string ErpNo, string SessionId)
        {
            Fee_StudentLedgerModel studentLedgerModel = new Fee_StudentLedgerModel();
            Fee_StudentLedgerList studentLedgerList = new Fee_StudentLedgerList();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_StudentLedgerNew", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ErpNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                studentLedgerModel.Student= ds.Tables[1].Rows[0][0].ToString();
                studentLedgerModel.AdmNo = ds.Tables[1].Rows[0][1].ToString();
                studentLedgerModel.Father = ds.Tables[1].Rows[0][2].ToString();
                studentLedgerModel.ERP = ds.Tables[1].Rows[0][3].ToString();
                studentLedgerModel.Due = Convert.ToInt32(ds.Tables[1].Rows[0][4]);
                studentLedgerModel.Concession = Convert.ToInt32(ds.Tables[1].Rows[0][5]);
                studentLedgerModel.discount = Convert.ToInt32(ds.Tables[1].Rows[0][6]);
                studentLedgerModel.Advance = Convert.ToInt32(ds.Tables[1].Rows[0][7]);
                studentLedgerModel.Payable = Convert.ToInt32(ds.Tables[1].Rows[0][8]);
                studentLedgerModel.ActualPaid = Convert.ToInt32(ds.Tables[1].Rows[0][9]);
                studentLedgerModel.Balance = Convert.ToInt32(ds.Tables[1].Rows[0][10]);
                studentLedgerModel.F = Convert.ToInt32(ds.Tables[1].Rows[0][11]);
                studentLedgerModel.T = Convert.ToInt32(ds.Tables[1].Rows[0][12]);
                studentLedgerModel.ClassName = ds.Tables[1].Rows[0][13].ToString();

                

                studentLedgerModel.StudentLedgerList = ds.Tables[0].AsEnumerable().Select(r => new Fee_StudentLedgerList
                {
                    Sno = r.Field<Int64>("Sno"),
                    Duration = r.Field<string>("Duration"),
                    DueDate = r.Field<string>("DueDate"),
                    ERP = r.Field<string>("ERP"),
                    Due = r.Field<int>("Due"),
                    Concession = r.Field<int>("Concession"),
                    discount = r.Field<int>("discount"),
                    advance = r.Field<int>("advance"),
                    Payable = r.Field<int>("Payable"),
                    ActualPaid = r.Field<int>("ActualPaid"),
                    Balance = r.Field<int>("Balance"),
                    PreviousBalance = r.Field<int>("PreviousBalance"),
                    Status = r.Field<string>("Status")
                }).ToList();
                studentLedgerModel.StudentReceiptList = ds.Tables[2].AsEnumerable().Select(r => new Fee_StudentReceiptList
                {
                    Sno = r.Field<Int64>("Sno"),
                    ERPNo = r.Field<string>("ERPNo"),
                    ReceiptNo = r.Field<string>("ReceiptNo"),
                    TransDate = r.Field<string>("TransDate"),
                    Duration = r.Field<string>("Duration"),
                    Mode = r.Field<string>("Mode"),
                    Due = r.Field<int>("Due"),
                    Concession = r.Field<int>("Concession"),
                    discount = r.Field<int>("discount"),
                    advance = r.Field<int>("advance"),
                    Payable = r.Field<int>("Payable"),
                    Paid = r.Field<int>("Paid"),
                    Balance = r.Field<int>("Balance"),
                    RecAmount = r.Field<int>("RecAmount"),
                    Excess = r.Field<int>("Excess"),
                    Status = r.Field<string>("Status")
                }).ToList();
            }
            return studentLedgerModel;

        }

        public List<Fee_InstallmentDetailsList> Get_Fee_InstallmentDetailsList(string erpNo,string dueDate,string sessionId)
        {
            List<Fee_InstallmentDetailsList> installmentDetailsList = new List<Fee_InstallmentDetailsList>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_StudentLedgerNew_InstallmentDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@DueDate", dueDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                installmentDetailsList = ds.Tables[0].AsEnumerable().Select(r => new Fee_InstallmentDetailsList
                {
                    Sno = r.Field<Int64>("SNo"),
                    Duration = r.Field<string>("Duration"),
                    HeadName = r.Field<string>("HeadName"),
                    Due = r.Field<int>("Due"),
                    Concession = r.Field<int>("Concession"),
                    discount = r.Field<int>("discount"),
                    advance = r.Field<int>("advance"),
                    Payable = r.Field<int>("Payable"),
                    ActualPaid = r.Field<int>("ActualPaid"),
                    Balance = r.Field<int>("Balance"),
                    PreviousBalance = r.Field<int>("PreviousBalance")
                }).ToList();
            }
            return installmentDetailsList;
        }
    }
}
