using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using ISas.Entities;
using ISas.Repository.Interface;

namespace ISas.Repository.Implementation
{
    public class LoginData : ILoginData
    {
        public DataTable CheckUserLogin(string username, string password)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Login_CheckValidUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@UserPwd", password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
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
                    studentsession.PrintOrder = Convert.ToInt32(dr.GetValue(2).ToString());
                    sessions.Add(studentsession);
                }
                con.Close();
            }
            return sessions;
        }
        public string GetRoleByUserID(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserId", UserId);
                return con.Query<string>("Usp_getRoleByUserID", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public Tuple<string, string, string,string,string> GetUserID_By_UserName(string UserName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("Usp_UserIDbyUserName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@UserName", UserName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string userid = dt.Rows[0][0].ToString();
                string displayname = dt.Rows[0][1].ToString();
                string displayImage = dt.Rows[0][2].ToString();
                string displaytitle = dt.Rows[0][3].ToString();
                string displayClass = dt.Rows[0][4].ToString();
                return new Tuple<string, string, string,string,string>(userid, displayname, displayImage,displaytitle,displayClass);
            }
        }

        //public IEnumerable<Session> GetAllSession()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
        //    {
        //        return con.Query<Session>("sp_Login_FormLoad", null, null, true, 0, CommandType.StoredProcedure).ToList();
        //    }
        //}
        //public IEnumerable<Role> GetAllRoles()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        return con.Query<Role>("sp_Login_GetAllRole", null, null, true, 0, CommandType.StoredProcedure).ToList();
        //    }
        //}
        public IEnumerable<Module> GetAllModule(int userid)
        {
            List<Module> modules = new List<Module>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Login_GetAllModule", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Module module = new Module();
                    module.ModuleName = dr["ModuleName"].ToString();
                    modules.Add(module);
                }
                con.Close();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dt);

                //var para = new DynamicParameters();
                //para.Add("@UserId", userid);
                //return con.Query<Module>("sp_Login_GetAllModule", null, null, true, 0, CommandType.StoredProcedure).ToList();



            }
            return modules;


        }
        //public IEnumerable<submenu> GetAllSubMenu(int userid,string modulename)
        public IEnumerable<Role > GetAllRoles(int userid)
        {
            List<Role> Roles = new List<Role>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Login_GetAllRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userid);
                //cmd.Parameters.AddWithValue("@ModuleName", modulename);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    Role role = new Role();
                    //submenu submenu = new submenu();
                    role.RoleId = dr.GetValue(0).ToString();
                    role.RoleName = dr.GetValue(1).ToString();
                    role.DisplayRoleName = dr.GetValue(2).ToString();
                    role.Module = new Module();
                    role.Module.ModuleId = dr.GetValue(3).ToString();
                    role.Module.ModuleName = dr.GetValue(4).ToString();
                    role.Module.DisplayOrder = Convert.ToInt32(dr.GetValue(5));
                    //role.Module.ModuleIcon = dr.GetValue(6);
                    role.Active = Convert.ToBoolean(dr.GetValue(7));
                    role.PrintOrder = Convert.ToInt32(dr.GetValue(8));
                    role.CreatedBy = dr.GetValue(9).ToString();
                    role.CreatedDate=Convert.ToDateTime(dr.GetValue(10));
                    role.ModifiedBy = dr.GetValue(11).ToString();
                    role.ModifiedDate = Convert.ToDateTime(dr.GetValue(12));
                    role.Controller = dr.GetValue(13).ToString();
                    role.Action = dr.GetValue(14).ToString();
                    //role.Parameter = dr.GetValue(15).ToString();
                    role.Module.IconName = dr.GetValue(15).ToString();
                    Roles.Add(role);
                }
                con.Close();
            }
            return Roles;
        }

    }
}
