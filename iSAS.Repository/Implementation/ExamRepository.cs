using ISas.Entities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;


namespace ISas.Repository.Implementation
{
    public class ExamRepository : IExam
    {
        public IEnumerable<ExamNameList> GetExamNameList(string SessionId, string UserId,string mode)
        {
            List<ExamNameList> examnamelist = new List<ExamNameList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry_FormLoad", con);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@mode", mode);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ExamNameList examname = new ExamNameList();
                    examname.ExamId = dt.Rows[x][0].ToString();
                    examname.ExamName = dt.Rows[x][1].ToString();
                    examname.PrintOrder = Convert.ToInt32(dt.Rows[x][2]);
                    examnamelist.Add(examname);
                }
                con.Close();
            }
            return examnamelist;
        }


        public IEnumerable<ExamNameList> GetExamNameList_Retest(string SessionId, string UserId)
        {
            List<ExamNameList> examnamelist = new List<ExamNameList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry_FormLoad_Retest", con);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ExamNameList examname = new ExamNameList();
                    examname.ExamId = dt.Rows[x][0].ToString();
                    examname.ExamName = dt.Rows[x][1].ToString();
                    examname.PrintOrder = Convert.ToInt32(dt.Rows[x][2]);
                    examnamelist.Add(examname);
                }
                con.Close();
            }
            return examnamelist;
        }

        public IEnumerable<ExamNameList> GetExamNameListForMR(string SessionId, string UserId)
        {
            List<ExamNameList> examnamelist = new List<ExamNameList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("Exam_GetExamNameListForMR", con);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ExamNameList examname = new ExamNameList();
                    examname.ExamId = dt.Rows[x][0].ToString();
                    examname.ExamName = dt.Rows[x][1].ToString();
                    examname.PrintOrder = Convert.ToInt32(dt.Rows[x][2]);
                    examnamelist.Add(examname);
                }
                con.Close();
            }
            return examnamelist;
        }
        public IEnumerable<ClassList> GetClassList(string examid, string userid)
        {

            List<ClassList> classlist = new List<ClassList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@Mode", "GetClasses");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ClassList classname = new ClassList();
                    classname.ClassId = dt.Rows[x][0].ToString();
                    classname.ClassName = dt.Rows[x][1].ToString();
                    classname.PrintOrder = Convert.ToInt32(dt.Rows[x][2].ToString());
                    classlist.Add(classname);
                }
                con.Close();
            }
            return classlist;
        }
        public IEnumerable<ClassList> GetClassListForReportCard(string examid, string userid,string sessionId)
        {

            List<ClassList> classlist = new List<ClassList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("Exam_GetExamClassList_ForUser_ForReportCard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ClassList classname = new ClassList();
                    classname.ClassId = dt.Rows[x][0].ToString();
                    classname.ClassName = dt.Rows[x][1].ToString();
                    classname.PrintOrder = Convert.ToInt32(dt.Rows[x][2].ToString());
                    classlist.Add(classname);
                }
                con.Close();
            }
            return classlist;
        }


        public IEnumerable<SectionList> GetSectionList(string examid, string classid, string userid)
        {

            List<SectionList> sectionlist = new List<SectionList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@Mode", "GetSections");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    SectionList sectionname = new SectionList();
                    sectionname.SectionId = dt.Rows[x][0].ToString();
                    sectionname.SectionName = dt.Rows[x][1].ToString();
                    sectionname.PrintOrder = Convert.ToInt32(dt.Rows[x][2].ToString());
                    sectionlist.Add(sectionname);
                }
                con.Close();
            }
            return sectionlist;
        }
        public IEnumerable<SubjectList> GetSubjectList(string examid, string userid, string classid, string sectionid)
        {

            List<SubjectList> subjectlist = new List<SubjectList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_Cascading", con);
                if (classid == "CLS0000004" || classid == "CLS0000005" || classid == "CLS0000006" || classid == "CLS0000007" || classid == "CLS0000008")
                    cmd = new SqlCommand("sp_Exam_MarksEntry_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@Mode", "GetSubjects");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    SubjectList subjectname = new SubjectList();
                    subjectname.SubjectId = dt.Rows[x][0].ToString();
                    subjectname.SubjectName = dt.Rows[x][1].ToString();
                    subjectname.PrintOrder = Convert.ToInt32(dt.Rows[x][2].ToString());
                    subjectname.IsMarkBased = Convert.ToBoolean(dt.Rows[x][3]);
                    subjectname.IsGradeBased = Convert.ToBoolean(dt.Rows[x][4]);
                    subjectlist.Add(subjectname);
                }
                con.Close();
            }
            return subjectlist;
        }
        public IEnumerable<GradingList> GetGradeList(string classid, string subjectid)
        {
            List<GradingList> gradinglist = new List<GradingList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_Cascading", con);
                if (classid == "CLS0000004" || classid == "CLS0000005" || classid == "CLS0000006" || classid == "CLS0000007" || classid == "CLS0000008")
                    cmd = new SqlCommand("sp_Exam_MarksEntry_Cascading", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@Mode", "GetGradingList");
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    GradingList grade = new GradingList();
                    grade.GradingId = dt.Rows[x][0].ToString();
                    grade.GradingName = dt.Rows[x][1].ToString();
                    gradinglist.Add(grade);
                }
                con.Close();
            }
            return gradinglist;
        }
        public IEnumerable<AssessmentNameList> GetAssessmentList(string examid, string userid, string classid, string sectionid, string subjectid)
        {
            List<AssessmentNameList> assessmentlist = new List<AssessmentNameList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_Cascading", con);
                if (classid == "CLS0000004" || classid == "CLS0000005" || classid == "CLS0000006" || classid == "CLS0000007" || classid == "CLS0000008")
                    cmd = new SqlCommand("sp_Exam_MarksEntry_Cascading", con);


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@Mode", "GetAssessments");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    AssessmentNameList assessment = new AssessmentNameList();
                    assessment.AssessmentId = dt.Rows[x][0].ToString();
                    assessment.AssessmentName = dt.Rows[x][1].ToString();
                    assessment.PrintOrder = Convert.ToInt32(dt.Rows[x][2].ToString());
                    assessmentlist.Add(assessment);
                }
                con.Close();
            }
            return assessmentlist;
        }

        
        public IEnumerable<StudentsMarkList> GetStudentMarkList(string examid, string userid, string classid, string sectionid, string subjectid, string assessmentid,string sessionId)
        {
            List<StudentsMarkList> studentmarklist = new List<StudentsMarkList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_Cascading", con);
                if (classid == "CLS0000004" || classid == "CLS0000005" || classid == "CLS0000006" || classid == "CLS0000007" || classid == "CLS0000008")
                    cmd = new SqlCommand("sp_Exam_MarksEntry_Cascading", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@AssessmentId", assessmentid);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@Mode", "GetStudentMarkList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataSet ds = new DataSet();
                //DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    StudentsMarkList studentmark = new StudentsMarkList();
                    studentmark.Student = new Student();
                    studentmark.Student.StudentERPNo = ds.Tables[0].Rows[x][0].ToString();
                    studentmark.Student.StudentAdmNo = ds.Tables[0].Rows[x][1].ToString();
                    studentmark.Student.StudentRollNo = Convert.ToInt32(ds.Tables[0].Rows[x][2].ToString());
                    studentmark.Student.StudentName = ds.Tables[0].Rows[x][3].ToString();

                    studentmark.StudentsMarkFromDBList = new StudentsMarkFromDBList();

                    studentmark.StudentsMarkFromDBList.IsStudentAbsent = Convert.ToBoolean(ds.Tables[0].Rows[x][4].ToString());
                    studentmark.StudentsMarkFromDBList.IsStudentonML = Convert.ToBoolean(ds.Tables[0].Rows[x][5].ToString());
                    studentmark.StudentsMarkFromDBList.IsStudentExempt = Convert.ToBoolean(ds.Tables[0].Rows[x][6].ToString());
                    studentmark.StudentsMarkFromDBList.StudentMark = ds.Tables[0].Rows[x][7].ToString();
                    studentmark.StudentsMarkFromDBList.StudentGrade = ds.Tables[0].Rows[x][8].ToString();
                    studentmarklist.Add(studentmark);
                }
                con.Close();
            }
            return studentmarklist;
        }
        public IEnumerable<StudentsMarkList> GetRetestStudentMarkList(string examid, string userid, string classid, string sectionid, string subjectid, string assessmentid, string sessionId)
        {
            List<StudentsMarkList> studentmarklist = new List<StudentsMarkList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_Retest_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@AssessmentId", assessmentid);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@Mode", "GetStudentMarkList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    StudentsMarkList studentmark = new StudentsMarkList();
                    studentmark.Student = new Student();
                    studentmark.Student.StudentERPNo = ds.Tables[0].Rows[x][0].ToString();
                    studentmark.Student.StudentAdmNo = ds.Tables[0].Rows[x][1].ToString();
                    studentmark.Student.StudentRollNo = Convert.ToInt32(ds.Tables[0].Rows[x][2].ToString());
                    studentmark.Student.StudentName = ds.Tables[0].Rows[x][3].ToString();
                    studentmark.StudentsMarkFromDBList = new StudentsMarkFromDBList();

                    studentmark.StudentsMarkFromDBList.IsStudentAbsent = Convert.ToBoolean(ds.Tables[0].Rows[x][4].ToString());
                    studentmark.StudentsMarkFromDBList.IsStudentonML = Convert.ToBoolean(ds.Tables[0].Rows[x][5].ToString());
                    studentmark.StudentsMarkFromDBList.IsStudentExempt = Convert.ToBoolean(ds.Tables[0].Rows[x][6].ToString());
                    studentmark.StudentsMarkFromDBList.IsRetestStudent = Convert.ToBoolean(ds.Tables[0].Rows[x][7].ToString());
                    studentmark.StudentsMarkFromDBList.StudentMark = ds.Tables[0].Rows[x][8].ToString();
                    studentmark.StudentsMarkFromDBList.StudentGrade = ds.Tables[0].Rows[x][9].ToString();
                    studentmarklist.Add(studentmark);
                }
                con.Close();
            }
            return studentmarklist;
        }
        public DataTable MaxMarkConfiguration(string examid, string classid, string sectionid, string subjectid, string assessmentid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_GetMaxMarkConfiguration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@AssessmentId", assessmentid);
                con.Open();
                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable RetestMaxMarkConfiguration(string examid, string classid, string sectionid, string subjectid, string assessmentid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_GetMaxMarkConfiguration_Retest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@AssessmentId", assessmentid);
                con.Open();
                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }


        public Tuple<int, string> SaveClassMarks(DataTable dt, string sessionid, string examid, string userid, string classid, string sectionid, string subjectid, string Assessmentid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_CRUD", con);
                if (classid == "CLS0000004" || classid == "CLS0000005" || classid == "CLS0000006" || classid == "CLS0000007" || classid == "CLS0000008")
                    cmd = new SqlCommand("sp_Exam_MarksEntry_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassMarkListDT", dt);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@AssessmentId", Assessmentid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> SaveClassMarks_Retest(DataTable dt, string sessionid, string examid, string userid, string classid, string sectionid, string subjectid, string Assessmentid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_Retest_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassMarkListDT", dt);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@AssessmentId", Assessmentid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> RetestStudent_CRUD(string sessionId, string erpNo, string userId, bool value, string subjectId, string assessmentId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_RetestStudent_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionId", sessionId);
                cmd.Parameters.AddWithValue("@erpNo", erpNo);
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                cmd.Parameters.AddWithValue("@assessmentId", assessmentId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
        public IEnumerable<ParentSubjectList> GetParentSubjectList(string examid, string userid, string classid, string sectionid)
        {
            List<ParentSubjectList> subjectlist = new List<ParentSubjectList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_StudentWiseMarksEntry_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@Mode", "GetParentSubjectList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ParentSubjectList subjectname = new ParentSubjectList();
                    subjectname.ParentSubjectId = dt.Rows[x][0].ToString();
                    subjectname.ParentSubjectName = dt.Rows[x][1].ToString();
                    subjectname.PrintOrder = Convert.ToInt32(dt.Rows[x][2].ToString());
                    subjectname.IsMarkBased = Convert.ToBoolean(dt.Rows[x][3]);
                    subjectname.IsGradeBased = Convert.ToBoolean(dt.Rows[x][4]);
                    subjectlist.Add(subjectname);
                }
                con.Close();
            }
            return subjectlist;
        }
        public IEnumerable<ClassStudentList> GetClassStudentList(string userid, string classid, string sectionid, string subjectid, string sessionid)
        {
            List<ClassStudentList> classstudentlist = new List<ClassStudentList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@Mode", "GetStudentList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ClassStudentList classstudent = new ClassStudentList();
                    classstudent.ERPNo = dt.Rows[x][0].ToString();
                    classstudent.AdmNo = dt.Rows[x][1].ToString();
                    classstudent.RollNo = Convert.ToInt32(dt.Rows[x][2].ToString());
                    classstudent.Student = dt.Rows[x][3].ToString();
                    classstudentlist.Add(classstudent);
                }
                con.Close();
            }
            return classstudentlist;
        }
        public IEnumerable<StudentCoScholasticMarkList> GetStudentCoScholasticMarkList(string examid, string userid, string classid, string sectionid, string subjectid, string erpno)
        {
            List<StudentCoScholasticMarkList> studentCoScholasticMarkList = new List<StudentCoScholasticMarkList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_StudentWiseMarksEntry_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@Mode", "GetStudentCoScholasticMarkList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@MainSubjectId", subjectid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    StudentCoScholasticMarkList studentCoScholasticMark = new StudentCoScholasticMarkList();
                    studentCoScholasticMark.StudentCoScholsticMarkFromDBList = new StudentCoScholasticMarkFromDBList();
                    //studentCoScholasticMark.Student.StudentERPNo = dt.Rows[x][0].ToString();
                    studentCoScholasticMark.StudentCoScholsticMarkFromDBList.SubjectId = dt.Rows[x][0].ToString();
                    studentCoScholasticMark.StudentCoScholsticMarkFromDBList.SubjectName = dt.Rows[x][1].ToString();
                    studentCoScholasticMark.StudentCoScholsticMarkFromDBList.Parameter1 = dt.Rows[x][2].ToString();
                    studentCoScholasticMark.StudentCoScholsticMarkFromDBList.Parameter2 = dt.Rows[x][3].ToString();
                    studentCoScholasticMark.StudentCoScholsticMarkFromDBList.Parameter3 = dt.Rows[x][4].ToString();
                    studentCoScholasticMarkList.Add(studentCoScholasticMark);
                }
                con.Close();
            }
            return studentCoScholasticMarkList;
        }
        public IEnumerable<StudentActivityMarkList> GetStudentActivityMarkList1(string examid, string assessmentid, string userid, string classid, string sectionid, string subjectid,string sessionId)
        {
            List<StudentActivityMarkList> studentActivityMarkList = new List<StudentActivityMarkList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_ActivityMarksEntry_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@AssessmentId", assessmentid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@Mode", "GetStudentActivityMarkList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                int colCount = dt.Columns.Count;
                for (int x = 0; x < count; x++)
                {
                    StudentActivityMarkList studentActivityMark = new StudentActivityMarkList();
                    studentActivityMark.ERPNo = dt.Rows[x][0].ToString();
                    studentActivityMark.AdmNo = dt.Rows[x][1].ToString();
                    studentActivityMark.RollNo = Convert.ToInt32(dt.Rows[x][2].ToString());
                    studentActivityMark.StudentName = dt.Rows[x][3].ToString();

                    for (int y = 4; y < (colCount - 1); y++)
                    {
                        studentActivityMark.StudentActivityFromDBList.Add(new StudentActivityMarkFromDB { MarksValue = dt.Rows[x][y].ToString() });
                    }
                    studentActivityMark.Total = dt.Rows[x][colCount - 1].ToString();
                    studentActivityMarkList.Add(studentActivityMark);
                }
                con.Close();
            }
            return studentActivityMarkList;
        }
        public DataSet GetActivityHeaderList(string examid, string userid, string classid, string sectionid, string subjectid, string assessmentId)
        {
            DataSet ds = new DataSet();
            //List<HeaderList> headerList = new List<HeaderList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_ActivityMarksEntry_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@Mode", "GetActivitySubjectList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.Parameters.AddWithValue("@AssessmentId", assessmentId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                //int count = ds.Tables[1].Rows.Count;
                //for (int x = 0; x < count; x++)
                //{
                //    HeaderList header = new HeaderList();
                //    header.Subject1 = ds.Tables[1].Rows[x][0].ToString();
                //    header.Subject2 = ds.Tables[1].Rows[x][0].ToString();
                //    header.Subject3 = ds.Tables[1].Rows[x][2].ToString();
                //    header.Subject4 = ds.Tables[1].Rows[x][3].ToString();
                //    header.Subject5 = ds.Tables[1].Rows[x][4].ToString();
                //    header.Subject6 = ds.Tables[1].Rows[x][5].ToString();
                //    headerList.Add(header);
                //}
                //con.Close();
            }
            return ds;
        }
        public string Student_CoScholasticMarksEntry_CRUD(DataTable dt, string sessionId, string examId, string userId, string classId, string sectionId, string erpNo)
        {
            string message = "";
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntryCoScholastic6to8_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentCoScholasticMarkListDT", dt);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@ExamId", examId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            message = dt1.Rows[0][1].ToString();
            return message;
        }
        public Tuple<int, string> Student_ActivityMark_CRUD(ActivityMarksEntryModel model)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntryCoScholastic6to8_CRUD1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("ERPNo");
                dt.Columns.Add("Subject1Mark");
                dt.Columns.Add("Subject2Mark");
                dt.Columns.Add("Subject3Mark");
                dt.Columns.Add("Subject4Mark");
                dt.Columns.Add("Subject5Mark");
                //int rowcount = model.StudentsActivityMarkList.Count;
                if (model != null && model.StudentsActivityMarkList.Count > 0)
                {
                    for (int x = 0; x < model.StudentsActivityMarkList.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = model.StudentsActivityMarkList[x].ERPNo;

                        for (int i = 1; i < 6; i++)
                        {
                            if (model.StudentsActivityMarkList[x].StudentActivityFromDBList.Count > (i - 1))
                                row[i] = model.StudentsActivityMarkList[x].StudentActivityFromDBList[i - 1].MarksValue;

                            else
                                row[i] = "";
                        }
                        dt.Rows.Add(row);
                    }
                }

                cmd.Parameters.AddWithValue("@StudentCoScholasticMarkListDT", dt);
                cmd.Parameters.AddWithValue("@SessionId", model.SelectedSessionId);
                cmd.Parameters.AddWithValue("@ClassId", model.SelectedClassId);
                cmd.Parameters.AddWithValue("@SectionId", model.SelectedSectionId);
                cmd.Parameters.AddWithValue("@ExamId", model.SelectedExamId);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@MainSubjectId", model.SelectedSubjectId);
                cmd.Parameters.AddWithValue("@AssessmentId", model.SelectedAssessmentId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }

        public Exam_MarksVerificationModel GetClassVerificationList(string sessionid, string classid, string sectionid, string examid, string subjectid)
        {
            Exam_MarksVerificationModel result = new Exam_MarksVerificationModel();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string procName = "sp_Exam_ReportCard6to8_Part1Verification";

                //if (classid == "CLS0000012" || classid == "CLS0000013")
                //    procName = "sp_Exam_ReportCard6to8_Part1Verification_910";

                SqlCommand cmd = new SqlCommand(procName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@Mode", "GetClassVerificationList_Academic");
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                for(int i =0; i < dt.Columns.Count; i++)
                {
                    result.TitleList.Add(new System.Web.Mvc.SelectListItem { Text = dt.Columns[i].ColumnName });
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    List<SelectListItem> newRow = new List<SelectListItem>();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        newRow.Add(new System.Web.Mvc.SelectListItem { Value = dt.Rows[i][j].ToString() ?? "" });
                    }
                    result.ValuesList.Add(newRow);
                }

                //int count = dt.Rows.Count;
                //for (int x = 0; x < count; x++)
                //{
                //    ClassVerificationList classVerification = new ClassVerificationList();
                //    classVerification.ERPNo = dt.Rows[x][0].ToString();
                //    classVerification.AdmNo = dt.Rows[x][1].ToString();
                //    classVerification.RollNo = Convert.ToInt32(dt.Rows[x][2].ToString());
                //    classVerification.Student = dt.Rows[x][3].ToString();
                //    classVerification.SubjectName = dt.Rows[x][4].ToString();
                //    classVerification.Assess1Mark = dt.Rows[x][5].ToString();
                //    classVerification.Assess2Mark = dt.Rows[x][6].ToString();
                //    classVerification.Assess3Mark = dt.Rows[x][7].ToString();
                //    classVerification.Assess4Mark = dt.Rows[x][8].ToString();
                //    classVerification.Assess5Mark = dt.Rows[x][9].ToString();
                //    classVerification.Assess6Mark = dt.Rows[x][10].ToString();
                //    classVerification.TotalMark = dt.Rows[x][11].ToString();
                //    classVerification.Grade = dt.Rows[x][12].ToString();
                //    classVerificationList.Add(classVerification);
                //}
                con.Close();
            }
            return result;
        }
        public Exam_MarksVerificationCoScholasticModels GetClassVerificationCoScholasticList(string sessionid, string classid, string sectionid, string examid, string subjectid)
        {
            Exam_MarksVerificationCoScholasticModels model = new Exam_MarksVerificationCoScholasticModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_ReportCard6to8_Part2Verification", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid);
                cmd.CommandTimeout = 0;
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                for(int i =0; i < dt.Columns.Count; i++)
                {
                    model.HeaderNameList.Add(dt.Columns[i].ColumnName);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    List<string> currentRow = new List<string>();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        currentRow.Add(dt.Rows[i][j].ToString());
                    }
                    model.ValueList.Add(currentRow);
                }

                //int count = dt.Rows.Count;
                //for (int x = 0; x < count; x++)
                //{
                //    ClassVerificationCoScholasticList classVerificationCoScholastic = new ClassVerificationCoScholasticList();
                //    classVerificationCoScholastic.ERPNo = dt.Rows[x][0].ToString();
                //    classVerificationCoScholastic.AdmNo = dt.Rows[x][1].ToString();
                //    classVerificationCoScholastic.RollNo = Convert.ToInt32(dt.Rows[x][2].ToString());
                //    classVerificationCoScholastic.Student = dt.Rows[x][3].ToString();
                //    classVerificationCoScholastic.SubjectName = dt.Rows[x][4].ToString();
                //    classVerificationCoScholastic.Assess1Mark = dt.Rows[x][5].ToString();
                //    classVerificationCoScholastic.Assess2Mark = dt.Rows[x][6].ToString();
                //    classVerificationCoScholastic.Assess3Mark = dt.Rows[x][7].ToString();
                //    classVerificationCoScholastic.TotalMark = dt.Rows[x][8].ToString();
                //    classVerificationCoScholastic.Grade = dt.Rows[x][9].ToString();
                //    classVerificationCoScholasticList.Add(classVerificationCoScholastic);
                //}
                con.Close();
            }
            return model;
        }


        public DataSet GradeCard_GradeCard1to2(string classid, string sectionid, string sessionid, string erpno, string examid)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_GradeCard1to2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            return ds;
        }
    }

}

