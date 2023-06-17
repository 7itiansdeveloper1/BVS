using ISas.Entities.DashboardEntities;
using ISas.Entities.TimeTable_Entities;
using ISas.Repository.DashboardRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class DashBoard_StudentStataticsRepo : IDashBoard_StudentStataticsRepo
    {
        #region  Admission Details
        public StudentAdmissionDetailsMainModels GetStudentAdmissionDetails()
        {
            StudentAdmissionDetailsMainModels model = new StudentAdmissionDetailsMainModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentStatatics", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.StudentAdmissionDetails = ds.Tables[0].AsEnumerable().Select(r => new StudentAdmissionDetailsSubModel
                {
                    BOY = r.Field<int>("BOY"),
                    ClassId = r.Field<string>("ClassId"),
                    ClassPO = r.Field<int>("ClassPO"),
                    EWS = r.Field<int>("EWS"),
                    FullClassName = r.Field<string>("FullClassName"),
                    GEN = r.Field<int>("GEN"),
                    GIRL = r.Field<int>("GIRL"),
                    NewAdm = r.Field<int>("NewAdm"),
                    NSO = r.Field<int>("NSO"),
                    OBC = r.Field<int>("OBC"),
                    OldAdm = r.Field<int>("OldAdm"),
                    SC = r.Field<int>("SC"),
                    SecId = r.Field<string>("SecId"),
                    SectionPO = r.Field<int>("SectionPO"),
                    ST = r.Field<int>("ST"),
                    Strength = r.Field<int>("Strength"),
                    TC = r.Field<int>("TC"),
                }).ToList();

                model.Strength = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                model.NewAdm = Convert.ToInt32(ds.Tables[1].Rows[0][1]);
                model.OldAdm = Convert.ToInt32(ds.Tables[1].Rows[0][2]);
                model.TC = Convert.ToInt32(ds.Tables[1].Rows[0][3]);
                model.NSO = Convert.ToInt32(ds.Tables[1].Rows[0][4]);
                model.BOY = Convert.ToInt32(ds.Tables[1].Rows[0][5]);
                model.GIRL = Convert.ToInt32(ds.Tables[1].Rows[0][6]);
                model.GEN = Convert.ToInt32(ds.Tables[1].Rows[0][7]);
                model.SC = Convert.ToInt32(ds.Tables[1].Rows[0][8]);
                model.ST = Convert.ToInt32(ds.Tables[1].Rows[0][9]);
                model.OBC = Convert.ToInt32(ds.Tables[1].Rows[0][10]);
                model.EWS = Convert.ToInt32(ds.Tables[1].Rows[0][11]);
            }
            return model;
        }
        public List<StudentAdmissionDetailsSubModel> GetStudentAdmissionDetails_Charts()
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentStatatics_Chart", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new StudentAdmissionDetailsSubModel
                {
                    ClassId = r.Field<string>("ClassId"),
                    FullClassName = r.Field<string>("FullClassName"),
                    Strength = r.Field<int>("Strength"),
                    NewAdm = r.Field<int>("NewAdm"),
                    OldAdm = r.Field<int>("OldAdm"),
                    TC = r.Field<int>("TC"),
                    NSO = r.Field<int>("NSO"),
                    BOY = r.Field<int>("BOY"),
                    GIRL = r.Field<int>("GIRL"),

                    EWS = r.Field<int>("EWS"),
                    GEN = r.Field<int>("GEN"),
                    OBC = r.Field<int>("OBC"),
                    SC = r.Field<int>("SC"),
                    ST = r.Field<int>("ST"),
                }).ToList();
            }
        }
        public List<_StudentDetails> _GetStudentAdmissionDetails(string ClassSectionId, string StaticMode)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentStatatics_ClassList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                cmd.Parameters.AddWithValue("@StaticMode", StaticMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new _StudentDetails
                {
                    AdmNo = r.Field<string>("Adm No"),
                    DOA = r.Field<string>("DOA"),
                    Father = r.Field<string>("Father"),
                    SMSNo = r.Field<string>("SMSNo"),
                    Sno = r.Field<Int64>("Sno"),
                    StudentName = r.Field<string>("Student"),
                    ERP = r.Field<string>("ERP"),
                }).ToList();
            }
        }
        #endregion

        #region  FeeCollection Details
        public FeeCollectionDetailsModel GetFeeCollectionDetails()
        {
            FeeCollectionDetailsModel model = new FeeCollectionDetailsModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_FeeStatatics", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.FeeCollectionDetails = ds.Tables[0].AsEnumerable().Select(r => new FeeCollectionDetailsSubModel
                {
                    AnnualBalance = r.Field<int>("AnnualBalance"),
                    AnnualDue = r.Field<int>("AnnualDue"),
                    AnnualReceived = r.Field<int>("AnnualReceived"),
                    Balance = r.Field<int>("Balance"),
                    ClassSectionId = r.Field<string>("ClassSectionId"),
                    DefaulterStudent = r.Field<int>("DefaulterStudent"),
                    Due = r.Field<int>("Due"),
                    Fullclass = r.Field<string>("Fullclass"),
                    PaidStudent = r.Field<int>("PaidStudent"),
                    Received = r.Field<int>("Received"),
                    Strength = r.Field<int>("Strength"),
                }).ToList();

                model.Strength = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                model.PaidStudent = Convert.ToInt32(ds.Tables[1].Rows[0][1]);
                model.DefaulterStudent = Convert.ToInt32(ds.Tables[1].Rows[0][2]);
                model.Due = Convert.ToInt32(ds.Tables[1].Rows[0][3]);
                model.Received = Convert.ToInt32(ds.Tables[1].Rows[0][4]);
                model.Balance = Convert.ToInt32(ds.Tables[1].Rows[0][5]);
                model.AnnualDue = Convert.ToInt32(ds.Tables[1].Rows[0][6]);
                model.AnnualReceived = Convert.ToInt32(ds.Tables[1].Rows[0][7]);
                model.AnnualBalance = Convert.ToInt32(ds.Tables[1].Rows[0][8]);
            }
            return model;
        }

        public FeeCollectionDetailsModel GetFeeCollectionDetails_Charts()
        {
            FeeCollectionDetailsModel model = new FeeCollectionDetailsModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_FeeStatatics_Chart", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.FeeCollectionDetails = ds.Tables[0].AsEnumerable().Select(r => new FeeCollectionDetailsSubModel
                {
                    AnnualBalance = r.Field<int>("AnnualBalance"),
                    AnnualDue = r.Field<int>("AnnualDue"),
                    AnnualReceived = r.Field<int>("AnnualReceived"),
                    Balance = r.Field<int>("Balance"),
                    ClassSectionId = r.Field<string>("ClassSectionId"),
                    DefaulterStudent = r.Field<int>("DefaulterStudent"),
                    Due = r.Field<int>("Due"),
                    Fullclass = r.Field<string>("Fullclass"),
                    PaidStudent = r.Field<int>("PaidStudent"),
                    Received = r.Field<int>("Received"),
                    Strength = r.Field<int>("Strength"),
                    //AReceivedPercent = r.Field<int>("AReceivedPercent"),
                    //PaidCountPercent = r.Field<int>("PaidCountPercent"),
                    //ReceivedPercent = r.Field<int>("ReceivedPercent"),

                    // ReceivedPercent = r.Field<int>("Due") < 0 ? (r.Field<int>("Received") / r.Field<int>("Due")) * 100 : 0,
                    //  AReceivedPercent = r.Field<int>("AnnualDue") < 0 ? (r.Field<int>("AnnualReceived") / r.Field<int>("AnnualDue")) * 100 : 0,
                    // PaidCountPercent = r.Field<int>("Strength") < 0 ? (r.Field<int>("PaidStudent") / r.Field<int>("Strength")) * 100 : 0,

                }).ToList();

                model.Strength = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                model.PaidStudent = Convert.ToInt32(ds.Tables[1].Rows[0][1]);
                model.DefaulterStudent = Convert.ToInt32(ds.Tables[1].Rows[0][2]);
                model.Due = Convert.ToInt32(ds.Tables[1].Rows[0][3]);
                model.Received = Convert.ToInt32(ds.Tables[1].Rows[0][4]);
                model.Balance = Convert.ToInt32(ds.Tables[1].Rows[0][5]);
                model.AnnualDue = Convert.ToInt32(ds.Tables[1].Rows[0][6]);
                model.AnnualReceived = Convert.ToInt32(ds.Tables[1].Rows[0][7]);
                model.AnnualBalance = Convert.ToInt32(ds.Tables[1].Rows[0][8]);

                model.ReceivedPercent = Convert.ToInt32(ds.Tables[1].Rows[0][9]);
                model.AReceivedPercent = Convert.ToInt32(ds.Tables[1].Rows[0][10]);
                model.PaidCountPercent = Convert.ToInt32(ds.Tables[1].Rows[0][11]);
            }
            return model;
        }

        public List<_StudentDetails> _GetFeeCollectionDetails(string ClassSectionId, string StaticMode, string SessionId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_FeeStatatics_ClassList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                cmd.Parameters.AddWithValue("@Mode", StaticMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables.Count > 0)
                    return ds.Tables[0].AsEnumerable().Select(r => new _StudentDetails
                    {
                        Sno = r.Field<Int64>("Sno"),
                        ERP = r.Field<string>("ERP"),
                        DOA = r.Field<string>("DOA"),
                        AdmNo = r.Field<string>("Adm No"),
                        StudentName = r.Field<string>("Student"),
                        Duration = r.Field<string>("Duration"),
                        Due = r.Field<int>("Due"),
                        Paid = r.Field<int>("Paid"),
                        Balance = r.Field<int>("Balance"),
                        Excess = r.Field<int>("Excess"),
                        SMSNo = r.Field<string>("SMSNo"),
                        RecNo = r.Field<string>("RecNo"),
                        Mode = r.Field<string>("Mode"),
                        Father = r.Field<string>("Father"),
                    }).ToList();
                return new List<_StudentDetails>();
            }
        }


        public List<_StudentDetails> _GetDefaulterOrBalanceDetails(string ClassSectionId, string SessionId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_DefaulterReport_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                cmd.Parameters.AddWithValue("@DefaulterType", "F");
                cmd.Parameters.AddWithValue("@DefaulterAsonDate", Convert.ToDateTime(DateTime.Now.ToShortDateString().Replace("-", "/")));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new _StudentDetails
                {
                    Sno = r.Field<Int64>("sno"),
                    ERP = r.Field<string>("ERPNo"),
                    StudentName = r.Field<string>("Student"),
                    AdmNo = r.Field<string>("AdmNo"),
                    Class = r.Field<string>("Class"),
                    Duration = r.Field<string>("Duration"),
                    Balance = r.Field<int>("Balance"),
                    SMSNo = r.Field<string>("SMSNo"),
                    Father = r.Field<string>("Father"),
                }).ToList();
            }
        }
        #endregion

        public StudentDozearModel GetStudentDozear(string erpNo, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StudentDozearModel model = new StudentDozearModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_Dozear", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.StudentDetails.ERP = ds.Tables[0].Rows[0][0].ToString();
                    model.StudentDetails.AdmNo = ds.Tables[0].Rows[0][1].ToString();
                    model.StudentDetails.RollNo = Convert.ToInt32(ds.Tables[0].Rows[0][2]);
                    model.StudentDetails.DOA = ds.Tables[0].Rows[0][3].ToString();
                    model.StudentDetails.StudentName = ds.Tables[0].Rows[0][4].ToString();
                    model.StudentDetails.Class = ds.Tables[0].Rows[0][5].ToString();
                    model.StudentDetails.DOB = ds.Tables[0].Rows[0][6].ToString();
                    model.StudentDetails.BG = ds.Tables[0].Rows[0][7].ToString();
                    model.StudentDetails.Religion = ds.Tables[0].Rows[0][8].ToString();
                    model.StudentDetails.Category = ds.Tables[0].Rows[0][9].ToString();
                    model.StudentDetails.Gender = ds.Tables[0].Rows[0][10].ToString();
                    model.StudentDetails.Father = ds.Tables[0].Rows[0][11].ToString();
                    model.StudentDetails.FMobile = ds.Tables[0].Rows[0][12].ToString();
                    model.StudentDetails.FEmail = ds.Tables[0].Rows[0][13].ToString();
                    model.StudentDetails.Mother = ds.Tables[0].Rows[0][14].ToString();
                    model.StudentDetails.MMobile = ds.Tables[0].Rows[0][15].ToString();
                    model.StudentDetails.SMSNo = ds.Tables[0].Rows[0][16].ToString();
                    model.StudentDetails.Address = ds.Tables[0].Rows[0][17].ToString();
                    model.StudentDetails.SAddhar = ds.Tables[0].Rows[0][18].ToString();
                    model.StudentDetails.FAddhar = ds.Tables[0].Rows[0][19].ToString();
                    model.StudentDetails.MAddhar = ds.Tables[0].Rows[0][20].ToString();
                    model.StudentDetails.FProf = ds.Tables[0].Rows[0][21].ToString();
                    model.StudentDetails.MProf = ds.Tables[0].Rows[0][22].ToString();
                    model.StudentDetails.FQuli = ds.Tables[0].Rows[0][23].ToString();
                    model.StudentDetails.MQuli = ds.Tables[0].Rows[0][24].ToString();
                    model.StudentDetails.Income = Convert.ToDecimal(ds.Tables[0].Rows[0][25]);
                    model.StudentDetails.S = ds.Tables[0].Rows[0][26].ToString();
                    model.StudentDetails.F = ds.Tables[0].Rows[0][27].ToString();
                    model.StudentDetails.M = ds.Tables[0].Rows[0][28].ToString();
                }
                model.SubjectTeachers = ds.Tables[1].AsEnumerable().Select(r => new SubjectTeacherModel
                {
                    ImageURL = r.Field<string>("ImageUrl"),
                    SubjectName = r.Field<string>("SubjectName"),
                    TeacherName = r.Field<string>("SubjectTeacher"),
                }).ToList();
                if (ds.Tables[2].Columns.Count > 1)
                {
                    for (int i = 1; i < ds.Tables[2].Columns.Count; i++)
                    {
                        model.DaysList.Add(ds.Tables[2].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        TimeTable_PeriodModel periodDetails = new TimeTable_PeriodModel();
                        string TempPeriod = ds.Tables[2].Rows[i][0].ToString();
                        List<string> periodStr = TempPeriod.Split('_').ToList(); ;
                        if (periodStr.Count == 2)
                        {
                            periodDetails.PeriodName = periodStr[0];
                            periodDetails.PeriodTime = periodStr[1];
                        }
                        List<TimeTable_InfoModel> tempPeriodInfoList = new List<TimeTable_InfoModel>();
                        for (int j = 1; j < ds.Tables[2].Columns.Count; j++)
                        {
                            string currentStr = ds.Tables[2].Rows[i][j].ToString();
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
                        model.PeriodDetailsList.Add(periodDetails);
                    }
                }
                model.AttenDetails = ds.Tables[3].AsEnumerable().Select(r => new AttendanceDetailModel
                {
                    A = r.Field<string>("A"),
                    HalfDay = r.Field<string>("P/2"),
                    L = r.Field<string>("L"),
                    MnthName = r.Field<string>("MnthName"),
                    OD = r.Field<string>("OD"),
                    P = r.Field<string>("P"),
                }).ToList();
                model.FeeDetails = ds.Tables[5].AsEnumerable().Select(r => new FeeDetailsModel
                {
                    Balance = r.Field<int>("Balance"),
                    Bill = r.Field<string>("Bill"),
                    Due = r.Field<int>("Due"),
                    DueDate = r.Field<string>("DueDate"),
                    Duration = r.Field<string>("Duration"),
                    Excess = r.Field<int>("Excess"),
                    InvoiceNo = r.Field<string>("InvoiceNo"),
                    Paid = r.Field<int>("Paid"),
                    ReceiptNo = r.Field<string>("ReceiptNo"),
                    sno = r.Field<long>("sno"),
                    Status = r.Field<string>("Status"),
                }).ToList();

                model.SMSDetails = ds.Tables[6].AsEnumerable().Select(r => new SMSDetailsModel
                {
                    SMSDate = r.Field<string>("SMSDate"),
                    SMSDeliveryDate = r.Field<string>("SMSDeliveryDate"),
                    SMSStatus = r.Field<string>("SMSStatus"),
                    SMSTExt = r.Field<string>("SMSTExt"),
                    Sno = r.Field<long>("Sno"),
                }).ToList();

                model.BookHistory = ds.Tables[7].AsEnumerable().Select(r => new BookHistoryModel
                {
                    AccessionNo = r.Field<string>("AccessionNo"),
                    BookTitle = r.Field<string>("BookTitle"),
                    Fine = r.Field<int>("Fine"),
                    IssueDate = r.Field<string>("IssueDate"),
                    ReturnDate = r.Field<string>("ReturnDate"),
                    Sno = r.Field<string>("Sno"),
                }).ToList();


                if (ds.Tables[8].Rows.Count > 0)
                {
                    model.TransportDetails.AvailedTransport = ds.Tables[8].Rows[0][0].ToString();
                    model.TransportDetails.Route = ds.Tables[8].Rows[0][1].ToString();
                    model.TransportDetails.Stop = ds.Tables[8].Rows[0][2].ToString();
                    model.TransportDetails.Facility = ds.Tables[8].Rows[0][3].ToString();
                    model.TransportDetails.Charges = ds.Tables[8].Rows[0][4].ToString();
                    model.TransportDetails.PickupTime = ds.Tables[8].Rows[0][5].ToString();
                    model.TransportDetails.DropTime = ds.Tables[8].Rows[0][6].ToString();
                    model.TransportDetails.Driver = ds.Tables[8].Rows[0][7].ToString();
                    model.TransportDetails.Helper = ds.Tables[8].Rows[0][8].ToString();
                    model.TransportDetails.VehicleNo = ds.Tables[8].Rows[0][9].ToString();
                    model.TransportDetails.VehicleType = ds.Tables[8].Rows[0][10].ToString();
                }
                model.SiblingNames = ds.Tables[9].AsEnumerable().Select(r => new StudentSiblingDetailsModel
                {
                    ERPNo = r.Field<string>("ERPNo"),
                    SiblingName = r.Field<string>("Student"),
                }).ToList();
                return model;
            }
        }

        public List<AttendanceDetailModel> StudentAttendanceDetails(string erpNo, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StudentDozearModel model = new StudentDozearModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_Dozear", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[3].AsEnumerable().Select(r => new AttendanceDetailModel
                {
                    A = r.Field<string>("A"),
                    HalfDay = r.Field<string>("P/2"),
                    L = r.Field<string>("L"),
                    MnthName = r.Field<string>("MnthName"),
                    OD = r.Field<string>("OD"),
                    P = r.Field<string>("P"),
                }).ToList();
            }
        }

        public List<StudentDetailsModel> GetClassStudentDetails(string classSectionId, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StudentDozearModel model = new StudentDozearModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentStatatics_Child", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassSectionId", classSectionId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@Mode", "Student");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new StudentDetailsModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    Student = r.Field<string>("Student"),
                    Father = r.Field<string>("Father"),
                    Mother = r.Field<string>("Mother"),
                    SMSNo = r.Field<string>("SMSNo"),
                    Category = r.Field<string>("Category"),
                    ClassId = r.Field<string>("ClassId"),
                    FullClassName = r.Field<string>("FullClassName"),
                    Gender = r.Field<string>("Gender"),
                    NewAdm = r.Field<bool>("NewAdm"),
                    NSO = r.Field<bool>("NSO"),
                    OldAdm = r.Field<bool>("OldAdm"),
                    Religion = r.Field<string>("Religion"),
                    SectionId = r.Field<string>("SectionId"),
                    TC = r.Field<bool>("TC"),
                }).ToList();
            }
        }

        public List<StudentDetailsModel> GetFeeDefaulterDetails(string classSectionId, string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StudentDozearModel model = new StudentDozearModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_StudentStatatics_Child", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassSectionId", classSectionId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@Mode", "Fee");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new StudentDetailsModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    Student = r.Field<string>("Student"),
                    Father = r.Field<string>("Father"),
                   // Mother = r.Field<string>("Mother"),
                    SMSNo = r.Field<string>("SMSNo"),
                }).ToList();
            }
        }
    }
}
