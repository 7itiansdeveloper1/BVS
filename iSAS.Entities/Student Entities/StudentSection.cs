using System;


namespace ISas.Entities
{
   public class StudentSection
    {
        public string SecId { get; set; }
        public string SecName { get; set; }
        public int PrintOrder { get; set; }
        public bool Active { get; set; }
        
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
