using ISas.Entities.StaffEntities;
using ISas.Entities.TimeTable_Entities;
using ISas.Repository.StaffRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.StaffRepository.Repository
{
    public class Staff_StaffDetailMasterRepo : IStaff_StaffDetailMasterRepo
    {
        public DropDownListFor_Staff_StaffDetailMaster GetStaffDetailMasterDropDownList(string CorresCountryID,
            string CorresStateID, string PermCountryID, string PermStateID)
        {
            DropDownListFor_Staff_StaffDetailMaster dropDownMaster = new DropDownListFor_Staff_StaffDetailMaster();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Staff_StaffDetailMaster_DorpDownList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CorresCountryID", CorresCountryID);
                cmd.Parameters.AddWithValue("@CorresStateID", CorresStateID);
                cmd.Parameters.AddWithValue("@PermCountryID", PermCountryID);
                cmd.Parameters.AddWithValue("@PermStateID", PermStateID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                dropDownMaster.NewStaffId = ds.Tables[0].Rows[0][0] == null ? "" : ds.Tables[0].Rows[0][0].ToString();
                dropDownMaster.NewEmpcode = ds.Tables[0].Rows[0][1] == null ? "" : ds.Tables[0].Rows[0][1].ToString();

                dropDownMaster.BloodGroupList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("BldGrpName"),
                    Value = r.Field<string>("BldGrpId"),
                    Selected = r.Field<bool>("Default"),
                }).ToList();

                dropDownMaster.CategoryList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CatName"),
                    Value = r.Field<string>("CatID"),
                }).ToList();

                dropDownMaster.RelignList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ReligonName"),
                    Value = r.Field<string>("ReligonId"),
                    Selected = r.Field<bool>("DEFAULT"),
                }).ToList();

                dropDownMaster.NationalityList = ds.Tables[4].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("NatName"),
                    Value = r.Field<string>("NatID"),
                    Selected = r.Field<bool>("DEFAULT"),
                }).ToList();

                dropDownMaster.QualificationList = ds.Tables[5].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("QualifName"),
                    Value = r.Field<string>("qualifID"),
                }).ToList();

                dropDownMaster.DepartmentList = ds.Tables[6].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DeptName"),
                    Value = r.Field<string>("DeptID"),
                }).ToList();

                dropDownMaster.DesignationList = ds.Tables[7].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DesigName"),
                    Value = r.Field<string>("DesigID"),
                }).ToList();

                dropDownMaster.BankList = ds.Tables[8].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("BankName"),
                    Value = r.Field<string>("BankID"),
                }).ToList();

                dropDownMaster.CoresStateList = ds.Tables[9].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StateName"),
                    Value = r.Field<string>("StateID"),
                }).ToList();

                dropDownMaster.CoresCityList = ds.Tables[10].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CityName"),
                    Value = r.Field<string>("CityID"),
                }).ToList();

                dropDownMaster.PermStateList = ds.Tables[11].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StateName"),
                    Value = r.Field<string>("StateID"),
                }).ToList();

                dropDownMaster.PermCityList = ds.Tables[12].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CityName"),
                    Value = r.Field<string>("CityID"),
                }).ToList();

                dropDownMaster.CountryList = ds.Tables[13].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CountryName"),
                    Value = r.Field<string>("CountryID"),
                }).ToList();

                dropDownMaster.AreaList = ds.Tables[14].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("AreaName"),
                    Value = r.Field<string>("AreaID"),
                }).ToList();



                dropDownMaster.RefIdTypeList.Add(new SelectListItem { Text = "VotorID Card", Value = "VotorID Card" });
                dropDownMaster.RefIdTypeList.Add(new SelectListItem { Text = "Pan Card", Value = "Pan Card" });
                dropDownMaster.RefIdTypeList.Add(new SelectListItem { Text = "Aadhar Card", Value = "Aadhar Card" });
                dropDownMaster.RefIdTypeList.Add(new SelectListItem { Text = "Driving License", Value = "Driving License" });
                dropDownMaster.RefIdTypeList.Add(new SelectListItem { Text = "Ratioin Card", Value = "Ratioin Card" });
                dropDownMaster.RefIdTypeList.Add(new SelectListItem { Text = "Electricity Bill", Value = "Electricity Bill" });
                dropDownMaster.RefIdTypeList.Add(new SelectListItem { Text = "Passport", Value = "Passport" });
                dropDownMaster.RefIdTypeList.Add(new SelectListItem { Text = "Bank Passbook", Value = "Bank Passbook" });


                dropDownMaster.StatusList.Add(new SelectListItem { Text = "Working", Value = "Working" });
                dropDownMaster.StatusList.Add(new SelectListItem { Text = "On Holiday", Value = "On Holiday" });
                dropDownMaster.StatusList.Add(new SelectListItem { Text = "On Maternity Leave", Value = "On Maternity Leave" });
                dropDownMaster.StatusList.Add(new SelectListItem { Text = "Suspended", Value = "Suspended" });
                dropDownMaster.StatusList.Add(new SelectListItem { Text = "Halted", Value = "Halted" });
                dropDownMaster.StatusList.Add(new SelectListItem { Text = "Resigned", Value = "Resigned" });
                dropDownMaster.StatusList.Add(new SelectListItem { Text = "Under Notice Period", Value = "Under Notice Period" });
                dropDownMaster.StatusList.Add(new SelectListItem { Text = "Left", Value = "Left" });
                dropDownMaster.StatusList.Add(new SelectListItem { Text = "Terminted", Value = "Terminted" });


                dropDownMaster.ConveyanceList.Add(new SelectListItem { Text = "Self", Value = "Self" });
                dropDownMaster.ConveyanceList.Add(new SelectListItem { Text = "Moterbike", Value = "Moterbike" });
                dropDownMaster.ConveyanceList.Add(new SelectListItem { Text = "Car", Value = "Car" });
                dropDownMaster.ConveyanceList.Add(new SelectListItem { Text = "Scotter", Value = "Scotter" });
                dropDownMaster.ConveyanceList.Add(new SelectListItem { Text = "Public Transport", Value = "Public Transport" });
                dropDownMaster.ConveyanceList.Add(new SelectListItem { Text = "School Transport", Value = "School Transport" });
                dropDownMaster.ConveyanceList.Add(new SelectListItem { Text = "Other", Value = "Other" });
            }
            return dropDownMaster;
        }

        public Tuple<int, string> Staff_StaffDetailMaster_CRUD(Staff_StaffDetailMasterModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Staff_StaffDetailMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", model.Function);

                #region Student Info
                cmd.Parameters.AddWithValue("@StaffID", model.StaffID);
                cmd.Parameters.AddWithValue("@Sex", model.Sex);
                cmd.Parameters.AddWithValue("@Fname", model.FName);
                cmd.Parameters.AddWithValue("@MName", model.MName);
                cmd.Parameters.AddWithValue("@LName", model.LName);

                if (!string.IsNullOrEmpty(model.StaffDOB))
                    cmd.Parameters.AddWithValue("@StaffDOB ", Convert.ToDateTime(model.StaffDOB).Date);

                cmd.Parameters.AddWithValue("@Cat", model.Cat);
                cmd.Parameters.AddWithValue("@Relgn", model.Relgn);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Mob", model.Mob);
                cmd.Parameters.AddWithValue("@Nationality", model.Nationality);
                cmd.Parameters.AddWithValue("@BldGrp", model.BldGrp);
                cmd.Parameters.AddWithValue("@Age", model.Age);
                cmd.Parameters.AddWithValue("@Mstatus", model.MStatus);
                cmd.Parameters.AddWithValue("@Qualif", model.Qualif);
                cmd.Parameters.AddWithValue("@AOI", model.AOI);
                cmd.Parameters.AddWithValue("@Texp", model.TExp);
                cmd.Parameters.AddWithValue("@IExp", model.IExp);
                cmd.Parameters.AddWithValue("@Oexp", model.OExp);
                cmd.Parameters.AddWithValue("@StaffImg", model.StaffImg);
                #endregion

                #region Office Details
                cmd.Parameters.AddWithValue("@StaffCode", model.StaffCode);
                cmd.Parameters.AddWithValue("@StaffGrp", model.StaffGrp);
                //cmd.Parameters.AddWithValue("@Wing", model.Wing);
                cmd.Parameters.AddWithValue("@Dept", model.Dept);
                cmd.Parameters.AddWithValue("@Desig", model.Desig);

                if (!string.IsNullOrEmpty(model.StaffDOJ))
                    cmd.Parameters.AddWithValue("@StaffDOJ", Convert.ToDateTime(model.StaffDOJ).Date);
                cmd.Parameters.AddWithValue("@Bank", model.Bank);
                cmd.Parameters.AddWithValue("@StaffType", model.StaffType);
                cmd.Parameters.AddWithValue("@BankIFSC", model.BankIFSC);
                cmd.Parameters.AddWithValue("@BankNo", model.BankNo);
                cmd.Parameters.AddWithValue("@Pancard", model.Pancard);
                cmd.Parameters.AddWithValue("@CurrStat", model.CurrStat);

                if (!string.IsNullOrEmpty(model.StaffDOL))
                    cmd.Parameters.AddWithValue("@StaffDOL", Convert.ToDateTime(model.StaffDOL).Date);
                cmd.Parameters.AddWithValue("@Lreason", model.LReason);
                cmd.Parameters.AddWithValue("@Convenyce", model.Convenyce);
                cmd.Parameters.AddWithValue("@VehNo", model.VehNo);
                cmd.Parameters.AddWithValue("@FHName", model.FHName);

                if (!string.IsNullOrEmpty(model.DOAnniversary))
                    cmd.Parameters.AddWithValue("@DOAnniversary", Convert.ToDateTime(model.DOAnniversary).Date);

                cmd.Parameters.AddWithValue("@MotherName", model.MotherName);
                cmd.Parameters.AddWithValue("@ContactNo", model.ContactNo);
                cmd.Parameters.AddWithValue("@EmergNo", model.EmergNo);
                cmd.Parameters.AddWithValue("@StaffRef1", model.StaffRef1);
                cmd.Parameters.AddWithValue("@Ref1Workin", model.Ref1Workin);
                cmd.Parameters.AddWithValue("@Ref1IDType", model.Ref1IDType);
                cmd.Parameters.AddWithValue("@Ref1IDNo", model.Ref1IDNo);
                cmd.Parameters.AddWithValue("@StaffRef2", model.StaffRef2);
                cmd.Parameters.AddWithValue("@Ref2Workin", model.Ref2Workin);
                cmd.Parameters.AddWithValue("@Ref2IDType", model.Ref2IDType);
                cmd.Parameters.AddWithValue("@Ref2IDNo", model.Ref2IDNo);
                //cmd.Parameters.AddWithValue("@TG", model.TG);
                //cmd.Parameters.AddWithValue("@Shft", model.Shft);
                cmd.Parameters.AddWithValue("@PunchCode", model.PunchCode);
                cmd.Parameters.AddWithValue("@SGS", model.SGS);
                cmd.Parameters.AddWithValue("@SGA", model.SGA);
                cmd.Parameters.AddWithValue("@SGM", model.SGM);
                cmd.Parameters.AddWithValue("@Active", model.Active);
                #endregion

                #region Address Info
                //Corres Add
                cmd.Parameters.AddWithValue("@CorrAdd", model.CorrAdd);
                cmd.Parameters.AddWithValue("@CorrArea ", model.CorrArea);
                cmd.Parameters.AddWithValue("@CorrCity ", model.CorrCity);
                cmd.Parameters.AddWithValue("@CorrState", model.CorrState);
                cmd.Parameters.AddWithValue("@CorrCountry", model.CorrCountry);
                cmd.Parameters.AddWithValue("@CorrPinCode", model.CorrPinCode);
                cmd.Parameters.AddWithValue("@CorrMobile", model.CorrMobile);
                cmd.Parameters.AddWithValue("@CorrPhNo1", model.CorrPhNo1);
                cmd.Parameters.AddWithValue("@CorrPhNo2", model.CorrPhNo2);

                //Perm Add
                cmd.Parameters.AddWithValue("@PermAdd", model.PermAdd);
                cmd.Parameters.AddWithValue("@PermArea ", model.PermArea);
                cmd.Parameters.AddWithValue("@PermCity ", model.PermCity);
                cmd.Parameters.AddWithValue("@PermState", model.PermState);
                cmd.Parameters.AddWithValue("@PermCountry", model.PermCountry);
                cmd.Parameters.AddWithValue("@PermPinCode", model.PermPinCode);
                cmd.Parameters.AddWithValue("@PermMobile", model.PermMobile);
                cmd.Parameters.AddWithValue("@PermPh1", model.PermPh1);
                cmd.Parameters.AddWithValue("@PermPh2", model.PermPh2);
                #endregion
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@AadharNo", model.AadharNo);
                if (!string.IsNullOrEmpty(model.DOC))
                    cmd.Parameters.AddWithValue("@DOC", Convert.ToDateTime(model.DOC).Date);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public IEnumerable<Staff_StaffDetailMasterModel> GetAllStaff_StaffDetailMasterList(string StaffID, string IncludeInactive)
        {
            List<Staff_StaffDetailMasterModel> ListOfStaffDetails = new List<Staff_StaffDetailMasterModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_Staff_StaffDetailMaster_List", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffID", StaffID);
                cmd.Parameters.AddWithValue("@IncludeInactive", IncludeInactive);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                ListOfStaffDetails = ds.Tables[0].AsEnumerable().Select(r => new Staff_StaffDetailMasterModel
                {
                    StaffID = r.Field<string>("StaffID"),
                    Sex = r.Field<string>("Sex"),
                    FName = r.Field<string>("FName"),
                    MName = r.Field<string>("MName"),
                    LName = r.Field<string>("LName"),
                    StaffImg = r.Field<string>("StaffImg"),
                    StaffDOB = r.Field<string>("StaffDOB"),
                    Cat = r.Field<string>("Cat"),
                    Relgn = r.Field<string>("Relgn"),
                    Mob = r.Field<string>("Mob"),
                    Email = r.Field<string>("Email"),
                    Nationality = r.Field<string>("Nationality"),
                    BldGrp = r.Field<string>("BldGrp"),
                    Age = r.Field<int>("Age"),
                    MStatus = r.Field<string>("MStatus"),
                    Qualif = r.Field<string>("Qualif"),
                    AOI = r.Field<string>("AOI"),
                    TExp = r.Field<decimal>("TExp"),
                    IExp = r.Field<decimal>("IExp"),
                    OExp = r.Field<decimal>("OExp"),
                    TotExp = r.Field<decimal>("TotExp"),
                    StaffCode = r.Field<string>("StaffCode"),
                    //Wing = r.Field<string>("ERP"),
                    StaffGrp = r.Field<string>("StaffGrp"),
                    Dept = r.Field<string>("Dept"),
                    Desig = r.Field<string>("Desig"),
                    StaffDOJ = r.Field<string>("StaffDOJ"),
                    Bank = r.Field<string>("Bank"),
                    StaffType = r.Field<string>("StaffType"),
                    BankIFSC = r.Field<string>("BankIFSC"),
                    BankNo = r.Field<string>("BankNo"),
                    Pancard = r.Field<string>("Pancard"),
                    CurrStat = r.Field<string>("CurrStat"),
                    StaffDOL = r.Field<string>("StaffDOL"),
                    LReason = r.Field<string>("LReason"),
                    Convenyce = r.Field<string>("Convenyce"),
                    VehNo = r.Field<string>("VehNo"),
                    FHName = r.Field<string>("FHName"),
                    MotherName = r.Field<string>("MotherName"),
                    ContactNo = r.Field<string>("ContactNo"),
                    EmergNo = r.Field<string>("EmergNo"),
                    StaffRef1 = r.Field<string>("StaffRef1"),
                    Ref1Workin = r.Field<string>("Ref1Workin"),
                    Ref1IDType = r.Field<string>("Ref1IDType"),
                    Ref1IDNo = r.Field<string>("Ref1IDNo"),
                    StaffRef2 = r.Field<string>("StaffRef2"),
                    Ref2Workin = r.Field<string>("Ref2Workin"),
                    Ref2IDType = r.Field<string>("Ref2IDType"),
                    Ref2IDNo = r.Field<string>("Ref2IDNo"),
                    CorrAdd = r.Field<string>("CorrAdd"),
                    CorrArea = r.Field<string>("CorrArea"),
                    CorrCity = r.Field<string>("CorrCity"),
                    CorrState = r.Field<string>("CorrState"),
                    CorrCountry = r.Field<string>("CorrCountry"),
                    CorrPinCode = r.Field<string>("CorrPinCode"),
                    CorrMobile = r.Field<string>("CorrMobile"),
                    CorrPhNo1 = r.Field<string>("CorrPhNo1"),
                    CorrPhNo2 = r.Field<string>("CorrPhNo2"),
                    PermAdd = r.Field<string>("PermAdd"),
                    PermArea = r.Field<string>("PermArea"),
                    PermCity = r.Field<string>("PermCity"),
                    PermState = r.Field<string>("PermState"),
                    PermCountry = r.Field<string>("PermCountry"),
                    PermPinCode = r.Field<string>("PermPinCode"),
                    PermMobile = r.Field<string>("PermMobile"),
                    PermPh1 = r.Field<string>("PermPh1"),
                    PermPh2 = r.Field<string>("PermPh2"),
                    Active = r.Field<bool>("Active"),
                    JoinSession = r.Field<string>("JoinSession"),
                    //TG = r.Field<int>("ERP"),
                    //Shft = r.Field<int>("ERP"),
                    PunchCode = r.Field<string>("PunchCode"),
                    SGS = r.Field<bool>("SGS"),
                    SGA = r.Field<bool>("SGA"),
                    SGM = r.Field<bool>("SGM"),
                    DOAnniversary = r.Field<string>("DOAnniversary"),
                    AadharNo = r.Field<string>("AadharNo"),
                    DOC = r.Field<string>("DOC"),

                }).ToList();
            }
            return ListOfStaffDetails;
        }
        public List<TeacherDetailModel> GetTeacherList()
        {
            List<TeacherDetailModel> ListOfStaffDetails = new List<TeacherDetailModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from dbo.GetTachingStaffList() order by StaffName", con);
                cmd.CommandType = CommandType.Text;
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                ListOfStaffDetails = ds.Tables[0].AsEnumerable().Select(r => new TeacherDetailModel
                {
                    StaffID = r.Field<string>("StaffID"),
                    DeptName = r.Field<string>("DeptName"),
                    Email = r.Field<string>("Email"),
                    Mob = r.Field<string>("Mob"),
                    StaffCode = r.Field<string>("StaffCode"),
                    StaffName = r.Field<string>("StaffName"),
                    
                }).ToList();
            }
            return ListOfStaffDetails;
        }


        public StaffProfileDetailsModel GetStaffProfileDetails(string UserID)
        {
            StaffProfileDetailsModel model = new StaffProfileDetailsModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetStaffProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model = ds.Tables[0].AsEnumerable().Select(r => new StaffProfileDetailsModel
                {
                    StaffID = r.Field<string>("StaffID"),
                    Active = r.Field<bool>("Active"),
                    Address1 = r.Field<string>("Address1"),
                    Address2 = r.Field<string>("Address2"),
                    BloodGroup = r.Field<string>("Blood Group"),
                    Category = r.Field<string>("Category"),
                    Department = r.Field<string>("Department"),
                    Designation = r.Field<string>("Designation"),
                    DOB = r.Field<string>("DOB"),
                    DOJ = r.Field<string>("DOJ"),
                    DOL = r.Field<string>("DOL"),
                    FatherOrHusName = r.Field<string>("F/H Name"),
                    HRNo = r.Field<string>("HRNo"),
                    ImageURL = r.Field<string>("ImageURL"),
                    MartialStatus = r.Field<string>("Martial Status"),
                    MobileNo = r.Field<string>("Mobile No"),
                    Mother = r.Field<string>("Mother"),
                    Ph1 = r.Field<string>("Ph1"),
                    Ph2 = r.Field<string>("Ph2"),
                    Qualification = r.Field<string>("Qualification"),
                    ReligonName = r.Field<string>("ReligonName"),
                    StaffName = r.Field<string>("Staff Name"),
                    Email = r.Field<string>("Email"),
                    EmergencyContactNo = r.Field<string>("EmergencyNo"),
                    Gender = r.Field<string>("Gender"),
                    StaffType = r.Field<string>("StaffType"),
                }).FirstOrDefault();
            }
            return model;
        }


        public Staff_StaffDetailMasterModel GetStaff_StaffDetailMasterByStaffID(string StaffID)
        {
            return GetAllStaff_StaffDetailMasterList(StaffID, "1").FirstOrDefault();
        }

        public string GetStaffIDByUserID(string UserID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetStaffIdByUserId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else return "";
        }



        public TimeTable_SetupModels GetStaffTimeTable(string StaffId)
        {
            TimeTable_SetupModels model = new TimeTable_SetupModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_TeacherTimeTable", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TeacherId", StaffId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();


                if (ds.Tables[0].Columns.Count > 1)
                {
                    for (int i = 1; i < ds.Tables[0].Columns.Count; i++)
                    {
                        model.DaysList.Add(ds.Tables[0].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TimeTable_PeriodModel periodDetails = new TimeTable_PeriodModel();
                        periodDetails.PeriodName = ds.Tables[0].Rows[i][0].ToString();
                        List<TimeTable_InfoModel> tempPeriodInfoList = new List<TimeTable_InfoModel>();
                        for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                        {
                            string currentStr = ds.Tables[0].Rows[i][j].ToString();
                            TimeTable_InfoModel newRowInfo = new TimeTable_InfoModel();
                            if (!string.IsNullOrEmpty(currentStr))
                            {
                                List<string> strList = currentStr.Split('_').ToList();
                                if (strList.Count == 2)
                                {
                                    newRowInfo.SubjectName = strList[1];
                                    newRowInfo.TeacherName = strList[0];
                                }
                            }
                            tempPeriodInfoList.Add(newRowInfo);
                        }

                        periodDetails.PeriodInfoList.AddRange(tempPeriodInfoList);
                        model.PeriodDetailsList.Add(periodDetails);
                    }
                }
            }
            return model;
        }
    }
}
