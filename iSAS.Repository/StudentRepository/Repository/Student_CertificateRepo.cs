using ISas.Entities.Student_Entities;
using ISas.Repository.StudentRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.StudentRepository.Repository
{
    public class Student_CertificateRepo : IStudent_CertificateRepo
    {
        public List<Student_CertificateModels> getCertificateList(string sessionId, string studcertificateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Student_Certificate_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@StudentCertificateId", studcertificateId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0].AsEnumerable().Select(r => new Student_CertificateModels
                {
                    ERPNO = r.Field<string>("ERPNO"),
                    AdmissionNo = r.Field<string>("AdmNo"),
                    ClassId = r.Field<string>("ClassId"),
                    CertificateID = r.Field<string>("CertificateID"),
                    SessionId = r.Field<int>("SessId").ToString(),
                    AppliedDate = r.Field<string>("AppliedDate"),
                    IssueDate = r.Field<string>("IssueDate"),
                    LastExamGiven = r.Field<string>("LastExamGiven"),
                    LastExamDate = r.Field<string>("LastExamDate"),
                    Remark = r.Field<string>("Remark"),
                    StudentCertificateId = r.Field<string>("StudentCertificateId"),

                    SessId = r.Field<int>("SessId").ToString(),
                    Student = r.Field<string>("Student"),
                    CertificateType  = r.Field<string>("CertificateType"),
                }).ToList();
            }
        }
        public Student_CertificateModels getCertificateById(string sessionId, string studcertificateId)
        {
            return getCertificateList(sessionId, studcertificateId).FirstOrDefault();
        }
        public List<SelectListItem> getCertificateListDropDown()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("select CertificateId, CertificateDisplayName from School_CertificateMaster  where Active=1", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("CertificateDisplayName"),
                    Value = r.Field<string>("CertificateId"),
                }).ToList();
            }
        }
        public Tuple<int, string> Student_Certificate_CRUD(Student_CertificateModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_Student_Certificate_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentCertificateId", model.StudentCertificateId);
                cmd.Parameters.AddWithValue("@CertificateID", model.CertificateID);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@ERPNO", model.ERPNO);

                if (!string.IsNullOrWhiteSpace(model.AppliedDate))
                    cmd.Parameters.AddWithValue("@AppliedDate", Convert.ToDateTime(model.AppliedDate).Date);

                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", model.SectionId);

                if (!string.IsNullOrWhiteSpace(model.IssueDate))
                    cmd.Parameters.AddWithValue("@IssueDate", Convert.ToDateTime(model.IssueDate).Date);

                if (!string.IsNullOrWhiteSpace(model.LastExamDate))
                    cmd.Parameters.AddWithValue("@LastExamDate", Convert.ToDateTime(model.LastExamDate).Date);

                cmd.Parameters.AddWithValue("@LastExamGiven", model.LastExamGiven);
                cmd.Parameters.AddWithValue("@Remark", model.Remark);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@Mode", model.Mode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Student_Certificate_CRUD(string studCertificateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_Student_Certificate_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentCertificateId", studCertificateId);
                //cmd.Parameters.AddWithValue("@CertificateID", model.CertificateID);
                //cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                //cmd.Parameters.AddWithValue("@ERPNO", model.ERPNO);
                //cmd.Parameters.AddWithValue("@AppliedDate", Convert.ToDateTime(model.AppliedDate).Date);
                //cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                //cmd.Parameters.AddWithValue("@SectionId", model.SectionId);
                //cmd.Parameters.AddWithValue("@IssueDate", Convert.ToDateTime(model.IssueDate).Date);
                //cmd.Parameters.AddWithValue("@LastExamDate", Convert.ToDateTime(model.LastExamDate).Date);
                //cmd.Parameters.AddWithValue("@LastExamGiven", model.LastExamGiven);
                //cmd.Parameters.AddWithValue("@Remark", model.Remark);
                //cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@Mode", "CANCEL");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public DataSet getStudentCertificateDetails(string erpNo, string certificateId, string userId, string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("Sp_Student_Certificate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@CertificateId", certificateId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        public DataTable Student_Undertaking_DownloadForm(string erpNo, string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Undertaking_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

    }
}
