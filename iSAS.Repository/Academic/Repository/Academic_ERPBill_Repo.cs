using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_ERPBill_Repo : IAcademic_ERPBill_Repo
    {
        public Academic_ERPBillModel GetERPBill()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Academic_ERPBillModel model = new Academic_ERPBillModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ERPInvoice_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.invoice.invoiceId = Convert.ToInt32(ds.Tables[0].Rows[0]["invoiceId"]);
                    model.invoice.invoiceNo = ds.Tables[0].Rows[0]["invoiceNo"].ToString();
                    model.invoice.invoiceDate = ds.Tables[0].Rows[0]["invoiceDate"].ToString();
                    model.invoice.invoiceFromDate = ds.Tables[0].Rows[0]["invoiceFromDate"].ToString();
                    model.invoice.invoiceToDate = ds.Tables[0].Rows[0]["invoiceToDate"].ToString();
                    model.invoice.invoiceState = ds.Tables[0].Rows[0]["invoiceState"].ToString();
                    model.invoice.invoiceCode = ds.Tables[0].Rows[0]["invoiceCode"].ToString();
                    model.invoice.invoiceGSTNo = ds.Tables[0].Rows[0]["invoiceGSTNo"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    model.invoiceDetail = ds.Tables[1].AsEnumerable().Select(r => new invoiceDetail
                    {
                        invoiceId = r.Field<int>("invoiceId"),
                        product = r.Field<string>("product"),
                        hsnCode = r.Field<string>("hsnCode"),
                        Qty = r.Field<int>("Qty"),
                        Unit = r.Field<string>("Unit"),
                        RatePerUnit = r.Field<int>("RatePerUnit"),
                        discount = r.Field<int>("discount"),
                        taxAbleAmount = r.Field<int>("taxAbleAmount"),
                        tax1Amount = r.Field<int>("tax1Amount"),
                        tax2Amount = r.Field<int>("tax2Amount"),
                        totalAmount = r.Field<int>("totalAmount"),
                    }).ToList();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    model.invoicesummary.amountBeforeTax = Convert.ToInt32(ds.Tables[2].Rows[0]["amountBeforeTax"]);
                    model.invoicesummary.totalTax = Convert.ToInt32(ds.Tables[2].Rows[0]["totalTax"]);
                    model.invoicesummary.amountAfterTax = Convert.ToInt32(ds.Tables[2].Rows[0]["amountAfterTax"]);
                    model.invoicesummary.amountinwWord = ds.Tables[2].Rows[0]["amountinwWord"].ToString();

                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    model.billparty.BillParty = ds.Tables[3].Rows[0]["BillParty"].ToString();
                    model.billparty.address1 = ds.Tables[3].Rows[0]["address1"].ToString();
                    model.billparty.address2 = ds.Tables[3].Rows[0]["address2"].ToString();
                    model.billparty.Phone = ds.Tables[3].Rows[0]["Phone"].ToString();
                    model.billparty.EMail = ds.Tables[3].Rows[0]["EMail"].ToString();
                    model.billparty.State = ds.Tables[3].Rows[0]["State"].ToString();

                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    model.sellerparty.FromParty = ds.Tables[4].Rows[0]["FromParty"].ToString();
                    model.sellerparty.address1 = ds.Tables[4].Rows[0]["address1"].ToString();
                    model.sellerparty.address2 = ds.Tables[4].Rows[0]["address2"].ToString();
                    model.sellerparty.Phone = ds.Tables[4].Rows[0]["Phone"].ToString();
                    model.sellerparty.EMail = ds.Tables[4].Rows[0]["EMail"].ToString();
                    model.sellerparty.Bankname = ds.Tables[4].Rows[0]["Bankname"].ToString();
                    model.sellerparty.bankIFSC = ds.Tables[4].Rows[0]["bankIFSC"].ToString();
                    model.sellerparty.condition1 = ds.Tables[4].Rows[0]["condition1"].ToString();
                    model.sellerparty.condition2 = ds.Tables[4].Rows[0]["condition2"].ToString();
                }
                return model;
            }
        }
    }
}
