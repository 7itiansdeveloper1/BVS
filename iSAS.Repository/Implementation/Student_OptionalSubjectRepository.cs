using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ISas.Entities;
using System.Configuration;
using System.Data;
using ISas.Repository.Interface;
using System.Web.Mvc;
using System.Linq;

namespace ISas.Repository.Implementation
{

    public class Student_OptionalSubjectRepository : IStudent_OptionalSubject
    {
        //private string ClassTeacherName = "";
        public IEnumerable< OptionalSubjectParametersList> GetOptionalSubjectParametersList(string sessionid, string classid, string userid)
        {
            List<OptionalSubjectParametersList> optionalSubjectParametersList = new List<OptionalSubjectParametersList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_OptionalSubject_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetOptionalSubjectGroup");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //SqlDataReader dr = cmd.ExecuteReader();
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    OptionalSubjectParametersList optionalsubjectparameter = new OptionalSubjectParametersList();
                    optionalsubjectparameter.FieldName = ds.Tables[0].Rows[x][0].ToString();
                    optionalsubjectparameter.FieldDisplayName = ds.Tables[0].Rows[x][1].ToString();
                    optionalSubjectParametersList.Add(optionalsubjectparameter);
                }
                con.Close();
            }
            return optionalSubjectParametersList;
        }
        public Tuple<List<StudentList>, List<SelectListItem>> GetStudentList(string sessionid, string classid, string sectionid, string userid,string mode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<StudentList> _studentList = new List<StudentList>();
                List<SelectListItem> subjectList = new List<SelectListItem>();
                SqlCommand cmd = new SqlCommand("sp_Student_OptionalSubject_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", @mode);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                //if (mode == "Student" || mode == "FName" || mode == "MName")
                //{
                    for (int x = 0; x < count; x++)
                    {
                        StudentList _student = new StudentList();
                        _student.Student = new Student();
                        _student.Student.StudentERPNo = ds.Tables[0].Rows[x][0].ToString();
                        _student.Student.StudentAdmNo = ds.Tables[0].Rows[x][1].ToString();
                        _student.Student.StudentRollNo = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                        _student.Student.StudentName = ds.Tables[0].Rows[x][3].ToString();
                        _student.Parameter1 = ds.Tables[0].Rows[x][4].ToString();
                        if (mode == "Student" || mode == "Father" || mode == "Mother")
                        {
                            _student.Parameter2 = ds.Tables[0].Rows[x][5].ToString();
                            _student.Parameter3 = ds.Tables[0].Rows[x][6].ToString();
                        }
                        _studentList.Add(_student);
                    }

                subjectList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId"),
                }).ToList();

                con.Close();
                return new Tuple<List<StudentList>, List<SelectListItem>>(_studentList, subjectList);
            }
        }
        public IEnumerable<OptionalSubjectList> GetOptionalSubjectList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<OptionalSubjectList> _optionalSubjectList = new List<OptionalSubjectList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_OptionalSubject_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if(mode=="3rdLanguage")
                cmd.Parameters.AddWithValue("@Function", "3rdLanguageSubjectList");
                else if (mode == "3rdLanguageIX")
                cmd.Parameters.AddWithValue("@Function", "3rdLanguageIXSubjectList");
                else if (mode == "NSQF")
                cmd.Parameters.AddWithValue("@Function", "NSQFSubjectList");
                else if (mode == "ArtEducation")
                cmd.Parameters.AddWithValue("@Function", "ArtEducationSubjectList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    OptionalSubjectList _optionalsubject= new OptionalSubjectList();

                    _optionalsubject.SubjectId = ds.Tables[0].Rows[x][0].ToString();
                    _optionalsubject.SubjectName = ds.Tables[0].Rows[x][1].ToString();
                    _optionalSubjectList.Add(_optionalsubject);
                }
                con.Close();
            }
            return _optionalSubjectList;
        }
       
        public Tuple<int, string> Student_OptionalSubject_CRUD(DataTable dt, string userid,string sessionid,string mode)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_OptionalSubject_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FiledName", mode);
                cmd.Parameters.AddWithValue("@Stduent_Update_CRUD_Type", dt);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@LoginUserId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }
    }
}
