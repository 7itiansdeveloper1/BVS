using ISas.Entities.Exam_Entities;
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
    public class Exam_TemplateSetupRepo : IExam_TemplateSetupRepo
    {
        public List<Exam_TemplateSetupModels> GetExamTempleteList(string Exam_TemplateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ExamTemplateSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Exam_TemplateId", Exam_TemplateId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Exam_TemplateSetupModels
                {
                    Classes = r.Field<string>("Classes"),
                    EndDate = r.Field<string>("EndDate"),
                    Exam_TemplateDisplayName = r.Field<string>("Exam_TemplateDisplayName"),
                    FeedingLastDate = r.Field<string>("FeedingLastDate"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    PrintOrder = r.Field<Int16>("PrintOrder"),
                    ResultDate = r.Field<string>("ResultDate"),
                    StartDate = r.Field<string>("StartDate"),
                    TemplateId = r.Field<string>("TemplateId"),
                    TemplateName = r.Field<string>("TemplateName"),
                }).ToList();
            }
        }

        public Exam_TemplateSetupModels GetExamTempleteById(string TempleteId)
        {
            return GetExamTempleteList(TempleteId).FirstOrDefault();
        }

        public Tuple<int, string> Exam_TemplateSetup_CRUD(Exam_TemplateSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ExamTemplateSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Exam_TemplateId", model.TemplateId);
                cmd.Parameters.AddWithValue("@Exam_TemplateName ", model.TemplateName);
                cmd.Parameters.AddWithValue("@Exam_TemplateDisplayName", model.Exam_TemplateDisplayName);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);

                if (!string.IsNullOrEmpty(model.StartDate))
                    cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(model.StartDate).Date);

                if (!string.IsNullOrEmpty(model.EndDate))
                    cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(model.EndDate).Date);

                if (!string.IsNullOrEmpty(model.ResultDate))
                    cmd.Parameters.AddWithValue("@ResultDate", Convert.ToDateTime(model.ResultDate).Date);

                if (!string.IsNullOrEmpty(model.FeedingLastDate))
                    cmd.Parameters.AddWithValue("@FeedingLastDate", Convert.ToDateTime(model.FeedingLastDate).Date);


                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public Tuple<int, string> Exam_TemplateSetup_CRUD(string TemplateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ExamTemplateSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Exam_TemplateId", TemplateId);
                cmd.Parameters.AddWithValue("@TransactionMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #region Templete Setup
        public List<SelectListItem> TempleteClassSetupDetails(string Exam_TemplateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ExamTemplateClassSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamTemplateId", Exam_TemplateId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassSectionId"),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();
            }
        }


        public Tuple<int, string> TempleteClassSetup_CRUD(Exam_Template_ClassSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_ExamTemplateClassSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamTemplateId", model.ExamTemplateId);
                cmd.Parameters.AddWithValue("@ClassSectionId ",string.Join(",", model.ClassList.Where(r=> r.Selected).Select(r=> r.Value).ToList()));
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@TransactionMode", "SAVE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #endregion

        #region Grading Setup
        public Exam_Template_GradingSetupModels TempleteGradingSetup_FormLoad(string Exam_TemplateId)
        {
            Exam_Template_GradingSetupModels model = new Exam_Template_GradingSetupModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_GradingSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamTemplateId", Exam_TemplateId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                model.AcademicList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("GradingName"),
                    Value = r.Field<string>("GradingId"),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();

                model.NonAcademicList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("GradingName"),
                    Value = r.Field<string>("GradingId"),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();

                model.ReportCardList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ReportCardName"),
                    Value = r.Field<string>("ReportCardId"),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();

                model.ExamTemplateId = Exam_TemplateId;
                return model;
            }
        }


        public Tuple<int, string> TempleteGradingSetup_CRUD(Exam_Template_GradingSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_GradingSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamTemplateId", model.ExamTemplateId);
                cmd.Parameters.AddWithValue("@AcademicGradingId", model.AcademicId);
                cmd.Parameters.AddWithValue("@NonAcademicGradingId", model.NonAcademicId);
                cmd.Parameters.AddWithValue("@ReportCardId", model.ReportCardId);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #endregion
    }
}
