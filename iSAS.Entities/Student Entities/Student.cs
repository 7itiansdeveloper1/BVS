using ISas.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities
{
    public class Student
    {
        public string StudentERPNo { get; set; }
        public string StudentAdmNo { get; set; }
        public int StudentRollNo { get; set; }
        public string StudentName { get; set; }
        public string StudentClass { get; set; }
        public string StudentSection { get; set; }
        public string StudentAadhaarNo { get; set; }
        public DateTime StudentDateOfBirth { get; set; }
        public DateTime StudentDateOfAdmission { get; set; }

        public string FatherName { get; set; }
        public string FatherProfession { get; set; }
        public string FatherQualification { get; set; }
        public string FatherMobileNo { get; set; }
        public string FatherEmailId { get; set; }
        public string FatherAadhaarNo { get; set; }

        public string MotherName { get; set; }
        public string MotherProfession { get; set; }
        public string MotherQualification { get; set; }
        public string MotherMobileNo { get; set; }
        public string MotherEmailId { get; set; }
        public string MotherAadhaarNo { get; set; }

        public string GuardianName { get; set; }

        public string StudentAddress { get; set; }
        public string StudentAddressCity { get; set; }
        public string StudentAddressState { get; set; }
        public string StudentAddressCountry { get; set; }
        public string StudentHomeMobileNo { get; set; }
        public string StudentHomePhNo1 { get; set; }
        public string StudentHomePhNo2 { get; set; }
        public string SmsMobileNo { get; set; }

        public bool IsStudentNso { get; set; }
        public bool IsStudentTc { get; set; }
        public string StudentAdmissionSession { get; set; }
        public string StudentAdmissionClass { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string StuentPhotoURL { get; set; }

        public string FatherPhotoURL { get; set; }

        public string MotherPhotoURL { get; set; }

        public string GardianPhotoURL { get; set; }

        public string GFPhotoURL { get; set; }

        public string GMPhotoURL { get; set; }

        public string PPhotoURL { get; set; }
    }

    public class Student_ReportModel
    {
        public Student_ReportModel()
        {
            ClassSectionList = new List<ClassSectionModel>();
            HeaderNameList = new List<string>();
            ValueList = new List<List<string>>();
            WingList = new List<SelectListItem>();
            ReportNameList = new List<SelectListItem>();
            ReportHeader = new ReportHeaderEntities();
        }

        public string Print { get; set; }
        public string ReportType { get; set; }
        public string OrderBy { get; set; }

        [Display(Name ="Report Name")]
        public string ReportName { get; set; }
        public string WingId { get; set; }
        public bool Status { get; set; }
        public string ClassSectionIds { get; set; }
        public string hiddenClassSectionIds { get; set; }

        public string ReportFilterType { get; set; }

        public string SelectedClassNames { get; set; }
        public string SelectedReportName { get; set; }

        public ReportHeaderEntities ReportHeader { get; set; }

        public List<ClassSectionModel> ClassSectionList { get; set; }
        public List<string> HeaderNameList { get; set; }
        public List<List<string>> ValueList { get; set; }
        public List<SelectListItem> WingList { get; set; }
        public List<SelectListItem> ReportNameList { get; set; }
    }

    public class ClassSectionModel
    {
        public bool Selected { get; set; }
        public string ClassSectionId { get; set; }
        public string ClassSectionName { get; set; }
        public string WingId { get; set; }
        public string ClassName { get; set; }
    }
}