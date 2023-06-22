using System;
using System.Collections.Generic;
using System.Data;
using ISas.Entities;

namespace ISas.Repository.Interface
{
    public interface ILoginData
    {
        IEnumerable<Role> GetAllRoles(int userid);

        IEnumerable<Module> GetAllModule(int userid);
        //IEnumerable<Session> GetAllSession();
        DataTable CheckUserLogin(string username, string password);
        //DataTable CheckUserLogin();
        string GetRoleByUserID(string UserId);
        Tuple<string, string, string,string,string> GetUserID_By_UserName(string UserName);
        IEnumerable<Session> GetAllSessions();
        //string Get_checkUsernameExits(string username);
        //bool Get_CheckUserRoles(string UserId);
        //string GetUserName_BY_UserID(string UserId);
        ////IEnumerable<AllroleandUser> DisplayAllUser_And_Roles();
    }
}
