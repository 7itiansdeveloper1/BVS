using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;
using System.Data;
using System.Configuration;
using ISas.Repository.ExamRepository.IRepository;

namespace ISas.Repository.ExamRepository.Repository
{
    public class Exam_AnswersheetRepo: IExam_AnswersheetRepo
    {
        public StudentAssessmentResult GetAnswersheet(string erpNo, int qpId)
        {
            StudentAssessmentResult studentAssessmentResult = new StudentAssessmentResult();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_Paperchecking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@qpId", qpId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    studentAssessmentResult.answersheetList = ds.Tables[0].AsEnumerable().Select(r => new Exam_AnswersheetModel
                    {
                        qNo = r.Field<int>("qNo"),
                        qText = r.Field<string>("qText"),
                        Oa = r.Field<string>("a"),
                        Ob = r.Field<string>("b"),
                        Oc = r.Field<string>("c"),
                        Od = r.Field<string>("d"),
                        qMark = r.Field<decimal>("qMark"),
                        correctAnswer = r.Field<string>("correctAnswer"),
                        ans = r.Field<string>("ans"),
                        ansResult = r.Field<string>("ansResult"),
                        markScored = r.Field<decimal>("markScored"),
                        markScored1 = r.Field<string>("markScored1"),
                        parValue = r.Field<string>("parValue"),
                        isHavingParagraph = r.Field<Boolean>("isHavingParagraph"),
                        qNature = r.Field<string>("qNature"),
                        ansText = r.Field<string>("ansText"),

                    }).ToList();

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    studentAssessmentResult.SubjectDisplayName = ds.Tables[1].Rows[0]["SubjectDisplayName"].ToString();
                    studentAssessmentResult.AssessmentName = ds.Tables[1].Rows[0]["AssessmentName"].ToString();
                    studentAssessmentResult.maxMark = Convert.ToInt32(ds.Tables[1].Rows[0]["maxMark"]);
                    studentAssessmentResult.result = ds.Tables[1].Rows[0]["result"].ToString();
                    studentAssessmentResult.markObtained = Convert.ToDecimal(ds.Tables[1].Rows[0]["markObtained"]);
                }
                
            }
            return studentAssessmentResult;

        }

    }
}
