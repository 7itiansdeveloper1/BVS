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
    
    public partial class Exam_AssessmentMaster
    {
        public string AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public string ExamId { get; set; }
        public Nullable<int> PrintOrder { get; set; }
        public Nullable<bool> IsAcademic { get; set; }
        public Nullable<bool> IsNonAcademic { get; set; }
        public Nullable<bool> IsCoScholastic { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsCA { get; set; }
        public Nullable<bool> IsWS { get; set; }
        public string GradingScaleId { get; set; }
    
        public virtual Exam_ExamMaster Exam_ExamMaster { get; set; }
    }
}
