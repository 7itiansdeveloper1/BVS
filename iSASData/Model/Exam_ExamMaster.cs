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
    
    public partial class Exam_ExamMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exam_ExamMaster()
        {
            this.Exam_AssessmentMaster = new HashSet<Exam_AssessmentMaster>();
        }
    
        public string ExamID { get; set; }
        public string ExamName { get; set; }
        public Nullable<int> PrintOrder { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> ExamDate { get; set; }
        public string ReportCardDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exam_AssessmentMaster> Exam_AssessmentMaster { get; set; }
    }
}
