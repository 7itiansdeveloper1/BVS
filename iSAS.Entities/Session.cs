using System;

namespace ISas.Entities
{
   public class Session
    {
        public string SessId { get; set; }
        public string SessName { get; set; }
        public string SessionDisplayName { get; set; }
        public DateTime SessionStartDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public string SessionFirstAdmNo { get; set; }
        public string SessionLastAdmNo { get; set; }
        public string SessionFirstUID { get; set; }
        public bool IsDefault { get; set; }
        public int PrintOrder { get; set; }
        public bool Active { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
