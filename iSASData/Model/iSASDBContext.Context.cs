﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class iSASDB : DbContext
    {
        public iSASDB()
            : base("name=iSASDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Exam_AssessmentMaster> Exam_AssessmentMaster { get; set; }
        public virtual DbSet<Exam_ExamMaster> Exam_ExamMaster { get; set; }
        public virtual DbSet<Exam_GradingMaster> Exam_GradingMaster { get; set; }
        public virtual DbSet<Exam_SubjectMaster> Exam_SubjectMaster { get; set; }
        public virtual DbSet<Fee_StructureMaster> Fee_StructureMaster { get; set; }
        public virtual DbSet<School_AdmissionCategory> School_AdmissionCategory { get; set; }
        public virtual DbSet<School_AreaMaster> School_AreaMaster { get; set; }
        public virtual DbSet<School_AuthModuleMaster> School_AuthModuleMaster { get; set; }
        public virtual DbSet<School_BloodGroupMaster> School_BloodGroupMaster { get; set; }
        public virtual DbSet<School_CategoryMaster> School_CategoryMaster { get; set; }
        public virtual DbSet<School_CityMaster> School_CityMaster { get; set; }
        public virtual DbSet<School_ClassMaster> School_ClassMaster { get; set; }
        public virtual DbSet<School_ClubMaster> School_ClubMaster { get; set; }
        public virtual DbSet<School_CountryMaster> School_CountryMaster { get; set; }
        public virtual DbSet<School_DesignationMaster> School_DesignationMaster { get; set; }
        public virtual DbSet<School_DocMaster> School_DocMaster { get; set; }
        public virtual DbSet<School_DocumentMaster> School_DocumentMaster { get; set; }
        public virtual DbSet<School_HouseMaster> School_HouseMaster { get; set; }
        public virtual DbSet<School_MeritParameterMaster> School_MeritParameterMaster { get; set; }
        public virtual DbSet<School_MinorityCategoryMaster> School_MinorityCategoryMaster { get; set; }
        public virtual DbSet<School_NationalityMaster> School_NationalityMaster { get; set; }
        public virtual DbSet<School_ProfessionMaster> School_ProfessionMaster { get; set; }
        public virtual DbSet<School_QualificationMaster> School_QualificationMaster { get; set; }
        public virtual DbSet<School_ReligonMaster> School_ReligonMaster { get; set; }
        public virtual DbSet<School_RoleMaster> School_RoleMaster { get; set; }
        public virtual DbSet<School_SectionMaster> School_SectionMaster { get; set; }
        public virtual DbSet<School_SessionMaster> School_SessionMaster { get; set; }
        public virtual DbSet<School_StateMaster> School_StateMaster { get; set; }
        public virtual DbSet<School_UserRoleMaster> School_UserRoleMaster { get; set; }
        public virtual DbSet<School_UsersInfo> School_UsersInfo { get; set; }
        public virtual DbSet<School_WingMaster> School_WingMaster { get; set; }
        public virtual DbSet<Staff_DeptMaster> Staff_DeptMaster { get; set; }
        public virtual DbSet<Staff_ShiftMaster> Staff_ShiftMaster { get; set; }
        public virtual DbSet<Staff_StaffDetailMaster> Staff_StaffDetailMaster { get; set; }
        public virtual DbSet<Staff_TimeGroupMaster> Staff_TimeGroupMaster { get; set; }
        public virtual DbSet<Student_AdmissionMaster> Student_AdmissionMaster { get; set; }
        public virtual DbSet<Transport_PickedUpBy> Transport_PickedUpBy { get; set; }
        public virtual DbSet<Transport_RouteMaster> Transport_RouteMaster { get; set; }
        public virtual DbSet<Transport_StopMaster> Transport_StopMaster { get; set; }
        public virtual DbSet<Transport_TransportMode> Transport_TransportMode { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<webpages_Membership> webpages_Membership { get; set; }
        public virtual DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual DbSet<webpages_Roles> webpages_Roles { get; set; }
        public virtual DbSet<Fee_ClassWiseDue> Fee_ClassWiseDue { get; set; }
        public virtual DbSet<Outbox> Outboxes { get; set; }
        public virtual DbSet<Sent> Sents { get; set; }
        public virtual DbSet<SMS_GroupMaster> SMS_GroupMaster { get; set; }
        public virtual DbSet<Strength> Strengths { get; set; }
        public virtual DbSet<Document_TransactionMaster> Document_TransactionMaster { get; set; }
        public virtual DbSet<Merit_TransactionMaster> Merit_TransactionMaster { get; set; }
        public virtual DbSet<Student_RegistrationMaster> Student_RegistrationMaster { get; set; }
    }
}
