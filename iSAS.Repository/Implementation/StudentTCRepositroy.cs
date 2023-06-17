using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISas.Entities;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ISas.Repository.Interface;
using System.Web.Mvc;

namespace ISas.Repository.Implementation
{
    public class StudentTCRepositroy : IStudentTC
    {

        public IEnumerable<Student_TC> GetStudentListForTC(string sessionid, string classid, string sectionid, string userid,string erpNo)
        {
            List<Student_TC> studentlistfortc = new List<Student_TC>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Student_TC_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@Mode", "GetClassStudentListwithTC");
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    Student_TC tcstudent = new Student_TC();
                    tcstudent.ERPNo = dt.Rows[x][0].ToString();
                    tcstudent.AdmNo = dt.Rows[x][1].ToString();
                    tcstudent.RollNo = Convert.ToInt32(dt.Rows[x][2].ToString());
                    tcstudent.Student = dt.Rows[x][3].ToString();
                    tcstudent.MotherName = dt.Rows[x][4].ToString();
                    tcstudent.FatherName = dt.Rows[x][5].ToString();
                    tcstudent.Nationality = dt.Rows[x][6].ToString();
                    tcstudent.BelongtoSCST = dt.Rows[x][7].ToString();
                    tcstudent.DOA = dt.Rows[x][8].ToString();
                    tcstudent.AdmissionClass = dt.Rows[x][9].ToString();
                    tcstudent.DOB = dt.Rows[x][10].ToString();
                    tcstudent.PendingDue = Convert.ToInt32(dt.Rows[x][11]);// PendingDue
                    tcstudent.IssueBook = Convert.ToInt32(dt.Rows[x][12]);// PendingDue
                    studentlistfortc.Add(tcstudent);
                }
                con.Close();
            }
            return studentlistfortc;
        }

        public Tuple<int, string> StudentTC_CRUD(Student_TC model)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StringBuilder subjectid = new StringBuilder() ;
                foreach (var item in model.DropDownList._tcSubjectList)
                {
                    if (item.IsSelected)
                    {
                        subjectid.Append(item.SubjectId + ",");
                    }
                }
                subjectid.Remove(subjectid.ToString().LastIndexOf(","), 1);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_TC_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TC_No", model.TCNo);
                cmd.Parameters.AddWithValue("@SessionId", model.Session);
                cmd.Parameters.AddWithValue("@ERPNO", model.ERPNo);
                cmd.Parameters.AddWithValue("@TC_CreationDate", Convert.ToDateTime(model.CreationDate).Date);
                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", model.SectionId);
                cmd.Parameters.AddWithValue("@BelongToSCST", model.BelongtoSCST);
                cmd.Parameters.AddWithValue("@LastClassInFigure", model.LastClassinFigure);
                cmd.Parameters.AddWithValue("@LastClassInWord", model.LastClassinWord);
                cmd.Parameters.AddWithValue("@LastSchoolWithExamResult", model.LastSchoolWithExamResult);
                cmd.Parameters.AddWithValue("@FailedInSameClass", model.FailedinSameClass);
                cmd.Parameters.AddWithValue("@IsQualifiedforHighClass", model.IsQualifiedForHigherClass);
                cmd.Parameters.AddWithValue("@HigherClassId", model.HigherClassinFigure);
                cmd.Parameters.AddWithValue("@HigherClassInWord", model.HigherClassinWord);
                cmd.Parameters.AddWithValue("@MonthForDues", model.MonthForDues);
                cmd.Parameters.AddWithValue("@NatureofConcession", model.NatureOFConcession);
                cmd.Parameters.AddWithValue("@TotalWorkingDay", model.TotalWorkingDays);
                cmd.Parameters.AddWithValue("@WorkingDaysPresent", model.WorkingDaysPresents);
                cmd.Parameters.AddWithValue("@WeitherNCCCadit", model.WeitherNCCCadit);
                cmd.Parameters.AddWithValue("@ExtraCircullarActivity", model.ExtraCurricularActivities);
                cmd.Parameters.AddWithValue("@GeneralConduct", model.GeneralConduct);
                if (model.AppliedDate!=null)
                cmd.Parameters.AddWithValue("@TC_AppliedDate", Convert.ToDateTime(model.AppliedDate).Date);
                else
                cmd.Parameters.AddWithValue("@TC_AppliedDate", "1900-01-01");

                if (model.IssueDate != null)
                    cmd.Parameters.AddWithValue("@TC_IssueDate", Convert.ToDateTime(model.IssueDate).Date);
                else
                    cmd.Parameters.AddWithValue("@TC_IssueDate", "1900-01-01");

                cmd.Parameters.AddWithValue("@TC_Reason", model.ReasonForTC);
                cmd.Parameters.AddWithValue("@TC_Description", model.Description);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid.ToString());
                cmd.Parameters.AddWithValue("@DOBProof", model.DOBProof);
                cmd.Parameters.AddWithValue("@isSchoolUnderGovt", model.isSchoolUnderGovt);
                if (model.struckOffDate != null)
                    cmd.Parameters.AddWithValue("@struckOffDate", Convert.ToDateTime(model.struckOffDate).Date);
                else
                    cmd.Parameters.AddWithValue("@struckOffDate", "1900-01-01");

                cmd.Parameters.AddWithValue("@Mode ", "SAVE");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }
        public Tuple<int, string> StudentTC1_CRUD(Student_TC model)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                StringBuilder subjectid = new StringBuilder();
                foreach (var item in model.DropDownList._tcSubjectList)
                {
                    if (item.IsSelected)
                    {
                        subjectid.Append(item.SubjectId + ",");
                    }
                }
                subjectid.Remove(subjectid.ToString().LastIndexOf(","), 1);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_TC1_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TC_No", model.TCNo);
                cmd.Parameters.AddWithValue("@SessionId", model.Session);
                cmd.Parameters.AddWithValue("@ERPNO", model.ERPNo);
                cmd.Parameters.AddWithValue("@TC_CreationDate", Convert.ToDateTime(model.CreationDate).Date);
                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", model.SectionId);
                cmd.Parameters.AddWithValue("@BelongToSCST", model.BelongtoSCST);
                cmd.Parameters.AddWithValue("@LastClassInFigure", model.LastClassinFigure);
                cmd.Parameters.AddWithValue("@LastClassInWord", model.LastClassinWord);
                cmd.Parameters.AddWithValue("@LastSchoolWithExamResult", model.LastSchoolWithExamResult);
                cmd.Parameters.AddWithValue("@FailedInSameClass", model.FailedinSameClass);
                cmd.Parameters.AddWithValue("@IsQualifiedforHighClass", model.IsQualifiedForHigherClass);
                cmd.Parameters.AddWithValue("@HigherClassId", model.HigherClassinFigure);
                cmd.Parameters.AddWithValue("@HigherClassInWord", model.HigherClassinWord);
                cmd.Parameters.AddWithValue("@MonthForDues", model.MonthForDues);
                cmd.Parameters.AddWithValue("@NatureofConcession", model.NatureOFConcession);
                cmd.Parameters.AddWithValue("@TotalWorkingDay", model.TotalWorkingDays);
                cmd.Parameters.AddWithValue("@WorkingDaysPresent", model.WorkingDaysPresents);
                cmd.Parameters.AddWithValue("@WeitherNCCCadit", model.WeitherNCCCadit);
                cmd.Parameters.AddWithValue("@ExtraCircullarActivity", model.ExtraCurricularActivities);
                cmd.Parameters.AddWithValue("@GeneralConduct", model.GeneralConduct);
                if (model.AppliedDate != null)
                    cmd.Parameters.AddWithValue("@TC_AppliedDate", Convert.ToDateTime(model.AppliedDate).Date);
                else
                    cmd.Parameters.AddWithValue("@TC_AppliedDate", "1900-01-01");
                cmd.Parameters.AddWithValue("@TC_IssueDate", Convert.ToDateTime(model.IssueDate).Date);
                cmd.Parameters.AddWithValue("@TC_Reason", model.ReasonForTC);
                cmd.Parameters.AddWithValue("@TC_Description", model.Description);
                cmd.Parameters.AddWithValue("@SubjectId", subjectid.ToString());
                cmd.Parameters.AddWithValue("@Mode ", "SAVE");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }

        public IEnumerable<Student_TC> Student_TC_LandingPage(string TCID)
        {
            List<Student_TC> studnettcList = new List<Student_TC>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("exec sp_Student_TC_LandingPage '" + TCID + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;    

                for (int x = 0; x < count; x++)
                {
                    Student_TC tcDetails = new Student_TC();
                    tcDetails.Session = ds.Tables[0].Rows[x][0].ToString();
                    tcDetails.TCNo = ds.Tables[0].Rows[x][1].ToString();
                    tcDetails.CreationDate = ds.Tables[0].Rows[x][2].ToString();
                    tcDetails.ERPNo = ds.Tables[0].Rows[x][3].ToString();
                    tcDetails.AdmNo = ds.Tables[0].Rows[x][4].ToString();
                    tcDetails.ClassId = ds.Tables[0].Rows[x][5].ToString();
                    tcDetails.ClassName = ds.Tables[0].Rows[x][6].ToString();
                    tcDetails.SectionId = ds.Tables[0].Rows[x][7].ToString();
                    tcDetails.SectionName = ds.Tables[0].Rows[x][8].ToString();
                    tcDetails.Student = ds.Tables[0].Rows[x][9].ToString();
                    tcDetails.MotherName = ds.Tables[0].Rows[x][10].ToString();
                    tcDetails.FatherName = ds.Tables[0].Rows[x][11].ToString();
                    tcDetails.Nationality = ds.Tables[0].Rows[x][12].ToString();
                    tcDetails.BelongtoSCST = ds.Tables[0].Rows[x][13].ToString();
                    tcDetails.DOA = ds.Tables[0].Rows[x][14].ToString();
                    tcDetails.AdmissionClass = ds.Tables[0].Rows[x][15].ToString();
                    tcDetails.DOB = ds.Tables[0].Rows[x][16].ToString();
                    tcDetails.LastClassinFigure = ds.Tables[0].Rows[x][17].ToString();
                    tcDetails.LastClassinWord = ds.Tables[0].Rows[x][18].ToString();
                    tcDetails.LastSchoolWithExamResult = ds.Tables[0].Rows[x][19].ToString();
                    tcDetails.FailedinSameClass = ds.Tables[0].Rows[x][20].ToString();
                    tcDetails.IsQualifiedForHigherClass =Convert.ToBoolean(ds.Tables[0].Rows[x][21].ToString());
                    tcDetails.HigherClassinFigure = ds.Tables[0].Rows[x][22].ToString();
                    tcDetails.HigherClassinWord = ds.Tables[0].Rows[x][23].ToString();
                    tcDetails.MonthForDues = ds.Tables[0].Rows[x][24].ToString();
                    tcDetails.NatureOFConcession = ds.Tables[0].Rows[x][25].ToString();
                    tcDetails.TotalWorkingDays = ds.Tables[0].Rows[x][26].ToString();
                    tcDetails.WorkingDaysPresents  = ds.Tables[0].Rows[x][27].ToString();
                    tcDetails.WeitherNCCCadit = ds.Tables[0].Rows[x][28].ToString();
                    tcDetails.ExtraCurricularActivities = ds.Tables[0].Rows[x][29].ToString();
                    tcDetails.GeneralConduct = ds.Tables[0].Rows[x][30].ToString();
                    tcDetails.AppliedDate = ds.Tables[0].Rows[x][31].ToString();
                    tcDetails.IssueDate = ds.Tables[0].Rows[x][32].ToString();
                    tcDetails.ReasonForTC = ds.Tables[0].Rows[x][33].ToString();
                    tcDetails.Description = ds.Tables[0].Rows[x][34].ToString();
                    tcDetails.IsTCCancelled = Convert.ToBoolean(ds.Tables[0].Rows[x][35]);
                    tcDetails.DOBProof = ds.Tables[0].Rows[x][36].ToString();
                    tcDetails.isSchoolUnderGovt = ds.Tables[0].Rows[x][37].ToString();
                    tcDetails.struckOffDate= ds.Tables[0].Rows[x][38].ToString();
                    tcDetails.fileKey= ds.Tables[0].Rows[x]["fileKey"].ToString();
                    tcDetails.filePath = ds.Tables[0].Rows[x]["filePath"].ToString();
                    tcDetails.fileName = ds.Tables[0].Rows[x]["fileName"].ToString();
                    studnettcList.Add(tcDetails);
                }
                con.Close();
                
                

            }
            return studnettcList;
        }
        public IEnumerable<Student_TC> Student_TC1_LandingPage(string TCID)
        {
            List<Student_TC> studnettcList = new List<Student_TC>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("exec sp_Student_TC1_LandingPage '" + TCID + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                for (int x = 0; x < count; x++)
                {
                    Student_TC tcDetails = new Student_TC();
                    tcDetails.Session = ds.Tables[0].Rows[x][0].ToString();
                    tcDetails.TCNo = ds.Tables[0].Rows[x][1].ToString();
                    tcDetails.CreationDate = ds.Tables[0].Rows[x][2].ToString();
                    tcDetails.ERPNo = ds.Tables[0].Rows[x][3].ToString();
                    tcDetails.AdmNo = ds.Tables[0].Rows[x][4].ToString();
                    tcDetails.ClassId = ds.Tables[0].Rows[x][5].ToString();
                    tcDetails.ClassName = ds.Tables[0].Rows[x][6].ToString();
                    tcDetails.SectionId = ds.Tables[0].Rows[x][7].ToString();
                    tcDetails.SectionName = ds.Tables[0].Rows[x][8].ToString();
                    tcDetails.Student = ds.Tables[0].Rows[x][9].ToString();
                    tcDetails.MotherName = ds.Tables[0].Rows[x][10].ToString();
                    tcDetails.FatherName = ds.Tables[0].Rows[x][11].ToString();
                    tcDetails.Nationality = ds.Tables[0].Rows[x][12].ToString();
                    tcDetails.BelongtoSCST = ds.Tables[0].Rows[x][13].ToString();
                    tcDetails.DOA = ds.Tables[0].Rows[x][14].ToString();
                    tcDetails.AdmissionClass = ds.Tables[0].Rows[x][15].ToString();
                    tcDetails.DOB = ds.Tables[0].Rows[x][16].ToString();
                    tcDetails.LastClassinFigure = ds.Tables[0].Rows[x][17].ToString();
                    tcDetails.LastClassinWord = ds.Tables[0].Rows[x][18].ToString();
                    tcDetails.LastSchoolWithExamResult = ds.Tables[0].Rows[x][19].ToString();
                    tcDetails.FailedinSameClass = ds.Tables[0].Rows[x][20].ToString();
                    tcDetails.IsQualifiedForHigherClass = Convert.ToBoolean(ds.Tables[0].Rows[x][21].ToString());
                    tcDetails.HigherClassinFigure = ds.Tables[0].Rows[x][22].ToString();
                    tcDetails.HigherClassinWord = ds.Tables[0].Rows[x][23].ToString();
                    tcDetails.MonthForDues = ds.Tables[0].Rows[x][24].ToString();
                    tcDetails.NatureOFConcession = ds.Tables[0].Rows[x][25].ToString();
                    tcDetails.TotalWorkingDays = ds.Tables[0].Rows[x][26].ToString();
                    tcDetails.WorkingDaysPresents = ds.Tables[0].Rows[x][27].ToString();
                    tcDetails.WeitherNCCCadit = ds.Tables[0].Rows[x][28].ToString();
                    tcDetails.ExtraCurricularActivities = ds.Tables[0].Rows[x][29].ToString();
                    tcDetails.GeneralConduct = ds.Tables[0].Rows[x][30].ToString();
                    tcDetails.AppliedDate = ds.Tables[0].Rows[x][31].ToString();
                    tcDetails.IssueDate = ds.Tables[0].Rows[x][32].ToString();
                    tcDetails.ReasonForTC = ds.Tables[0].Rows[x][33].ToString();
                    tcDetails.Description = ds.Tables[0].Rows[x][34].ToString();
                    tcDetails.IsTCCancelled = Convert.ToBoolean(ds.Tables[0].Rows[x][35]);
                    studnettcList.Add(tcDetails);
                }
                con.Close();



            }
            return studnettcList;
        }

        public Student_TC Student_TC_FormLoad()
        {
            //List<Student_TC> studnettcList = new List<Student_TC>();
            Student_TC model = new Student_TC();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("exec sp_Student_NEWTC_FormLoad" , con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    model.LastTCNo = ds.Tables[0].Rows[x][0].ToString();
                    model.TCCount = ds.Tables[0].Rows[x][1].ToString();
                }

                model.DropDownList.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text=r.Field<string>("ClassName"),
                    Value=r.Field<string>("ClassId")

                }).ToList();

                model.DropDownList.SessionList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SessionDisplayName"),
                    Value = r.Field<string>("SessId"),
                    //Selected = r.Field<bool>("Default")
                }).ToList();
                //List<TCSubjectList> subjectlist = new List<TCSubjectList>();
                //int count1 = ds.Tables[3].Rows.Count;
                //for (int x = 0; x < count1; x++)
                //{
                //    TCSubjectList subject = new TCSubjectList();
                //    subject.SubjectId = ds.Tables[3].Rows[x][0].ToString();
                //    subject.SubjectName = ds.Tables[3].Rows[x][1].ToString();
                //    subject.PrintOrder = Convert.ToInt32(ds.Tables[3].Rows[x][2].ToString());
                //    subjectlist.Add(subject);
                //}

                //model.DropDownList._tcSubjectList = subjectlist;

                model.DropDownList.HigherClassOptionList.Add(new SelectListItem { Text = "NO", Value = "false", Selected = false });
                model.DropDownList.HigherClassOptionList.Add(new SelectListItem { Text = "YES", Value = "true", Selected = false });

                con.Close();
            }
            return model;
        }
        public Student_TC Student_TC1_FormLoad()
        {
            //List<Student_TC> studnettcList = new List<Student_TC>();
            Student_TC model = new Student_TC();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("exec sp_Student_NEWTC1_FormLoad", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    model.LastTCNo = ds.Tables[0].Rows[x][0].ToString();
                    model.TCCount = ds.Tables[0].Rows[x][1].ToString();
                }

                model.DropDownList.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassId")

                }).ToList();

                model.DropDownList.SessionList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SessionDisplayName"),
                    Value = r.Field<string>("SessId"),
                    
                }).ToList();
                
                model.DropDownList.HigherClassOptionList.Add(new SelectListItem { Text = "NO", Value = "false", Selected = false });
                model.DropDownList.HigherClassOptionList.Add(new SelectListItem { Text = "YES", Value = "true", Selected = false });

                con.Close();
            }
            return model;
        }

        public IEnumerable<TCSubjectList> GetTCSubjectList(string erpno)
        {

            List<TCSubjectList> subjectlist = new List<TCSubjectList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_GetTCSubjcectList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //SqlDataReader dr = cmd.ExecuteReader();
                //ds.Tables[0].Rows
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    TCSubjectList subject = new TCSubjectList();
                    subject.SubjectId = ds.Tables[0].Rows[x][0].ToString();
                    subject.SubjectName = ds.Tables[0].Rows[x][1].ToString();
                    subject.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][2].ToString());
                    subject.IsSelected = Convert.ToBoolean(ds.Tables[0].Rows[x][3]);
                    subjectlist.Add(subject);
                }
                con.Close();
            }
            return subjectlist;
        }

        public DataSet TC_Certificate(string erpno)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Report_StudentTCCertificate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            return ds;
        }
        public DataSet TC_Certificate1(string erpno)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Report_StudentTCCertificate1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            return ds;
        }

        public Tuple<int, string> StudentTC_CRUD(string TCNo, string SessionId, string ERPNo)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_TC_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TC_No", TCNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@Mode ", "CANCEL");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> StudentTC1_CRUD(string TCNo, string SessionId, string ERPNo)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_TC1_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TC_No", TCNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@Mode ", "CANCEL");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }

        public string getTCNumberByFilter(string admNo, string dob)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_getTCNumberByFilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@admNo", admNo);
                    cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dob));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    return dt.Rows[0][0].ToString();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
