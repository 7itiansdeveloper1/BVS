using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_DueSetupModels
    {
        public Fee_DueSetupModels()
        {
            DueSummeryList = new List<Fee_DueSummery>();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            InstallmentList = new List<SelectListItem>();
            HeadList = new List<SelectListItem>();
            CopyToClassList = new List<SelectListItem>();
        }

        public int RecordId { get; set; }

        [Display(Name = "Installment")]
        public string InstallName { get; set; }

        [Display(Name = "Head Name")]
        public string HeadName { get; set; }

        [Display(Name = "Amount")]
        public int Amount { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }

        [Display(Name ="Class")]
        public string ClassId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }

        public List<Fee_DueSummery> DueSummeryList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> InstallmentList { get; set; }
        public List<SelectListItem> HeadList { get; set; }

        public List<SelectListItem> CopyToClassList { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
        public string StructId { get; set; }
        public string StructName { get; set; }
       
    }

    

    public class Fee_DueSummery
    {
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public string Class { get; set; }
        public int Amount { get; set; }
        public int Strength { get; set; }
        public int AppliedStuent { get; set; }
        public bool IsEditable { get; set; }
        public bool CreateInvoice { get; set; }

        //Extra Feild
        public string StructId { get; set; }
        public string StructName { get; set; }
    }
}
