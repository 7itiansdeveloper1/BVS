using ISas.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.FeesEntities
{
    public class Fee_ReportHtmlModel
    {
        public Fee_ReportHtmlModel()
        {
            ReportHeader = new ReportHeaderEntities();
            StudentInfo = new StudentInfoModel();
            HeadDetails = new List<HeadDetailsModel>();
        }

        public string Duration { get; set; }
        public int? Balance { get; set; }
        public int? Excess { get; set; }
        public string ReceiptStatus { get; set; }
        public int? Exempted{ get; set; }
        public int? TotalDue { get; set; }
        public int? TotalPaid { get; set; }
        public int? AmounttoPay { get; set; }
        public int? AdvanceAmount { get; set; }
        public int? creditNote { get; set; }

        public ReportHeaderEntities ReportHeader { get; set; }
        public StudentInfoModel StudentInfo { get; set; }
        public List<HeadDetailsModel> HeadDetails { get; set; }
    }
    public class StudentInfoModel
    {
        public bool Selected { get; set; }

        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string ERP { get; set; }
        public string DOA { get; set; }
        public string AdmNo { get; set; }
        public string RollNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Address { get; set; }
        public string FMobileNo { get; set; }
        public string MMobileNo { get; set; }
        public string SMSNo { get; set; }
        public string AlternateNumber { get; set; }
        public string TransRefNo { get; set; }
        public string TransMode { get; set; }
        public string TransBank { get; set; }
        public string TransBranch { get; set; }
        public string TransReferenceNo { get; set; }
        public string TransDate { get; set; }
        public string FeeType { get; set; }
        public string TransportRefNo { get; set; }
        public string ClassId { get; set; }
        public string ERPNo1 { get; set; }
        public string PaymentMode { get; set; }
        public string AmountInWord { get; set; }
        public bool IsDraft { get; set; }
    }
    public class HeadDetailsModel
    {
        public string ERPNo { get; set; }
        public string Duration { get; set; }
        public string HeadName { get; set; }
        public int Due { get; set; }
        public int Paid { get; set; }

        public string InvoiceNo { get; set; }
        public string ReceiptNo { get; set; }
        public string Period { get; set; }

      
        public int dueAmount { get; set; }
        public int creditNote { get; set; }
        public int paid { get; set; }
        public short PrintOrder { get; set; }
    }
}
