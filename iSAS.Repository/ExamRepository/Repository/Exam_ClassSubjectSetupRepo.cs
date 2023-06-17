using ISas.Entities.Exam_Entities;
using ISas.Repository.ExamRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.ExamRepository.Repository
{
    public class Exam_ClassSubjectSetupRepo: IExam_ClassSubjectSetupRepo
    {
        public Exam_ClassSubjectSetup ClassSubjectSetup(string function)
        {
            Exam_ClassSubjectSetup model = new Exam_ClassSubjectSetup();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_ClassSubjectSetup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@function", function);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.classList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("className"),
                        Value = r.Field<string>("classSectionId"),
                    }).ToList();

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    model.subjectList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("SubjectName"),
                        Value = r.Field<string>("SubjectId"),
                    }).ToList();
                }

            }
            return model;

        }

        public List<SelectListItem> ClassSubjectSetup_assessment(string function,string classsectionid,string subjectid)
        {
            List<SelectListItem> assesmentList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_ClassSubjectSetup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@classSectionId", classsectionid);
                cmd.Parameters.AddWithValue("@subjectId", subjectid);
                cmd.Parameters.AddWithValue("@function", function);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    assesmentList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("AssessmentName"),
                        Value = r.Field<string>("AssessmentId"),
                        Selected=r.Field<bool>("IsSelected")
                    }).ToList();
                }
            }
            return assesmentList;

        }
        public List<ClassSubject> ClassSubjectSetup_classSubject(string function, string classsectionid)
        {
            List<ClassSubject> classSubjetList = new List<ClassSubject>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_ClassSubjectSetup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@classSectionId", classsectionid);
                cmd.Parameters.AddWithValue("@function", function);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    classSubjetList = ds.Tables[0].AsEnumerable().Select(r => new ClassSubject
                    {
                        subjectId = r.Field<string>("subjectId"),
                        SubjectName = r.Field<string>("SubjectName"),
                        AssessmentName = r.Field<string>("AssessmentName"),
                    }).ToList();
                }
            }
            return classSubjetList;

        }
        public Tuple<int, string> ClassSubjectSetup_CRUD(Exam_ClassSubjectSetup model)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_ClassSubjectSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@classSectionId", model.classSectionId);
                cmd.Parameters.AddWithValue("@subjectId", model.subjectId);
                cmd.Parameters.AddWithValue("@assessmentId", string.Join(",", model.assessmentsIds));
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0]), dt.Rows[0][1].ToString());
        }
    }
}
