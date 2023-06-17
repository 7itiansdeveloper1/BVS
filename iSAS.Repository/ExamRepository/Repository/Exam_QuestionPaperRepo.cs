using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ISas.Repository.ExamRepository.IRepository;

namespace ISas.Repository.ExamRepository.Repository
{
    public class Exam_QuestionPaperRepo: IExam_QuestionPaperRepo
    {
        public Exam_QuestionPaperModels QuestionPaper_FormLoad(string sessionId,string userId)
        {
            Exam_QuestionPaperModels model = new Exam_QuestionPaperModels();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_QuestionPaperMaster_TRANSACTION", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if ( ds.Tables[0].Rows.Count > 0)
                {
                    model.availablePaperList = ds.Tables[0].AsEnumerable().Select(r => new Exam_AvailablePaperList
                    {
                        //Sno = r.Field<int>("Sno"),
                        ClassId = r.Field<string>("ClassId"),
                        ClassName= r.Field<string>("ClassName"),
                        AssessmentId= r.Field<string>("AssessmentId"),
                        AssessmentName= r.Field<string>("AssessmentName"),
                        subjectId= r.Field<string>("subjectId"),
                        SubjectName= r.Field<string>("SubjectName"),
                        MM= r.Field<int>("MM"),
                        ClassPrintOrder= r.Field<int>("ClassPrintOrder"),
                        ExamPrintOrder= r.Field<int>("ExamPrintOrder"),
                        isQPCreated = r.Field<bool>("isQPCreated"),
                    }).ToList();

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    model.qPaperList = ds.Tables[1].AsEnumerable().Select(r => new Exam_QuestionPaperList
                    {
                        qpId = r.Field<int>("qpId"),
                        classId = r.Field<string>("classId"),
                        ClassName = r.Field<string>("ClassName"),
                        SubjectId = r.Field<string>("SubjectId"),
                        SubjectName = r.Field<string>("SubjectName"),
                        assessmentId = r.Field<string>("assessmentId"),
                        AssessmentName = r.Field<string>("AssessmentName"),
                        docPath= r.Field<string>("docPath"),
                        createdBy = r.Field<string>("createdBy"),
                        MaxMark = r.Field<int>("MaxMark"),
                        isActive= r.Field<bool>("isActive"),
                    }).ToList();
                }
            }
            return model;

        }
        public Tuple<int, string> QuestionPaper_CRUD(int qpId, string classId,string subjectId,string assessmentId,int maxmark,string userId,bool isActive)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_QuestionPaper_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qpId", qpId);
                cmd.Parameters.AddWithValue("@classId", classId);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                cmd.Parameters.AddWithValue("@assessmentId", assessmentId);
                cmd.Parameters.AddWithValue("@maxMark", maxmark);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@isActive", isActive);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> QuestionPaperUpload_CRUD(int qpId, string docPath, string userId,string mode)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_UploadQuestionPaper_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qpId", qpId);
                cmd.Parameters.AddWithValue("@docPath", docPath);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@Mode", mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }
        public Exam_AnswersheetModels Exam_Answersheet_TRANSACTION(string sessionId, string userId,int qpId,string className,string assessmentName,string subjectName)
        {
            Exam_AnswersheetModels model = new Exam_AnswersheetModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_Answersheet_TRANSACTION", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@qpId", qpId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                model.ClassName = className;
                model.AssessmentName = assessmentName;
                model.SubjectName = subjectName;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.answersheetLists = ds.Tables[0].AsEnumerable().Select(r => new AnswersheetList
                    {
                        //Sno = r.Field<int>("Sno"),
                        ERPNO = r.Field<string>("ERPNO"),
                        Student = r.Field<string>("Student"),
                        Class = r.Field<string>("Class"),
                        ansDocPath = r.Field<string>("ansDocPath"),
                        
                    }).ToList();

                }
            }
            return model;

        }
    }
}
