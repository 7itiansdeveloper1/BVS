using ISas.Entities.Examination_Entities;
using ISas.Repository.ExaminationRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.Repository
{
    public class Examination_RemarksEntryRepo : IExamination_RemarksEntryRepo
    {
        public List<SelectListItem> Get_RemarksEntryDropDowns(string SessionId, string UserId, string ClassId, string Mode, string ExamId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_RemarksEntry_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId ", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassID", ClassId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamId);
                cmd.Parameters.AddWithValue("@Mode", Mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (Mode == "FormLoad")
                    return ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("ExamTemplateName"),
                        Value = r.Field<string>("ExamTemplateId"),
                    }).ToList();

                else if (Mode == "GetClassList")
                    return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("ClassName"),
                        Value = r.Field<string>("ClassID"),
                    }).ToList();

                else if (Mode == "GetSectionList")
                    return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("SecName"),
                        Value = r.Field<string>("SecID"),
                    }).ToList();

                else
                    return new List<SelectListItem>();
            }
        }
        public Examination_RemarksEntryModels Get_RemarksEntryStudentDetails(string SessionId, string UserId, string ClassId, string SectionId, string ExamTempleteId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Examination_RemarksEntryModels model = new Examination_RemarksEntryModels();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_RemarksEntry_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId ", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassID", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", ExamTempleteId);
                cmd.Parameters.AddWithValue("@Mode", "GetClassStudentList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.RemarksList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Remark"),
                    Value = r.Field<string>("RemarkId"),
                }).ToList();
                model.StudentDetails = ds.Tables[1].AsEnumerable().Select(r => new RemarksEntry_StudentDetailsModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ERPNo = r.Field<string>("ERPNo"),
                    RemarkId = r.Field<string>("RemarkId"),
                    RollNo = r.Field<int>("RollNo"),
                    Student = r.Field<string>("Student"),
                }).ToList();
                return model;
            }
        }
        public Tuple<int, string> Examination_RemarksEntry_CRUD(DataTable dt, Examination_RemarksEntryModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_RemarksEntry_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@ExamTemplateId", model.ExamId);
                cmd.Parameters.AddWithValue("@DataTable", dt);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
    }
}
