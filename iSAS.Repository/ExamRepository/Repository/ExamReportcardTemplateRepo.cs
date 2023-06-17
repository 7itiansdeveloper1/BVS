using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using ISas.Repository.ExamRepository.IRepository;
namespace ISas.Repository.ExamRepository.Repository
{
    public class ExamReportcardTemplateRepo: IExamReportcardTemplateRepo
    {

        public ExamReportcardTemplateModels ExamReportcardTemplate_Transaction_FormLoad ()
        {
            ExamReportcardTemplateModels examReportcardTemplateModels = new ExamReportcardTemplateModels();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamReportcardTemplate_Transaction",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count>0)
                {
                    examReportcardTemplateModels.examList = dt.AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("examName"),
                        Value = r.Field<string>("examId")
                    }).ToList(); 
                }


            }
            return examReportcardTemplateModels;
        }

        public ExamReportcardTemplateModels ExamReportcardTemplate_Transaction_GetTemplateList(string termId)
        {
            ExamReportcardTemplateModels examReportcardTemplateModels = new ExamReportcardTemplateModels();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamReportcardTemplate_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@termId", termId);
                cmd.Parameters.AddWithValue("@Mode", "GetTemplateList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                examReportcardTemplateModels.examId = termId;

                if (dt.Rows.Count > 0)
                {
                    examReportcardTemplateModels.reportcardTemplateList = dt.AsEnumerable().Select(r => new ReportcardTemplateList
                    {
                        examId=r.Field<string>("examId"),
                        classId = r.Field<string>("classId"),
                        className =r.Field<string>("className"),
                        termStartDate = r.Field<string>("termStartDate"),
                        termEndDate = r.Field<string>("termEndDate"),
                        reportCardDate = r.Field<string>("reportCardDate"),
                        termFirstPTMDate = r.Field<string>("termFirstPTMDate"),
                        termLastPTMDate = r.Field<string>("termLastPTMDate"),
                        haveOrientation = r.Field<bool>("haveOrientation"),
                        isTermLockforClass = r.Field<bool>("isTermLockforClass")
                    }).ToList();
                }


            }
            return examReportcardTemplateModels;
        }

        public Tuple<int, string> ExamReportcardTemplate_CRUD(ExamReportcardTemplateModels model)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamReportcardTemplate_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("classId");
                dt.Columns.Add("termStartDate");
                dt.Columns.Add("termEndDate");
                dt.Columns.Add("reportCardDate");
                dt.Columns.Add("haveOrientation");
                dt.Columns.Add("termFirstPTMDate");
                dt.Columns.Add("termLastPTMDate");
                if (model != null && model.reportcardTemplateList.Count > 0)
                {
                    for (int x = 0; x < model.reportcardTemplateList.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row["classId"] = model.reportcardTemplateList[x].classId.ToString();
                        row["termStartDate"] = model.reportcardTemplateList[x].termStartDate;
                        row["termEndDate"] = model.reportcardTemplateList[x].termEndDate;
                        row["reportCardDate"] = model.reportcardTemplateList[x].reportCardDate;
                        row["haveOrientation"] = model.reportcardTemplateList[x].haveOrientation;
                        row["termFirstPTMDate"] = model.reportcardTemplateList[x].termFirstPTMDate;
                        row["termLastPTMDate"] = model.reportcardTemplateList[x].termLastPTMDate;
                        dt.Rows.Add(row);
                    }
                }
                cmd.Parameters.AddWithValue("@Dt", dt);
                cmd.Parameters.AddWithValue("@examId", model.examId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Exam_TermLock_CRUD(string classId, string examId, bool value)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_TermLock_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@ExamId", examId);
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
