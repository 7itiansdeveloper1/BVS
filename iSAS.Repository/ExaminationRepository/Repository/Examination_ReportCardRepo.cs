using ISas.Entities.Examination_Entities;
using ISas.Repository.ExaminationRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.Repository
{
    public class Examination_ReportCardRepo : IExamination_ReportCardRepo
    {
        public List<SelectListItem> Get_ReportCardDropDowns(string SessionId, string UserId, string ClassId, string Mode, string ExamId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ReportCard_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId ", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamId);
                cmd.Parameters.AddWithValue("@Mode", Mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (Mode == "FormLoad")
                    return ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("ExamTemplateName"),
                        Value = r.Field<string>("ExamTemplateId"),
                    }).ToList();

                else if (Mode == "GetClassList")
                    return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("ClassName"),
                        Value = r.Field<string>("ClassID"),
                    }).ToList();

                else if (Mode == "GetSectionList")
                    return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("SecName"),
                        Value = r.Field<string>("SecID"),
                    }).ToList();

                else
                    return new List<SelectListItem>();
            }
        }
        public Examination_ReportCardModels Get_ReportCardStudentDetails(string SessionId, string UserId, string ClassId, string SectionId, string ExamTempleteId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Examination_ReportCardModels model = new Examination_ReportCardModels();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ReportCard_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId ", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassID", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamTempleteId);
                cmd.Parameters.AddWithValue("@Mode", "GetClassStudentList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.StudentDetails = ds.Tables[0].AsEnumerable().Select(r => new ReportCard_StudentDetailsModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ERPNo = r.Field<string>("ERPNo"),
                    RollNo = r.Field<int>("RollNo"),
                    Student = r.Field<string>("Student"),
                }).ToList();
                return model;
            }
        }

        public Examination_ReportCard_HtmlPrintModel Get_StudentReportDetails(Examination_ReportCardModels parm)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Examination_ReportCard_HtmlPrintModel model = new Examination_ReportCard_HtmlPrintModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ReportCard6to8", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ERPNo", string.Join(",", parm.StudentDetails.Where(r => r.Selected).Select(r => r.ERPNo).ToList()));
                cmd.Parameters.AddWithValue("@ExamId", parm.ExamId);
                cmd.Parameters.AddWithValue("@SessionId", parm.SessionId);
                cmd.Parameters.AddWithValue("@ClassSectionId", parm.ClassId + parm.SectionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.HeaderDetails.Header1 = ds.Tables[0].Rows[0][0].ToString();
                    model.HeaderDetails.Header2 = ds.Tables[0].Rows[0][1].ToString();
                    model.HeaderDetails.Header3 = ds.Tables[0].Rows[0][2].ToString();
                    model.HeaderDetails.Header4 = ds.Tables[0].Rows[0][3].ToString();
                    model.HeaderDetails.LogoURL = ds.Tables[0].Rows[0][4].ToString();
                    model.HeaderDetails.Place = ds.Tables[0].Rows[0][5].ToString();
                    model.HeaderDetails.SessionName = ds.Tables[0].Rows[0][6].ToString();
                    model.HeaderDetails.ReportcardDate = ds.Tables[0].Rows[0][7].ToString();
                    model.HeaderDetails.CBSELogo = ds.Tables[0].Rows[0][8].ToString();
                }
                model.StudentDetailsList = ds.Tables[1].AsEnumerable().Select(r => new Examination_ReportCard_StudentDetailsModel
                {
                    AdmNo = r.Field<string>("Adm No"),
                    Address = r.Field<string>("Address"),
                    RollNo = r.Field<int>("RollNo"),
                    Student = r.Field<string>("Student"),
                    Attendance = r.Field<string>("Attendance"),
                    Class = r.Field<string>("Class"),
                    DOA = r.Field<string>("DOA"),
                    DOB = r.Field<string>("DOB"),
                    ERP = r.Field<string>("ERP"),
                    FAther = r.Field<string>("FAther"),
                    FMobileNo = r.Field<string>("FMobileNo"),
                    Hgt = r.Field<string>("Hgt"),
                    MMobileNo = r.Field<string>("MMobileNo"),
                    Mother = r.Field<string>("Mother"),
                    PTMAttendance = r.Field<string>("PTMAttendance"),
                    Remark = r.Field<string>("Remark"),
                    SMSNo = r.Field<string>("SMSNo"),
                    StudentImage = r.Field<string>("StudentImage"),
                    Wgt = r.Field<string>("Wgt"),
                }).ToList();
                model.MarksOrGradeList = ds.Tables[2].AsEnumerable().Select(r => new Examination_ReportCard_MarksOrGradeDetailsModel
                {
                    ERP = r.Field<string>("ERP"),
                    Grade = r.Field<string>("Grade"),
                    HalfYearly = r.Field<decimal>("Half Yearly"),
                    NoteBook = r.Field<decimal>("Note Book"),
                    PT = r.Field<decimal>("PT"),
                    SEA = r.Field<decimal>("SEA"),
                    SubjectDisplayName = r.Field<string>("SubjectDisplayName"),
                    Total = r.Field<decimal>("Total"),
                }).ToList();
                model.CoScholasticAreasList = ds.Tables[3].AsEnumerable().Select(r => new Examination_ReportCard_CoScholasticAreasModel
                {
                    ERP = r.Field<string>("ERP"),
                    SubjectDisplayName = r.Field<string>("SubjectDisplayName"),
                    IsDiscipline = r.Field<bool>("IsDiscipline"),
                    Term1 = r.Field<string>("Term 1"),
                }).ToList();
                model.ResultDetailList = ds.Tables[4].AsEnumerable().Select(r => new Examination_ReportCard_ResultDetailsModel
                {
                    ERPNo = r.Field<string>("ERPNo"),
                    Result = r.Field<string>("Result"),
                }).ToList();
                model.DistinctERPNos = model.StudentDetailsList.Select(r => r.ERP).Distinct().ToList();
                return model;
            }
        }
    }
}
