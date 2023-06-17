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
    public class Examination_ProfileEntryRepo : IExamination_ProfileEntryRepo
    {
        public List<SelectListItem> Get_ProfileEntryDropDowns(string SessionId, string UserId, string ClassId, string Mode, string ExamId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ProfileEntry_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId ", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassID", ClassId);
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
        public Examination_ProfileEntryModels Get_ProfileEntryStudentDetails(string SessionId, string UserId, string ClassId, string SectionId, string ExamTempleteId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Examination_ProfileEntryModels model = new Examination_ProfileEntryModels();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ProfileEntry_Transaction", con);
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

                model.StudentDetails = ds.Tables[0].AsEnumerable().Select(r => new ProfileEntry_StudentDetailsModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ERPNo = r.Field<string>("ERPNo"),
                    Attendance = r.Field<string>("Attendance"),
                    RollNo = r.Field<int>("RollNo"),
                    Student = r.Field<string>("Student"),
                    Hgt = r.Field<string>("Hgt"),
                    PTMAttendance = r.Field<string>("PTMAttendance"),
                    Wgt = r.Field<string>("Wgt"),
                }).ToList();

                if (model != null && model.StudentDetails != null && model.StudentDetails.Count > 0)
                {
                    for (int i = 0; i < model.StudentDetails.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(model.StudentDetails[i].Attendance))
                        {
                            List<string> strList = model.StudentDetails[i].Attendance.Split('/').ToList();
                            if(strList.Count == 2)
                            {
                                model.StudentDetails[i].Attendance = strList[0];
                                model.StudentDetails[i].Attendance1 = strList[1];
                            }
                        }
                        if (!string.IsNullOrEmpty(model.StudentDetails[i].PTMAttendance))
                        {
                            List<string> strList = model.StudentDetails[i].PTMAttendance.Split('/').ToList();
                            if (strList.Count == 2)
                            {
                                model.StudentDetails[i].PTMAttendance = strList[0];
                                model.StudentDetails[i].PTMAttendance1 = strList[1];
                            }
                        }
                    }
                }
                return model;
            }
        }
        public Tuple<int, string> Examination_ProfileEntry_CRUD(DataTable dt, Examination_ProfileEntryModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ProfileEntry_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", model.ExamId);
                cmd.Parameters.AddWithValue("@DataTable", dt);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
    }
}
