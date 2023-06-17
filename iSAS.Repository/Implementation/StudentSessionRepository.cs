using System;
using System.Collections.Generic;
using ISas.Entities;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ISas.Repository.Interface;

namespace ISas.Repository.Implementation
{
    public class StudentSessionRepository : IStudentSession
    {
        public IEnumerable<Session> GetAllSessions()
        {
            List<Session> sessions = new List<Session>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllSession", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    Session studentsession = new Session();
                    //submenu submenu = new submenu();
                    studentsession.SessId = dr.GetValue(0).ToString();
                    studentsession.SessionDisplayName = dr.GetValue(1).ToString();
                    studentsession.PrintOrder = Convert.ToInt32( dr.GetValue(2).ToString());
                    studentsession.IsDefault = Convert.ToBoolean(dr.GetValue(3));
                    sessions.Add(studentsession);
                }
                con.Close();
            }
            return sessions;
        }
    }
}
