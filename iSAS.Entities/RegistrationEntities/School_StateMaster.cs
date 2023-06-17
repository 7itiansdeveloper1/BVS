using System;

namespace ISas.Entities.RegistrationEntities
{
    public class School_StateMaster
    {
        public string StateID { get; set; }
        public string StateName { get; set; }
        public Nullable<bool> Default { get; set; }
        public int PrintOrder { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CBy { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
        public Nullable<int> MBy { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
    }
}
