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
    
    public partial class School_MinorityCategoryMaster
    {
        public School_MinorityCategoryMaster()
        {
            this.Student_AdmissionMaster = new HashSet<Student_AdmissionMaster>();
        }
    
        public string MCatID { get; set; }
        public string MCatName { get; set; }
        public Nullable<bool> Default { get; set; }
        public Nullable<byte> PrintOrder { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CBy { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
        public Nullable<int> MBy { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
    
        public virtual ICollection<Student_AdmissionMaster> Student_AdmissionMaster { get; set; }
    }
}
