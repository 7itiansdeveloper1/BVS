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
    
    public partial class School_DocumentMaster
    {
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
