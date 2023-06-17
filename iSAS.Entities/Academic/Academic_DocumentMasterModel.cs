using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Academic
{
    public class Academic_DocumentMasterModel
    {
        public string DocNo { get; set; }

        [StringLength(50, ErrorMessage = "Only 50 Char. is Allowed..!")]
        [Required(ErrorMessage = "Document Name is Req..!")]
        [Display(Name = "Document Name")]
        public string DocName { get; set; }
        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }
        [Display(Name = "Is Active")]
        public bool Active { get; set; }
        [Display(Name = "For Staff")]
        public bool DocForStaff { get; set; }
        [Display(Name = "For Student")]
        public bool DocForStudent { get; set; }
        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
        public bool IsDeletable { get; set; }

    }
}
