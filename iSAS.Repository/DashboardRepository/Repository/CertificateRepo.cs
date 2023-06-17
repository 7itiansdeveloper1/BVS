using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ISas.Repository.DashboardRepository.IRepository;
using ISas.Entities.DashboardEntities;
using System.Collections.Generic;
using System.Linq;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class CertificateRepo: ICertificateRepo
    {
        public List<CertificateModels> Dashboard_CertificatePath(string erpNo,string sessionId)
        {
            List<CertificateModels> certificateList = new List<CertificateModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Dashboard_CertificatePath",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                return ds.Tables[0].AsEnumerable().Select(r => new CertificateModels
                {
                    CertificateName = r.Field<string>("CertificateName"),
                    HavingCertificate = r.Field<bool>("HavingCertificate"),
                    CertificateDate = r.Field<string>("CertificateDate"),
                    ReportName = r.Field<string>("ReportName"),
                    Student = r.Field<string>("Student"),
                }).ToList();
            }




            //string resumeFile = dt.Rows[0][1].ToString();
            //if (System.IO.File.Exists(resumeFile))
            //{
            //    model.HavingCertificate = true;

            //}
            //else
            //{
            //    model.HavingCertificate = false;
            //}

            //model.DownloadFilePath= dt.Rows[0][0].ToString();
            //model.CertificateName= dt.Rows[0][0].ToString();
            //model.HavingCertificate =Convert.ToBoolean(dt.Rows[0][1]);

           
        }

        public DataSet GetCertifricateReport(string certificateId, string ERPNo)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Dashboard_CertificatePath", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@CetificateId", certificateId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            return ds;
        }
    }
}
