//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ISas.Web.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Staff_DeptMaster
    {
        public Staff_DeptMaster()
        {
            this.Staff_StaffDetailMaster = new HashSet<Staff_StaffDetailMaster>();
        }
    
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public Nullable<bool> Default { get; set; }
        public Nullable<byte> PrintOrder { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CBy { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
        public Nullable<int> MBy { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
    
        public virtual ICollection<Staff_StaffDetailMaster> Staff_StaffDetailMaster { get; set; }
    }
}
