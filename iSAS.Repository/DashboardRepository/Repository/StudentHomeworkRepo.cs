using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class StudentHomeworkRepo: IStudentHomeworkRepo
    {
        public List<SelectListItem> StudentHomework_FormLoad(string erpNo, string sessionId)
        {
            List<SelectListItem> ist = new List<SelectListItem>();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_StudentHomework_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt.AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId"),
                }).ToList();
            }

        }
        public List<homework> StudentHomework_GetHomeWorkList(string erpNo, string sessionId,string fromdate,string todate,string hcategory,string subjectid,string status)
        {
            List<homework> ist = new List<homework>();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_StudentHomework_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@fromDate", Convert.ToDateTime(fromdate).Date);
                cmd.Parameters.AddWithValue("@toDate", Convert.ToDateTime(todate).Date);
                cmd.Parameters.AddWithValue("@hwCategory", hcategory);
                cmd.Parameters.AddWithValue("@subjectId", subjectid);
                cmd.Parameters.AddWithValue("@statsus", status);
                
                cmd.Parameters.AddWithValue("@Mode", "GetHomeWorkList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt.AsEnumerable().Select(r => new homework
                {
                    homeWorkId = r.Field<string>("HomeWorkId"),
                    homeWorkTitle = r.Field<string>("Title"),
                    subject = r.Field<string>("SubjectName"),
                    homeWorkDate = r.Field<string>("HomeWorkDate"),
                    submissionDate = r.Field<string>("SubmissionDate"),
                    status = r.Field<string>("Status"),
                    isReviewed = r.Field<string>("IsReviewed"),
                }).ToList();
            }

        }
        public homework StudentHomework_HomeWorkDetail(string erpNo, string homeworkId)
        {
            homework ohomework = new homework();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_StudentHomework_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@homeworkId", homeworkId);
                cmd.Parameters.AddWithValue("@Mode", "HomeWorkDetail");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ohomework.homeWorkId = dt.Rows[0]["HomeWorkId"].ToString();
                    ohomework.homeWorkTitle = dt.Rows[0]["Title"].ToString();
                    ohomework.subject = dt.Rows[0]["SubjectName"].ToString();
                    ohomework.homeWorkDate = dt.Rows[0]["HomeWorkDate"].ToString();
                    ohomework.submissionDate = dt.Rows[0]["SubmissionDate"].ToString();
                    ohomework.attachments= dt.Rows[0]["AttachmentReference"].ToString();
                    ohomework.descriptionAttachment = dt.Rows[0]["DiscriptionReference"].ToString();
                    ohomework.referedBy = dt.Rows[0]["UploadedBy"].ToString();
                    ohomework.AttachFiles = dt.Rows[0]["AttachFiles"].ToString();
                    ohomework.VedioLink1 = dt.Rows[0]["VedioLink1"].ToString();
                    ohomework.VedioLink2 = dt.Rows[0]["VedioLink2"].ToString();
                    ohomework.status = dt.Rows[0]["Status"].ToString();
                    ohomework.isReviewed = dt.Rows[0]["IsReviewed"].ToString();
                    ohomework.ansDescription = dt.Rows[0]["AnsDiscription"].ToString();
                    ohomework.FeedbackattachmentsFilePath = dt.Rows[0]["RevertAttachFile"].ToString();
                    ohomework.remark = dt.Rows[0]["Remark"].ToString();

                }
            }
            return ohomework;
        }

        public Tuple<int, string> Student_HomeWorkMaster_CRUD(homework model,string mode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_HomeWorkMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HomeWorkId", model.homeWorkId);
                cmd.Parameters.AddWithValue("@StudentId", model.studentId);
                cmd.Parameters.AddWithValue("@AttachmentReference", model.AttachFiles);
                cmd.Parameters.AddWithValue("@DiscriptionReference", model.descriptionAttachment);
                cmd.Parameters.AddWithValue("@VLink1", model.VedioLink1);
                cmd.Parameters.AddWithValue("@VLink2", model.VedioLink2);
                cmd.Parameters.AddWithValue("@isSubmitted", model.isSubmited);
                cmd.Parameters.AddWithValue("@mode", mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        
    }
}
