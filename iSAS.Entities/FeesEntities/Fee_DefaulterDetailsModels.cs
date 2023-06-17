using ISas.Entities.CommonEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_FilterDefaulterDetailModel
    {
        public Fee_FilterDefaulterDetailModel()
        {
            FeeCategoryList = new List<SelectListItem>();
            InstallmentList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            DefaulterList = new List<Fee_DefaulterDetailsModels>();
            ReportHeaders = new ReportHeaderEntities();

            HeaderNameList1 = new List<string>();
            ValueList1 = new List<List<string>>();

            HeaderNameList2 = new List<string>();
            ValueList2 = new List<List<string>>();
        }

        [Display(Name = "Fee Category")]
        public string FeeCategoryId { get; set; }

        [Display(Name = "Installment")]
        public string InstallmentId { get; set; }

        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Display(Name = "SMS Text")]
        public string SMSText { get; set; }

        [Display(Name = "SMS Type")]
        public string SMSType { get; set; }

        public string Date { get; set; }

        public string DefaulterType { get; set; }
        public string ReportType { get; set; }
        public string SessionId { get; set; }

        public bool IncludeInactive { get; set; }
        public bool IncludeNonAdmitted { get; set; }
        public bool IncludePaid { get; set; }

        public ReportHeaderEntities ReportHeaders { get; set; }


        public List<string> HeaderNameList1 { get; set; }
        public List<List<string>> ValueList1 { get; set; }

        public List<string> HeaderNameList2 { get; set; }
        public List<List<string>> ValueList2 { get; set; }


        public List<SelectListItem> FeeCategoryList { get; set; }
        public List<SelectListItem> InstallmentList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }

        public List<Fee_DefaulterDetailsModels> DefaulterList { get; set; }
    }
    public class Fee_DefaulterDetailsModels
    {
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string Duration { get; set; }
        public int Balance { get; set; }
        public string SMSNo { get; set; }
        public string FatherName { get; set; }

        public bool Selected { get; set; }
    }
    public class DefaulterLetter_ReportModel
    {
        public DefaulterLetter_ReportModel()
        {
            HeaderDetails = new ReportHeaderEntities();
        }
        public string ReportName { get; set; }
        public ReportHeaderEntities HeaderDetails { get; set; }
        public List<DefaultLetter_Report_StudDetailsModel> StudentDetails { get; set; }
    }

    public class DefaultLetter_Report_StudDetailsModel
    {
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Father { get; set; }
        public string Class { get; set; }
        public string Duration { get; set; }
        public int Balance { get; set; }
        public string SMSNo { get; set; }
        public string ByDate { get; set; }

    }
}
