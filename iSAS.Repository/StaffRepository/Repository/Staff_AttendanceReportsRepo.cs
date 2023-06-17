﻿using ISas.Entities.StaffEntities;
using ISas.Repository.StaffRepository.IRepository;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;

namespace ISas.Repository.StaffRepository.Repository
{
    public class Staff_AttendanceReportsRepo : IStaff_AttendanceReportsRepo
    {
        public Staff_AttendanceReportsModels Staff_AttendanceReports_FormLoad()
        {
            Staff_AttendanceReportsModels model = new Staff_AttendanceReportsModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModuleName", "Staff Attendance");
                cmd.Parameters.AddWithValue("@ReportType", "Detail");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.ReportNameList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ReportDisplayName"),
                    Value = r.Field<string>("ReportName")
                }).ToList();

                model.DepartmentList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DeptName"),
                    Value = r.Field<string>("DeptID")
                }).ToList();

                model.StaffList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("StaffId"),
                    Text = r.Field<string>("StaffName")
                }).ToList();
            }
            return model;
        }

        public Staff_AttendanceReportsModels GetStaff_AttendanceReports(string StaffIds, string Reporttype, bool InActive, string OrderBy, string ReportName, string ReportFilterBy, string FromDate, string ToDate)
        {
            Staff_AttendanceReportsModels model = new Staff_AttendanceReportsModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StaffAttendance_Report", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if(!string.IsNullOrEmpty(FromDate))
                cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(FromDate).Date);

                if (!string.IsNullOrEmpty(ToDate))
                    cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(ToDate).Date);

                cmd.Parameters.AddWithValue("@StaffId", StaffIds);
                cmd.Parameters.AddWithValue("@Reporttype", Reporttype);
                cmd.Parameters.AddWithValue("@InActive", InActive);
                cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
                cmd.Parameters.AddWithValue("@Mode", ReportFilterBy);
                cmd.Parameters.AddWithValue("@ReportName", ReportName);

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
            }
            return model;
        }
    }
}
