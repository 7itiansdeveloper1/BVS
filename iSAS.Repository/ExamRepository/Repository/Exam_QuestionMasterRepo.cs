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
    public class Exam_QuestionMasterRepo : IExam_QuestionMasterRepo
    {
        public List<Exam_QuestionMasterModels> QuestionMasterFormLoad()
        {
            List<Exam_QuestionMasterModels> questionMaster = new List<Exam_QuestionMasterModels>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_QuestionMaster_TRANSACTION", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    questionMaster = dt.AsEnumerable().Select(r => new Exam_QuestionMasterModels
                    {
                        qpId = r.Field<int>("qpId"),
                        qId = r.Field<int>("qId"),
                        qNo = r.Field<int>("qNo"),
                        qText = r.Field<string>("qText"),
                        qNature = r.Field<string>("qNature"),
                        Oa = r.Field<string>("a"),
                        Ob = r.Field<string>("b"),
                        Oc = r.Field<string>("c"),
                        Od = r.Field<string>("d"),
                        ans = r.Field<string>("ans"),
                        qMark = r.Field<decimal>("qMark"),
                        isActive = r.Field<bool>("isActive"),
                        className = r.Field<string>("className"),
                        subjectName = r.Field<string>("subjectName"),
                        assessmentName = r.Field<string>("assessmentName"),
                        maxMark = r.Field<int>("maxMark")
                    }).ToList();
                }

            }
            return questionMaster;

        }

        public Exam_QuestionMasterModels Get_QuestionDetails(string qpId, string className, string subjectName, string assessmentName, int maxMark, int qid)
        {
            
            Exam_QuestionMasterModels model = new Exam_QuestionMasterModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_QuestionMaster_TRANSACTION", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@qId", qid);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                model.className = className;
                model.subjectName = subjectName;
                model.assessmentName = assessmentName;
                model.maxMark = maxMark;
                if (dt.Rows.Count > 0)
                {
                    model.qpId = Convert.ToInt32(dt.Rows[0]["qpId"]);
                    model.qId = Convert.ToInt32(dt.Rows[0]["qId"]);
                    model.qNo = Convert.ToInt32(dt.Rows[0]["qNo"]);
                    model.qText = dt.Rows[0]["qText"].ToString();
                    model.qNature = dt.Rows[0]["qNature"].ToString();
                    model.Oa = dt.Rows[0]["a"].ToString();
                    model.Ob = dt.Rows[0]["b"].ToString();
                    model.Oc = dt.Rows[0]["c"].ToString();
                    model.Od = dt.Rows[0]["d"].ToString();
                    model.ans = dt.Rows[0]["ans"].ToString();
                    model.qMark = Convert.ToDecimal(dt.Rows[0]["qMark"]);
                    model.parValue = dt.Rows[0]["parValue"].ToString();
                }
            }
            return model;
        }
        public Tuple<int, string> QuestionMaster_CRUD( Exam_QuestionMasterModels model,string userId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_QuestionMaster_CRUD",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qpId",model.qpId);
                cmd.Parameters.AddWithValue("@qId",model.qId);
                cmd.Parameters.AddWithValue("@qNo", model.qNo);
                cmd.Parameters.AddWithValue("@qNature", model.qNature);
                cmd.Parameters.AddWithValue("@qText", model.qText);
                cmd.Parameters.AddWithValue("@a", model.Oa);
                cmd.Parameters.AddWithValue("@b", model.Ob);
                cmd.Parameters.AddWithValue("@c", model.Oc);
                cmd.Parameters.AddWithValue("@d", model.Od);
                cmd.Parameters.AddWithValue("@ans", model.ans);
                cmd.Parameters.AddWithValue("@qMark", model.qMark);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@parValue", model.parValue);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0]), dt.Rows[0][1].ToString());
        }
    }
}
