using ISas.Entities.Academic;
using ISas.Entities.DashboardEntities;
using ISas.Entities.TimeTable_Entities;
using ISas.Repository.DashboardRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class DashboardRepo : IDashboardRepo
    {
        public Dashboard_StaffModel GetDashBoard_StaffDetails(string UserId, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Dashboard_StaffModel model = new Dashboard_StaffModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StaffLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.CircularList = ds.Tables[2].AsEnumerable().Select(r => new Common_NECN_LandingModel
                {
                    UploadTitle = r.Field<string>("UploadTitle"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadID = r.Field<string>("UploadId"),
                    CreationDate = r.Field<string>("CreationDate"),
                    HavingAttachment = r.Field<bool>("HavingAttachment"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    UploadAttachment = r.Field<string>("UploadAttachment"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),
                    IsNew = r.Field<bool>("IsNew"),
                }).ToList();

                //model.NewsList = ds.Tables[0].AsEnumerable().Select(r => new Common_NECN_LandingModel
                //{
                //    UploadTitle = r.Field<string>("UploadTitle"),
                //    UploadDescription = r.Field<string>("UploadDescription"),
                //    UploadID = r.Field<string>("UploadId"),

                //    CreationDate = r.Field<string>("CreationDate"),
                //    HavingAttachment = r.Field<bool>("HavingAttachment"),
                //    IsExpiry = r.Field<bool>("IsExpiry"),
                //    UploadAttachment = r.Field<string>("UploadAttachment"),
                //    UploadEndDate = r.Field<string>("UploadEndDate"),
                //    UploadStartDate = r.Field<string>("UploadStartDate"),

                //}).ToList();
                //model.EventsList = ds.Tables[1].AsEnumerable().Select(r => new Common_NECN_LandingModel
                //{
                //    UploadTitle = r.Field<string>("UploadTitle"),
                //    UploadDescription = r.Field<string>("UploadDescription"),
                //    UploadID = r.Field<string>("UploadId"),
                //    CreationDate = r.Field<string>("CreationDate"),
                //    HavingAttachment = r.Field<bool>("HavingAttachment"),
                //    IsExpiry = r.Field<bool>("IsExpiry"),
                //    UploadAttachment = r.Field<string>("UploadAttachment"),
                //    UploadEndDate = r.Field<string>("UploadEndDate"),
                //    UploadStartDate = r.Field<string>("UploadStartDate"),
                //}).ToList();
                //model.NoticeList = ds.Tables[3].AsEnumerable().Select(r => new Common_NECN_LandingModel
                //{
                //    UploadTitle = r.Field<string>("UploadTitle"),
                //    UploadDescription = r.Field<string>("UploadDescription"),
                //    UploadID = r.Field<string>("UploadId"),

                //    CreationDate = r.Field<string>("CreationDate"),
                //    HavingAttachment = r.Field<bool>("HavingAttachment"),
                //    IsExpiry = r.Field<bool>("IsExpiry"),
                //    UploadAttachment = r.Field<string>("UploadAttachment"),
                //    UploadEndDate = r.Field<string>("UploadEndDate"),
                //    UploadStartDate = r.Field<string>("UploadStartDate"),
                //}).ToList();
                model.AttendanceSummary = ds.Tables[4].AsEnumerable().Select(r => new Staff_AttendanceSummaryModel
                {
                    HD = r.Field<int>("HD"),
                    A = r.Field<int>("A"),
                    L = r.Field<int>("L"),
                    P = r.Field<int>("P"),
                    SL = r.Field<int>("SL"),
                    TotalAttendance = r.Field<string>("TotalAttendance"),
                }).FirstOrDefault();
                model.ToDoDetails = ds.Tables[5].AsEnumerable().Select(r => new ToDo_TaskEntitiesModel
                {
                    ToDoId = r.Field<int>("Id"),
                    //sno = r.Field<long>("sno"),
                    ToDoDate = r.Field<string>("toDoDate"),
                    ToDoDescription = r.Field<string>("ToDoDescription"),
                    ToDoSubject = r.Field<string>("ToDoSubject"),
                }).ToList();
                model.Student_BrithdayList = ds.Tables[6].AsEnumerable().Select(r => new Student_BirthdayDetailsModel
                {
                    DOB = r.Field<string>("DOB"),
                    Age = r.Field<int>("Age"),
                    DOB1 = r.Field<string>("DOB1"),
                    FullClassName = r.Field<string>("FullClassName"),
                    ImageURL = r.Field<string>("ImageURL"),
                    Student = r.Field<string>("Student"),
                    stud_uid = r.Field<string>("stud_uid"),
                }).ToList();
                model.Staff_BrithdayList = ds.Tables[7].AsEnumerable().Select(r => new Staff_BirthdayDetailsModel
                {
                    DOB = r.Field<string>("DOB"),
                    Age = r.Field<int>("Age"),
                    DOB1 = r.Field<string>("DOB1"),
                    ImageURL = r.Field<string>("ImageURL"),
                    StaffName = r.Field<string>("StaffName"),
                    StaffID = r.Field<string>("StaffID"),
                }).ToList();
                model.Staff_TimeTableDetails = ds.Tables[8].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("WeekDays"),
                    Value = r.Field<string>("Periods"),
                }).ToList();
                model.Staff_AdjustmentDetails = ds.Tables[9].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Periodname"),
                    Value = r.Field<string>("FullClassName"),
                }).ToList();
                model.Class_AttnSummary = ds.Tables[10].AsEnumerable().Select(r => new Class_AttendanceSummaryModel
                {
                    A = r.Field<int>("A"),
                    AttStatus = r.Field<string>("AttStatus"),
                    ClassStrength = r.Field<int>("ClassStrength"),
                    L = r.Field<int>("L"),
                    P = r.Field<int>("P"),
                }).FirstOrDefault();
                model.foodForThought = ds.Tables[11].Rows[0][0].ToString();
                if (model.Class_AttnSummary == null)
                    model.Class_AttnSummary = new Class_AttendanceSummaryModel();
                if (model.AttendanceSummary == null)
                    model.AttendanceSummary = new Staff_AttendanceSummaryModel();
                return model;
            }
        }
        public Dashboard_StudentModel GetDashBoard_StudentDetails(string UserId, string UserRole, string Date, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Dashboard_StudentModel dashBoardDetails = new Dashboard_StudentModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoardLandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserRole", UserRole);
                if (!string.IsNullOrEmpty(Date))
                    cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date).Date);

                cmd.Parameters.AddWithValue("@SessionId", SessionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                dashBoardDetails.NewsList = ds.Tables[0].AsEnumerable().Select(r => new Common_NECN_LandingModel
                {
                    UploadTitle = r.Field<string>("UploadTitle"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadID = r.Field<string>("UploadId"),

                    CreationDate = r.Field<string>("CreationDate"),
                    HavingAttachment = r.Field<bool>("HavingAttachment"),
                    IsNew = r.Field<bool>("IsNew"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    UploadAttachment = r.Field<string>("UploadAttachment"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),

                }).ToList();
                dashBoardDetails.EventsList = ds.Tables[1].AsEnumerable().Select(r => new Common_NECN_LandingModel
                {
                    UploadTitle = r.Field<string>("UploadTitle"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadID = r.Field<string>("UploadId"),
                    CreationDate = r.Field<string>("CreationDate"),
                    HavingAttachment = r.Field<bool>("HavingAttachment"),
                    IsNew = r.Field<bool>("IsNew"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    UploadAttachment = r.Field<string>("UploadAttachment"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),
                }).ToList();
                dashBoardDetails.CircularList = ds.Tables[2].AsEnumerable().Select(r => new Common_NECN_LandingModel
                {
                    UploadTitle = r.Field<string>("UploadTitle"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadID = r.Field<string>("UploadId"),
                    CreationDate = r.Field<string>("CreationDate"),
                    HavingAttachment = r.Field<bool>("HavingAttachment"),
                    IsNew = r.Field<bool>("IsNew"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    UploadAttachment = r.Field<string>("UploadAttachment"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),
                }).ToList();
                dashBoardDetails.NoticeList = ds.Tables[3].AsEnumerable().Select(r => new Common_NECN_LandingModel
                {
                    UploadTitle = r.Field<string>("UploadTitle"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadID = r.Field<string>("UploadId"),

                    CreationDate = r.Field<string>("CreationDate"),
                    HavingAttachment = r.Field<bool>("HavingAttachment"),
                    IsNew = r.Field<bool>("IsNew"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    UploadAttachment = r.Field<string>("UploadAttachment"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),
                }).ToList();

                //dashBoardDetails.HomeWorkList = ds.Tables[4].AsEnumerable().Select(r => new SelectListItem
                //{
                //    Text = r.Field<string>("Topic"),
                //    Value = r.Field<string>("HomeWorkId"),
                //}).ToList();

                dashBoardDetails.RouteDetails = ds.Tables[4].AsEnumerable().Select(r => new Dash_Stud_RouteDetailsModel
                {
                    AvailedTransport = r.Field<string>("AvailedTransport"),
                    Charges = r.Field<string>("Charges"),
                    Driver = r.Field<string>("Driver"),
                    DropTime = r.Field<string>("DropTime"),
                    Facility = r.Field<string>("Facility"),
                    Helper = r.Field<string>("Helper"),
                    PickupTime = r.Field<string>("PickupTime"),
                    Route = r.Field<string>("Route"),
                    Stop = r.Field<string>("Stop"),
                    VehicleNo = r.Field<string>("VehicleNo"),
                    VehicleType = r.Field<string>("VehicleType"),
                }).FirstOrDefault();
                if (ds.Tables[6].Rows.Count > 0)
                    dashBoardDetails.ClassTeacherName = ds.Tables[5].Rows[0][0].ToString();

                dashBoardDetails.SubjectTeacherList = ds.Tables[6].AsEnumerable().Select(r => new Dash_Stud_SubjectTeacherModel
                {
                    SubjectName = r.Field<string>("SubjectName"),
                    TeacherName = r.Field<string>("SubjectTeacher"),
                    ImageUrl = r.Field<string>("ImageUrl"),
                }).ToList();

                dashBoardDetails.ResultList = ds.Tables[7].AsEnumerable().Select(r => new Dash_Stud_ResultModel
                {
                    ClassId = r.Field<string>("ClassId"),
                    Duration = r.Field<string>("Duration"),
                    ExamId = r.Field<string>("ExamId"),
                    ExamName = r.Field<string>("ExamName"),
                    ExamStatus = r.Field<string>("ExamStatus"),
                    IsDownload = r.Field<bool>("IsDownload"),
                    Result = r.Field<string>("Result"),
                    ResultAnounceDate = r.Field<string>("ResultAnounceDate"),
                    SectionId = r.Field<string>("SectionId"),
                }).ToList();


                dashBoardDetails.ERPNo = ds.Tables[8].Rows[0][0].ToString();
                dashBoardDetails.DOB = ds.Tables[8].Rows[0][1].ToString();
                dashBoardDetails.InvitationType = ds.Tables[8].Rows[0][2].ToString();

                dashBoardDetails.HomeWorkList = ds.Tables[9].AsEnumerable().Select(r => new Academic_HomeWorkMasterModels
                {
                    Title = r.Field<string>("Title"),
                    AttachmentReference = r.Field<string>("AttachmentReference"),
                    textEditorPDFFilePath = r.Field<string>("DiscriptionReference"),
                    UploadDate = r.Field<string>("HomeWorkDate"),
                    SubmissionDate = r.Field<string>("SubmissionDate"),
                }).ToList();
                dashBoardDetails.AssignmentList = ds.Tables[10].AsEnumerable().Select(r => new Academic_HomeWorkMasterModels
                {
                    Title = r.Field<string>("Title"),
                    AttachmentReference = r.Field<string>("AttachmentReference"),
                    textEditorPDFFilePath = r.Field<string>("DiscriptionReference"),
                    UploadDate = r.Field<string>("HomeWorkDate"),
                    SubmissionDate = r.Field<string>("SubmissionDate"),
                }).ToList();

                dashBoardDetails.DirectoryList = ds.Tables[11].AsEnumerable().Select(r => new Academic_DirectoryMasterModels
                {
                    Name = r.Field<string>("Name"),
                    Contact = r.Field<string>("Contact"),
                    EmailId = r.Field<string>("EmailId"),
                }).ToList();
                dashBoardDetails.SyllabusList = ds.Tables[12].AsEnumerable().Select(r => new Dash_SyllabusDetailsModels
                {
                    Title = r.Field<string>("Title"),
                    AttachmentReference = r.Field<string>("AttachmentReference"),
                    DescriptionReferenctURL = r.Field<string>("DescriptionReferenctURL"),
                    UploadDate = r.Field<string>("UploadDate"),
                    SyllabusId = r.Field<string>("SyllabusId"),
                    UploadBy = r.Field<string>("UploadBy"),
                }).ToList();

                if (ds.Tables[13].Columns.Count > 1)
                {
                    for (int i = 1; i < ds.Tables[13].Columns.Count; i++)
                    {
                        dashBoardDetails.DaysList.Add(ds.Tables[13].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[13].Rows.Count; i++)
                    {
                        TimeTable_PeriodModel periodDetails = new TimeTable_PeriodModel();
                        string TempPeriod = ds.Tables[13].Rows[i][0].ToString();
                        List<string> periodStr = TempPeriod.Split('_').ToList(); ;
                        if (periodStr.Count == 2)
                        {
                            periodDetails.PeriodName = periodStr[0];
                            periodDetails.PeriodTime = periodStr[1];
                        }
                        List<TimeTable_InfoModel> tempPeriodInfoList = new List<TimeTable_InfoModel>();
                        for (int j = 1; j < ds.Tables[13].Columns.Count; j++)
                        {
                            string currentStr = ds.Tables[13].Rows[i][j].ToString();
                            TimeTable_InfoModel newRowInfo = new TimeTable_InfoModel();
                            if (!string.IsNullOrEmpty(currentStr))
                            {
                                //   newRowInfo.SubjectId = currentStr.Substring(0, 10);
                                //    newRowInfo.TeacherId = currentStr.Substring(0, 20).Substring(20 - 10);
                                List<string> strList = currentStr.Split('@').ToList();
                                if (strList.Count == 2)
                                {
                                    newRowInfo.SubjectName = strList[0].Replace("#", " || ");
                                    // newRowInfo.TeacherName = strList[2];
                                }
                            }
                            tempPeriodInfoList.Add(newRowInfo);
                        }

                        periodDetails.PeriodInfoList.AddRange(tempPeriodInfoList);
                        dashBoardDetails.PeriodDetailsList.Add(periodDetails);
                    }
                }

                dashBoardDetails.AttenDetails = ds.Tables[14].AsEnumerable().Select(r => new AttendanceSummaryModel
                {
                    A = r.Field<decimal>("A"),
                    HalfDay = r.Field<decimal>("P/2"),
                    L = r.Field<decimal>("L"),
                    // MnthName = r.Field<string>("UploadDate"),
                    OD = r.Field<int>("OD"),
                    P = r.Field<decimal>("P"),
                    FinalAttendance = r.Field<string>("FinalAttendance"),
                    AttDefaulterMessage = r.Field<string>("AttDefaulterMessage"),
                    Percentage = r.Field<decimal>("Percentage")
                }).FirstOrDefault();

                
                dashBoardDetails.FeeSummary = ds.Tables[15].AsEnumerable().Select(r => new FeeSummaryModel
                {
                    Flag = r.Field<string>("Flag"),
                    DueDate = r.Field<string>("DueDate"),
                    PayableAmount = r.Field<string>("PayableAmount"),
                    
                }).FirstOrDefault();

                dashBoardDetails.SMSDetails = ds.Tables[16].AsEnumerable().Select(r => new SMSDetailsModel
                {
                    SMSDate = r.Field<string>("SMSDate"),
                    SMSDeliveryDate = r.Field<string>("SMSDeliveryDate"),
                    SMSStatus = r.Field<string>("SMSStatus"),
                    SMSTExt = r.Field<string>("SMSTExt"),
                }).ToList();

                dashBoardDetails.BookHistory = ds.Tables[17].AsEnumerable().Select(r => new BookHistoryModel
                {
                    AccessionNo = r.Field<string>("AccessionNo"),
                    BookTitle = r.Field<string>("BookTitle"),
                    Fine = r.Field<int>("Fine"),
                    IssueDate = r.Field<string>("IssueDate"),
                    Sno = r.Field<string>("Sno"),
                    ReturnDate = r.Field<string>("ReturnDate"),
                }).ToList();

                dashBoardDetails.foodForThought = ds.Tables[18].Rows[0][0].ToString();
                return dashBoardDetails;
            }

        }

        public List<Tuple<string, string>> GetStudentAttenDetails(int Month, int Year, string UserID)
        {
            List<Tuple<string, string>> attenDetailList = new List<Tuple<string, string>>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_GetStudentAttendance", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserID);
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Year", Year);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                attenDetailList = ds.Tables[0].AsEnumerable().Select(r => new Tuple<string, string>(r.Field<string>("AttDate"), r.Field<string>("AttStaus")) { }).ToList();
            }
            return attenDetailList;
        }
        public List<Tuple<string, string>> GetStudentAttenDetails_ByERPNo(int Month, int Year, string UserID)
        {
            List<Tuple<string, string>> attenDetailList = new List<Tuple<string, string>>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_GetStudentAttend_ByERPNo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ERPNo", UserID);
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Year", Year);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                attenDetailList = ds.Tables[0].AsEnumerable().Select(r => new Tuple<string, string>(r.Field<string>("AttDate"), r.Field<string>("AttStaus")) { }).ToList();
            }
            return attenDetailList;
        }

        public List<AttnDetailsModel> GetStudentAttenDetails_BySession(int SessionId, string UserID)
        {
            List<AttnDetailsModel> attenDetailList = new List<AttnDetailsModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_GetStudentAnnualAttendance", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                attenDetailList = ds.Tables[0].AsEnumerable().Select(r => new AttnDetailsModel
                {
                    MonthName = r.Field<string>("MonthName"),
                    PresentCount = r.Field<int>("PresentCount"),
                    AbsentCount = r.Field<int>("AbsentCount"),
                    HolidayCount = r.Field<int>("HolidayCount"),
                    WeekOffCount = r.Field<int>("WeekOffCount"),
                    LeaveCount = r.Field<int>("LeaveCount"),
                    HalfDaysCount = r.Field<int>("HalfDaysCount"),

                }).ToList();
            }
            return attenDetailList;
        }

        public DashboardModel_Admin GetAdminDashboardDetails(string Date, string SessionId,string UserId)
        {
            DashboardModel_Admin dashBoardDetails = new DashboardModel_Admin();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string dadas = Convert.ToDateTime(Date).Date.ToString("yyyy-MM-dd");
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_LandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttDate", Convert.ToDateTime(Date).Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                dashBoardDetails.FeeCollection = ds.Tables[0].AsEnumerable().Select(r => new CollectionDetailModel
                {
                    CASH = r.Field<int>("CASH"),
                    CHEQUE = r.Field<int>("CHEQUE"),
                    ONLINE = r.Field<int>("ONLINE"),
                    TOTAL = r.Field<int>("TOTAL")
                }).FirstOrDefault();
                dashBoardDetails.TransportCollection = ds.Tables[1].AsEnumerable().Select(r => new CollectionDetailModel
                {
                    CASH = r.Field<int>("CASH"),
                    CHEQUE = r.Field<int>("CHEQUE"),
                    ONLINE = r.Field<int>("ONLINE"),
                    TOTAL = r.Field<int>("TOTAL")
                }).FirstOrDefault();

                dashBoardDetails.AttnStatusList = ds.Tables[2].AsEnumerable().Select(r => new AttendanceStatusModel
                {
                    AttenStatus = r.Field<string>("AttendanceStatus"),
                    NoofCount = r.Field<int>("NoofCount"),
                }).ToList();

                if (ds.Tables[4].Rows.Count > 0)
                {
                    dashBoardDetails.AdmissionDetails.Registration = Convert.ToInt32(ds.Tables[4].Rows[0][0]);
                    dashBoardDetails.AdmissionDetails.Admission = Convert.ToInt32(ds.Tables[4].Rows[0][1]);
                    dashBoardDetails.AdmissionDetails.TC = Convert.ToInt32(ds.Tables[4].Rows[0][2]);
                    dashBoardDetails.AdmissionDetails.NSO = Convert.ToInt32(ds.Tables[4].Rows[0][3]);
                }

                dashBoardDetails.noticeCircularList = ds.Tables[6].AsEnumerable().Select(r => new NoticeCircularList
                {
                    UploadID = r.Field<string>("UploadID"),
                    UploadTitle = r.Field<string>("UploadTitle"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    CreationDate = r.Field<string>("CreationDate"),
                    AttachPath = r.Field<string>("AttachPath"),
                    IsNew = r.Field<bool>("IsNew"),
                    HavingAttachment = r.Field<bool>("HavingAttachment")

                }).ToList();

                dashBoardDetails.homeWorkList = ds.Tables[7].AsEnumerable().Select(r => new HomeWorkList
                {
                    HomeWorkId = r.Field<string>("HomeWorkId"),
                    Title = r.Field<string>("Title"),
                    HomeWorkCategory = r.Field<string>("HomeWorkCategory"),
                    UploadDate = r.Field<string>("UploadDate"),
                    SubmissionDate = r.Field<string>("SubmissionDate"),
                    SubjectName = r.Field<string>("SubjectName"),
                    ClassName = r.Field<string>("ClassName"),
                    DiscriptionReference = r.Field<string>("DiscriptionReference")
                }).ToList();
                dashBoardDetails.classNotesList = ds.Tables[8].AsEnumerable().Select(r => new ClassNotesList
                {
                    HomeWorkId = r.Field<string>("HomeWorkId"),
                    Title = r.Field<string>("Title"),
                    HomeWorkCategory = r.Field<string>("HomeWorkCategory"),
                    UploadDate = r.Field<string>("UploadDate"),
                    SubmissionDate = r.Field<string>("SubmissionDate"),
                    SubjectName = r.Field<string>("SubjectName"),
                    ClassName = r.Field<string>("ClassName"),
                    DiscriptionReference = r.Field<string>("DiscriptionReference")
                }).ToList();

                dashBoardDetails.studentBirthdayList = ds.Tables[9].AsEnumerable().Select(r => new StudentBirthdayList
                {
                    Student = r.Field<string>("Student"),
                    Age = r.Field<int>("Age"),
                    DOB = r.Field<string>("DOB"),
                    DOB1 = r.Field<string>("DOB1"),
                    FullClassName = r.Field<string>("FullClassName"),
                    ImageURL = r.Field<string>("ImageURL")
                    
                }).ToList();
                dashBoardDetails.staffBirthdayList = ds.Tables[10].AsEnumerable().Select(r => new StaffBirthdayList
                {
                    StaffName = r.Field<string>("StaffName"),
                    Age = r.Field<int>("Age"),
                    DOB = r.Field<string>("DOB"),
                    DOB1 = r.Field<string>("DOB1"),
                    ImageURL = r.Field<string>("ImageURL")

                }).ToList();
                dashBoardDetails.sMSList = ds.Tables[11].AsEnumerable().Select(r => new SMSList
                {
                    MessageSend = r.Field<string>("MessageSend"),
                    SendTime = r.Field<string>("SendTime")
                }).ToList();

                dashBoardDetails.foodForThought = ds.Tables[12].Rows[0][0].ToString();
                //for (int i = 2; i < ds.Tables[3].Columns.Count - 1; i++)
                //{
                //    dashBoardDetails.SectionList.Add(ds.Tables[3].Columns[i].ColumnName);
                //}

                //dashBoardDetails.StudentStrengthList = ds.Tables[3].AsEnumerable().Select(r => new StudentStrenghtModel
                //{
                //    ClassName = r.Field<string>("ClassName"),
                //    Sec1 = r.Field<int>("A"),
                //    Sec2 = r.Field<int>("B"),
                //    Sec3 = r.Field<int>("C"),
                //    Sec4 = r.Field<int>("D"),
                //    Sec5 = r.Field<int>("E"),
                //    Total = r.Field<int>("Total"),
                //}).ToList();

                //for(int i =0; i < ds.Tables[3].Rows.Count; i++)
                //{
                //    for (int j = 0; j < dashBoardDetails.SectionList.Count - 1; j++)
                //    {
                //        dashBoardDetails.StudentStrengthList[i].SectionList.Add(new Tuple<string, string>(dashBoardDetails.SectionList[j], ds.Tables[3].Rows[i][j + 2].ToString()));
                //    }
                //}
            }
            return dashBoardDetails;
        }

        public DashboardModel_Admin GetAdminDashboardDetails_Atten(string Date, string SessionId)
        {
            DashboardModel_Admin dashBoardDetails = new DashboardModel_Admin();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string dadas = Convert.ToDateTime(Date).Date.ToString("yyyy-MM-dd");
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_LandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AttDate", Convert.ToDateTime(Date).Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@SessionId", SessionId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();


                dashBoardDetails.AttnStatusList = ds.Tables[2].AsEnumerable().Select(r => new AttendanceStatusModel
                {
                    AttenStatus = r.Field<string>("AttendanceStatus"),
                    NoofCount = r.Field<int>("NoofCount"),
                }).ToList();
            }
            return dashBoardDetails;
        }
        public DataSet GetAdminDashboardDetails_Strength(string Date, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_LandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttDate", Convert.ToDateTime(Date).Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }

        public DataSet GetAdminDashboardDetails_StaffAtten(string Date, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_LandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttDate", Convert.ToDateTime(Date).Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }

        public List<AttendanceDetailModel> AttendanceDetails(string ERPNo, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_Student_AttendanceSummary", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new AttendanceDetailModel
                {
                    A = r.Field<string>("A"),
                    HalfDay = r.Field<string>("P/2"),
                    L = r.Field<string>("L"),
                    MnthName = r.Field<string>("MnthName"),
                    OD = r.Field<string>("OD"),
                    P = r.Field<string>("P"),
                    MinPercentTextColor= r.Field<string>("MinPercentTextColor"),
                    Percentage = r.Field<decimal>("Percentage"),
                }).ToList();

                
            }
        }

        public string GetAttendanceDefaulterMessage(string ERPNo, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_Student_AttendanceSummary_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].Rows[0]["AttDefaulterMessage"].ToString();
            }
        }
    }
}
