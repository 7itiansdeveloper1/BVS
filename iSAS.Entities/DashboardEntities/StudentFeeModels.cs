
using System.Collections.Generic;

namespace ISas.Entities.DashboardEntities
{
    public class StudentFeeDetailsModel
    {
        public string Duration { get; set; }
        public string DueDate { get; set; }
        public string HeadName { get; set; }
        public string FeeHead { get; set; }
        public int Due { get; set; }
        public int Paid { get; set; }
        public string TransDate { get; set; }
        public string Status { get; set; }
    }

    public class StudentLedgerDetailsModel
    {
        public int OpeningBalance { get; set; }
        public int FeePaid { get; set; }
        public int TransportPaid { get; set; }
        public int CreditNote { get; set; }
        public int ClosingBalance { get; set; }
        public int Discount { get; set; }
        public int amtToPay { get; set; }

        public string SessionName { get; set; }
        public string ERP { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Father { get; set; }
        public string Class { get; set; }
        public string Address { get; set; }
        public string SMSNo { get; set; }
       
        public string FeeStructureName { get; set; }
        public List<StudentFeeStatusModel> FeeDetails { get; set; }
    }
    public class StudentFeeDashbaord
    {
        public int OpeningBalance { get; set; }
        public int FeePaid { get; set; }
        public int TransportPaid { get; set; }
        public int CreditNote { get; set; }
        public int ClosingBalance { get; set; }
        public int Discount { get; set; }
        public int amtToPay { get; set; }

        

        public string SessionName { get; set; }
        public string ERP { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Father { get; set; }
        public string Class { get; set; }
        public string Address { get; set; }
        public string SMSNo { get; set; }
        public string studentImage { get; set; }
        public string FeeStructureName { get; set; }
        public List<StudentFeeList> feeDueList { get; set; }
        public List<StudentFeeList> feePaidList { get; set; }
        public int paidamt { get; set; }
        public string duedatestring { get; set; }
    }

    public class StudentFeeStatusModel
    {
        public List<StudentFeeStatusModel> feeDetailsList { get; set; }
        public string DueDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Duration { get; set; }
        public int Due { get; set; }
        public int Paid { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string Bill { get; set; }
        public string Status { get; set; }
        public int Balance { get; set; }
        public int Excess { get; set; }
        public string ERPNo { get; set; }
        public string Mode { get; set; }

        public int Advance { get; set; }
        public int Payable { get; set; }
        public int Concession { get; set; }

        public int Discount { get; set; }
        public int LedgerInHand { get; set; }

        public string StudentERPNo { get; set; }
        public bool IsCancelable { get; set; }

        //Extra Feild
        public int BalanceAmount
        {
            get
            {
                return Due - Paid;
            }
        }
        public long sno { get; set; }

        public bool EnableNoDueReceipt { get; set; }
    }

    public class StudentFeeList
    {
        public bool isSelected { get; set; }
        public long sno { get; set; }
        public string ERPNo { get; set; }
        public string Installment { get; set; }
        public string DueDate { get; set; }
        public string Duration { get; set; }
        public int Due { get; set; }
        public int creditNote { get; set; }
        public int Concession { get; set; }
        public int Payable { get; set; }
        public int PaidAmount { get; set; }
        public int Balance { get; set; }
        public int Excess { get; set; }
        public string ReceiptNos { get; set; }
        public string transRefNo { get; set; }
        public string Status { get; set; }
    }
    
    public class StudentFeeDetails_RptModel
    {
        //public string ERP { get; set; }
        //public string DOA { get; set; }
        //public string AdmNo { get; set; }
        //public string RollNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        //public string Gender { get; set; }
        //public string DOB { get; set; }
        public string Father { get; set; }
        //public string Mother { get; set; }
        //public string Address { get; set; }
        //public string FMobileNo { get; set; }
        //public string MMobileNo { get; set; }
        //public string SMSNo { get; set; }
        //public string Alternate { get; set; }
        //public string Number { get; set; }
    }

    public class FeeBillingInfoModel
    {
        public FeeBillingInfoModel()
        {
            FeeDetailList = new List<FeeBillingInfo_FeeDetailModel>();
        }

        //School Info
        public string ClientName { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string Ph { get; set; }

        //Student Info
        public string ERP { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string Father { get; set; }
        public string SMSNo { get; set; }
        public string Duration { get; set; }
        public string InvoiceNo { get; set; }
        public string DueDate { get; set; }

        public List<FeeBillingInfo_FeeDetailModel> FeeDetailList { get; set; }
    }

    public class FeeBillingInfo_FeeDetailModel
    {
        public string Duration { get; set; }
        public string DueDate { get; set; }
        public string HeadName { get; set; }
        public string FeeHead { get; set; }
        public int Due { get; set; }
        public int Paid { get; set; }
        public string TransDate { get; set; }
        public string Status { get; set; }
    }
}

