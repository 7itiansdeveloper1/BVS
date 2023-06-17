using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class StudentFeeDetailsRepo : IStudentFeeDetailsRepo
    {
        public StudentFeeStatusModel GetFeeStatusDetailsList(string UserID, int SessionID)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StudentFeeStatusModel model = new StudentFeeStatusModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentFeeStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.feeDetailsList = ds.Tables[0].AsEnumerable().Select(r => new StudentFeeStatusModel
                {
                    Duration = r.Field<string>("Duration"),
                    Due = r.Field<int>("Due"),
                    Paid = r.Field<int>("Paid"),
                    ReceiptNo = r.Field<string>("ReceiptNo"),
                    Status = r.Field<string>("Status"),
                    Balance = r.Field<int>("Balance"),
                    Excess = r.Field<int>("Excess"),
                    ERPNo = r.Field<string>("ERPNo"),
                    ReceiptDate = r.Field<string>("TransDate"),
                    Mode = r.Field<string>("Mode"),
                    Advance = r.Field<int>("Advance"),
                    Discount = r.Field<int>("discount"),
                    Payable = r.Field<int>("Payable"),
                    Concession = r.Field<int>("Concession"),
                    IsCancelable = r.Field<bool>("IsCancelable"),
                }).ToList();
                if (ds.Tables[2].Rows.Count > 0)
                    model.EnableNoDueReceipt = Convert.ToBoolean(ds.Tables[2].Rows[0][0]);
                else
                    model.EnableNoDueReceipt = false;
                return model;
            }

        }

        public List<StudentFeeStatusModel> GetFeeStatusDetails_TransportList(string UserID, int SessionID)
        {
            List<StudentFeeStatusModel> feeDetailsList = new List<StudentFeeStatusModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentFeeStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                feeDetailsList = ds.Tables[1].AsEnumerable().Select(r => new StudentFeeStatusModel
                {
                    //DueDate = r.Field<string>("DueDate"),
                    //InvoiceNo = r.Field<string>("InvoiceNo"),
                    Duration = r.Field<string>("Duration"),
                    Due = r.Field<int>("Due"),
                    Paid = r.Field<int>("Paid"),
                    ReceiptNo = r.Field<string>("ReceiptNo"),
                    //Bill = r.Field<string>("Bill"),
                    Status = r.Field<string>("Status"),
                    Balance = r.Field<int>("Balance"),
                    Excess = r.Field<int>("Excess"),
                    ERPNo = r.Field<string>("ERPNo"),
                    ReceiptDate = r.Field<string>("TransDate"),
                    Mode = r.Field<string>("Mode")
                }).ToList();
            }

            // if (!string.IsNullOrEmpty(FilterType) && FilterType == "Pa/id")
            //feeDetailsList = feeDetailsList.ToList();
            return feeDetailsList;
        }

        public StudentFeeDashbaord GetFeeStatusDetailsList_StudDash(string UserID, int SessionID,string studentImage)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StudentFeeDashbaord model = new StudentFeeDashbaord();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentFeeStatus_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.feeDueList = ds.Tables[0].AsEnumerable().Select(r => new StudentFeeList
                    {
                        isSelected = r.Field<bool>("isSelected"),
                        sno = r.Field<long>("sno"),
                        ERPNo = r.Field<string>("ERPNo"),
                        Installment= r.Field<string>("Installment"),
                        DueDate = r.Field<string>("DueDate"),
                        Duration = r.Field<string>("Duration"),
                        Payable = r.Field<int>("Payable"),
                        PaidAmount = r.Field<int>("PaidAmount"),
                        Balance = r.Field<int>("Balance")

                    }).ToList();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    model.feePaidList = ds.Tables[1].AsEnumerable().Select(r => new StudentFeeList
                    {
                        sno = r.Field<long>("sno"),
                        ERPNo = r.Field<string>("ERPNo"),
                        isSelected= r.Field<bool>("isSelected"),
                        DueDate = r.Field<string>("DueDate"),
                        Duration = r.Field<string>("Duration"),
                        Payable = r.Field<int>("Payable"),
                        PaidAmount = r.Field<int>("PaidAmount"),
                        Balance = r.Field<int>("Balance"),
                        ReceiptNos = r.Field<string>("ReceiptNos"),
                        transRefNo= r.Field<string>("transRefNo")

                    }).ToList();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    model.OpeningBalance = Convert.ToInt32( ds.Tables[2].Rows[0]["OpeningBalance"]);
                    model.Discount = Convert.ToInt32(ds.Tables[2].Rows[0]["Discount"]);
                    model.FeePaid = Convert.ToInt32(ds.Tables[2].Rows[0]["FeePaid"]);
                    model.TransportPaid = Convert.ToInt32(ds.Tables[2].Rows[0]["TransportPaid"]);
                    model.CreditNote = Convert.ToInt32(ds.Tables[2].Rows[0]["creditNote"]);
                    model.amtToPay = Convert.ToInt32(ds.Tables[2].Rows[0]["amtToPay"]);
                    model.ClosingBalance = Convert.ToInt32(ds.Tables[2].Rows[0]["Balance"]);
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    model.SessionName = ds.Tables[3].Rows[0]["SessionName"].ToString();
                    model.ERP = ds.Tables[3].Rows[0]["ERP"].ToString();
                    model.AdmNo = ds.Tables[3].Rows[0]["AdmNo"].ToString();
                    model.Student = ds.Tables[3].Rows[0]["Student"].ToString();
                    model.Father = ds.Tables[3].Rows[0]["Father"].ToString();
                    model.Class = ds.Tables[3].Rows[0]["ClassName"].ToString();
                    model.Address = ds.Tables[3].Rows[0]["Address"].ToString();
                    model.SMSNo = ds.Tables[3].Rows[0]["SMSNo"].ToString();
                    model.FeeStructureName = ds.Tables[3].Rows[0]["FeeStructureName"].ToString();
                }
                model.studentImage = studentImage;
                return model;
            }
        }

        public StudentFeeDashbaord GetFeeStatusDetailsList_StudDash(string UserID, int SessionID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StudentFeeDashbaord model = new StudentFeeDashbaord();
                //List<StudentFeeStatusModel> feeDetailsList = new List<StudentFeeStatusModel>();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentFeeStatus_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.feeDueList = ds.Tables[0].AsEnumerable().Select(r => new StudentFeeList
                    {
                        isSelected = r.Field<bool>("isSelected"),
                        sno = r.Field<long>("sno"),
                        ERPNo = r.Field<string>("ERPNo"),
                        DueDate = r.Field<string>("DueDate"),
                        Duration = r.Field<string>("Duration"),
                        Payable = r.Field<int>("Payable"),
                        PaidAmount = r.Field<int>("PaidAmount"),
                        Balance = r.Field<int>("Balance")

                    }).ToList();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    model.feePaidList = ds.Tables[1].AsEnumerable().Select(r => new StudentFeeList
                    {
                        sno = r.Field<long>("sno"),
                        ERPNo = r.Field<string>("ERPNo"),
                        DueDate = r.Field<string>("DueDate"),
                        Duration = r.Field<string>("Duration"),
                        Payable = r.Field<int>("Payable"),
                        PaidAmount = r.Field<int>("PaidAmount"),
                        Balance = r.Field<int>("Balance"),
                        ReceiptNos = r.Field<string>("ReceiptNos")
                    }).ToList();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    model.OpeningBalance = Convert.ToInt32(ds.Tables[2].Rows[0]["OpeningBalance"]);
                    model.Discount = Convert.ToInt32(ds.Tables[2].Rows[0]["Discount"]);
                    model.FeePaid = Convert.ToInt32(ds.Tables[2].Rows[0]["FeePaid"]);
                    model.TransportPaid = Convert.ToInt32(ds.Tables[2].Rows[0]["TransportPaid"]);
                    model.CreditNote = Convert.ToInt32(ds.Tables[2].Rows[0]["creditNote"]);
                    model.amtToPay = Convert.ToInt32(ds.Tables[2].Rows[0]["amtToPay"]);
                    model.ClosingBalance = Convert.ToInt32(ds.Tables[2].Rows[0]["Balance"]);
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    model.SessionName = ds.Tables[3].Rows[0]["SessionName"].ToString();
                    model.ERP = ds.Tables[3].Rows[0]["ERP"].ToString();
                    model.AdmNo = ds.Tables[3].Rows[0]["AdmNo"].ToString();
                    model.Student = ds.Tables[3].Rows[0]["Student"].ToString();
                    model.Father = ds.Tables[3].Rows[0]["Father"].ToString();
                    model.Class = ds.Tables[3].Rows[0]["ClassName"].ToString();
                    model.Address = ds.Tables[3].Rows[0]["Address"].ToString();
                    model.SMSNo = ds.Tables[3].Rows[0]["SMSNo"].ToString();
                    model.FeeStructureName = ds.Tables[3].Rows[0]["FeeStructureName"].ToString();
                }
                return model;
            }
        }

        public StudentLedgerDetailsModel GetFeeStatusDetailsList_StudDash1_OLD(string UserID, int SessionID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StudentLedgerDetailsModel model = new StudentLedgerDetailsModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentFeeStatus1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.FeeDetails = ds.Tables[0].AsEnumerable().Select(r => new StudentFeeStatusModel
                {
                    DueDate = r.Field<string>("DueDate"),
                    InvoiceNo = r.Field<string>("InvoiceNo"),
                    Duration = r.Field<string>("Duration"),
                    Due = r.Field<int>("Due"),
                    Paid = r.Field<int>("Paid"),
                    ReceiptNo = r.Field<string>("ReceiptNo"),
                    Bill = r.Field<string>("Bill"),
                    Status = r.Field<string>("Status"),
                    Balance = r.Field<int>("Balance"),
                    Excess = r.Field<int>("Excess"),
                    ERPNo = UserID, //r.Field<string>("ERPNo"),
                    ReceiptDate = r.Field<string>("transdate"),
                    sno = r.Field<long>("sno"),
                    Discount = r.Field<int>("Discount"),
                    LedgerInHand = r.Field<int>("LedgerInHand"),
                    Mode = r.Field<string>("Mode"),
                    Concession = r.Field<int>("Concession"),
                    Advance = r.Field<int>("Advance"),
                    Payable = r.Field<int>("Payable"),





                }).ToList();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    model.OpeningBalance = Convert.ToInt32(ds.Tables[1].Rows[0]["Opening Balance"]);
                    model.Discount = Convert.ToInt32(ds.Tables[1].Rows[0]["Discount"]);
                    model.FeePaid = Convert.ToInt32(ds.Tables[1].Rows[0]["Fee Paid"]);
                    model.TransportPaid = Convert.ToInt32(ds.Tables[1].Rows[0]["Transport Paid"]);
                    model.CreditNote = Convert.ToInt32(ds.Tables[1].Rows[0]["creditNote"]);
                    model.amtToPay = Convert.ToInt32(ds.Tables[1].Rows[0]["amtToPay"]);
                    model.ClosingBalance = Convert.ToInt32(ds.Tables[1].Rows[0]["Balance"]);
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    model.SessionName = ds.Tables[2].Rows[0][0].ToString();
                    model.ERP = ds.Tables[2].Rows[0][1].ToString();
                    model.AdmNo = ds.Tables[2].Rows[0][2].ToString();
                    model.Student = ds.Tables[2].Rows[0][3].ToString();
                    model.Father = ds.Tables[2].Rows[0][4].ToString();
                    model.Class = ds.Tables[2].Rows[0][5].ToString();
                    model.Address = ds.Tables[2].Rows[0][6].ToString();
                    model.SMSNo = ds.Tables[2].Rows[0][7].ToString();
                    model.FeeStructureName = ds.Tables[2].Rows[0][8].ToString();
                }

                return model;
            }
        }

        public StudentLedgerDetailsModel GetFeeStatusDetailsList_StudDash1(string UserID, int SessionID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StudentLedgerDetailsModel model = new StudentLedgerDetailsModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_StudentLedgerNew", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.FeeDetails = ds.Tables[0].AsEnumerable().Select(r => new StudentFeeStatusModel
                {
                    sno = r.Field<long>("Sno"),
                    Duration = r.Field<string>("Duration"),
                    DueDate = r.Field<string>("DueDate"),
                    ERPNo = r.Field<string>("ERP"),
                    Due = r.Field<int>("Due"),
                    Concession = r.Field<int>("Concession"),
                    Discount = r.Field<int>("discount"),
                    Paid = r.Field<int>("ActualPaid"),
                    Advance = r.Field<int>("advance"),
                    Payable = r.Field<int>("Payable"),
                    Balance = r.Field<int>("Balance"),
                    Status = r.Field<string>("Status")
                }).ToList();

                model.Student = ds.Tables[1].Rows[0][0].ToString();
                model.AdmNo = ds.Tables[1].Rows[0][1].ToString();
                model.Father = ds.Tables[1].Rows[0][2].ToString();
                model.ERP = ds.Tables[1].Rows[0][3].ToString();
                model.OpeningBalance = Convert.ToInt32(ds.Tables[1].Rows[0][4]);
                model.CreditNote = Convert.ToInt32(ds.Tables[1].Rows[0][5]);
                model.Discount = Convert.ToInt32(ds.Tables[1].Rows[0][6]);
                model.amtToPay = Convert.ToInt32(ds.Tables[1].Rows[0][8]);
                model.ClosingBalance = Convert.ToInt32(ds.Tables[1].Rows[0][10]);
                model.FeePaid = Convert.ToInt32(ds.Tables[1].Rows[0][11]);
                model.TransportPaid = Convert.ToInt32(ds.Tables[1].Rows[0][12]);
                model.Class = ds.Tables[1].Rows[0][13].ToString();
                model.SessionName = ds.Tables[1].Rows[0][14].ToString();
                model.Address = ds.Tables[1].Rows[0][15].ToString();
                model.SMSNo = ds.Tables[1].Rows[0][16].ToString();
                model.FeeStructureName = ds.Tables[1].Rows[0][17].ToString();
                return model;
            }
        }

        public List<StudentFeeDetailsModel> GetFeeDetailsListByDueDate(string UserID, string DueDate, string FeeMode, int SessionID)
        {
            List<StudentFeeDetailsModel> feeDetailsList = new List<StudentFeeDetailsModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_GetInstallmentStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);
                cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate).Date);
                cmd.Parameters.AddWithValue("@FeeMode", FeeMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                feeDetailsList = ds.Tables[0].AsEnumerable().Select(r => new StudentFeeDetailsModel
                {
                    Duration = r.Field<string>("Duration"),
                    DueDate = r.Field<string>("DueDate"),
                    HeadName = r.Field<string>("HeadName"),
                    FeeHead = r.Field<string>("FeeHead"),
                    Due = r.Field<int>("Due"),
                    Paid = r.Field<int>("Paid"),
                    TransDate = r.Field<string>("TransDate"),
                    Status = r.Field<string>("Status"),
                }).ToList();
            }
            return feeDetailsList;
        }

        public FeeBillingInfoModel GetFeeBillingInfo(string ERPNo, string DueDate, string SessionID, string userid)
        {
            FeeBillingInfoModel model = new FeeBillingInfoModel();
            DataSet ds = new DataSet();
            model.DueDate = DueDate;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_OnlinePayment_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);
                //cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate).Date);
                cmd.Parameters.AddWithValue("@DueDate", "");
                cmd.Parameters.AddWithValue("@DisplayDueDate", DueDate);
                cmd.Parameters.AddWithValue("@userId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                FeeBillingInfoModel schoolInfo = ds.Tables[0].AsEnumerable().Select(r => new FeeBillingInfoModel
                {
                    ClientName = r.Field<string>("ClientName"),
                    Add1 = r.Field<string>("Add1"),
                    Add2 = r.Field<string>("Add2"),
                    Ph = r.Field<string>("Ph"),
                }).FirstOrDefault();

                FeeBillingInfoModel studentInfo = ds.Tables[1].AsEnumerable().Select(r => new FeeBillingInfoModel
                {
                    ERP = r.Field<string>("ERP"),
                    AdmNo = r.Field<string>("AdmNo"),
                    Student = r.Field<string>("Student"),
                    Class = r.Field<string>("Class"),
                    Father = r.Field<string>("Father"),
                    SMSNo = r.Field<string>("SMSNo"),
                    InvoiceNo = r.Field<string>("InvoiceNo"),
                    Duration = r.Field<string>("Duration"),
                }).FirstOrDefault();

                if (schoolInfo != null)
                {
                    model.Add1 = schoolInfo.Add1;
                    model.ClientName = schoolInfo.ClientName;
                    model.Add2 = schoolInfo.Add2;
                    model.Ph = schoolInfo.Ph;
                }

                if (studentInfo != null)
                {
                    model.ERP = studentInfo.ERP;
                    model.AdmNo = studentInfo.AdmNo;
                    model.Student = studentInfo.Student;
                    model.Father = studentInfo.Father;
                    model.Class = studentInfo.Class;
                    model.SMSNo = studentInfo.SMSNo;
                    model.Duration = studentInfo.Duration;
                    model.InvoiceNo = studentInfo.InvoiceNo;
                }

                model.FeeDetailList = ds.Tables[2].AsEnumerable().Select(r => new FeeBillingInfo_FeeDetailModel
                {
                    Due = r.Field<int>("Due"),
                    DueDate = r.Field<string>("DueDate"),
                    FeeHead = r.Field<string>("FeeHead"),
                    HeadName = r.Field<string>("HeadName"),
                    Paid = r.Field<int>("Paid"),
                    Status = r.Field<string>("Status"),
                    TransDate = r.Field<string>("TransDate"),
                    Duration = r.Field<string>("Duration"),

                }).ToList();
            }
            return model;
        }


        public DataSet GetFeeDetails_ForReport(string UserID, string TransRefNo, string Mode, int SessionID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_GetFeeBills", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);
                cmd.Parameters.AddWithValue("@TransRefNo", TransRefNo);
                cmd.Parameters.AddWithValue("@Mode", Mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }

        public DataSet GetFeeInvoiceDetails_ForReport(string erpNo, string sessionId, string duedate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_GetFeeInvoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@DueDate", duedate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }
        public DataSet GetFeeDetails_ForStudentCopy(string UserID, string TransRefNo, int SessionID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string tranrefno = TransRefNo.Substring(1, TransRefNo.IndexOf("(")-2) ;
                
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_GetFeeBills", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@SessionId", SessionID);
                cmd.Parameters.AddWithValue("@TransRefNo", tranrefno);
                cmd.Parameters.AddWithValue("@Mode", "RECEIPT");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }
        public DataSet NoDuesRecept(string erpNo, string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_NoDueBills", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }
    }
}

