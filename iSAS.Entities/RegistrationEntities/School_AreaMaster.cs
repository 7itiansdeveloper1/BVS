using System;

namespace ISas.Entities.RegistrationEntities
{
    public class School_AreaMaster
    {
        public string AreaID { get; set; }
        public string AreaName { get; set; }
        public int PrintOrder { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CBy { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
        public Nullable<int> MBy { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
    }
}
