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
    public class Examination_SubjectMasterRepo : IExamination_SubjectMasterRepo
    {
        public Examination_SubjectMasterModels SubjectMaster_FormLoad()
        {
            Examination_SubjectMasterModels model = new Examination_SubjectMasterModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_SubjectMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamTemplateId", "");
                cmd.Parameters.AddWithValue("@SubjectPropertyId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.SubjectPrintOrderList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<int>("PrintOrder").ToString()
                }).ToList();

                model.SubjectList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId")
                }).ToList();
                return model;
            }
        }
        public List<Examination_SubjectMasterModels> GetSubjectMstList(string ExamTempleId, string SubjectId)
        {
            //List<Examination_SubjectMasterModels> subjectList = new List<Examination_SubjectMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_SubjectMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamTempleId);
                cmd.Parameters.AddWithValue("@SubjectPropertyId", SubjectId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[2].AsEnumerable().Select(r => new Examination_SubjectMasterModels
                {
                    SubjectPropertyId = r.Field<int>("SubjectPropertyId"),
                    TemplateId = r.Field<string>("ExamTemplateId"),
                    SubjectId = r.Field<string>("SubjectId"),
                    SubjectName = r.Field<string>("SubjectName"),
                    SubjectDisplayName = r.Field<string>("SubjectDisplayName"),
                    Assessment = r.Field<string>("Assessment"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    HavingChildSubject = r.Field<bool>("HavingChildSubject"),
                    TempleteName = r.Field<string>("Exam_TemplateName"),
                    SubjectPrintOrderId = r.Field<int>("PrintOrder").ToString(),
                }).ToList();
            }
        }
        public Examination_SubjectMasterModels GetSubjectMstById(string ExamTempleId, string SubjectId)
        {
            return GetSubjectMstList(ExamTempleId, SubjectId).FirstOrDefault();
        }
        public Tuple<int, string> Examination_SubjectMaster_CRUD(Examination_SubjectMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_SubjectMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamTemplateId", model.TemplateId);
                cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                cmd.Parameters.AddWithValue("@SubjectName", model.SubjectName);
                cmd.Parameters.AddWithValue("@SubjectDisplayName", model.SubjectDisplayName);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@PrintOrder", model.SubjectPrintOrderId);
                cmd.Parameters.AddWithValue("@TransactionMode", model.TransactionMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Examination_SubjectMaster_CRUD(int PropertyId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_SubjectMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@ExamTemplateId", model.TemplateId);
                cmd.Parameters.AddWithValue("@SubjectId", PropertyId);
                //cmd.Parameters.AddWithValue("@SubjectName", model.SubjectName);
                //cmd.Parameters.AddWithValue("@SubjectDisplayName", model.SubjectDisplayName);
                //cmd.Parameters.AddWithValue("@UserId", model.UserId);
                //cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                //cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@TransactionMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }



        public Examination_SubjectPropertyModel GetSubjectSetupPropertyDetails(string PropertyId)
        {
            Examination_SubjectPropertyModel model = new Examination_SubjectPropertyModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_SubjectMaster_PROPERTY_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectPropertyId", PropertyId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model = ds.Tables[0].AsEnumerable().Select(r => new Examination_SubjectPropertyModel
                {
                    Assessment = r.Field<string>("Assessment"),
                    DispalyGrade = r.Field<bool>("DispalyGrade"),
                    DispalyMark = r.Field<bool>("DispalyMark"),
                    DisplayOnReportCard = r.Field<bool>("DisplayOnReportCard"),
                    ExamTemplateId = r.Field<string>("ExamTemplateId"),
                    Exam_TemplateName = r.Field<string>("Exam_TemplateName"),
                    HavingChildSubject = r.Field<bool>("HavingChildSubject"),
                    IncludePracticalSubject = r.Field<bool>("IncludePracticalSubject"),
                    Is1stLanguage = r.Field<bool>("Is1stLanguage"),
                    Is2ndLanguage = r.Field<bool>("Is2ndLanguage"),
                    Is3rdLanguage = r.Field<bool>("Is3rdLanguage"),
                    IsAcademicSubject = r.Field<bool>("IsAcademicSubject"),
                    IsCompulsorySubject = r.Field<bool>("IsCompulsorySubject"),
                    IsGradeBased = r.Field<bool>("IsGradeBased"),
                    IsMarkBased = r.Field<bool>("IsMarkBased"),
                    IsNonAcademicSubject = r.Field<bool>("IsNonAcademicSubject"),
                    IsOptionalSubject = r.Field<bool>("IsOptionalSubject"),
                    ParentSubjectId = r.Field<string>("ParentSubjectId"),
                    // PrintOrder = r.Field<int>("PrintOrder"),
                    SubjectDisplayName = r.Field<string>("SubjectDisplayName"),
                    SubjectId = r.Field<string>("SubjectId"),
                    SubjectName = r.Field<string>("SubjectName"),
                    //SubjectPropertyId = r.Field<int>("SubjectPropertyId"),
                    //  SubjectType = r.Field<char>("SubjectType"),

                }).FirstOrDefault();

                if (model != null)
                    model.SubjectPropertyId = Convert.ToInt32(PropertyId);

                return model;
            }
        }
        public Tuple<int, string> Examination_SubjectPropertySetup_CRUD(Examination_SubjectPropertyModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_SubjectMaster_PROPERTY_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubjectPropertyId", model.SubjectPropertyId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", model.ExamTemplateId);
                cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                cmd.Parameters.AddWithValue("@HavingChildSubject", model.HavingChildSubject);


                //if (model.HavingChildSubject)
                //    cmd.Parameters.AddWithValue("@SubjectType", 'C');
                //else
                cmd.Parameters.AddWithValue("@SubjectType", model.SubjectType);

                cmd.Parameters.AddWithValue("@IsMarkBased", model.IsMarkBased);
                //cmd.Parameters.AddWithValue("@IsGradeBased", model.IsGradeBased);
                cmd.Parameters.AddWithValue("@IncludePracticalSubject", model.IncludePracticalSubject);
                cmd.Parameters.AddWithValue("@IsOptionalSubject", model.IsOptionalSubject);
                cmd.Parameters.AddWithValue("@IsCompulsorySubject", model.IsCompulsorySubject);

                cmd.Parameters.AddWithValue("@IsAcademicSubject", model.IsAcademicSubject);
                //cmd.Parameters.AddWithValue("@IsNonAcademicSubject", model.IsNonAcademicSubject);


                cmd.Parameters.AddWithValue("@ParentSubjectId", model.ParentSubjectId);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@Is1stLanguage", model.Is1stLanguage);
                cmd.Parameters.AddWithValue("@Is2ndLanguage", model.Is2ndLanguage);
                cmd.Parameters.AddWithValue("@Is3rdLanguage", model.Is3rdLanguage);
                cmd.Parameters.AddWithValue("@DisplayOnReportCard", model.DisplayOnReportCard);
                cmd.Parameters.AddWithValue("@DispalyMark", model.DispalyMark);
                cmd.Parameters.AddWithValue("@DispalyGrade", model.DispalyGrade);
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
