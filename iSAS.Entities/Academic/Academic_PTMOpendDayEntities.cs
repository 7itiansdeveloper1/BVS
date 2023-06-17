using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Academic
{
    public class Academic_PTMOpendDayEntities
    {
        public Academic_PTMOpendDayEntities()
        {
            AttenCategoryList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
        }

        public string SessionId { get; set; }

        [Required(ErrorMessage = "Date is Req..")]
        [Display(Name = "Date")]
        public string AttDate { get; set; }

        [Required(ErrorMessage = "Class is Req..")]
        [Display(Name = "Class")]
        public string ClassId { get; set; }

        public string Class { get; set; }

        [Required(ErrorMessage ="Category is Req..")]
        [Display(Name = "Category")]
        public string Category { get; set; }


        public bool IsActive { get; set; }
        public bool IsDeleteable { get; set; }

        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        public List<SelectListItem> CopyToClassList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> AttenCategoryList { get; set; }
    }
}
