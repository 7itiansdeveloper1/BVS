using ISas.Entities.Examination_Entities;
using ISas.Repository.ExaminationRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.Repository
{
    public class Examination_ChildSubjectSetupRepo : IExamination_ChildSubjectSetupRepo
    {
        public List<Examination_ChildSubjectSetupModels> GetChildSubjectList(string ExamTempleId, string SubjectId, int SubjectPropertyId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ChildSubjectMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamTempleId);
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                cmd.Parameters.AddWithValue("@SubjectPropertyId", SubjectPropertyId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Examination_ChildSubjectSetupModels
                {
                    SubjectPropertyId = r.Field<int>("SubjectPropertyId"),
                    ExamTemplateId = r.Field<string>("ExamTemplateId"),
                    SubjectId = r.Field<string>("ParentSubjectId"),
                    SubjectName = r.Field<string>("ParentSubjectName"),
                    SubjectDisplayName = r.Field<string>("SubjectDisplayName"),
                    Assessment = r.Field<string>("Assessment"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    HavingChildSubject = r.Field<bool>("HavingChildSubject"),
                    Exam_TemplateName = r.Field<string>("Exam_TemplateName"),
                    // SubjectPrintOrderId = r.Field<int>("PrintOrder").ToString(),
                    ChildSubjectId = r.Field<string>("ChildSubjectId"),
                    ChildSubjectName = r.Field<string>("ChildSubjectName"),
                    PrintOrder = r.Field<int>("PrintOrder"),

                }).ToList();
            }
        }
        public Examination_ChildSubjectSetupModels GetChildSubjectById(string ExamTempleId, string SubjectId, int SubjectPropertyId)
        {
            return GetChildSubjectList(ExamTempleId, SubjectId, SubjectPropertyId).FirstOrDefault();
        }
        public Tuple<int, string> Examination_ChildSubjectSetup_CRUD(Examination_ChildSubjectSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ChildSubjectMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamTemplateId", model.ExamTemplateId);
                cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                cmd.Parameters.AddWithValue("@ChildSubjectId", model.ChildSubjectId);
                cmd.Parameters.AddWithValue("@ChildSubjectName", model.ChildSubjectName);
                cmd.Parameters.AddWithValue("@ChildSubjectDisplayName", model.SubjectDisplayName);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@IsActive", true);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@TransactionMode", model.TransactionMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Examination_ChildSubjectSetup_CRUD(int PropertyId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ChildSubjectMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ChildSubjectId", PropertyId);
                cmd.Parameters.AddWithValue("@TransactionMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
