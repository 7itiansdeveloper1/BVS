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
    
    public partial class School_UsersInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public Nullable<System.Guid> Salt { get; set; }
        public string DisplayName { get; set; }
        public string UserReferenceNo { get; set; }
        public Nullable<int> UserRole { get; set; }
        public string UserEmail { get; set; }
        public string UserRecoveryEmail { get; set; }
        public string UserMobileNo { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CBy { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
        public Nullable<int> MBy { get; set; }
        public Nullable<System.DateTime> MDate { get; set; }
    }
}
