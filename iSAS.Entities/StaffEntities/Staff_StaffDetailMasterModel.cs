using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.StaffEntities
{
    public class Staff_StaffDetailMasterModel
    {
        public Staff_StaffDetailMasterModel()
        {
            DropDownList = new DropDownListFor_Staff_StaffDetailMaster();
            staffDocumentList=new List<StaffDocumentList>();
        }

        //public string imgCropped { get; set; }
        //public string Image1 { get; set; }

        public string Function { get; set; }

        //Staff General Details 
        [Display(Name = "Gender")]
        [StringLength(1)]
        public string Sex { get; set; }

        [Required(ErrorMessage = "First Name is Req..")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string FName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string MName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string LName { get; set; }

        [Display(Name = "Staff Image")]
        public string StaffImg { get; set; } //In DB It Was Image Type

        [DataType(DataType.Date)]
        [Display(Name = "DOB")]
        [StringLength(50)]
        public string StaffDOB { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Confirmation Date")]
        [StringLength(50)]
        public string DOC { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Anniversary Date")]
        [StringLength(50)]
        public string DOAnniversary { get; set; }

        [Display(Name = "Category")]
        [StringLength(10)]
        public string Cat { get; set; }

        [Display(Name = "Religon")]
        [StringLength(10)]
        public string Relgn { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Mobile no is Req..")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [StringLength(10)]
        public string Mob { get; set; }

        //[EmailAddress(ErrorMessage = "Not a valid email address..")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Email { get; set; }

        [Display(Name = "Nationality")]
        [StringLength(10)]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Blood group is required..")]
        [Display(Name = "Blood Group")]
        [StringLength(10)]
        public string BldGrp { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Marital Status")]
        [StringLength(1)]
        public string MStatus { get; set; }


        [Display(Name = "Area of Interst")]
        [StringLength(200, ErrorMessage = "Max Len 200..")]
        public string AOI { get; set; }

        [Display(Name = "Teaching Exp.")]
        public decimal TExp { get; set; }

        [Display(Name = "Industry Exp.")]
        public decimal IExp { get; set; }

        [Display(Name = "Other Exp.")]
        public decimal OExp { get; set; }

        public decimal TotExp { get; set; }

        [Display(Name = "Qualification")]
        [StringLength(10)]
        public string Qualif { get; set; }
        //Staff General Details End 


        [Display(Name = "Staff ID")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string StaffID { get; set; }

        [Display(Name = "Staff Code")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string StaffCode { get; set; }

        //[StringLength(5)]
        //public string Wing { get; set; }

        [Display(Name = "Staff Group")]
        [StringLength(10)]
        public string StaffGrp { get; set; }

        [Display(Name = "Department")]
        [StringLength(10)]
        public string Dept { get; set; }

        [Display(Name = "Designation")]
        [StringLength(10)]
        public string Desig { get; set; }

        [Display(Name = "Date of Joining")]
        [StringLength(50)]
        public string StaffDOJ { get; set; }

        [Display(Name = "Bank")]
        [StringLength(10)]
        public string Bank { get; set; }

        [Display(Name = "Staff Type")]
        [StringLength(10)]
        public string StaffType { get; set; }


        [Display(Name = "Bank IFSC Code")]
        [StringLength(50)]
        public string BankIFSC { get; set; }

        [Display(Name = "Bank A/C No")]
        [StringLength(50)]
        public string BankNo { get; set; }


        [Display(Name = "Pan Card No")]
        [StringLength(50)]
        public string Pancard { get; set; }


        [Display(Name = "Current Status")]
        [StringLength(50)]
        public string CurrStat { get; set; }


        [Display(Name = "Date of Leave")]
        [StringLength(50)]
        public string StaffDOL { get; set; }

        [Display(Name = "Reason of Leave")]
        [StringLength(200)]
        public string LReason { get; set; }

        [Display(Name = "Convenyce")]
        [StringLength(50)]
        public string Convenyce { get; set; }

        [Display(Name = "Vehicle No")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string VehNo { get; set; }

        //End Office Detail

        [Display(Name = "Hus's/Father's Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string FHName { get; set; }

        [Display(Name = "Mother's Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string MotherName { get; set; }


        [Display(Name = "Contact No")]
        [StringLength(20)]
        //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string ContactNo { get; set; }

        [Display(Name = "Emergency Contact No")]
        [StringLength(20)]
        //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string EmergNo { get; set; }

        [Display(Name = "Staff's Ref 1")]
        [StringLength(50)]
        public string StaffRef1 { get; set; }

        [Display(Name = "Worked In")]
        [StringLength(50)]
        public string Ref1Workin { get; set; }

        [Display(Name = "Refrence ID Type")]
        [StringLength(50)]
        public string Ref1IDType { get; set; }

        [Display(Name = "Refrence ID No")]
        [StringLength(50)]
        public string Ref1IDNo { get; set; }


        [Display(Name = "Staff's Ref 2")]
        [StringLength(50)]
        public string StaffRef2 { get; set; }

        [Display(Name = "Worked In")]
        [StringLength(50)]
        public string Ref2Workin { get; set; }

        [Display(Name = "Refrence ID Type")]
        [StringLength(50)]
        public string Ref2IDType { get; set; }

        [Display(Name = "Refrence ID No")]
        [StringLength(50)]
        public string Ref2IDNo { get; set; }

        //End Personal Details 

        [Display(Name = "Local Address")]
        [StringLength(150, ErrorMessage = "Max Len 150..")]
        public string CorrAdd { get; set; }

        [Display(Name = "Area")]
        [StringLength(50)]
        public string CorrArea { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string CorrCity { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string CorrState { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        public string CorrCountry { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Pin Code")]
        [StringLength(50)]
        public string CorrPinCode { get; set; }

        [Display(Name = "Mobile")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [StringLength(10)]
        public string CorrMobile { get; set; }

        [Display(Name = "Ph 1")]
        [StringLength(20)]
        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string CorrPhNo1 { get; set; }

        [Display(Name = "Ph 2")]
        [StringLength(20)]
        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string CorrPhNo2 { get; set; }

        //End Correspondence Address

        [Display(Name = "Permanent Address")]
        [StringLength(150, ErrorMessage = "Max Len 150..")]
        public string PermAdd { get; set; }

        [Display(Name = "Area")]
        [StringLength(50)]
        public string PermArea { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string PermCity { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string PermState { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        public string PermCountry { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Pin Code")]
        [StringLength(50)]
        public string PermPinCode { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Mobile")]
        [StringLength(10)]
        public string PermMobile { get; set; }

        [Display(Name = "Ph 1")]
        [StringLength(20)]
        //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PermPh1 { get; set; }

        [Display(Name = "Ph 2")]
        [StringLength(20)]
        //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PermPh2 { get; set; }

        public bool Active { get; set; }

        [StringLength(50)]
        public string JoinSession { get; set; }


        //public int TG { get; set; }
        //public int Shft { get; set; }

        [Display(Name = "Punch Code")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string PunchCode { get; set; }

        [Display(Name = "SMS Group")]
        public string SMSGroup { get; set; }

        public bool SGS { get; set; }
        public bool SGA { get; set; }
        public bool SGM { get; set; }

        public string UserId { get; set; }
        [Display(Name = "Aadhar No")]
        public string AadharNo { get; set; }


        //public int CBy { get; set; }
        //public string CDatev { get; set; }
        //public int MBy { get; set; }
        //public string MDate { get; set; }

        public DropDownListFor_Staff_StaffDetailMaster DropDownList { get; set; }
        public List<StaffDocumentList> staffDocumentList { get; set; }
    }
    public class DropDownListFor_Staff_StaffDetailMaster
    {
        public DropDownListFor_Staff_StaffDetailMaster()
        {
            CategoryList = new List<SelectListItem>();
            RelignList = new List<SelectListItem>();
            NationalityList = new List<SelectListItem>();
            BloodGroupList = new List<SelectListItem>();
            QualificationList = new List<SelectListItem>();

            DepartmentList = new List<SelectListItem>();
            DesignationList = new List<SelectListItem>();
            BankList = new List<SelectListItem>();
            StatusList = new List<SelectListItem>();
            ConveyanceList = new List<SelectListItem>();
            RefIdTypeList = new List<SelectListItem>();

            AreaList = new List<SelectListItem>();
            CountryList = new List<SelectListItem>();
            CoresStateList = new List<SelectListItem>();
            CoresCityList = new List<SelectListItem>();
            PermStateList = new List<SelectListItem>();
            PermCityList = new List<SelectListItem>();
        }

        public string NewStaffId { get; set; }
        public string NewEmpcode { get; set; }

        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> RelignList { get; set; }
        public List<SelectListItem> NationalityList { get; set; }
        public List<SelectListItem> BloodGroupList { get; set; }
        public List<SelectListItem> QualificationList { get; set; }

        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> DesignationList { get; set; }
        public List<SelectListItem> BankList { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> ConveyanceList { get; set; }

        //Wing, ShiftName, TimeGroup Has Been removed

        public List<SelectListItem> RefIdTypeList { get; set; }

        public List<SelectListItem> AreaList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> CoresStateList { get; set; }
        public List<SelectListItem> CoresCityList { get; set; }
        public List<SelectListItem> PermStateList { get; set; }
        public List<SelectListItem> PermCityList { get; set; }
        public List<Staff_DocumentList> staffDocumentList { get; set; }
    }
    public class StaffDocumentList
    {
        public string DocId { get; set; }
        public string DocName { get; set; }
        public string docAlias { get; set; }
        public string DocPath { get; set; }
        public int docNo { get; set; }
        public string certificateDate { get; set; }
        public string TrainBy { get; set; }

        

    }
    public class StaffProfileDetailsModel
    {
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string DOB { get; set; }
        public string Category { get; set; }
        public string ReligonName { get; set; }
        public string BloodGroup { get; set; }
        public string MartialStatus { get; set; }
        public string Qualification { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string DOJ { get; set; }
        public string DOL { get; set; }
        public string FatherOrHusName { get; set; }
        public string Mother { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Ph1 { get; set; }
        public string Ph2 { get; set; }
        public string MobileNo { get; set; }
        public bool Active { get; set; }
        public string HRNo { get; set; }
        public string ImageURL { get; set; }

        public string Gender { get; set; }
        public string Email { get; set; }
        public string StaffType { get; set; }
        public string EmergencyContactNo { get; set; }
    }
    public class TeacherDetailModel
    {
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string DeptName { get; set; }
        public string Email { get; set; }
        public string Mob { get; set; }
        public string StaffCode { get; set; }
    }
}
