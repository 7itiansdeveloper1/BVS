using ISas.Entities.TimeTable_Entities;
using ISas.Repository.TimeTable_Repo.IRepository;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.TimeTable_Repo.Repository
{
    public class TimeTable_StaffWorkLoadSetupRepo : ITimeTable_StaffWorkLoadSetupRepo
    {
        public StaffClassSetupModels StaffClassSetup_FormLoad(string StaffId)
        {
            StaffClassSetupModels model = new StaffClassSetupModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_Staff_ClassSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StaffId", StaffId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.ClassList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("FullClassName"),
                    Value = r.Field<string>("ClassSectionId"),
                    Selected = r.Field<bool>("Selected"),
                }).ToList();
                model.StaffId = StaffId;
            }
            return model;
        }
        public Tuple<int, string> StaffClassSetup_CRUD(StaffClassSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_Staff_ClassSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", model.StaffId);
                cmd.Parameters.AddWithValue("@ClassSecId", string.Join(",", model.ClassList.Where(r => r.Selected).Select(r => r.Value).ToList()));
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        #region Staff Subject Setup
        public StaffSubjectSetupModels StaffSubjectSetup_FormLoad(string StaffId, string ClassSecId)
        {
            StaffSubjectSetupModels model = new StaffSubjectSetupModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_Staff_SubjectSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StaffId", StaffId);
                cmd.Parameters.AddWithValue("@ClassSecId", ClassSecId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.SubjectList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId"),
                    Selected = r.Field<bool>("Selected"),
                }).ToList();
                model.StaffId = StaffId;
            }
            return model;
        }
        public Tuple<int, string> StaffSubjectClassSetup_CRUD(StaffSubjectSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_Staff_SubjectSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", model.StaffId);
                cmd.Parameters.AddWithValue("@ClassSecId", model.ClassSecId);
                cmd.Parameters.AddWithValue("@SubjectId", string.Join(",", model.SubjectList.Where(r => r.Selected).Select(r => r.Value).ToList()));
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #endregion


        #region Exam Subject Setup
        public StaffSubjectSetupModels ExamSubjectSetup_FormLoad(string StaffId, string ClassSecId)
        {
            StaffSubjectSetupModels model = new StaffSubjectSetupModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_Staff_SubjectSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StaffId", StaffId);
                cmd.Parameters.AddWithValue("@ClassSecId", ClassSecId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.SubjectList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId"),
                    Selected = r.Field<bool>("Selected"),
                }).ToList();
                model.StaffId = StaffId;
            }
            return model;
        }
        public Tuple<int, string> ExamSubjectClassSetup_CRUD(StaffSubjectSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_Staff_SubjectSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", model.StaffId);
                cmd.Parameters.AddWithValue("@ClassSecId", model.ClassSecId);
                cmd.Parameters.AddWithValue("@SubjectId", string.Join(",", model.SubjectList.Where(r => r.Selected).Select(r => r.Value).ToList()));
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #endregion
    }
}
