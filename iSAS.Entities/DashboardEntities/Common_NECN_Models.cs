using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.DashboardEntities
{
    public class Common_NECN_LandingModel
    {
        public string UploadID { get; set; }
        [Required(ErrorMessage = "Title is Req..")]
        [Display(Name = "Upload Title")]
        public string UploadTitle { get; set; }

        [Display(Name = "Upload Description")]
        public string UploadDescription { get; set; }

        [Display(Name = "Start Date")]
        public string UploadStartDate { get; set; }

        [Display(Name = "End Date")]
        public string UploadEndDate { get; set; }

        [Display(Name = "Is Disable")]
        public bool IsExpiry { get; set; }

        
        [Display(Name = "Created Date")]
        public string CreationDate { get; set; }

        public bool IsNew { get; set; }

        public bool HavingAttachment { get; set; }
        public string UploadAttachment { get; set; }
    }

    public class Common_NECN_DisplayListModel
    {
        public Common_NECN_DisplayListModel()
        {
            NewList = new List<Common_NECN_LandingModel>();
            OldList = new List<Common_NECN_LandingModel>();
        }

        public List<Common_NECN_LandingModel> NewList { get; set; }
        public List<Common_NECN_LandingModel> OldList { get; set; }
    }

    public class Common_NECN_MainModel
    {
        public Common_NECN_MainModel()
        {
            BasicDetails = new Common_NECN_LandingModel();
            UploadDocList = new List<Common_NECN_PhotoUploads>();
            SelectionGroups = new Common_NECN_SelectionGroup();
            
        }

        public string Function { get; set; }
        public string UploadType { get; set; }
        public string UserID { get; set; }

        [Display(Name ="Wings")]
        public string[] WingIds { get; set; }

        [Display(Name = "Classes")]
        public string[] ClassIds { get; set; }

        [Display(Name = "Departments")]
        public string[] DeprtmentIds { get; set; }

        [Display(Name = "Staffs")]
        public string[] StaffIds { get; set; }

        public Common_NECN_LandingModel BasicDetails { get; set; }
        public List<Common_NECN_PhotoUploads> UploadDocList { get; set; }
        public Common_NECN_SelectionGroup SelectionGroups { get; set; }
        
    }

    public class Common_NECN_PhotoUploads
    {
        public string UploadID { get; set; }
        public string AttachPath { get; set; }
        public string FileName { get; set; }
    }

    public class Common_NECN_SelectionGroup
    {
        public Common_NECN_SelectionGroup()
        {
            WingList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            StudentDetailsList = new List<Common_NECN_StudentDetails>();
            DeptList = new List<SelectListItem>();
            StaffList = new List<SelectListItem>();
            UploadedByList = new List<SelectListItem>();
        }
        [Required(ErrorMessage = "Uploaded by is Req..")]
        [Display(Name = "Uploaded By")]
        public string UploadedBy { get; set; }

        public List<SelectListItem> WingList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<Common_NECN_StudentDetails> StudentDetailsList { get; set; }
        public List<SelectListItem> DeptList { get; set; }
        public List<SelectListItem> StaffList { get; set; }
        public List<SelectListItem> UploadedByList { get; set; }
    }

    public class Common_NECN_StudentDetails
    {
        public bool Selected { get; set; }
        public string ClassName { get; set; }
        public string ERPNo { get; set; }
        public string StudentName { get; set; }
    }

    public class Event
    {
        public int EventID { get; set; }
        public string Subject { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Description { get; set; }
        public bool IsFullDay { get; set; }
        public string ThemeColor { get; set; }
    }
}
