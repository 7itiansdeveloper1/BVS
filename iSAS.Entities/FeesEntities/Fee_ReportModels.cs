using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_ReportModels
    {
        public Fee_ReportModels()
        {
            ClassSectionList = new List<ClassSectionModel>();
            HeaderNameList1 = new List<string>();
            ValueList1 = new List<List<string>>();

            HeaderNameList2 = new List<string>();
            ValueList2 = new List<List<string>>();

            HeaderNameList3 = new List<string>();
            ValueList3 = new List<List<string>>();
            ReportNameList = new List<SelectListItem>();
            FeeModeList = new List<SelectListItem>();
            FeeCategoryList = new List<SelectListItem>();
            FeeHeadList = new List<SelectListItem>();
        }
        //public List<FeeReportList1> FeeReportNameList { get; set; }

        public string Print { get; set; }
        [Display(Name = "Report Type")]
        public string ReportType { get; set; }
        public string OrderBy { get; set; }

        [Display(Name = "Report Name")]
        public string ReportName { get; set; }
        public bool Status { get; set; }
        public string[] ClassSectionIds { get; set; }

        [Display(Name = "Fee Category")]
        public string FeeCategory { get; set; }

        [Display(Name = "From Date")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        public string ToDate { get; set; }

        [Display(Name = "Fee Mode")]
        public string FeeMode { get; set; }

        [Display(Name = "Fee Type")]
        public string FeeType { get; set; }

        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public string Header4 { get; set; }
        public string ImageURL { get; set; }

        public string SessionId { get; set; }
        public bool IsCrystalEnabled { get; set; }

        public string SelectedReportName { get; set; }
        public string[] SelectedFeeHeadsId { get; set; }
        public List<SelectListItem> FeeModeList { get; set; }
        public List<SelectListItem> FeeHeadList { get; set; }
        public List<SelectListItem> FeeCategoryList { get; set; }
        public List<ClassSectionModel> ClassSectionList { get; set; }
        public List<string> HeaderNameList1 { get; set; }
        public List<List<string>> ValueList1 { get; set; }

        public List<string> HeaderNameList2 { get; set; }
        public List<List<string>> ValueList2 { get; set; }

        public List<string> HeaderNameList3 { get; set; }
        public List<List<string>> ValueList3 { get; set; }
        public List<SelectListItem> ReportNameList { get; set; }
    }
    
}
