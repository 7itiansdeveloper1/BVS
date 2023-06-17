using ISas.Entities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.Implementation
{
    public class MarksEntry_StudentWiseRepo : IMarksEntry_StudentWiseRepo
    {
        public Tuple<List<SelectListItem>, List<SelectListItem>> GetMainSubjectWithStudentList(string UserId, string ExamId, string ClassId, string SectionId,string sessionId)
        {
            List<SelectListItem> mainSubjectList = new List<SelectListItem>();
            List<SelectListItem> studentList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_StudentWiseMarksEntry_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamId", ExamId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@Mode", "GetMainSubjectList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                mainSubjectList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("ParentSubjectId"),
                    Text = r.Field<string>("ParentSubjectName"),
                }).ToList();

                SqlCommand cmd1 = new SqlCommand("sp_Exam_StudentWiseMarksEntry_Cascading", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@UserId", UserId);
                cmd1.Parameters.AddWithValue("@ExamId", ExamId);
                cmd1.Parameters.AddWithValue("@ClassId", ClassId);
                cmd1.Parameters.AddWithValue("@SectionId", SectionId);
                cmd1.Parameters.AddWithValue("@SessionId", sessionId);
                cmd1.Parameters.AddWithValue("@MainSubjectId", "");
                cmd1.Parameters.AddWithValue("@Mode", "GetStudentNameList");
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                ds = new DataSet();
                da1.Fill(ds);

                studentList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("ERPNo"),
                    Text = r.Field<string>("Student"),
                }).ToList();

                con.Close();
            }
            return new Tuple<List<SelectListItem>, List<SelectListItem>>(mainSubjectList, studentList);


        }

        public MarksEntryStudentWiseModel GetStudentWiseMarkList(string userid, string examid, string classid, string sectioinid, string mainsubjectid, string sessionid, string erpno)
        {
            MarksEntryStudentWiseModel model = new MarksEntryStudentWiseModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_StudentWiseMarksEntry_Cascading", con);
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectioinid);
                cmd.Parameters.AddWithValue("@MainSubjectId", mainsubjectid);
                cmd.Parameters.AddWithValue("@Mode", "GetStudentSubjectWiseMarkList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@TransactionMode", "MarksEntry");
                cmd.Parameters.AddWithValue("@ViewMode", null);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                int rowCount = dt.Rows.Count;
                int columnCount = dt.Columns.Count;

                //if (columnCount > 2)
                //  columnCount = columnCount;


                for (int j = 2; j < columnCount; j++)
                {
                    model.AssismentNameList.Add(new AssismentModel { AssismentName = dt.Columns[j].ColumnName });
                }


                for (int i = 0; i < rowCount; i++)
                {
                    SubjectModel tempSubjectListWithMarks = new SubjectModel();
                    tempSubjectListWithMarks.SubjectCode = dt.Rows[i][0].ToString();
                    tempSubjectListWithMarks.SubjectName = dt.Rows[i][1].ToString();

                    for (int j = 2; j < columnCount; j++)
                    {
                        AssismentModel tempAssment = new AssismentModel();
                        tempAssment.Marks = dt.Rows[i][j].ToString();
                        tempAssment.AssismentName = model.AssismentNameList[j - 2].AssismentName;
                        tempSubjectListWithMarks.AssismentWithMarksList.Add(tempAssment);
                    }
                    model.SubjectListWithMarks.Add(tempSubjectListWithMarks);
                }

                return model;
            }
        }

        public string SubjectWiseMarksEntry_CRUD(MarksEntryStudentWiseModel model)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                try
                {
                    con.Open();
                    if (model.SubjectListWithMarks != null && model.SubjectListWithMarks.Count > 0)
                    {
                        for (int i = 0; i < model.SubjectListWithMarks.Count; i++)
                        {
                            if (model.SubjectListWithMarks[i].AssismentWithMarksList != null && model.SubjectListWithMarks[i].AssismentWithMarksList.Count > 0)
                            {
                                for (int j = 0; j < model.SubjectListWithMarks[i].AssismentWithMarksList.Count; j++)
                                {
                                    SqlCommand cmd = new SqlCommand("sp_Exam_StudentWiseMarksEntry_CRUD", con);
                                    cmd.Parameters.AddWithValue("@ERPNo", model.ErpNo_Id);
                                    cmd.Parameters.AddWithValue("@Mark", model.SubjectListWithMarks[i].AssismentWithMarksList[j].Marks);
                                    cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                                    cmd.Parameters.AddWithValue("@UserId", model.UserID);
                                    cmd.Parameters.AddWithValue("@ExamId", model.ExamId);
                                    cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                                    cmd.Parameters.AddWithValue("@SectionId", model.SectionId);
                                    cmd.Parameters.AddWithValue("@SubjectId", model.SubjectListWithMarks[i].SubjectCode);
                                    cmd.Parameters.AddWithValue("@AssessmentName", model.SubjectListWithMarks[i].AssismentWithMarksList[j].AssismentName);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    con.Close();
                    message = "Marks is updated successfully, Please verify..!";
                }
                catch(Exception ex)
                {
                    message = ex.Message;
                }
            }
            return message;
        }

        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
