using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using ISas.Entities.Exam_Entities;

namespace ISas.Repository.ExamRepository.Repository
{
    public class Exam_TargetListRepo: IRepository.IExam_TargetListRepo
    {
        public List<SelectListItem> TargetListXI_Transaction_GetClassList(string userid, string sessionid, string classSectionId ,string subjectId)
        {
            //List<SelectListItem> classList = new List<SelectListItem>();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_TargetListXI_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassSectionId", classSectionId);
                cmd.Parameters.AddWithValue("@Mode", "GetClassList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                
            }
            return dt.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Field<string>("FullClassName"),
                Value = r.Field<string>("ClassSectionId"),
            }).ToList();
        }
        public List<SelectListItem> TargetListXI_Transaction_GetSubjectList(string userid, string sessionid, string classSectionId, string subjectId)
        {
            //List<SelectListItem> classList = new List<SelectListItem>();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_TargetListXI_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassSectionId", classSectionId);
                cmd.Parameters.AddWithValue("@Mode", "GetSubjects");
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return dt.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Field<string>("SubjectName"),
                Value = r.Field<string>("SubjectId"),
            }).ToList();
        }
        public Exam_TargetModels TargetListXI_Transaction_GetTargetList(string userid, string sessionid, string classSectionId, string subjectId,string subjectName,string className)
        {
            Exam_TargetModels model = new Exam_TargetModels();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_TargetListXI_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassSectionId", classSectionId);
                cmd.Parameters.AddWithValue("@Mode", "GetTargetList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            model.SubjectName = subjectName;
            model.ClassName = className;
            model.TargetList=
            dt.AsEnumerable().Select(r => new Exam_TargetList
            {
                ERPNO = r.Field<string>("ERPNO"),
                Student = r.Field<string>("Student"),
                UT = r.Field<string>("UT"),
                HY = r.Field<string>("HY"),
                UTBEST = r.Field<string>("UTBEST"),
                Final = r.Field<string>("Final"),
                Total= r.Field<string>("Total"),
                PassingMark = r.Field<string>("PassingMark"),
                Target = r.Field<string> ("Target")
            }).ToList();

            return model;
        }
        
    }

    
}
