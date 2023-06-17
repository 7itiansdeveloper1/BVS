//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iSASData.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class School_NationalityMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public School_NationalityMaster()
        {
            this.Staff_StaffDetailMaster = new HashSet<Staff_StaffDetailMaster>();
            this.Student_AdmissionMaster = new HashSet<Student_AdmissionMaster>();
        }
    
        public string NatID { get; set; }
        public string NatName { get; set; }
        public Nullable<bool> Default { get; set; }
        public Nullable<byte> PrintOrder { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CBy { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
        public Nullable<int> MBy { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Staff_StaffDetailMaster> Staff_StaffDetailMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_AdmissionMaster> Student_AdmissionMaster { get; set; }
    }
}
