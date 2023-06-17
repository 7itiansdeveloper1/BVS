using ISas.Entities.UserEntities;
using ISas.Repository.UserRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.UserRepo.Repository
{
    public class UserPermissionRepo : IUserPermissionRepo
    {
        public List<SelectListItem> ModuleList()
        {
            List<SelectListItem> moduleList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UserRoleManagement_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", "FormLoad");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                moduleList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ModuleName"),
                    Value = r.Field<Int16>("ModuleId").ToString()
                }).ToList();
            }
            return moduleList;
        }

        public UserPermissionModels ModuleRoleList(string ModuleID, string UserRefID)
        {
            UserPermissionModels model = new UserPermissionModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UserRoleManagement_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", "GetModuleRoleList");
                cmd.Parameters.AddWithValue("@ModuleId", ModuleID);
                cmd.Parameters.AddWithValue("@UserReferenceNo", UserRefID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.ModuleRoleList = ds.Tables[0].AsEnumerable().Select(r => new ModuleRoleDetailModel
                {
                    canDELETE = r.Field<bool>("canDELETE"),
                    canSAVE = r.Field<bool>("canSAVE"),
                    IsActive = r.Field<bool>("IsActive"),
                    canUPDATE = r.Field<bool>("canUPDATE"),
                    canVIEW = r.Field<bool>("canVIEW"),
                    MenuName = r.Field<string>("MenuName"),
                    RoleID = r.Field<string>("RoleID"),
                }).ToList();
            }
            return model;
        }

        public string UserPermission_CRUD(UserPermissionModels model)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string StrSAVERole = ""; string StrDELRole = ""; string StrUPDRole = ""; string StrVIWRole = "";

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UserRoleCreation_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserReferenceNo", model.UserReferenceNo);
                cmd.Parameters.AddWithValue("@UserType", model.UserType);
                cmd.Parameters.AddWithValue("@ModuleId", model.ModuleID);
                
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@Mode", model.Mode);

              
                StrSAVERole = string.Join(",", model.ModuleRoleList.Select(r => r.RoleID + "_" + r.canSAVE));
                StrDELRole = string.Join(",", model.ModuleRoleList.Select(r => r.RoleID + "_" + r.canDELETE));
                StrUPDRole = string.Join(",", model.ModuleRoleList.Select(r => r.RoleID + "_" + r.canUPDATE));
                StrVIWRole = string.Join(",", model.ModuleRoleList.Select(r => r.RoleID + "_" + r.canVIEW));

                cmd.Parameters.AddWithValue("@UseSAVERole", StrSAVERole);
                cmd.Parameters.AddWithValue("@UseDELRole", StrDELRole);
                cmd.Parameters.AddWithValue("@UseUPDRole", StrUPDRole);
                cmd.Parameters.AddWithValue("@UseVIEWRole", StrVIWRole);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                message = dt.Rows[0][1].ToString();
                return message;
            }
        }
        public Tuple<int, string> UserRoleMaster_CRUD(string roleId,bool isActive,string userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UserRoleMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RoleId", roleId);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
