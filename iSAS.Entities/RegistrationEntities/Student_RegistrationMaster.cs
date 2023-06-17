using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.RegistrationEntities
{
    public class Student_RegistrationMaster 
    {
        public Student_RegistrationMaster()
        {
            SchoolAdimCategories = new List<SelectListItem>();
            SessionList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            RegCategoryList = new List<SelectListItem>();
            AreaList = new List<SelectListItem>();
            CountryList = new List<SelectListItem>();

            Corres_StateList = new List<SelectListItem>();
            Corres_CityList = new List<SelectListItem>();

            Perm_StateList = new List<SelectListItem>();
            Perm_CityList = new List<SelectListItem>();

            RegStatusList = new List<SelectListItem>();
        }

        
        public string RegistrationClass { get; set; }

        [Display(Name = "Session")]
        public string Sess { get; set; }

        [Required(ErrorMessage = "Class is Req..")]
        [Display(Name = "Class")]
        public string RegClassId { get; set; }

        //[Remote("ValidateStudentRegistration_RegID", "RemoteValidations", AdditionalFields = "FormID")]
        [Required(ErrorMessage = "Reg No Req..")]
        [Display(Name = "Reg No")]
        public string RegID { get; set; }

        [Required(ErrorMessage = "Reg No Req..")]
        [Display(Name = "Form No")]
        public string FormID { get; set; }

        [Display(Name = "Reg Date")]
        public string RegDate { get; set; }

        [Display(Name = "Admission Category")]
        public string AdmCategoryId { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Amount")]
        public int Amt { get; set; }

        [Display(Name = "Interview Date")]
        public string IntrvwDate { get; set; }

        public string Remark { get; set; }

        [Required(ErrorMessage = "First Name Req..")]
        [Display(Name = "First Name")]
        public string Student_FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string Student_MiddleName { get; set; }

        //[Required(ErrorMessage = "Last Name Req..")]
        [Display(Name = "Last Name")]
        public string Student_LastName { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender is Req..")]
        public char Student_Gender { get; set; }

        [Display(Name = "DOB")]
        [Required(ErrorMessage = "DOB is Req..")]
        public string Student_DOB { get; set; }

        [Required(ErrorMessage = "First Name Req..")]
        [Display(Name = "First Name")]
        public string Father_FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string Father_MiddleName { get; set; }

        //[Required(ErrorMessage = "Last Name Req..")]
        [Display(Name = "Last Name")]
        public string Father_LastName { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [MaxLength(10, ErrorMessage = "Max 10 Digit.."), MinLength(10, ErrorMessage = "Min 10 Digit..")]
        [Display(Name = "Mobile No")]
        public string Father_MoblieNo { get; set; }

        [Required(ErrorMessage = "First Name Req..")]
        [Display(Name = "First Name")]
        public string Mother_FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string Mother_MiddleName { get; set; }

        //[Required(ErrorMessage = "Last Name Req..")]
        [Display(Name = "Last Name")]
        public string Mother_LastName { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [MaxLength(10, ErrorMessage = "Max 10 Digit.."), MinLength(10, ErrorMessage = "Min 10 Digit..")]
        [Display(Name = "Mobile No")]
        public string Mother_MobileNo { get; set; }

        [Required(ErrorMessage = "Corr. Add Req..")]
        [Display(Name = "Corr. Add")]
        public string Student_CorrAdd { get; set; }

        [Display(Name = "Area")]
        public string Student_CorrArea { get; set; }

        [Required(ErrorMessage = "City Req..")]
        [Display(Name = "City")]
        public string Student_CorrCity { get; set; }

        [Required(ErrorMessage = "State Req..")]
        [Display(Name = "State")]
        public string Student_CorrState { get; set; }

        [Required(ErrorMessage = "Country Req..")]
        [Display(Name = "Country")]
        public string Student_CorrCountry { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [MaxLength(10, ErrorMessage = "Max 10 Digit.."), MinLength(10, ErrorMessage = "Min 10 Digit..")]
        [Display(Name = "Mobile")]
        public string Student_CorrMobile { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [MaxLength(6, ErrorMessage = "Max 6 Digit.."), MinLength(6, ErrorMessage = "Min 6 Digit..")]
        [Display(Name = "Pin Code")]
        public string Student_CorrPinCode { get; set; }

        [Display(Name = "Perm. Add")]
        public string Student_PermAdd { get; set; }

        [Display(Name = "Area")]
        public string Student_PermArea { get; set; }

        [Display(Name = "City")]
        public string Student_PermCity { get; set; }

        [Display(Name = "State")]
        public string Student_PermState { get; set; }

        [Display(Name = "Country")]
        public string Student_PermCountry { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [MaxLength(10, ErrorMessage = "Max 10 Digit.."), MinLength(10, ErrorMessage = "Min 10 Digit..")]
        [Display(Name = "Mobile")]
        public string Student_PermMobile { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [MaxLength(6, ErrorMessage = "Max 6 Digit.."), MinLength(6, ErrorMessage = "Min 6 Digit..")]
        [Display(Name = "Pin Code")]
        public string Student_PermPinCode { get; set; }

        [Display(Name = "Reg. Status")]
        public string RegStatus { get; set; }

        public string CBy { get; set; }

        public string CDate { get; set; }

        public string MBy { get; set; }

        public string MDate { get; set; }


        public List<SelectListItem> SchoolAdimCategories { get; set; }
        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> RegCategoryList { get; set; }
        public List<SelectListItem> AreaList { get; set; }
        public List<SelectListItem> CountryList { get; set; }

        public List<SelectListItem> Corres_StateList { get; set; }
        public List<SelectListItem> Corres_CityList { get; set; }

        public List<SelectListItem> Perm_StateList { get; set; }
        public List<SelectListItem> Perm_CityList { get; set; }

        public List<SelectListItem> RegStatusList { get; set; }

        public List<School_DocumentMaster> DocList { get; set; }
    }
    public class RegistrationSlipModel
    {
        public string RegID { get; set; }
        public string Class { get; set; }
        public string RegDate { get; set; }
        public string AdmCategoryName { get; set; }
        public string IntrvwDate { get; set; }
        public string Student { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Father { get; set; }
        public string FMobileNo { get; set; }
        public string Mother { get; set; }
        public string MMobileNo { get; set; }
        public int RegAmount { get; set; }
        public string Remark { get; set; }
        public string Address1 { get; set; }
        public string SessionName { get; set; }
    }

}
