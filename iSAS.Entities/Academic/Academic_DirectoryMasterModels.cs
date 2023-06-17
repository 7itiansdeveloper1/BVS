using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Academic
{
    public class Academic_DirectoryMasterModels
    {
        public string DirectoryId { get; set; }

        [Required]
        [Display(Name ="Name / Department")]
        public string Name { get; set; }

        [Required]
        public string Contact { get; set; }


        public string EmailId { get; set; }
        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
