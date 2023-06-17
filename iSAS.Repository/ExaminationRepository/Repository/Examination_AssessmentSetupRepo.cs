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
    public class Examination_AssessmentSetupRepo : IExamination_AssessmentSetupRepo
    {
        public List<SelectListItem> AssessmentNameList()
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_AssessmentMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamTemplateId", "");
                cmd.Parameters.AddWithValue("@AssessmentPropertyId", "");
                cmd.Parameters.AddWithValue("@SubjectId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("AssessmentName"),
                    Value = r.Field<string>("AssessmentId")
                }).ToList();
            }
        }
        public List<Examination_AssessmentSetupModels> AssessmentList(string ExamTemplateId, int AssessmentPropertyId, string SubjectId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_AssessmentMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamTemplateId);
                cmd.Parameters.AddWithValue("@AssessmentPropertyId", AssessmentPropertyId);
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[1].AsEnumerable().Select(r => new Examination_AssessmentSetupModels
                {
                    AssessmentPropertyId = r.Field<int>("AssessmentPropertyId"),
                    ExamTemplateId = r.Field<string>("ExamTemplateId"),
                    Exam_TemplateName = r.Field<string>("Exam_TemplateName"),
                    SubjectId = r.Field<string>("SubjectId"),
                    SubjectName = r.Field<string>("SubjectName"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                    AssessmentId = r.Field<string>("AssessmentId"),
                    AssessmentName = r.Field<string>("AssessmentName"),
                    AssessmentDisplayName = r.Field<string>("AssessmentDisplayName"),
                    //   Assessment = r.Field<string>("Assessment"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                }).ToList();
            }
        }

        public Examination_AssessmentSetupModels GetAssessmentById(string ExamTempleteId, int AssessmentPropertyId, string SubjectId)
        {
            return AssessmentList(ExamTempleteId, AssessmentPropertyId, SubjectId).FirstOrDefault();
        }

        public Tuple<int, string> Examination_AssessmentSetup_CRUD(Examination_AssessmentSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_AssessmentMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                string assessmentIds = string.Join(",", model.AssessmentNameList.Where(r => r.Selected).Select(r => r.Value).ToList());

                if (model.TransactionMode == "UPDATE")
                    assessmentIds = model.AssessmentId;

                cmd.Parameters.AddWithValue("@ExamTemplateId", model.ExamTemplateId);
                cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                cmd.Parameters.AddWithValue("@AssessmentId", assessmentIds);
                cmd.Parameters.AddWithValue("@AssessmentName", model.AssessmentName);
                cmd.Parameters.AddWithValue("@AssessmentDisplayName", model.AssessmentDisplayName);
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

        public Tuple<int, string> Examination_AssessmentSetup_CRUD(string AssessmentId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_AssessmentMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AssessmentId", AssessmentId);
                cmd.Parameters.AddWithValue("@TransactionMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public AssessmentPropertySetupModel GetAssessmentPropertyDetails(int AssessmentPropertyId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_AssessmentMaster_PROPERTY_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AssessmentPropertyId", AssessmentPropertyId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new AssessmentPropertySetupModel
                {
                    ExamTemplateId = r.Field<string>("ExamTemplateId"),
                    Exam_TemplateName = r.Field<string>("Exam_TemplateName"),
                    SubjectId = r.Field<string>("SubjectId"),
                    SubjectName = r.Field<string>("SubjectName"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                    AssessmentId = r.Field<string>("AssessmentId"),
                    AssessmentName = r.Field<string>("AssessmentName"),
                    AssessmentDisplayName = r.Field<string>("AssessmentDisplayName"),
                    Assessment = r.Field<string>("Assessment"),
                    AssessmentDate = r.Field<string>("AssessmentDate"),
                    AssessmentNature = r.Field<string>("AssessmentNature"),
                    AssessmentPropertyId = r.Field<int>("AssessmentPropertyId"),
                    IsMarksFeedingon = r.Field<bool>("IsMarksFeedingon"),
                    MarkFeedingLastDate = r.Field<string>("MarkFeedingLastDate"),
                    MaxMark = r.Field<int>("MaxMark"),
                    MaxMarkWeightage = r.Field<int>("MaxMarkWeightage"),
                    PassingMark = r.Field<int>("PassingMark"),
                }).FirstOrDefault();
            }
        }


        public Tuple<int, string> Examination_AssessmentPropertySetup_CRUD(AssessmentPropertySetupModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_AssessmentMaster_PROPERTY_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AssessmentPropertyId", model.AssessmentPropertyId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", model.ExamTemplateId);
                cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                cmd.Parameters.AddWithValue("@AssessmentId", model.AssessmentId);
                cmd.Parameters.AddWithValue("@AssessmentNature", model.AssessmentNature);

                if (!string.IsNullOrEmpty(model.AssessmentDate))
                    cmd.Parameters.AddWithValue("@AssessmentDate", Convert.ToDateTime(model.AssessmentDate).Date);

                if (!string.IsNullOrEmpty(model.MarkFeedingLastDate))
                    cmd.Parameters.AddWithValue("@MarkFeedingLastDate", Convert.ToDateTime(model.MarkFeedingLastDate).Date);

                cmd.Parameters.AddWithValue("@IsMarksFeedingon", model.IsMarksFeedingon);
                cmd.Parameters.AddWithValue("@MaxMark", model.MaxMark);
                cmd.Parameters.AddWithValue("@PassingMark", model.PassingMark);
                cmd.Parameters.AddWithValue("@MaxMarkWeightage", model.MaxMarkWeightage);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@TransactionMode", model.TransactionMode);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
