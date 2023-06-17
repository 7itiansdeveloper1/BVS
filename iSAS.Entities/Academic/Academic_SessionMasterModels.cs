using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.Academic
{
    public class Academic_SessionMasterModel
    {
        
        public string SessId { get; set; }

        [Required(ErrorMessage ="Session Name is Requried..!")]
        [Display(Name = "Session Name")]
        public string SessionDisplayName { get; set; }

        [Required(ErrorMessage = "Session is Requried..!")]
        [Display(Name = "Session")]
        public string Session { get; set; }

        [Required(ErrorMessage = "Start date is Requried..!")]
        [Display(Name = "Start Date")]
        public string SDate { get; set; }

        [Required(ErrorMessage = "End date is Requried..!")]
        [Display(Name = "End Date")]
        public string EDate { get; set; }

        
        [Display(Name = "Start Adm. No.")]
        public string SAdmNo { get; set; }

        [Display(Name = "End Adm. No.")]
        public string EAdmNo { get; set; }

        [Required(ErrorMessage = "ERP No. is Requried..!")]
        [Display(Name = "ERP No.")]
        public string UID { get; set; }

        public bool IsDefault { get; set; }

        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Promotion Session")]
        public string PromotionSessionId { get; set; }
        //public string PSesssion { get; set; }

        [Display(Name = "Demotion Session")]
        public string DemotioinSessionId { get; set; }
        //public string DSesssion { get; set; }
        public string UserId { get; set; }
        public List<SelectListItem> SessionList { get; set; }
    }
}
