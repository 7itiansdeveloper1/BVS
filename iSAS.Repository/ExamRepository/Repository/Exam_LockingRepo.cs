using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ISas.Entities.Exam_Entities;
using System.Web.Mvc;
using ISas.Repository.ExaminationRepository.IRepository;
using ISas.Repository.ExamRepository.IRepository;

namespace ISas.Repository.ExamRepository.Repository
{
    public class Exam_LockingRepo: IExam_LockingRepo
    {
        public Exam_LockingModels Exam_LockingModels_Cascading()
        {
            Exam_LockingModels model = new Exam_LockingModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_AssessmentLock_Cascading",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode",null);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt= new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count>0)
                {
                    model.classList = dt.AsEnumerable().Select(r => new SelectListItem
                    {
                        Value = r.Field<string>("ClassId"),
                        Text = r.Field<string>("ClassName")
                    }
                    ).ToList();
                }
            }
            return model;
        }

        public List<classAssessmentList> Exam_LockingModels_Cascading(string classId)
        {
            List<classAssessmentList> classAssessmentList = new List<classAssessmentList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_AssessmentLock_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "GetAssessment");
                cmd.Parameters.AddWithValue("@ClassId", classId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
  
                if (dt.Rows.Count > 0)
                {
                    classAssessmentList = dt.AsEnumerable().Select(r => new classAssessmentList
                    {
                        classId = r.Field<string>("ClassId"),
                        assessmentId = r.Field<string>("assessmentId"),
                        assessmentName = r.Field<string>("assessmentName"),
                        isLocked = r.Field<bool>("isLocked"),
                    }
                    ).ToList();
                }
            }
            return classAssessmentList;
        }

        public Tuple<int, string> Exam_AssessmentLock_CRUD(string classId,string assId,bool value)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_AssessmentLock_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@AssessmentId", assId);
                cmd.Parameters.AddWithValue("@Value", value);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


    }
}
