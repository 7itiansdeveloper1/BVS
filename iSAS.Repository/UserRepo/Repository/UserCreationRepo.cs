using ISas.Entities.UserEntities;
using ISas.Repository.UserRepo.IRepository;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.UserRepo.Repository
{
    public class UserCreationRepo : IUserCreationRepo
    {
        public UserCreationModels UserCreation_FormLoad(string UserType)
        {
            UserCreationModels model = new UserCreationModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UserManagement_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", UserType);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.UserRoleList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("URoleName"),
                    Value = r.Field<int>("URoleID").ToString()
                }).ToList();

                if (ds.Tables.Count>1)
                {
                    model.RegisteredStaffOrStudent = ds.Tables[1].AsEnumerable().Select(r => new UserDetailsModels
                    {
                        DisplayName = r.Field<string>("DisplayName"),
                        IsAlreadyUser = r.Field<bool>("IsAlreadyUser"),
                        StaffOrStudent = r.Field<string>("Staff/Student"),
                        StaffOrStudentId = r.Field<string>("Staff/StudentId"),
                        UserId = r.Field<string>("UserId"),
                        UserMobileNo = r.Field<string>("UserMobileNo"),
                        UserName = r.Field<string>("UserName"),
                        UserRecoveryEmail = r.Field<string>("UserRecoveryEmail"),
                    }).ToList();
                }

                model.Not_RegisteredStaffOrStudent = model.RegisteredStaffOrStudent.Where(r => !r.IsAlreadyUser).ToList();
                model.RegisteredStaffOrStudent = model.RegisteredStaffOrStudent.Where(r => r.IsAlreadyUser).ToList();
                model.UserType = UserType;
            }
            return model;
        }

        //public Tuple<int, string> UserCreation_CRUD(UserCreationModels model)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_UserCreation_CRUD", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@UserReferenceNo", model.SelectedUserOrStaffID);
        //        cmd.Parameters.AddWithValue("@UserType", model.UserType);
        //        cmd.Parameters.AddWithValue("@UserAuthority", model.RoleID);
        //        cmd.Parameters.AddWithValue("@UserId", model.UserId);
        //        cmd.Parameters.AddWithValue("@Mode", model.Mode);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
        //    }
        //}
        public Tuple<int, string> UserCreation_CRUD(string userReferenceNo,string userType,string roleId,string userId,string mode )
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UserCreation_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserReferenceNo", userReferenceNo);
                cmd.Parameters.AddWithValue("@UserType", userType);
                cmd.Parameters.AddWithValue("@UserAuthority", roleId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Mode", mode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
