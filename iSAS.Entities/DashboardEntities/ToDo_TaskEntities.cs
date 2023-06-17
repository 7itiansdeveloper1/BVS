using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.DashboardEntities
{
    public class ToDo_TaskEntitiesModel
    {
        public int ToDoId { get; set; }
        public string ReferenceId { get; set; }

        [Display(Name ="Date")]
        [Required]
        public string ToDoDate { get; set; }

        [Display(Name = "Subject")]
        [Required]
        [StringLength(50)]
        public string ToDoSubject { get; set; }

        [Display(Name = "Description")]
        [Required]
        [StringLength(500)]
        public string ToDoDescription { get; set; }

        public string CRUDMode { get; set; }
        public bool IsCompleted { get; set; }
    }
}
