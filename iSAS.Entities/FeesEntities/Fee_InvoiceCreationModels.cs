using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_InvoiceCreationModels
    {
        public Fee_InvoiceCreationModels()
        {
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            FeeCategoryList = new List<SelectListItem>();
            FeeInvoiceList = new List<InvoiceDetails>();
            FeeStrectureList = new List<FeeStructureDetails>();
        }

        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Display(Name = "Fee Category")]
        public string FeeCategoryId { get; set; }

        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> FeeCategoryList { get; set; }
        public List<InvoiceDetails> FeeInvoiceList { get; set; }
        public List<FeeStructureDetails> FeeStrectureList { get; set; }

        //Extra Feild
        public bool IsSingleSave { get; set; }
        public string SelectERPNoId { get; set; }
    }
    public class InvoiceDetails
    {
        public string AdmNo { get; set; }
        public string ERPNo { get; set; }
        public string FeeCategory { get; set; }
        public bool SelectERPNoId { get; set; }
        public string StudentName { get; set; }
        public bool IsCreateInvoice { get; set; }
    }
    public class FeeStructureDetails
    {
        public string Installment { get; set; }
        public string DueDate { get; set; }
        public string Head { get; set; }
        public int Due { get; set; }
    }

    public class CancelInvoice_InvoiceDetailsModel
    {
        public bool IsSelected { get; set; }
        public string ERPNo { get; set; }
        public string TransRefNo { get; set; }
        public string Duration { get; set; }
        public int Amount { get; set; }
        public int PaidAmount { get; set; }
        public string InvoiceStatus { get; set; }
        public bool IsCancelable { get; set; }
    }
}
