using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Examination_Entities
{
    public class Examination_RemarksTempleteModels
    {
        public string TemplateId { get; set; }

        [Display(Name = "Template Name")]
        [Required(ErrorMessage ="Templete Name is Req..!")]
        public string TemplateName { get; set; }
        public bool IsRemarkTemplate { get; set; }
        public bool IsAcheivementTemplate { get; set; }
        public string Classes { get; set; }

        public string UserId { get; set; }
    }

    public class Examination_RemarksCenterModels
    {
        public string TempleteName { get; set; }
        public string TempleteId { get; set; }
        public string RemarkId { get; set; }

        [Required(ErrorMessage ="Remark is Req..!")]
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleteable { get; set; }
        public string UserId { get; set; }
    }

    public class Examination_TempleteClassSetupModel
    {
        public Examination_TempleteClassSetupModel()
        {
            ClassList = new List<SelectListItem>();
        }
        public string RemarksTempleteId { get; set; }
        public string UserId { get; set; }

        public List<SelectListItem> ClassList { get; set; }
    }
}
