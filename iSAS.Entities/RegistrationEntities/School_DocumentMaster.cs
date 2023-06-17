using System;

namespace ISas.Entities.RegistrationEntities
{
    public class School_DocumentMaster
    {
        public bool IsSelected { get; set; }
        public string DocNo { get; set; }
        public string DocName { get; set; }
        public Nullable<int> PrintOrder { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> DocForStaff { get; set; }
        public Nullable<bool> DocForStudent { get; set; }
        public Nullable<bool> DocForBoth { get; set; }
        public Nullable<int> CBy { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
        public Nullable<int> MBy { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
    }
}
