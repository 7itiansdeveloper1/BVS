using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_ConcessionModels
    {
        public Fee_ConcessionModels()
        {
            BankList = new List<SelectListItem>();
            ConcessionTypeList = new List<SelectListItem>();
            HeadList = new List<SelectListItem>();
            InstallmentList = new List<SelectListItem>();
            ConcessionCategoryList = new List<SelectListItem>();
            StudentDetails = new StudentSearchModel();
            ConsessionList = new List<ConsessioinDetailsModel>();
        }

        [Display(Name = "Date")]
        public string CreditNoteDate { get; set; }

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


        public string HeadId { get; set; }
        public string ConcessionCategoryId { get; set; }

        public string ConcessionTypeId { get; set; }
        public decimal ConcessionValue { get; set; }

        public string ConcessionFor { get; set; }
        public string InstallmentId { get; set; }
        public List<SelectListItem> BankList { get; set; }
        public List<SelectListItem> ConcessionTypeList { get; set; }
        public List<SelectListItem> HeadList { get; set; }
        public List<SelectListItem> ConcessionCategoryList { get; set; }
        public List<SelectListItem> InstallmentList { get; set; }
        public StudentSearchModel StudentDetails { get; set; }

        public List<ConsessioinDetailsModel> ConsessionList { get; set; }

        //Extra Feild
        public string CRUDFor { get; set; }

        public string Selected_CreditNoteId { get; set; }
        public string Selected_DueDate { get; set; }
        public string Selected_FeeHeadId { get; set; }
        public string Selected_ConcessionAmount { get; set; }
        public bool IsSingleSave { get; set; }
    }

    public class ConsessioinDetailsModel
    {
        //public string DueDate { get; set; }
        public string HeadId { get; set; }
        public string DisplayDueDate { get; set; }
        public string HeadName { get; set; }

        public int Amount { get; set; }
        public int ConcAmount { get; set; }
        public int DiscAmount { get; set; }
        public int PaidAmount { get; set; }
        public int Balance { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }
        public string CreditNoteRefNo { get; set; }
        public string TransRefNo { get; set; }
        public string ERPNo { get; set; }
        public bool IsSelected { get; set; }
    }
}
