using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Academic
{
    public class Academic_FOD
    {
        public int FODId { get; set; }
        [Required(ErrorMessage = "Message Text is Req..!")]
        [Display(Name = "Message Text")]
        public string MessageText { get; set; }

        [Display(Name = "Month Day")]
        public int MonthDay { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public List<Academic_FOD> fodList { get; set; }
    }
}
