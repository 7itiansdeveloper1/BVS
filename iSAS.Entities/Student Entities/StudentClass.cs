using System;

namespace ISas.Entities
{
   public class StudentClass
    {
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string WingID { get; set; }
        public string RealClass { get; set; }
        public string PromotedClass { get; set; }
        public bool Active { get; set; }
        public bool IsAlumniClass { get; set; }
        public int PrintOrder { get; set; }
        
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}
