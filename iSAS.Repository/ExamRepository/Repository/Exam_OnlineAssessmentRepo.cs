using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ISas.Entities.Exam_Entities;
using ISas.Repository.ExamRepository.IRepository;
namespace ISas.Repository.ExamRepository.Repository
{
    public class Exam_OnlineAssessmentRepo : IExam_OnlineAssessmentRepo
    {
        public List<Exam_OnlineAssessmentModels> Exam_OnlineAssessment_FormLoad(string erpNo)
        {
            List<Exam_OnlineAssessmentModels> onlineAssessmentList = new List<Exam_OnlineAssessmentModels>();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_OnlineAssessment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Exam_OnlineAssessmentModels onlineAssessment = new Exam_OnlineAssessmentModels();
                    onlineAssessment.qpId = Convert.ToInt32(dt.Rows[i]["qpId"].ToString());
                    onlineAssessment.subjectName = dt.Rows[i]["subjectName"].ToString();
                    onlineAssessment.maxMark = Convert.ToInt32(dt.Rows[i]["maxMark"].ToString());
                    onlineAssessment.setBy = dt.Rows[0]["setBy"].ToString();
                    onlineAssessment.Attend = Convert.ToBoolean(dt.Rows[i]["Attend"].ToString()) ;
                    onlineAssessment.Result = Convert.ToBoolean(dt.Rows[i]["Result"].ToString()) ;
                    onlineAssessment.docPath = dt.Rows[i]["docPath"].ToString();
                    onlineAssessment.ansDocPath = dt.Rows[i]["ansDocPath"].ToString();
                    onlineAssessment.TAT = dt.Rows[i]["TAT"].ToString();
                    onlineAssessmentList.Add(onlineAssessment);
                }
            }
            return onlineAssessmentList;

        }
        public Exam_QuestionMasterModels Exam_BrowseAssessment(int qpId, int qNo,string subjectName,int maxMark,string userId)
        {

            Exam_QuestionMasterModels model = new Exam_QuestionMasterModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_BrowseAssessment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@qpId", qpId);
                cmd.Parameters.AddWithValue("@qNo", qNo);
                cmd.Parameters.AddWithValue("@ERPNo", userId);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                model.subjectName = subjectName;
                model.maxMark = maxMark;
                model.qpId = qpId;
                if (dt.Rows.Count > 0)
                {
                    //model.qpId = Convert.ToInt32(dt.Rows[0]["qpId"]);
                    model.qId = Convert.ToInt32(dt.Rows[0]["qId"]);
                    model.qNo = Convert.ToInt32(dt.Rows[0]["qNo"]);
                    model.qText = dt.Rows[0]["qText"].ToString();
                    //model.qNature = dt.Rows[0]["qText"].ToString();
                    model.Oa = dt.Rows[0]["a"].ToString();
                    model.Ob = dt.Rows[0]["b"].ToString();
                    model.Oc = dt.Rows[0]["c"].ToString();
                    model.Od = dt.Rows[0]["d"].ToString();
                    model.ans = dt.Rows[0]["ans"].ToString();
                    model.ansText = dt.Rows[0]["ansText"].ToString();
                    model.QS = dt.Rows[0]["QS"].ToString();
                    model.qNature = dt.Rows[0]["qNature"].ToString();
                    model.parValue = dt.Rows[0]["parValue"].ToString();
                    model.isHavingParagraph = Convert.ToBoolean(dt.Rows[0]["isHavingParagraph"]);
                    model.qMark = Convert.ToDecimal(dt.Rows[0]["qMark"]);
                }
            }
            return model;
        }
        public void Exam_StudentResult_CRUD(string erpNo, int qId, string ans)
        {
            Exam_QuestionMasterModels model = new Exam_QuestionMasterModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_StudentResult_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@erpNo", erpNo);
                cmd.Parameters.AddWithValue("@qid", qId);
                cmd.Parameters.AddWithValue("@ans", ans);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Exam_StudentDisriptiveResult_CRUD(string erpNo, int qId, string ans)
        {
            Exam_QuestionMasterModels model = new Exam_QuestionMasterModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_StudentDisriptiveResult_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@erpNo", erpNo);
                cmd.Parameters.AddWithValue("@qid", qId);
                cmd.Parameters.AddWithValue("@ans", ans);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Tuple<int, string> AnswerSheetUpload_CRUD(int qpId, string ansDocPath, string erpNo,string mode)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_AnswersheetUpload_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qpId", qpId);
                cmd.Parameters.AddWithValue("@ansDocPath", ansDocPath);
                cmd.Parameters.AddWithValue("@erpNo", erpNo);
                cmd.Parameters.AddWithValue("@Mode", mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }
    }
}
