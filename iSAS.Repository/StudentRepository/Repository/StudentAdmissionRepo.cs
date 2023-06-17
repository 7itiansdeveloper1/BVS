using ISas.Entities.RegistrationEntities;
using ISas.Repository.StudentRegistrationRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.StudentRegistrationRepository.Repository
{
    public class StudentAdmissionRepo : IStudentAdmissionRepo
    {
        public DropDownListFor_StudentAdmission GetAdmissionDropDownList(string ClassID,
        string FatherCountryID, string FatherStateID, string MotherCountryID, string MotherStateID,
        string CorresCountryID, string CorresStateID, string PermCountryID, string PermStateID,
        string GuardianCountryID, string GuardinaStateID, string RouteID, string SessionId,bool AvaildTransport
        )
        {
            DropDownListFor_StudentAdmission model = new DropDownListFor_StudentAdmission();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_AdmissionMaster_DorpDownList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@FatherCountryID", FatherCountryID);
                cmd.Parameters.AddWithValue("@FatherStateID", FatherStateID);
                cmd.Parameters.AddWithValue("@MotherCountryID", MotherCountryID);
                cmd.Parameters.AddWithValue("@MotherStateID", MotherStateID);
                cmd.Parameters.AddWithValue("@CorresCountryID", CorresCountryID);
                cmd.Parameters.AddWithValue("@CorresStateID", CorresStateID);
                cmd.Parameters.AddWithValue("@PermCountryID", PermCountryID);
                cmd.Parameters.AddWithValue("@PermStateID", PermStateID);
                cmd.Parameters.AddWithValue("@GuardianCountryID", GuardianCountryID);
                cmd.Parameters.AddWithValue("@GuardinaStateID", GuardinaStateID);
                cmd.Parameters.AddWithValue("@RouteID", RouteID);
                cmd.Parameters.AddWithValue("@AvaildTransport", AvaildTransport);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.StaffNameList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StaffName"),
                    Value = r.Field<string>("StaffID"),
                }).ToList();

                model.SessionList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SessName"),
                    Value = r.Field<string>("SessId"),
                   // Selected = r.Field<bool>("Default")
                }).ToList();

                if (model.SessionList.Count > 0)
                    model.Session = model.SessionList.FirstOrDefault().Value;

                model.ClassList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassId")
                }).ToList();
                model.SectionList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SecName"),
                    Value = r.Field<string>("SecID")
                }).ToList();
                model.StatusList.Add(new SelectListItem { Text = "ACTIVE", Value = "ACTIVE", Selected = true });
                model.StatusList.Add(new SelectListItem { Text = "TC", Value = "TC" });
                model.StatusList.Add(new SelectListItem { Text = "NSO", Value = "NSO" });
                model.BloodGroupList = ds.Tables[4].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("BldGrpName"),
                    Value = r.Field<string>("BldGrpId"),
                    Selected = r.Field<bool>("Default")
                }).ToList();
                model.HouseList = ds.Tables[5].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("HouseName"),
                    Value = r.Field<string>("HouseID")
                }).ToList();
                model.ClubList = ds.Tables[6].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClubName"),
                    Value = r.Field<string>("ClubId")
                }).ToList();
                model.SubCategoryList = ds.Tables[7].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CatName"),
                    Value = r.Field<string>("CatID"),
                    Selected = r.Field<bool>("Default")
                }).ToList();
                model.MinorCategoryList = ds.Tables[8].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("MCatName"),
                    Value = r.Field<string>("MCatID"),
                    Selected = r.Field<bool>("Default")
                }).ToList();
                model.ReligionList = ds.Tables[9].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ReligonName"),
                    Value = r.Field<string>("ReligonId"),
                    Selected = r.Field<bool>("Default")
                }).ToList();
                model.NationalityList = ds.Tables[10].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("NatName"),
                    Value = r.Field<string>("NatID"),
                    Selected = r.Field<bool>("Default")
                }).ToList();
                model.ERPNo = ds.Tables[11].Rows[0][0] == null ? "" : ds.Tables[11].Rows[0][0].ToString();
                model.AdmNo = ds.Tables[12].Rows[0][0] == null ? "" : ds.Tables[12].Rows[0][0].ToString();
                model.QualificationList = ds.Tables[13].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("QualifName"),
                    Value = r.Field<string>("QualifID")
                }).ToList();
                model.ProfissionList = ds.Tables[14].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ProfName"),
                    Value = r.Field<string>("ProfId")
                }).ToList();

                model.AlumniList.Add(new SelectListItem { Text = "NO", Value = "false", Selected = true });
                model.AlumniList.Add(new SelectListItem { Text = "YES", Value = "true" });

                model.CountryList = ds.Tables[15].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CountryName"),
                    Value = r.Field<string>("CountryId")

                }).ToList();

                model.AreaList = ds.Tables[16].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("AreaName"),
                    Value = r.Field<string>("AreaID")
                }).ToList();

                model.ModeOfTransList = ds.Tables[17].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ModeName"),
                    Value = r.Field<string>("ID")
                }).ToList();

                model.SectionList = ds.Tables[18].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SecName"),
                    Value = r.Field<string>("SecId")
                }).ToList();
                model.Father_StateList = ds.Tables[19].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StateName"),
                    Value = r.Field<string>("StateId")
                }).ToList();
                model.Father_CityList = ds.Tables[20].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CityName"),
                    Value = r.Field<string>("CityId")
                }).ToList();
                model.Mother_StateList = ds.Tables[21].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StateName"),
                    Value = r.Field<string>("StateId")
                }).ToList();
                model.MotherCityList = ds.Tables[22].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CityName"),
                    Value = r.Field<string>("CityId")
                }).ToList();
                model.Corres_AddStateList = ds.Tables[23].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StateName"),
                    Value = r.Field<string>("StateId")
                }).ToList();
                model.Corres_AddCityList = ds.Tables[24].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CityName"),
                    Value = r.Field<string>("CityId")
                }).ToList();
                model.Perm_AddStateList = ds.Tables[25].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StateName"),
                    Value = r.Field<string>("StateId")
                }).ToList();
                model.Perm_AddCityList = ds.Tables[26].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CityName"),
                    Value = r.Field<string>("CityId")
                }).ToList();
                model.Gurdi_StateList = ds.Tables[27].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StateName"),
                    Value = r.Field<string>("StateId")
                }).ToList();
                model.Gurdi_CityList = ds.Tables[28].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CityName"),
                    Value = r.Field<string>("CityId")
                }).ToList();


                model.Pick_RouteList = ds.Tables[29].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("RouteName"),
                    Value = r.Field<string>("RouteID")
                }).ToList();
                model.Pick_StopList = ds.Tables[30].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StopName"),
                    Value = r.Field<string>("StopId")
                }).ToList();

                model.PickedByList = ds.Tables[31].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("PickedupBy"),
                    Value = r.Field<int>("PId").ToString()
                }).ToList();

                model.AdmCategoryList = ds.Tables[32].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("AdmCategoryName"),
                    Value = r.Field<string>("AdmCategoryId").ToString()
                }).ToList();

                //  model.IsAdmNoAutoIncrement = Convert.ToBoolean(ds.Tables[32].Rows[0][0]);
            }
            return model;
        }

        public Tuple<int, string> Student_Admission_CRUD(Student_AdmissionMaster_Model model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_AdmissionForm_CRUD_New", con);

                #region Office Info
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", model.Function);
                cmd.Parameters.AddWithValue("@Sess", model.OfficeInfo.Session);
                cmd.Parameters.AddWithValue("@Stud_RegNo", model.OfficeInfo.Stud_RegNo);
                cmd.Parameters.AddWithValue("@UID", model.OfficeInfo.Stud_UID);
                cmd.Parameters.AddWithValue("@Adm", model.OfficeInfo.Stud_AdmNo);
                cmd.Parameters.AddWithValue("@Stud_DOA ", Convert.ToDateTime(model.OfficeInfo.Stud_DOA).Date);
                cmd.Parameters.AddWithValue("@IsStaffWard", model.OfficeInfo.Stud_Staffward);
                cmd.Parameters.AddWithValue("@StaffWardID ", model.OfficeInfo.Stud_StaffwardID);
                cmd.Parameters.AddWithValue("@SRANo ", model.OfficeInfo.SRANo);
                #endregion
                #region Student Info
                if (model.StudentInfo.AdmissionType == 'N')
                {
                    cmd.Parameters.AddWithValue("@NewAdm", 1);
                    cmd.Parameters.AddWithValue("@OldAdm", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NewAdm", 0);
                    cmd.Parameters.AddWithValue("@OldAdm", 1);
                }
                cmd.Parameters.AddWithValue("@Stud_Gender", model.StudentInfo.Stud_Gender);
                cmd.Parameters.AddWithValue("@Parent", model.StudentInfo.Stud_Parents);
                cmd.Parameters.AddWithValue("@Stud_DOB", Convert.ToDateTime(model.StudentInfo.Stud_DOB).Date);
                cmd.Parameters.AddWithValue("@BirthPlace", model.StudentInfo.Stud_BirthPlace);
                cmd.Parameters.AddWithValue("@Stud_FirstName", model.StudentInfo.Stud_FirstName);
                cmd.Parameters.AddWithValue("@Stud_MiddleName", model.StudentInfo.Stud_MiddleName);
                cmd.Parameters.AddWithValue("@Stud_LastName", model.StudentInfo.Stud_LastName);
                cmd.Parameters.AddWithValue("@Stud_Class", model.StudentInfo.Stud_Class);
                cmd.Parameters.AddWithValue("@Stud_Section", model.StudentInfo.Stud_Section);
                cmd.Parameters.AddWithValue("@Stud_RollNo", model.StudentInfo.Stud_ClassRollNo);
                cmd.Parameters.AddWithValue("@Stud_BldGrp", model.StudentInfo.Stud_BldGrp);
                cmd.Parameters.AddWithValue("@Stud_House", model.StudentInfo.Stud_House);
                cmd.Parameters.AddWithValue("@Stud_Club", model.StudentInfo.Stud_Club);
                cmd.Parameters.AddWithValue("@Stud_Category", model.StudentInfo.Stud_Category);
                cmd.Parameters.AddWithValue("@Stud_Religion", model.StudentInfo.Stud_Religion);
                cmd.Parameters.AddWithValue("@Stud_MCategory", model.StudentInfo.Stud_MCategory);
                cmd.Parameters.AddWithValue("@Stud_Nationality", model.StudentInfo.Stud_Nationality);
                //cmd.Parameters.AddWithValue("@Stud_Emailadd", textBox14.Text.Trim());
                cmd.Parameters.AddWithValue("@Stud_Aadhaar", model.StudentInfo.Stud_Aadhar);
                cmd.Parameters.AddWithValue("@Stud_Height", model.StudentInfo.Stud_Height);
                cmd.Parameters.AddWithValue("@Stud_Weight", model.StudentInfo.Stud_Weight);
                cmd.Parameters.AddWithValue("@Stud_Age", model.StudentInfo.Stud_Age);
                cmd.Parameters.AddWithValue("@Stud_AdmClass", model.StudentInfo.AdmClass);
                cmd.Parameters.AddWithValue("@Stud_LastSchool", model.StudentInfo.Stud_LastSchool);
                cmd.Parameters.AddWithValue("@Stud_LastBoard", model.StudentInfo.Stud_LastBoard);
                cmd.Parameters.AddWithValue("@Stud_BoardLocation", model.StudentInfo.Stud_LastBoardLocation);
                cmd.Parameters.AddWithValue("@Stud_BoardNo", model.StudentInfo.Stud_LastBoardNo);
                cmd.Parameters.AddWithValue("@Stud_LastClass", model.StudentInfo.Stud_LastClass);
                cmd.Parameters.AddWithValue("@Stud_NoofYearAttend", model.StudentInfo.Stud_Noofyearattend);
                cmd.Parameters.AddWithValue("@Stud_LastPer", model.StudentInfo.Stud_LastPer);
                cmd.Parameters.AddWithValue("@Stud_LastAcheivement", model.StudentInfo.Stud_LastAchievement);
                cmd.Parameters.AddWithValue("@Stud_MProblemName", model.StudentInfo.Stud_MProblem);
                cmd.Parameters.AddWithValue("@Stud_MDoctorName", model.StudentInfo.Stud_MDoctor);
                cmd.Parameters.AddWithValue("@Stud_MAllergytoMedicne", model.StudentInfo.Stud_MAllergyMedicine);
                cmd.Parameters.AddWithValue("@Stud_MClinicAdd", model.StudentInfo.Stud_MClinicAdd);
                #endregion
                #region  Father Info
                cmd.Parameters.AddWithValue("@Father_FirstName", model.FatherInfo.Father_FirstName);
                cmd.Parameters.AddWithValue("@Father_MiddleName", model.FatherInfo.Father_MiddleName);
                cmd.Parameters.AddWithValue("@Father_LastName", model.FatherInfo.Father_LastName);
                cmd.Parameters.AddWithValue("@Father_Qualif", model.FatherInfo.Father_Qualif);
                cmd.Parameters.AddWithValue("@Father_Prof", model.FatherInfo.Father_Prof);
                cmd.Parameters.AddWithValue("@Father_Desg ", model.FatherInfo.Father_Desg);
                cmd.Parameters.AddWithValue("@Father_MoblieNo", model.FatherInfo.Father_MoblieNo);
                cmd.Parameters.AddWithValue("@Father_Aadhar", model.FatherInfo.Father_Aadhar);
                cmd.Parameters.AddWithValue("@Father_Annualincome", model.FatherInfo.Father_AnnualIncome);
                cmd.Parameters.AddWithValue("@Father_Age", model.FatherInfo.Father_Age);
                cmd.Parameters.AddWithValue("@Father_Email", model.FatherInfo.Father_Email);
                cmd.Parameters.AddWithValue("@Father_Alumni", model.FatherInfo.Father_Alumni);
                cmd.Parameters.AddWithValue("@Father_AlumniYear", model.FatherInfo.Father_AlumniYear);
                cmd.Parameters.AddWithValue("@Father_OffAdd", model.FatherInfo.Father_OffAdd);
                cmd.Parameters.AddWithValue("@Father_Area", model.FatherInfo.Father_Area);
                cmd.Parameters.AddWithValue("@Father_City", model.FatherInfo.Father_City);
                cmd.Parameters.AddWithValue("@Father_State", model.FatherInfo.Father_State);
                cmd.Parameters.AddWithValue("@Father_Country", model.FatherInfo.Father_Country);
                cmd.Parameters.AddWithValue("@Father_PinCode", model.FatherInfo.Father_PinCode);
                cmd.Parameters.AddWithValue("@Father_OffNo", model.FatherInfo.Father_OffNo);
                cmd.Parameters.AddWithValue("@Stud_GFatherName", model.FatherInfo.Stud_GFatherName);
                #endregion
                #region Mother Info
                cmd.Parameters.AddWithValue("@Mother_FirstName", model.MotherInfo.Mother_FirstName);
                cmd.Parameters.AddWithValue("@Mother_MiddleName", model.MotherInfo.Mother_MiddleName);
                cmd.Parameters.AddWithValue("@Mother_LastName", model.MotherInfo.Mother_LastName);
                cmd.Parameters.AddWithValue("@Mother_Qualif", model.MotherInfo.Mother_Qualif);
                cmd.Parameters.AddWithValue("@Mother_Prof", model.MotherInfo.Mother_Prpf);
                cmd.Parameters.AddWithValue("@Mother_Desg", model.MotherInfo.Mother_Desg);
                cmd.Parameters.AddWithValue("@Mother_MoblieNo", model.MotherInfo.Mother_MobileNo);
                cmd.Parameters.AddWithValue("@Mother_Aadhar", model.MotherInfo.Mother_Aadhar);
                cmd.Parameters.AddWithValue("@Mother_Annualincome", model.MotherInfo.Mother_AnnualIncome);
                cmd.Parameters.AddWithValue("@Mother_Age", model.MotherInfo.Mother_Age);
                cmd.Parameters.AddWithValue("@Mother_Email", model.MotherInfo.Mother_Email);
                cmd.Parameters.AddWithValue("@Mother_Alumni", model.MotherInfo.Mother_Alumni);
                cmd.Parameters.AddWithValue("@Mother_AlumniYear", model.MotherInfo.Mother_AlumniYear);
                cmd.Parameters.AddWithValue("@Mother_OffAdd", model.MotherInfo.Mother_OffAdd);
                cmd.Parameters.AddWithValue("@Mother_Area", model.MotherInfo.Mother_Area);
                cmd.Parameters.AddWithValue("@Mother_City", model.MotherInfo.Mother_City);
                cmd.Parameters.AddWithValue("@Mother_State", model.MotherInfo.Mother_State);
                cmd.Parameters.AddWithValue("@Mother_Country", model.MotherInfo.Mother_Country);
                cmd.Parameters.AddWithValue("@Mother_Pincode", model.MotherInfo.Mother_Pincode);
                cmd.Parameters.AddWithValue("@Mother_OffNo", model.MotherInfo.Mother_OffNo);
                cmd.Parameters.AddWithValue("@Stud_GMotherName", model.MotherInfo.Stud_GMotherName);
                #endregion
                #region Guardian info
                cmd.Parameters.AddWithValue("@Guardian_FirstName", model.GurdianInfo.Gardian_FirstName);
                cmd.Parameters.AddWithValue("@Guardian_MiddleName", model.GurdianInfo.Gardian_MiddleName);
                cmd.Parameters.AddWithValue("@Guardian_LastName", model.GurdianInfo.Gardian_LastName);
                cmd.Parameters.AddWithValue("@Guardian_Qualif", model.GurdianInfo.Gardian_Qualif);
                cmd.Parameters.AddWithValue("@Guardian_Prof", model.GurdianInfo.Gardian_Prof);
                cmd.Parameters.AddWithValue("@Guardian_Desg", model.GurdianInfo.Gardian_Desg);
                cmd.Parameters.AddWithValue("@Guardian_MoblieNo", model.GurdianInfo.Gardian_MobileNo);
                cmd.Parameters.AddWithValue("@Guardian_Aadhar", model.GurdianInfo.Gardian_Aadhar);
                cmd.Parameters.AddWithValue("@Guardian_Annualincome", model.GurdianInfo.Gardian_AnnualIncome);
                cmd.Parameters.AddWithValue("@Guardian_Age", model.GurdianInfo.Gardian_Age);
                cmd.Parameters.AddWithValue("@Guardian_Email", model.GurdianInfo.Gardian_Email);
                cmd.Parameters.AddWithValue("@Guardian_Alumni", model.GurdianInfo.Gardian_Alumni);
                cmd.Parameters.AddWithValue("@Guardian_AlumniYear", model.GurdianInfo.Gardian_AlumniYear);
                cmd.Parameters.AddWithValue("@Guardian_OffAdd", model.GurdianInfo.Gardian_OffAdd);
                cmd.Parameters.AddWithValue("@Guardian_Area", model.GurdianInfo.Gardian_Area);
                cmd.Parameters.AddWithValue("@Guardian_City", model.GurdianInfo.Gardian_City);
                cmd.Parameters.AddWithValue("@Guardian_State", model.GurdianInfo.Gardian_State);
                cmd.Parameters.AddWithValue("@Guardian_Country", model.GurdianInfo.Gardian_Country);
                cmd.Parameters.AddWithValue("@Guardian_Pincode", model.GurdianInfo.Gardian_PinCode);
                cmd.Parameters.AddWithValue("@Guardian_OffNo", model.GurdianInfo.Gardian_OffNo);
                cmd.Parameters.AddWithValue("@Guardian_RelactiontoChild", model.GurdianInfo.Gardian_Relation);

                #endregion
                #region CorrAdd info


                cmd.Parameters.AddWithValue("@Stud_CorrAdd", model.AddressInfo.Stud_CorrAdd);
                cmd.Parameters.AddWithValue("@Stud_CorrArea", model.AddressInfo.Stud_CorrArea);
                cmd.Parameters.AddWithValue("@Stud_CorrCity", model.AddressInfo.Stud_CorrCity);
                cmd.Parameters.AddWithValue("@Stud_CorrState", model.AddressInfo.Stud_CorrState);
                cmd.Parameters.AddWithValue("@Stud_CorrCountry", model.AddressInfo.Stud_CorrCountry);
                cmd.Parameters.AddWithValue("@Stud_CorrPinCode", model.AddressInfo.Stud_CorrPinCode);
                cmd.Parameters.AddWithValue("@Stud_CorrMobile", model.AddressInfo.Stud_CorrMobile);
                cmd.Parameters.AddWithValue("@Stud_CorrPhNo1", model.AddressInfo.Stud_CorrPhNo1);
                cmd.Parameters.AddWithValue("@Stud_CorrPhNo2", model.AddressInfo.Stud_CorrPhNo2);
                #endregion
                #region PermAdd Info
                cmd.Parameters.AddWithValue("@Stud_PermAdd", model.AddressInfo.Stud_PermAdd);
                cmd.Parameters.AddWithValue("@Stud_PermArea", model.AddressInfo.Stud_PermArea);
                cmd.Parameters.AddWithValue("@Stud_PermCity", model.AddressInfo.Stud_PermCity);
                cmd.Parameters.AddWithValue("@Stud_PermState", model.AddressInfo.Stud_PermState);
                cmd.Parameters.AddWithValue("@Stud_PermCountry", model.AddressInfo.Stud_PermCountry);
                cmd.Parameters.AddWithValue("@Stud_PermPinCode", model.AddressInfo.Stud_PermPinCode);
                cmd.Parameters.AddWithValue("@Stud_PermMobile", model.AddressInfo.Stud_PermMobile);
                cmd.Parameters.AddWithValue("@Stud_PermPh1", model.AddressInfo.Stud_PermPh1);
                cmd.Parameters.AddWithValue("@Stud_PermPh2", model.AddressInfo.Stud_PermPh2);
                #endregion
                #region Avail Facility Info

                cmd.Parameters.AddWithValue("@Trans", model.AvailFacilityInfo.Stud_TransReq);
                cmd.Parameters.AddWithValue("@PRouteId", model.AvailFacilityInfo.PRouteId);
                cmd.Parameters.AddWithValue("@PStopID", model.AvailFacilityInfo.PStopId);
                cmd.Parameters.AddWithValue("@DRouteId", "-1");
                cmd.Parameters.AddWithValue("@DStopID", "-1");
                cmd.Parameters.AddWithValue("@Facility", model.AvailFacilityInfo.Facility);
                cmd.Parameters.AddWithValue("@TransDate", model.AvailFacilityInfo.TranDate == null ? DateTime.Now : Convert.ToDateTime(model.AvailFacilityInfo.TranDate).Date); //
                cmd.Parameters.AddWithValue("@ModeofTransport", model.AvailFacilityInfo.ModeofTransport);
                cmd.Parameters.AddWithValue("@PickedupBy", model.AvailFacilityInfo.PickedupBy);
                cmd.Parameters.AddWithValue("@AvailSnacks", model.AvailFacilityInfo.AvailSnacks);
                cmd.Parameters.AddWithValue("@AvailSMS", model.AvailFacilityInfo.AvailSMS);
                cmd.Parameters.AddWithValue("@SMSF", model.AvailFacilityInfo.SMS_WHOM == 'F' ? 1 : 0);
                cmd.Parameters.AddWithValue("@SMSM", model.AvailFacilityInfo.SMS_WHOM == 'M' ? 1 : 0);
                cmd.Parameters.AddWithValue("@SMSG", model.AvailFacilityInfo.SMS_WHOM == 'G' ? 1 : 0);
                cmd.Parameters.AddWithValue("@SMSO", model.AvailFacilityInfo.SMS_WHOM == 'O' ? 1 : 0);
                cmd.Parameters.AddWithValue("@SMSNO", model.AvailFacilityInfo.SMSNo);
                cmd.Parameters.AddWithValue("@Stud_PickedUpName", model.AvailFacilityInfo.Stud_PickedUpName);
                #endregion
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@AdmCategoryId", model.OfficeInfo.AdmCategoryId);
                cmd.Parameters.AddWithValue("@FamilyId", model.OfficeInfo.FamilyId);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public IEnumerable<Student_AdmissionLandingPageModel> GetAllStudent_AdmissionsList(string UserId, int FilterPercent)
        {
            List<Student_AdmissionLandingPageModel> model = new List<Student_AdmissionLandingPageModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_AdmissionForm_StudentList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@FilterPercent", FilterPercent);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model = ds.Tables[0].AsEnumerable().Select(r => new Student_AdmissionLandingPageModel
                {
                    ERP = r.Field<string>("ERP"),
                    DOA = r.Field<string>("DOA"),
                    AdmNo = r.Field<string>("AdmNo"),
                    RollNo = r.Field<int>("RollNo"),
                    Student = r.Field<string>("Student"),
                    Class = r.Field<string>("Class"),
                    Gender = r.Field<string>("Gender"),
                    DOB = r.Field<string>("DOB"),
                    Father = r.Field<string>("Father"),
                    Mother = r.Field<string>("Mother"),
                    Address = r.Field<string>("Address"),
                    FMobileNo = r.Field<string>("FMobileNo"),
                    MMobileNo = r.Field<string>("MMobileNo"),
                    SMSNo = r.Field<string>("SMSNo"),
                    AlternateNumber = r.Field<string>("AlternateNumber"),
                }).ToList();
            }
            return model;
        }
        public Student_AdmissionMaster_Model GetStudent_AdmissionDetailsByERPNo(string ErpNo, string SessionId)
        {
            Student_AdmissionMaster_Model model = new Student_AdmissionMaster_Model();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_GetStudentDetailsByERPNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ErpNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                //con.Close();
                if (dt.Rows.Count == 1)
                {
                    #region Office Info
                    model.OfficeInfo.Session = dt.Rows[0]["Session"].ToString();
                    model.OfficeInfo.Stud_UID = dt.Rows[0]["Stud_UID"].ToString();
                    model.OfficeInfo.Stud_AdmNo = dt.Rows[0]["Stud_AdmNo"].ToString();
                    model.OfficeInfo.Stud_RegNo = dt.Rows[0]["Stud_RegNo"].ToString();
                    model.OfficeInfo.Stud_Staffward = Convert.ToBoolean(dt.Rows[0]["Stud_Staffward"]);
                    model.OfficeInfo.Stud_StaffwardID = dt.Rows[0]["Stud_StaffwardID"].ToString();
                    model.OfficeInfo.Stud_DOA = dt.Rows[0]["Stud_DOA"].ToString();
                    model.OfficeInfo.SRANo = dt.Rows[0]["SRANo"].ToString();
                    model.OfficeInfo.AdmCategoryId = dt.Rows[0]["AdmCategoryId"].ToString();
                    model.OfficeInfo.FamilyId = dt.Rows[0]["FamilyId"].ToString();
                    #endregion
                    #region Student Info
                    if (Convert.ToBoolean(dt.Rows[0]["NewAdm"]))
                        model.StudentInfo.AdmissionType = 'N';
                    else
                        model.StudentInfo.AdmissionType = 'O';

                    model.StudentInfo.Stud_Gender = Convert.ToChar(dt.Rows[0]["Stud_Gender"]);
                    model.StudentInfo.Stud_Parents = dt.Rows[0]["Stud_Parents"].ToString();
                    model.StudentInfo.Stud_DOB = dt.Rows[0]["Stud_DOB"].ToString();
                    model.StudentInfo.Stud_BirthPlace = dt.Rows[0]["Stud_BirthPlace"].ToString();
                    model.StudentInfo.Stud_FirstName = dt.Rows[0]["Stud_FirstName"].ToString();
                    model.StudentInfo.Stud_MiddleName = dt.Rows[0]["Stud_MiddleName"].ToString();
                    model.StudentInfo.Stud_LastName = dt.Rows[0]["Stud_LastName"].ToString();
                    model.StudentInfo.Stud_Class = dt.Rows[0]["Stud_Class"].ToString();
                    model.StudentInfo.Stud_Section = dt.Rows[0]["Stud_Section"].ToString();
                    model.StudentInfo.Stud_Status = dt.Rows[0]["Stud_Status"].ToString();
                    model.StudentInfo.Stud_ClassRollNo = Convert.ToInt32(dt.Rows[0]["Stud_ClassRollNo"]);
                    model.StudentInfo.Stud_BldGrp = dt.Rows[0]["Stud_BldGrp"].ToString();
                    model.StudentInfo.Stud_House = dt.Rows[0]["Stud_House"].ToString();
                    model.StudentInfo.Stud_Club = dt.Rows[0]["Stud_Club"].ToString();
                    model.StudentInfo.Stud_Category = dt.Rows[0]["Stud_Category"].ToString();
                    model.StudentInfo.Stud_Religion = dt.Rows[0]["Stud_Religion"].ToString();
                    model.StudentInfo.Stud_MCategory = dt.Rows[0]["Stud_MCategory"].ToString();
                    model.StudentInfo.Stud_Nationality = dt.Rows[0]["Stud_Nationality"].ToString();
                    model.StudentInfo.Stud_Aadhar = dt.Rows[0]["Stud_Aadhar"].ToString();
                    model.StudentInfo.Stud_Height = dt.Rows[0]["Stud_Height"].ToString();
                    model.StudentInfo.Stud_Weight = dt.Rows[0]["Stud_Weight"].ToString();
                    model.StudentInfo.Stud_Age = Convert.ToInt32(dt.Rows[0]["Stud_Age"]);
                    model.StudentInfo.AdmClass = dt.Rows[0]["AdmClass"].ToString();
                    model.StudentInfo.Stud_Image = dt.Rows[0]["Stud_Image"].ToString();
                    model.StudentInfo.SiblingNames = dt.Rows[0]["SiblingName"].ToString();

                    //Acadmic Background
                    model.StudentInfo.Stud_LastSchool = dt.Rows[0]["Stud_LastSchool"].ToString();
                    model.StudentInfo.Stud_LastBoard = dt.Rows[0]["Stud_LastBoard"].ToString();
                    model.StudentInfo.Stud_LastBoardLocation = dt.Rows[0]["Stud_LastBoardLocation"].ToString();
                    model.StudentInfo.Stud_LastBoardNo = dt.Rows[0]["Stud_LastBoardNo"].ToString();
                    model.StudentInfo.Stud_LastClass = dt.Rows[0]["Stud_LastClass"].ToString();
                    model.StudentInfo.Stud_LastAchievement = dt.Rows[0]["Stud_LastAchievement"].ToString();
                    model.StudentInfo.Stud_Noofyearattend = Convert.ToInt32(dt.Rows[0]["Stud_Noofyearattend"]);
                    model.StudentInfo.Stud_LastPer = dt.Rows[0]["Stud_LastPer"].ToString();

                    //Medical Info
                    model.StudentInfo.Stud_MProblem = dt.Rows[0]["Stud_MProblem"].ToString();
                    model.StudentInfo.Stud_MDoctor = dt.Rows[0]["Stud_MDoctor"].ToString();
                    model.StudentInfo.Stud_MAllergyMedicine = dt.Rows[0]["Stud_MAllergyMedicine"].ToString();
                    model.StudentInfo.Stud_MClinicAdd = dt.Rows[0]["Stud_MClinicAdd"].ToString();
                    #endregion
                    #region Father Info
                    model.FatherInfo.Father_FirstName = dt.Rows[0]["Father_FirstName"].ToString();
                    model.FatherInfo.Father_MiddleName = dt.Rows[0]["Father_MiddleName"].ToString();
                    model.FatherInfo.Father_LastName = dt.Rows[0]["Father_LastName"].ToString();
                    model.FatherInfo.Father_Qualif = dt.Rows[0]["Father_Qualif"].ToString();
                    model.FatherInfo.Father_Prof = dt.Rows[0]["Father_Prof"].ToString();
                    model.FatherInfo.Father_MoblieNo = dt.Rows[0]["Father_MoblieNo"].ToString();
                    model.FatherInfo.Father_Aadhar = dt.Rows[0]["Father_Aadhar"].ToString();
                    model.FatherInfo.Father_AnnualIncome = Convert.ToInt32(dt.Rows[0]["Father_AnnualIncome"]);
                    model.FatherInfo.Father_Email = dt.Rows[0]["Father_Email"].ToString();
                    model.FatherInfo.Father_Alumni = Convert.ToBoolean(dt.Rows[0]["Father_Alumni"]);
                    model.FatherInfo.Father_AlumniYear = dt.Rows[0]["Father_AlumniYear"].ToString();
                    model.FatherInfo.Father_Age = Convert.ToInt32(dt.Rows[0]["Father_Age"]);
                    model.FatherInfo.Stud_GFatherName = dt.Rows[0]["Stud_GFatherName"].ToString();

                    //Image
                    model.FatherInfo.Grand_FatherImage = dt.Rows[0]["Grand_FatherImage"].ToString();
                    model.FatherInfo.Father_Image = dt.Rows[0]["Father_Image"].ToString();

                    //Address
                    model.FatherInfo.Father_OffAdd = dt.Rows[0]["Father_OffAdd"].ToString();
                    model.FatherInfo.Father_Area = dt.Rows[0]["Father_Area"].ToString();
                    model.FatherInfo.Father_City = dt.Rows[0]["Father_City"].ToString();
                    model.FatherInfo.Father_State = dt.Rows[0]["Father_State"].ToString();
                    model.FatherInfo.Father_Country = dt.Rows[0]["Father_Country"].ToString();
                    model.FatherInfo.Father_PinCode = dt.Rows[0]["Father_PinCode"].ToString();
                    model.FatherInfo.Father_MoblieNo = dt.Rows[0]["Father_MoblieNo"].ToString();
                    model.FatherInfo.Father_OffNo = dt.Rows[0]["Father_OffNo"].ToString();
                    #endregion
                    #region Mother Info
                    model.MotherInfo.Mother_FirstName = dt.Rows[0]["Mother_FirstName"].ToString();
                    model.MotherInfo.Mother_MiddleName = dt.Rows[0]["Mother_MiddleName"].ToString();
                    model.MotherInfo.Mother_LastName = dt.Rows[0]["Mother_LastName"].ToString();
                    model.MotherInfo.Mother_Qualif = dt.Rows[0]["Mother_Qualif"].ToString();
                    model.MotherInfo.Mother_Prpf = dt.Rows[0]["Mother_Prpf"].ToString();
                    model.MotherInfo.Mother_MobileNo = dt.Rows[0]["Mother_MobileNo"].ToString();
                    model.MotherInfo.Mother_Aadhar = dt.Rows[0]["Mother_Aadhar"].ToString();
                    model.MotherInfo.Mother_AnnualIncome = Convert.ToInt32(dt.Rows[0]["Mother_AnnualIncome"]);
                    model.MotherInfo.Mother_Email = dt.Rows[0]["Mother_Email"].ToString();
                    model.MotherInfo.Mother_Alumni = Convert.ToBoolean(dt.Rows[0]["Mother_Alumni"]);
                    model.MotherInfo.Mother_AlumniYear = dt.Rows[0]["Mother_AlumniYear"].ToString();
                    model.MotherInfo.Mother_Age = Convert.ToInt32(dt.Rows[0]["Mother_Age"]);
                    model.MotherInfo.Stud_GMotherName = dt.Rows[0]["Stud_GMotherName"].ToString();

                    //Image
                    model.MotherInfo.Grand_MotherImage = dt.Rows[0]["Grand_MotherImage"].ToString();
                    model.MotherInfo.Mother_Image = dt.Rows[0]["Mother_Image"].ToString();

                    //Address
                    model.MotherInfo.Mother_OffAdd = dt.Rows[0]["Mother_OffAdd"].ToString();
                    model.MotherInfo.Mother_Area = dt.Rows[0]["Mother_Area"].ToString();
                    model.MotherInfo.Mother_City = dt.Rows[0]["Mother_City"].ToString();
                    model.MotherInfo.Mother_State = dt.Rows[0]["Mother_State"].ToString();
                    model.MotherInfo.Mother_Country = dt.Rows[0]["Mother_Country"].ToString();
                    model.MotherInfo.Mother_Pincode = dt.Rows[0]["Mother_Pincode"].ToString();
                    model.MotherInfo.Mother_MobileNo = dt.Rows[0]["Mother_MobileNo"].ToString();
                    model.MotherInfo.Mother_OffNo = dt.Rows[0]["Mother_OffNo"].ToString();
                    #endregion
                    #region Guradian Info
                    model.GurdianInfo.Gardian_FirstName = dt.Rows[0]["Gardian_FirstName"].ToString();
                    model.GurdianInfo.Gardian_MiddleName = dt.Rows[0]["Gardian_MiddleName"].ToString();
                    model.GurdianInfo.Gardian_LastName = dt.Rows[0]["Gardian_LastName"].ToString();
                    model.GurdianInfo.Gardian_Qualif = dt.Rows[0]["Gardian_Qualif"].ToString();
                    model.GurdianInfo.Gardian_Prof = dt.Rows[0]["Gardian_Prof"].ToString();
                    model.GurdianInfo.Gardian_MobileNo = dt.Rows[0]["Gardian_MobileNo"].ToString();
                    model.GurdianInfo.Gardian_Aadhar = dt.Rows[0]["Gardian_Aadhar"].ToString();
                    model.GurdianInfo.Gardian_AnnualIncome = Convert.ToInt32(dt.Rows[0]["Gardian_AnnualIncome"]);
                    model.GurdianInfo.Gardian_Email = dt.Rows[0]["Gardian_Email"].ToString();
                    model.GurdianInfo.Gardian_Alumni = Convert.ToBoolean(dt.Rows[0]["Gardian_Alumni"]);
                    model.GurdianInfo.Gardian_AlumniYear = dt.Rows[0]["Gardian_AlumniYear"].ToString();
                    model.GurdianInfo.Gardian_Age = Convert.ToInt32(dt.Rows[0]["Gardian_Age"]);
                    model.GurdianInfo.Gardian_Relation = dt.Rows[0]["Gardian_Relation"].ToString();

                    //Image
                    model.GurdianInfo.Gardian_image = dt.Rows[0]["Gardian_image"].ToString();

                    //Address
                    model.GurdianInfo.Gardian_OffAdd = dt.Rows[0]["Gardian_OffAdd"].ToString();
                    model.GurdianInfo.Gardian_Area = dt.Rows[0]["Gardian_Area"].ToString();
                    model.GurdianInfo.Gardian_City = dt.Rows[0]["Gardian_City"].ToString();
                    model.GurdianInfo.Gardian_State = dt.Rows[0]["Gardian_State"].ToString();
                    model.GurdianInfo.Gardian_Country = dt.Rows[0]["Gardian_Country"].ToString();
                    model.GurdianInfo.Gardian_PinCode = dt.Rows[0]["Gardian_PinCode"].ToString();
                    model.GurdianInfo.Gardian_MobileNo = dt.Rows[0]["Gardian_MobileNo"].ToString();
                    model.GurdianInfo.Gardian_OffNo = dt.Rows[0]["Gardian_OffNo"].ToString();
                    #endregion
                    #region Address Info
                    //Local Address
                    model.AddressInfo.Stud_CorrAdd = dt.Rows[0]["Stud_CorrAdd"].ToString();
                    model.AddressInfo.Stud_CorrCountry = dt.Rows[0]["Stud_CorrCountry"].ToString();
                    model.AddressInfo.Stud_CorrState = dt.Rows[0]["Stud_CorrState"].ToString();
                    model.AddressInfo.Stud_CorrCity = dt.Rows[0]["Stud_CorrCity"].ToString();
                    model.AddressInfo.Stud_CorrArea = dt.Rows[0]["Stud_CorrArea"].ToString();
                    model.AddressInfo.Stud_CorrPinCode = dt.Rows[0]["Stud_CorrPinCode"].ToString();
                    model.AddressInfo.Stud_CorrMobile = dt.Rows[0]["Stud_CorrMobile"].ToString();
                    model.AddressInfo.Stud_CorrPhNo1 = dt.Rows[0]["Stud_CorrPhNo1"].ToString();
                    model.AddressInfo.Stud_CorrPhNo2 = dt.Rows[0]["Stud_CorrPhNo2"].ToString();

                    // Per Address
                    model.AddressInfo.Stud_PermAdd = dt.Rows[0]["Stud_PermAdd"].ToString();
                    model.AddressInfo.Stud_PermCountry = dt.Rows[0]["Stud_PermCountry"].ToString();
                    model.AddressInfo.Stud_PermState = dt.Rows[0]["Stud_PermState"].ToString();
                    model.AddressInfo.Stud_PermCity = dt.Rows[0]["Stud_PermCity"].ToString();
                    model.AddressInfo.Stud_PermArea = dt.Rows[0]["Stud_PermArea"].ToString();
                    model.AddressInfo.Stud_PermPinCode = dt.Rows[0]["Stud_PermPinCode"].ToString();
                    model.AddressInfo.Stud_PermMobile = dt.Rows[0]["Stud_PermMobile"].ToString();
                    model.AddressInfo.Stud_PermPh1 = dt.Rows[0]["Stud_PermPh1"].ToString();
                    model.AddressInfo.Stud_PermPh2 = dt.Rows[0]["Stud_PermPh2"].ToString();
                    #endregion
                    #region AvailFacility Info
                    //Traportation
                    model.AvailFacilityInfo.Stud_TransReq = Convert.ToBoolean(dt.Rows[0]["Stud_TransReq"]);
                    model.AvailFacilityInfo.Facility = dt.Rows[0]["Facility"].ToString();
                    model.AvailFacilityInfo.Stud_PickedUpName = dt.Rows[0]["Stud_PickedUpName"].ToString();
                    model.AvailFacilityInfo.PickedUPBy_Image = dt.Rows[0]["PickedUPBy_Image"].ToString();
                    model.AvailFacilityInfo.PRouteId = dt.Rows[0]["PRouteId"].ToString();
                    model.AvailFacilityInfo.PStopId = dt.Rows[0]["PStopId"].ToString();
                    model.AvailFacilityInfo.TranDate = dt.Rows[0]["TranDate"].ToString();
                    model.AvailFacilityInfo.ModeofTransport = dt.Rows[0]["ModeofTransport"].ToString();
                    model.AvailFacilityInfo.PickedupBy = dt.Rows[0]["PickedupBy"].ToString();

                    //SMS
                    model.AvailFacilityInfo.AvailSMS = Convert.ToBoolean(dt.Rows[0]["AvailSMS"]);
                    model.AvailFacilityInfo.AvailSnacks = Convert.ToBoolean(dt.Rows[0]["AvailSnacks"]);

                    if (Convert.ToBoolean(dt.Rows[0]["SMSF"]))
                        model.AvailFacilityInfo.SMS_WHOM = 'F';

                    else if (Convert.ToBoolean(dt.Rows[0]["SMSM"]))
                        model.AvailFacilityInfo.SMS_WHOM = 'M';

                    else if (Convert.ToBoolean(dt.Rows[0]["SMSG"]))
                        model.AvailFacilityInfo.SMS_WHOM = 'G';

                    else if (Convert.ToBoolean(dt.Rows[0]["SMSO"]))
                        model.AvailFacilityInfo.SMS_WHOM = 'O';

                    model.AvailFacilityInfo.SMSNo = dt.Rows[0]["SMSNO"].ToString();
                    model.LastModifiedBy = dt.Rows[0]["LastModifiedBy"].ToString();
                    #endregion
                }
            }
            return model;
        }

        public List<SelectListItem> GetModeOfTransportList(bool AvaildTransport)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                int avlTrns = AvaildTransport == true ? 1 : 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("select [ID],ModeName  from GetModeofTransportList("+ avlTrns +") order by PrintOrder  -- 17  ", con);
                cmd.CommandType = CommandType.Text;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ModeName"),
                    Value = r.Field<string>("ID"),
                }).ToList();
            }
        }
    }
}


