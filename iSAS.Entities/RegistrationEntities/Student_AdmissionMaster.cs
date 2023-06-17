using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.RegistrationEntities
{
    public class Student_AdmissionMaster_Model
    {
        public Student_AdmissionMaster_Model()
        {
            OfficeInfo = new OfficeInfoModel();
            StudentInfo = new StudentInfoModel();
            FatherInfo = new FatherInfoModel();
            MotherInfo = new MotherInfoModel();
            GurdianInfo = new GurdianInfoModel();
            AddressInfo = new AddressInfoModel();
            AvailFacilityInfo = new AvailFacilityModel();
            DropDownList = new DropDownListFor_StudentAdmission();
        }

        public string Function { get; set; }

        public OfficeInfoModel OfficeInfo { get; set; }
        public StudentInfoModel StudentInfo { get; set; }
        public FatherInfoModel FatherInfo { get; set; }
        public MotherInfoModel MotherInfo { get; set; }
        public GurdianInfoModel GurdianInfo { get; set; }
        public AddressInfoModel AddressInfo { get; set; }
        public AvailFacilityModel AvailFacilityInfo { get; set; }
        public DropDownListFor_StudentAdmission DropDownList { get; set; }

        public string UserId { get; set; }
        public string LastModifiedBy { get; set; }
    }

    public class OfficeInfoModel
    {
        [StringLength(10)]
        public string Session { get; set; }

        //[StringLength(10)]
        //public string Stud_Wing { get; set; }

        [Display(Name = "SRA No")]
        [StringLength(20, ErrorMessage = "Max 20 Char is Allowed..!")]
        public string SRANo { get; set; }

        [Display(Name = "ERP No")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string Stud_UID { get; set; }

        [Display(Name = "Adm No")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string Stud_AdmNo { get; set; }


        [Display(Name = "Reg No")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string Stud_RegNo { get; set; }

        [Required(ErrorMessage = "DOA is Req..")]
        [Display(Name = "DOA")]
        public string Stud_DOA { get; set; } // Date Feild

        [Display(Name = "Staff Ward")]
        public bool Stud_Staffward { get; set; }

        [Display(Name = "Staff Name")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string Stud_StaffwardID { get; set; }

        [Display(Name = "Adm. Category")]
        public string AdmCategoryId { get; set; }

        [Display(Name = "Family Id")]
        public string FamilyId { get; set; }
    }

    public class StudentInfoModel
    {
        public string SiblingNames { get; set; }

        public bool NewAdm { get; set; }
        public bool OldAdm { get; set; }

        [Display(Name = "Gender")]
        public char Stud_Gender { get; set; }

        [Display(Name = "New / Old")]
        public char AdmissionType { get; set; } // Extra Feild

        [Display(Name = "Parents")]
        public string Stud_Parents { get; set; }

        [Required(ErrorMessage = "DOB is Req..")]
        [Display(Name = "DOB")]
        public string Stud_DOB { get; set; }

        [Display(Name = "Birth Place")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_BirthPlace { get; set; }

        [Required(ErrorMessage = "First Name is Req..")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_LastName { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "Class is Req..")]
        [StringLength(10)]
        public string Stud_Class { get; set; }

        [Display(Name = "Section")]
        [Required(ErrorMessage = "Section is Req..")]
        [StringLength(10)]
        public string Stud_Section { get; set; }

        [Display(Name = "Roll No")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Range(maximum: 99, minimum: 0, ErrorMessage = "Invalid Roll No..")]
        public int Stud_ClassRollNo { get; set; }

        //[StringLength(10)]
        //public string Stud_RollNo { get; set; }

        [Display(Name = "Blood Group")]
        [StringLength(10)]
        public string Stud_BldGrp { get; set; }

        [Display(Name = "House")]
        [StringLength(10)]
        public string Stud_House { get; set; }

        [Display(Name = "Club")]
        [StringLength(10)]
        public string Stud_Club { get; set; }

        [Display(Name = "Category")]
        [StringLength(10)]
        public string Stud_Category { get; set; }

        [Display(Name = "Religion")]
        [StringLength(10)]
        public string Stud_Religion { get; set; }

        [Display(Name = "Minority Group")]
        [StringLength(10)]
        public string Stud_MCategory { get; set; }

        [Display(Name = "Nationality")]
        [StringLength(10)]
        public string Stud_Nationality { get; set; }

        [Display(Name = "Aadhar No")]
        [StringLength(20)]
        public string Stud_Aadhar { get; set; }

        [Display(Name = "Height (cm)")]
        [StringLength(10)]
        public string Stud_Height { get; set; }

        [Display(Name = "Weight (kg)")]
        [StringLength(10)]
        public string Stud_Weight { get; set; }

        [Display(Name = "Age (years)")]
        public int Stud_Age { get; set; }

        [Display(Name = "Admited Class")]
        [StringLength(10)]
        public string AdmClass { get; set; }

        //public string Stud_Emailadd { get; set; }

        [Display(Name = "Last School")]
        [StringLength(150, ErrorMessage = "Max Len 150..")]
        public string Stud_LastSchool { get; set; }

        [Display(Name = "Last Board")]
        [StringLength(100, ErrorMessage = "Max Len 100..")]
        public string Stud_LastBoard { get; set; }

        [Display(Name = "Board Location")]
        [StringLength(100, ErrorMessage = "Max Len 100..")]
        public string Stud_LastBoardLocation { get; set; } // In Proc It Was only as BoardLocation

        [Display(Name = "Board No")]
        [StringLength(20, ErrorMessage = "Max Len 20..")]
        public string Stud_LastBoardNo { get; set; } // In Proc It was only as BoardNo

        [Display(Name = "Last Class")]
        [StringLength(20, ErrorMessage = "Max LEn 20..")]
        public string Stud_LastClass { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "No Of Year Attend")]
        public int Stud_Noofyearattend { get; set; }

        [Display(Name = "Last Per/Grade")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string Stud_LastPer { get; set; }

        [Display(Name = "Last Achievement")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string Stud_LastAchievement { get; set; }


        //Medical Problem Entities
        [Display(Name = "Name of Problem")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_MProblem { get; set; }

        [Display(Name = "Name of Doctor")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_MDoctor { get; set; }

        [Display(Name = "Allergy to Medicine")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_MAllergyMedicine { get; set; }

        [Display(Name = "Clinic Add")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_MClinicAdd { get; set; }

        [Display(Name = "Student Image")]
        public string Stud_Image { get; set; } //It was Image type of in proc

        //Stud_Activity

        [Display(Name = "Status")]
        public string Stud_Status { get; set; }


    }

    public class FatherInfoModel
    {
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Father_FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Father_MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Father_LastName { get; set; }

        [Display(Name = "Qualification")]
        [StringLength(50)]
        public string Father_Qualif { get; set; }

        [Display(Name = "Profession")]
        [StringLength(50)]
        public string Father_Prof { get; set; }

        [StringLength(50)]
        public string Father_Desg { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Mobile No")]
        [StringLength(50)]
        public string Father_MoblieNo { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Aadhar No")]
        [StringLength(20)]
        public string Father_Aadhar { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Annual Income")]
        public int Father_AnnualIncome { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Age (years)")]
        public int Father_Age { get; set; }


        [EmailAddress]
        [StringLength(50)]
        [Display(Name = "Email Id")]
        public string Father_Email { get; set; }

        [Display(Name = "Alumni")]
        public bool Father_Alumni { get; set; }

        [Display(Name = "Year")]
        [StringLength(50)]
        public string Father_AlumniYear { get; set; }

        [Display(Name = "Office Address")]
        [StringLength(150, ErrorMessage = "Max Len 150..")]
        public string Father_OffAdd { get; set; }

        [Display(Name = "District")]
        [StringLength(50)]
        public string Father_Area { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string Father_City { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string Father_State { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        public string Father_Country { get; set; }

        //[StringLength(50)]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Pin Code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "It must be 6 digit only")]
        public string Father_PinCode { get; set; }

        [Display(Name = "Office No")]
        [StringLength(50)]
        public string Father_OffNo { get; set; }

        //public string Father_FaxNo { get; set; }
        [Display(Name = "Father Image")]
        public string Father_Image { get; set; } // Image Type In Proc and Name Change

        [Display(Name = "Grand Father Image")]
        public string Grand_FatherImage { get; set; }

        [Display(Name = "Grand Father Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_GFatherName { get; set; }

    }

    public class MotherInfoModel
    {
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Mother_FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Mother_MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Mother_LastName { get; set; }

        [Display(Name = "Qualification")]
        [StringLength(50)]
        public string Mother_Qualif { get; set; }

        [Display(Name = "Profession")]
        [StringLength(50)]
        public string Mother_Prpf { get; set; }// In Proc It it Prof

        [Display(Name = "Designation")]
        [StringLength(50)]
        public string Mother_Desg { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Mobile")]
        [StringLength(50)]
        public string Mother_MobileNo { get; set; }

        [Display(Name = "Aadhar No")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [StringLength(20)]
        public string Mother_Aadhar { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Annual Income")]
        public int Mother_AnnualIncome { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Age (years)")]
        public int Mother_Age { get; set; }

        [EmailAddress]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        [Display(Name = "Email Id")]
        public string Mother_Email { get; set; }

        [Display(Name = "Alumni")]
        public bool Mother_Alumni { get; set; }

        [Display(Name = "Year")]
        [StringLength(10)]
        public string Mother_AlumniYear { get; set; }

        [Display(Name = "Office Address")]
        [StringLength(150, ErrorMessage = "Max Len 150..")]
        public string Mother_OffAdd { get; set; }

        [Display(Name = "District")]
        [StringLength(50)]
        public string Mother_Area { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string Mother_City { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string Mother_State { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        public string Mother_Country { get; set; }

        [Display(Name = "Pin Code")]
        //[StringLength(50)]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "It must be 6 digit only")]
        public string Mother_Pincode { get; set; }

        [Display(Name = "Office No")]
        [StringLength(50)]
        public string Mother_OffNo { get; set; }

        //[StringLength(50)]
        //public string Mother_FaxNo { get; set; }

        [Display(Name = "Mother Image")]
        public string Mother_Image { get; set; }// Image Type in proc and also change name

        [Display(Name = "GM Image")]
        public string Grand_MotherImage { get; set; }

        [Display(Name = "Grand Mother Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Stud_GMotherName { get; set; }
    }

    public class GurdianInfoModel
    {
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Gardian_FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Gardian_MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string Gardian_LastName { get; set; }

        [Display(Name = "Qualification")]
        [StringLength(50)]
        public string Gardian_Qualif { get; set; }

        [Display(Name = "Profession")]
        [StringLength(50)]
        public string Gardian_Prof { get; set; }

        [Display(Name = "Designation")]
        [StringLength(50)]
        public string Gardian_Desg { get; set; }

        [Display(Name = "Mobile No")]
        [StringLength(50)]
        public string Gardian_MobileNo { get; set; }

        [Display(Name = "Aadhar No")]
        [StringLength(20)]
        public string Gardian_Aadhar { get; set; }

        [Display(Name = "Annual Income")]
        public int Gardian_AnnualIncome { get; set; }

        [Display(Name = "Age (years)")]
        public int Gardian_Age { get; set; }

        [EmailAddress]
        [StringLength(50)]
        [Display(Name = "Email Id")]
        public string Gardian_Email { get; set; }

        [Display(Name = "Alumni")]
        public bool Gardian_Alumni { get; set; }

        [Display(Name = "Year")]
        [StringLength(10)]
        public string Gardian_AlumniYear { get; set; }

        [Display(Name = "Relation to Child")]
        [StringLength(50)]
        public string Gardian_Relation { get; set; } //Name Change In Proc

        [Display(Name = "Office Address")]
        [StringLength(150)]
        public string Gardian_OffAdd { get; set; }

        [Display(Name = "District")]
        [StringLength(50)]
        public string Gardian_Area { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string Gardian_City { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string Gardian_State { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "It must be 6 digit only")]
        [Display(Name = "Pin Code")]
        public string Gardian_PinCode { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        public string Gardian_Country { get; set; }

        [Display(Name = "Office No")]
        [StringLength(50)]
        public string Gardian_OffNo { get; set; }

        //[StringLength(50)]
        //public string Gardian_FaxNo { get; set; }
        [Display(Name = "Guardian Image")]
        public string Gardian_image { get; set; }
    }

    public class AddressInfoModel
    {
        //Corres Add
        [Display(Name = "Local Address")]
        [StringLength(150, ErrorMessage = "Max Len 150..")]
        public string Stud_CorrAdd { get; set; }

        [Display(Name = "District")]
        [StringLength(50)]
        public string Stud_CorrArea { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string Stud_CorrCity { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string Stud_CorrState { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        public string Stud_CorrCountry { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Pin Code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "It must be 6 digit only")]
        public string Stud_CorrPinCode { get; set; }

        [Display(Name = "Mobile")]
        [StringLength(50)]
        public string Stud_CorrMobile { get; set; }

        [Display(Name = "PH 1")]
        [StringLength(50)]
        public string Stud_CorrPhNo1 { get; set; }

        [Display(Name = "PH 2")]
        [StringLength(50)]
        public string Stud_CorrPhNo2 { get; set; }

        //Perm Add

        [Display(Name = "Permanment Address")]
        [StringLength(150, ErrorMessage = "Max Len 150..")]
        public string Stud_PermAdd { get; set; }

        [Display(Name = "District")]
        [StringLength(50)]
        public string Stud_PermArea { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string Stud_PermCity { get; set; }

        [Display(Name = "State")]
        [StringLength(50)]
        public string Stud_PermState { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        public string Stud_PermCountry { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "Pin Code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "It must be 6 digit only")]
        public string Stud_PermPinCode { get; set; }

        [Display(Name = "Mobile")]
        [StringLength(50)]
        public string Stud_PermMobile { get; set; }

        [Display(Name = "PH 1")]
        [StringLength(50)]
        public string Stud_PermPh1 { get; set; }

        [Display(Name = "PH 2")]
        [StringLength(50)]
        public string Stud_PermPh2 { get; set; }
    }

    public class AvailFacilityModel
    {
        [Display(Name = "Route Name")]
        [StringLength(10)]
        public string PRouteId { get; set; }

        [Display(Name = "Stop Name")]
        [StringLength(10)]
        public string PStopId { get; set; }

        [StringLength(5)]
        public string Facility { get; set; }

        [Display(Name = "Transport Availed")]
        public bool Stud_TransReq { get; set; }

        [Display(Name = "Date")]
        public string TranDate { get; set; } //Date Time

        [StringLength(10)]
        public string DRouteId { get; set; }

        [StringLength(10)]
        public string DStopId { get; set; }

        [Display(Name = "Mode Of Transport")]
        [StringLength(10)]
        public string ModeofTransport { get; set; }

        [Display(Name = "Picked Up By")]
        [StringLength(10)]
        public string PickedupBy { get; set; }

        [Display(Name = "Avail SMS")]
        public bool AvailSMS { get; set; }

        [Display(Name = "SMS On")]
        public char SMS_WHOM { get; set; } //Extra Feild

        public bool SMSF { get; set; }
        public bool SMSM { get; set; }
        public bool SMSG { get; set; }
        public bool SMSO { get; set; }

        [Display(Name = "Avail Snacks")]
        public bool AvailSnacks { get; set; }

        public bool Doctoroncall { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Number Only..")]
        [Display(Name = "SMS No")]
        [StringLength(10)]
        public string SMSNo { get; set; }

        [StringLength(50, ErrorMessage = "Max Len 50..")]
        [Display(Name = "Picked Up Name")]
        public string Stud_PickedUpName { get; set; }

        [Display(Name = "Picked By Image")]
        public string PickedUPBy_Image { get; set; }
    }

    public class DropDownListFor_StudentAdmission
    {
        public DropDownListFor_StudentAdmission()
        {
            SessionList = new List<SelectListItem>();
            StaffNameList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            StatusList = new List<SelectListItem>();
            BloodGroupList = new List<SelectListItem>();
            HouseList = new List<SelectListItem>();
            ClubList = new List<SelectListItem>();
            SubCategoryList = new List<SelectListItem>();
            ReligionList = new List<SelectListItem>();
            MinorCategoryList = new List<SelectListItem>();
            NationalityList = new List<SelectListItem>();


            QualificationList = new List<SelectListItem>();
            ProfissionList = new List<SelectListItem>();
            AlumniList = new List<SelectListItem>();
            CountryList = new List<SelectListItem>();
            AreaList = new List<SelectListItem>();
            Father_StateList = new List<SelectListItem>();
            Father_CityList = new List<SelectListItem>();

            Mother_StateList = new List<SelectListItem>();
            MotherCityList = new List<SelectListItem>();

            Corres_AddStateList = new List<SelectListItem>();
            Corres_AddCityList = new List<SelectListItem>();

            Perm_AddStateList = new List<SelectListItem>();
            Perm_AddCityList = new List<SelectListItem>();

            Pick_RouteList = new List<SelectListItem>();
            Pick_StopList = new List<SelectListItem>();

            ModeOfTransList = new List<SelectListItem>();
            PickedByList = new List<SelectListItem>();

            Gurdi_StateList = new List<SelectListItem>();
            Gurdi_CityList = new List<SelectListItem>();
            AdmCategoryList = new List<SelectListItem>();
        }

        public bool IsAdmNoAutoIncrement { get; set; }
        public string AdmNo { get; set; }
        public string ERPNo { get; set; }
        public string Session { get; set; }
        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> StaffNameList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> BloodGroupList { get; set; }
        public List<SelectListItem> HouseList { get; set; }
        public List<SelectListItem> ClubList { get; set; }
        public List<SelectListItem> SubCategoryList { get; set; }
        public List<SelectListItem> ReligionList { get; set; }
        public List<SelectListItem> MinorCategoryList { get; set; }
        public List<SelectListItem> NationalityList { get; set; }


        public List<SelectListItem> QualificationList { get; set; }
        public List<SelectListItem> ProfissionList { get; set; }
        public List<SelectListItem> AlumniList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> AreaList { get; set; }
        public List<SelectListItem> Father_StateList { get; set; }
        public List<SelectListItem> Father_CityList { get; set; }

        public List<SelectListItem> Mother_StateList { get; set; }
        public List<SelectListItem> MotherCityList { get; set; }

        public List<SelectListItem> Corres_AddStateList { get; set; }
        public List<SelectListItem> Corres_AddCityList { get; set; }

        public List<SelectListItem> Perm_AddStateList { get; set; }
        public List<SelectListItem> Perm_AddCityList { get; set; }

        public List<SelectListItem> Pick_RouteList { get; set; }
        public List<SelectListItem> Pick_StopList { get; set; }

        public List<SelectListItem> ModeOfTransList { get; set; }
        public List<SelectListItem> PickedByList { get; set; }

        public List<SelectListItem> Gurdi_StateList { get; set; }
        public List<SelectListItem> Gurdi_CityList { get; set; }
        public List<SelectListItem> AdmCategoryList { get; set; }
        
    }



    public class Student_AdmissionLandingPageModel
    {
        public string ERP { get; set; }
        public string DOA { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Address { get; set; }
        public string FMobileNo { get; set; }
        public string MMobileNo { get; set; }
        public string SMSNo { get; set; }
        public string AlternateNumber { get; set; }

    }

    /*
    //public class DocumentInfoModel
    //{
    //    Doc1
    //       Doc2
    //        Doc3
    //        Doc4
    //        Doc5
    //        Doc6
    //        Doc7
    //        Doc8
    //        Doc9
    //        Doc10
    //}

    public class OtherInfoModel
    {
        Father_birthday
        Mother_birthday
            Fat_MotAnniver
            Father_Hobby
            Mother_Hobby
            Stud_ChildSuspended
            Stud_Support
            Stud_Ref
            Stud_IdentMark
            Stud_ReasonJoin
            Stud_HostelReq
            
            Stud_InternetReq
            Stud_Convence
            Stud_Suggestion
            Stud_LocalRefName1
            Stud_LocalRefName2
            Stud_LocalRef1Add
            Stud_LocalRef2Add
            Stud_LocalRef1Relation
            Stud_LocalRef2Relation
            Stud_LocalRef1Contact
            Stud_LocalRef2Contact
            Remark
            Stud_OldTcNo
            Stud_OldReasonToLeave
            Stud_LastSchoolAdd
            Stud_UniversityNo
            Stud_FeeStruct
            Stud_RefundAmt
            Stud_TotalFamilyMem
            Stud_FamilyIncome
            SameAddress
            NSO
            TC
            Suspend


            AdmSession



            CBy
            CDate
            MBy
            MDate
    }

    public class SiblingsInfoModel
    {
        Stud_BS1Name
          Stud_BS1Age
            Stud_BS1Gender
            Stud_BS1Class
            Stud_BS1Institute
            Stud_BS2Name
            Stud_BS2Age
            Stud_BS2Gender
            Stud_BS2Class
            Stud_BS2Institute
            Stud_BS3Name
            Stud_BS3Age
            Stud_BS3Gender
            Stud_BS3Class
            Stud_BS3Institute
            sibling
            Stud_SiblingUID
    }

    */
}
