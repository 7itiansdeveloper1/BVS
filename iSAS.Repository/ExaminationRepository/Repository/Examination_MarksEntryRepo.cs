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
    public class Examination_MarksEntryRepo : IExamination_MarksEntryRepo
    {
        public Examination_MarksEntryModels GetMarksEntryFormLoadDetails(string UserId)
        {
            Examination_MarksEntryModels model = new Examination_MarksEntryModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_MarksEntry_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.SessionList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SessionName"),
                    Value = r.Field<Int16>("SessionId").ToString(),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();

                model.ExamList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ExamTemplateName"),
                    Value = r.Field<string>("ExamTemplateId"),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();

                model.ClassList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassId"),
                }).ToList();
                return model;
            }
        }

        public List<SelectListItem> GetClassList(string SessionId, string UserId, string ExamId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_MarksEntry_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId ", SessionId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "GetClassList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassId"),
                }).ToList();
            }
        }
        public List<SelectListItem> GetSectionList(string SessionId, string UserId, string ExamId, string ClassId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_MarksEntry_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId ", SessionId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "GetSectionList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SectionName"),
                    Value = r.Field<string>("SectionId"),
                }).ToList();
            }
        }
        public List<SelectListItem> GetSubjectList(string SessionId, string UserId, string ExamId, string ClassId, string SectionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_MarksEntry_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId ", SessionId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "GetSubjectList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId"),
                }).ToList();
            }
        }

        public List<SelectListItem> GetAssismentList(string SessionId, string UserId, string ExamId, string ClassId, string SectionId, string SubjectId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_MarksEntry_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId ", SessionId);
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "GetAssessmentList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("AssessmentName"),
                    Value = r.Field<string>("AssessmentId"),
                }).ToList();
            }
        }
        public Examination_MarksEntryModels GetStudentDetails(Examination_MarksEntryModels param)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Examination_MarksEntryModels model = new Examination_MarksEntryModels();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_MarksEntry_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", param.SessionId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", param.ExamId);
                cmd.Parameters.AddWithValue("@ClassId", param.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", param.SectionId);
                cmd.Parameters.AddWithValue("@SubjectId", param.SubjectId);
                cmd.Parameters.AddWithValue("@AssessmentId", param.AssessmentId);
                cmd.Parameters.AddWithValue("@UserId", param.UserId);
                cmd.Parameters.AddWithValue("@Mode", "GetStudentList");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.MaxMark = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                model.PassingMark = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                model.MaxMarkWeightage = Convert.ToInt32(ds.Tables[0].Rows[0][2]);
                model.IsMarkBased = Convert.ToBoolean(ds.Tables[0].Rows[0][3]);

                model.StudentsMarkList = ds.Tables[1].AsEnumerable().Select(r => new Examination_StudendtMarkDetails
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ERPNo = r.Field<string>("ERPNo"),
                    Father = r.Field<string>("Father"),
                    Grade = r.Field<string>("Grade"),
                    IsAbsent = r.Field<bool>("IsAbsent"),
                    IsExempt = r.Field<bool>("IsExempt"),
                    IsML = r.Field<bool>("IsML"),
                    MarkObtained = r.Field<string>("MarkObtained"),
                    RollNo = r.Field<int>("RollNo"),
                    sno = r.Field<long>("sno"),
                    Student = r.Field<string>("Student"),
                }).ToList();


                if(model.IsMarkBased == false)
                {
                    model.GradeList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("Text"),
                        Value = r.Field<string>("Value"),
                    }).ToList();
                }
                return model;
            }
        }
        public Tuple<int, string> Examination_MarksEntry_CRUD(DataTable dt, Examination_MarksEntryModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_MarksEntry_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", model.SectionId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", model.ExamId);
                cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                cmd.Parameters.AddWithValue("@AssessmentId", model.AssessmentId);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@ClassMarkListDT", dt);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }

    }
}
