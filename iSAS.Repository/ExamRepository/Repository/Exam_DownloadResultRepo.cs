using System.Collections.Generic;
using System.Linq;
using ISas.Entities.Exam_Entities;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ISas.Repository.ExamRepository.IRepository;
namespace ISas.Repository.ExamRepository.Repository
{
    public class Exam_DownloadResultRepo: IExam_DownloadResultRepo
    {
        public List<Exam_DownloadResultModels> DownloadResult(string erpNo, string sessionId)
        {
            List<Exam_DownloadResultModels> downloadResultList = new List<Exam_DownloadResultModels>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_DownloadResult_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    downloadResultList = dt.AsEnumerable().Select(r => new Exam_DownloadResultModels
                    {
                        //Sno = r.Field<int>("Sno"),
                        SessionId = r.Field<string>("SessionId"),
                        ClassId = r.Field<string>("ClassId"),
                        SectionId = r.Field<string>("SectionId"),
                        ExamId = r.Field<string>("ExamId"),
                        ERPNo = r.Field<string>("ERPNo"),
                        Student = r.Field<string>("Student"),
                        ExamName = r.Field<string>("ExamName")
                    }).ToList();

                }
            }
            return downloadResultList;
        }

    }
}
