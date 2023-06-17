using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Academic
{
    public class Academic_ERPBillModel
    {
        public Academic_ERPBillModel()
        {
            invoice = new invoice();
            invoiceDetail = new List<invoiceDetail>();
            invoicesummary = new invoiceSummary();
            sellerparty = new SellerParty();
            billparty = new billParty();
        }

        public invoiceSummary invoicesummary { get; set; }
        public SellerParty sellerparty { get; set; }
        public billParty billparty { get; set; }
        public invoice invoice { get; set; }

        public List<invoiceDetail> invoiceDetail { get; set; }
    }

    public class invoice
    {
        public int invoiceId { get; set; }
        public string invoiceNo { get; set; }
        public string invoiceDate { get; set; }
        public string invoiceFromDate { get; set; }
        public string invoiceToDate { get; set; }
        public string invoiceState { get; set; }
        public string invoiceCode { get; set; }
        public string invoiceGSTNo { get; set; }

    }

    public class invoiceDetail
    {
        public int invoiceId { get; set; }
        public string product { get; set; }
        public string hsnCode { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public int RatePerUnit { get; set; }
        public int discount { get; set; }
        public int taxAbleAmount { get; set; }
        public int tax1Amount { get; set; }
        public int tax2Amount { get; set; }
        public int totalAmount { get; set; }

    }
    public class invoiceSummary
    {
        public int amountBeforeTax { get; set; }
        public int totalTax { get; set; }
        public int amountAfterTax { get; set; }
        public string amountinwWord { get; set; }

    }

    public class SellerParty
    {
        public string FromParty { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string Bankname { get; set; }
        public string bankIFSC { get; set; }
        public string condition1 { get; set; }
        public string condition2 { get; set; }
    }
    public class billParty
    {
        public string BillParty { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string State { get; set; }
    }
}
