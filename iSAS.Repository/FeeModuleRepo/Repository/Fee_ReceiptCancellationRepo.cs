using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_ReceiptCancellationRepo : IFee_ReceiptCancellationRepo
    {
        public List<ReceiptDetailModel> GetReceiptDetailList(string FromDate, string ToDate, string QueryFor,
           string FeeType, string ERPNo, string RecNo, string UserId,string sessionId)
        {
            List<ReceiptDetailModel> receiptList = new List<ReceiptDetailModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ReciptCancellation_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(FromDate))
                    cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(FromDate).Date);

                if (!string.IsNullOrEmpty(FromDate))
                    cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(ToDate).Date);


                cmd.Parameters.AddWithValue("@QueryFor", QueryFor);

                cmd.Parameters.AddWithValue("@FeeType", FeeType);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@RecNo", RecNo);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                receiptList = ds.Tables[0].AsEnumerable().Select(r => new ReceiptDetailModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    Amount = r.Field<int>("Amount"),
                    ClassName = r.Field<string>("Class"),
                    Date = r.Field<string>("RecDate"),
                    ERPNo = r.Field<string>("ERPNo"),
                    IsCancel = r.Field<bool>("IsCancel"),
                    IsDishon = r.Field<bool>("IsDishon"),
                    //IsPrint = r.Field<bool>("IsPrint"),
                    //IsView = r.Field<bool>("IsView"),
                    Mode = r.Field<string>("Mode"),
                    ReceiptNo = r.Field<string>("RecNo"),
                    StudentName = r.Field<string>("Student"),
                    TransReferenceNo = r.Field<string>("TransReferenceNo"),
                    Status = r.Field<string>("Status"),
                    NewReceiptNo = r.Field<string>("RecNo"),
                    FeeType = r.Field<string>("FeeType"),
                    IsEditable = r.Field<bool>("IsEditable"),
                }).ToList();
            }
            return receiptList;
        }


        public ReceiptDetailModel GetReceiptDetailById(string FromDate, string ToDate, string QueryFor, 
            string FeeType, string ERPNo, string RecNo, string UserId, string sessionId)
        {
            return GetReceiptDetailList(FromDate, ToDate, QueryFor, FeeType, ERPNo, RecNo, UserId,sessionId).FirstOrDefault();
        }

        public Fee_ReceiptCancellationModels GetDishonurReceiptDetails(string SessionId, string RecptNo, string TransReferenceNo, string ERPNo)
        {
            Fee_ReceiptCancellationModels model = new Fee_ReceiptCancellationModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_EditReceipt_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@RecptNo", RecptNo);
                cmd.Parameters.AddWithValue("@QueryFor", "GetDishonorStudentList");
                cmd.Parameters.AddWithValue("@TransReferenceNo", TransReferenceNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model = ds.Tables[0].AsEnumerable().Select(r => new Fee_ReceiptCancellationModels
                {
                    TransRefNo = r.Field<string>("TransRefNo"),
                    Mode = r.Field<string>("Mode"),
                    TransBank = r.Field<string>("TransBank"),
                    TransBranch = r.Field<string>("TransBranch"),
                    TransReferenceNo = r.Field<string>("TransReferenceNo"),
                    TransDate = r.Field<string>("TransDate"),
                }).FirstOrDefault();

                if (model != null)
                {
                    model.ReceiptDetail_OfDishList = ds.Tables[1].AsEnumerable().Select(r => new ReceiptDetailModel
                    {
                        ERPNo = r.Field<string>("ERPNo"),
                        ReceiptNo = r.Field<string>("ReceiptNo"),
                        AdmNo = r.Field<string>("stud_AdmNo"),
                        StudentName = r.Field<string>("Student"),
                        ClassName = r.Field<string>("Class"),
                        Duration = r.Field<string>("Duration"),
                        Due = r.Field<int>("Due"),
                        Paid = r.Field<int>("Paid"),
                        Balance = r.Field<int>("Balance"),
                        Excess = r.Field<int>("Excess"),
                    }).ToList();

                    if (model.ReceiptDetail_OfDishList != null)
                    {
                        model.ChequeAmt = model.ReceiptDetail_OfDishList.Select(r => r.Paid).DefaultIfEmpty(0).Sum();

                        model.Selected_ReceiptNo = RecptNo;
                        model.Selected_ERPNo = ERPNo;
                        model.DishDate = DateTime.Now.ToShortDateString().Replace("-", "/");
                    }
                }
            }
            return model;
        }

        public Tuple<int, string> Fee_ReceiptCancellation_CRUD(Fee_ReceiptCancellationModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_EditReceipt_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", model.Selected_ERPNo);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@RecptNo", model.Selected_ReceiptNo);
                cmd.Parameters.AddWithValue("@Mode", model.CRUDMode);
                cmd.Parameters.AddWithValue("@DishonFine", model.DishAmt);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                if (!string.IsNullOrEmpty(model.DishDate))
                    cmd.Parameters.AddWithValue("@DishonDate", Convert.ToDateTime(model.DishDate).Date);
                string disERPNos = string.Join(",", model.ReceiptDetail_OfDishList.Where(r => r.Selected).Select(r => r.ERPNo).ToList());
                cmd.Parameters.AddWithValue("@ERPNoList", disERPNos);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public Tuple<int, string> Fee_Receipt_CRUD(ReceiptDetailModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeReceipt_UPDATE", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ERPNo", model.ERPNo);
                cmd.Parameters.AddWithValue("@OldReceiptNo", model.ReceiptNo);
                cmd.Parameters.AddWithValue("@RecType", model.FeeType); // -- ?//
                cmd.Parameters.AddWithValue("@NewReceiptNo", model.NewReceiptNo);
                cmd.Parameters.AddWithValue("@NewTransRefNo", model.TransReferenceNo);

                if(!string.IsNullOrEmpty(model.Date))
                cmd.Parameters.AddWithValue("@NewTransDate", Convert.ToDateTime(model.Date).Date);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

    }
}
