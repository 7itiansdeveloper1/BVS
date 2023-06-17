using System;
using System.Collections.Generic;
using ISas.Entities;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ISas.Repository.Interface;

namespace ISas.Repository.Implementation
{
    public class StudentSectionRepository : IStudentSection
    {
        public IEnumerable<StudentSection> GetAllSections(string studentclass,string userid)
        {
            List<StudentSection> sections = new List<StudentSection>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_GetClassWiseSection", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassID", studentclass);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    StudentSection studentsection = new StudentSection();
                    //submenu submenu = new submenu();
                    studentsection.SecId = dr.GetValue(0).ToString();
                    studentsection.SecName = dr.GetValue(1).ToString();
                    studentsection.PrintOrder = Convert.ToInt32(dr.GetValue(2).ToString());
                    sections.Add(studentsection);
                }
                con.Close();
            }
            return sections;
        }
    }
}
