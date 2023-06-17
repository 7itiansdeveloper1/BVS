using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_Tran_LandingModel
    {
        public Fee_Tran_LandingModel()
        {
            FeeCollection = new FeeModesModel();
            TransportCollection = new FeeModesModel();
            ClassList = new List<SelectListItem>();
        }

        [Display(Name ="From Date")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        public string ToDate { get; set; }

        public string SearchType { get; set; }
        public string SearchText { get; set; }
        public string SelectedClassID { get; set; }

        public List<SelectListItem> ClassList { get; set; }

        public FeeModesModel FeeCollection { get; set; }
        public FeeModesModel TransportCollection { get; set; }
    }

    public class FeeModesModel
    {
        public int Cash { get; set; }
        public int Cheque { get; set; }
        public int Online { get; set; }
        public int Total { get; set; }
    }

    public class StudentDetailsModel
    {
        public Int64 SNo { get; set; }
        public string AdmNo { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ERPNo { get; set; }
        public string MobileNo { get; set; }
        public string AdmCategoryName { get; set; }
        public string AvailSnacks { get; set; }
        public string AvailTransport { get; set; }

    }

    public class ReceiptDetailModel
    {
        //Don't Delete Property From this Model It was Using at multiple Place
        //By Shailendra Kumar On 31-JAN-2018

        public Int64 SNo { get; set; }
        public string ReceiptNo { get; set; }
        public string Date { get; set; }
        public string AdmNo { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string Mode { get; set; }
        public string ERPNo { get; set; }
        public string Status { get; set; }

        public string TransReferenceNo { get; set; }
        public int Amount { get; set; }
        //public bool IsPrint { get; set; }
        //public bool IsView { get; set; }
        public bool IsCancel { get; set; }
        public bool IsDishon { get; set; }


        public string Duration { get; set; }
        public int Due { get; set; }
        public int Paid { get; set; }
        public int Balance { get; set; }
        public int Excess { get; set; }

        public bool Selected { get; set; }


        public string FeeType { get; set; }
        public bool IsEditable { get; set; }
        public string UserId { get; set; }
        public string NewReceiptNo { get; set; }
    }



    public class FeeReceiptModel
    {
        public FeeReceiptModel()
        {
            InstallmentList = new List<SelectListItem>();
            HeadList = new List<FeeHeadModel>();
            FeeModeList = new List<SelectListItem>();
            BankList = new List<SelectListItem>();
            StudentDetails = new StudentSearchModel();
        }

        [Display(Name = "Receipt Date")]
        public string ReceiptDate { get; set; }

        [Display(Name = "Installment")]
        public string InstallmentId { get; set; }

        [Display(Name = "Fee Type")]
        public string FeeType { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Amt. Due")]
        public int AmountToPay { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Advance")]
        public int Advance { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Discount")]
        public int Discount { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Net Pay")]
        public int NetPay { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "In Hand")]
        public int PaidAmount { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Balance")]
        public int Balance { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Excess")]
        public int Excess { get; set; }


        [Display(Name = "Mode")]
        public string FeeModeId { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Amount")]
        public int TrnAmount { get; set; }

        [Display(Name = "Bank")]
        public string SelectedBankId { get; set; }

        [Display(Name = "Branch")]
        public string BranchName { get; set; }

        [Display(Name = "Trans. No")]
        public string TransactionNo { get; set; }

        [Display(Name = "Trans. Date")]
        public string TransactionDate { get; set; }

        [Display(Name = "Print On Save")]
        public bool IsPrintOnSave { get; set; }

        [Display(Name = "Late Fee")]
        public int LateFee { get; set; }
        public int MinFine { get; set; }
        public bool IsDiscountDisable { get; set; }
        public int MaxDiscountAmt { get; set; }
        public string HddnERPNo { get; set; }

        public StudentSearchModel StudentDetails { get; set; }

        public List<SelectListItem> InstallmentList { get; set; }
        public List<FeeHeadModel> HeadList { get; set; }
        public List<SelectListItem> FeeModeList { get; set; }
        public List<SelectListItem> BankList { get; set; }
    }

    public class FeeHeadModel
    {
        public string HeadName { get; set; }
        public int Amount { get; set; }
        public string DueDate { get; set; }
        public string Duration { get; set; }
    }
}
