using System;

namespace ISas.Entities.RegistrationEntities
{
    class School_ClassMaster
    {
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string WingID { get; set; }
        public Nullable<int> RealClass { get; set; }
        public string PromotedClass { get; set; }
        public Nullable<byte> PrintOrder { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> IsAlumniClass { get; set; }
        public Nullable<int> CBy { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
        public Nullable<int> MBy { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
    }
}
