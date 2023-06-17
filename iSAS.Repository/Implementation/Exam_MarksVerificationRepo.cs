using ISas.Entities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.Implementation
{
    public class Exam_MarksVerificationRepo : IExam_MarksVerificationRepo
    {
        public Exam_MarksVerificationModels GetStudentWiseMarksDetails(string userid, string sessionid, string classid, string sectionid, string erpno, string examid, string mainsubjectid)
        {
            Exam_MarksVerificationModels model = new Exam_MarksVerificationModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string mode = erpno == "0" ? "ClassWiseGradeVerification" : "StudentWiseGradeVerification";

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_GradeCardVerification_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@Mode", mode);
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@MainSubjectId", mainsubjectid);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
               
                da.Fill(ds);
                con.Close();

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    model.HeaderNameList.Add(ds.Tables[0].Columns[i].ColumnName);
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    List<string> newRow = new List<string>();
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        newRow.Add(ds.Tables[0].Rows[i][j].ToString());
                    }
                    model.ValueList.Add(newRow);
                }
            }
            return model;
        }


        #region Marks Verification 1

        public List<SelectListItem> GetDropDown_OnFormLoad(string SessionId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntryVerification_Cascading_New_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamId", "");
                cmd.Parameters.AddWithValue("@ClassId", "");
                cmd.Parameters.AddWithValue("@SectionId", "");
                cmd.Parameters.AddWithValue("@Category", "");
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ExamName"),
                    Value = r.Field<string>("ExamId"),
                }).ToList();
            }
        }
        public List<SelectListItem> GetDropDownListByMode(Exam_MarksVerificationModels param)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntryVerification_Cascading_New_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", param.SessionId);
                cmd.Parameters.AddWithValue("@UserId", param.UserId);
                cmd.Parameters.AddWithValue("@ExamId", param.ExamId);
                cmd.Parameters.AddWithValue("@ClassId", param.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", param.SectionId);
                cmd.Parameters.AddWithValue("@Category", param.Category);
                cmd.Parameters.AddWithValue("@Mode", param.Mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Text"),
                    Value = r.Field<string>("Value"),
                }).ToList();
            }
        }
        public Exam_MarksVerificationModels StudentWiseMarksDetials(Exam_MarksVerificationModels param)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Exam_MarksVerificationModels model = new Exam_MarksVerificationModels();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntryVerification_Cascading_New_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", param.SessionId);
                cmd.Parameters.AddWithValue("@UserId", param.UserId);
                cmd.Parameters.AddWithValue("@ExamId", param.ExamId);
                cmd.Parameters.AddWithValue("@ClassId", param.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", param.SectionId);
                cmd.Parameters.AddWithValue("@Category", param.Category);
                cmd.Parameters.AddWithValue("@Mode", param.Mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.Strength = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                for (int i = 0; i < ds.Tables[1].Columns.Count; i++)
                {
                    model.HeaderNameList.Add(ds.Tables[1].Columns[i].ColumnName);
                }

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    List<string> newRow = new List<string>();
                    for (int j = 0; j < ds.Tables[1].Columns.Count; j++)
                    {
                        newRow.Add(ds.Tables[1].Rows[i][j].ToString());
                    }
                    model.ValueList.Add(newRow);
                }

                return model;
            }
        }
        #endregion
    }
}
