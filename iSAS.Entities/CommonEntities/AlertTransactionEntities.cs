using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.CommonEntities
{
    public class AlertTransactionModel
    {
        public int AlertNo { get; set; }

        [StringLength(500)]
        public string AText { get; set; }
        public string UserId { get; set; }
        public string AlertForUser { get; set; }
        public bool MarkRead { get; set; }
        public bool IsCancelled { get; set; }

        [StringLength(20)]
        public string Mode { get; set; }

        [StringLength(50)]
        public string AlertFor { get; set; }


        public string ATitle { get; set; }
        public string Temp_SendToImageURL { get; set; }
        public string Temp_SendToName { get; set; }
        public string Temp_HTMLClassName { get; set; }
    }

    public class Alert_EventManagerModel
    {
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string EventDiscription { get; set; }
    }
}
