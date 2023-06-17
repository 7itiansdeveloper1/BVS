using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.UserEntities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using ISas.Repository.UserRepo.IRepository;

namespace ISas.Repository.UserRepo.Repository
{
    public class StudentPermissionRepo: IStudentPermissionRepo
    {
        public StudentRoleModels StudentRoleAssign_Transaction_GetRoleList(string classId)
        {
            StudentRoleModels studentRoleModels = new StudentRoleModels();
            using ( SqlConnection con=new SqlConnection( ConfigurationManager.ConnectionStrings["iSASDB"].ToString() )) {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentRoleAssign_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@Mode", "GetRoleList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if(dt.Rows.Count>0)
                {

                    studentRoleModels.studentRoleAssignList = dt.AsEnumerable().Select(r => new StudentRoleAssignList
                    {
                        isSelected=r.Field<Boolean>("isSelected"),
                        roleId = r.Field<string>("roleId"),
                        displayRoleName = r.Field<string>("displayRoleName"),
                        displayOrder = r.Field<int>("displayOrder")
                    }).ToList();
                }
            }
            return studentRoleModels;
        }

        public StudentRoleModels StudentRoleAssign_Transaction_FormLoad()
        {
            StudentRoleModels studentRoleModels  = new StudentRoleModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentRoleAssign_Transaction",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    studentRoleModels.classList = dt.AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("className"),
                        Value = r.Field<string>("classId").ToString()
                    }).ToList();
                }
            }
            return studentRoleModels;
        }

        public string StudentRoleAssign_CRUD(StudentRoleModels model)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string RoleIds = ""; 
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentRoleAssign_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", model.classId);
                cmd.Parameters.AddWithValue("@UserId", model.userId);
                RoleIds = string.Join(",", model.studentRoleAssignList.Select(r => r.roleId + "_" +  r.isSelected));
                cmd.Parameters.AddWithValue("@RolesId", RoleIds);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                message = dt.Rows[0][1].ToString();
                return message;
            }
        }
    }
}
