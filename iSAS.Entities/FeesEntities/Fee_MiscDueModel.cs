using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_MiscDueModel
    {
        public Fee_MiscDueModel()
        {
            HeadList = new List<SelectListItem>();
            InstallmentList = new List<SelectListItem>();
            //MiscDueList = new List<Fee_MiscDuesDetails>();
            // StudentDetails = new StudentSearchModel();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            StrectureList = new List<SelectListItem>();
            MiscDueStudentDetails = new List<Fee_MiscDuesStudentDetailsModel>();
            StudentDuesList = new List<Fee_StudentHeadWiseDueDetailsModel>();
            
    }

        [Display(Name = "Head")]
        public string HeadId { get; set; }

        [Display(Name = "Installment")]
        public string InstallmentId { get; set; }

        [Display(Name = "Amount")]
        public int Amount { get; set; }

        public string ERPNos { get; set; }

        [Display(Name = "Class")]
        public string ClassId_ForClassWise { get; set; }

        [Display(Name = "Section")]
        public string SectionId_ForClassWise { get; set; }

        [Display(Name = "Structure")]
        public string StrectureId { get; set; }

        public string SessionId { get; set; }
        public string UserId{ get; set; }
        public List<SelectListItem> HeadList { get; set; }
        public List<SelectListItem> InstallmentList { get; set; }

        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> StrectureList { get; set; }

        //public List<Fee_MiscDuesDetails> MiscDueList { get; set; }
        //public StudentSearchModel StudentDetails { get; set; }
        public List<Fee_MiscDuesStudentDetailsModel> MiscDueStudentDetails { get; set; }
        public List<Fee_StudentHeadWiseDueDetailsModel> StudentDuesList { get; set; }
        //Extra Feild
        public string CRUDFor { get; set; }
        public string Selected_TransRefNo { get; set; }
    }

    public class Fee_MiscDuesStudentDetailsModel
    {
        public bool Selected { get; set; }
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public string StudentName { get; set; }
        public string Father { get; set; }
        public string TransRefNo { get; set; }
        public string Amount { get; set; }
        public string SessionId { get; set; }
        public bool Iscancellable { get; set; }
    }

    public class Fee_MiscDuesDetails
    {
        //public string DueDate { get; set; }
        public string HeadId { get; set; }
        public string DisplayDueDate { get; set; }
        public string HeadName { get; set; }
        public int Amount { get; set; }
        public int PaidAmount { get; set; }
        public int Balance { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }
        public string TransRefNo { get; set; }
    }


    public class Fee_MiscDueStudentWiseModel
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string CRUDFor { get; set; }
        public string ERPNo_ForStudentWise { get; set; }

        [Display(Name ="Head")]
        public string HeadId_StudentWise { get; set; }

        [Display(Name = "Amount")]
        public int Amount_StudentWise { get; set; }
        public List<Fee_StudentHeadWiseDueDetailsModel> StudentDuesList { get; set; }
    }

    public class Fee_StudentHeadWiseDueDetailsModel
    {
        public string ERPNo { get; set; }
        public string DueDate { get; set; }
        public string HeadName { get; set; }
        public int Amount { get; set; }
        public int Paid { get; set; }
        public bool Editable { get; set; }
        public bool Cancelable { get; set; }
        public string TransRefNo { get; set; }
        public string IsChargesAvailed { get; set; }

    }
}
