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
    public class Student_AttendanceReportRepo : IStudent_AttendanceReportRepo
    {
        public Student_AttendanceReportModels Student_AttendanceReport_FormLoad(string ModuleName, string ReportName)
        {
            Student_AttendanceReportModels model = new Student_AttendanceReportModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModuleName", ModuleName);
                cmd.Parameters.AddWithValue("@ReportType", ReportName);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.ClassSectionList = ds.Tables[0].AsEnumerable().Select(r => new ClassSectionModel
                {
                    ClassSectionId = r.Field<string>("ClassSectionId"),
                    ClassSectionName = r.Field<string>("ClassNameWithSection"),
                    WingId = r.Field<string>("WingId"),
                    ClassName = r.Field<string>("ClassName"),
                }).ToList();

                model.ReportNameList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("ReportName"),
                    Text = r.Field<string>("ReportDisplayName"),
                }).ToList();
            }
            return model;
        }

        public Student_AttendanceReportModels Student_AttendanceReport(string fromdate, string todate, string ClassSectionId, string InActive, string ReportName, string SessionId, string StudentId)
        {
            Student_AttendanceReportModels model = new Student_AttendanceReportModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_Reports", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(fromdate))
                    cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(fromdate).Date);

                if (!string.IsNullOrEmpty(todate))
                    cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(todate).Date);

                if (string.IsNullOrEmpty(ClassSectionId))
                    ClassSectionId = null;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ReportName", ReportName);
                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                cmd.Parameters.AddWithValue("@InActive", InActive);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    model.HeaderNameList.Add(ds.Tables[0].Columns[i].ColumnName);
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    List<string> rowVal = new List<string>();
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        rowVal.Add(ds.Tables[0].Rows[i][j].ToString());
                    }
                    model.ValueList.Add(rowVal);
                }
                model.SelectedReportName = ds.Tables[1].Rows[0][0].ToString();
            }
            return model;
        }


        //public Student_AttendanceReportModels Student_AttendanceDetailReport(string fromdate, string todate, string ClassSectionId, bool InActive, string ReportName, string SessionId, string StudentId)
        //{
        //    Student_AttendanceReportModels model = new Student_AttendanceReportModels();
        //    DataSet ds = new DataSet();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_StudentAttendance_Reports", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        if (!string.IsNullOrEmpty(fromdate))
        //            cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(fromdate).Date);

        //        if (!string.IsNullOrEmpty(todate))
        //            cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(todate).Date);


        //        cmd.Parameters.AddWithValue("@SessionId", SessionId);
        //        cmd.Parameters.AddWithValue("@ReportName", ReportName);
        //        cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
        //        cmd.Parameters.AddWithValue("@InActive", InActive);
        //        cmd.Parameters.AddWithValue("@StudentId", StudentId);


        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        con.Close();

        //        model.AttnDetailReportList = ds.Tables[0].AsEnumerable().Select(r => new Stud_AttnReportDetailModel
        //        {
        //             AdmNo = r.Field<string>("AdmNo"),
        //             Attendance = r.Field<string>("Attendance Status"),
        //             ERPNo = r.Field<string>("ERPNo"),
        //             RollNo = r.Field<int>("RollNo"),
        //             Student  = r.Field<string>("Student"),
        //        }).ToList();
        //        model.SelectedReportName = ds.Tables[1].Rows[0][0].ToString();
        //    }
        //    return model;
        //}
    }
}
