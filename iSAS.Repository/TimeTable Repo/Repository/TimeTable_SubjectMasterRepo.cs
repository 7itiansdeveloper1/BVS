using ISas.Entities.TimeTable_Entities;
using ISas.Repository.TimeTable_Repo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.TimeTable_Repo.Repository
{
    public class TimeTable_SubjectMasterRepo: ITimeTable_SubjectMasterRepo
    {

        public TimeTable_SubjectMasterModel TimeTable_SubjectMaster_Transaction()
        {
            TimeTable_SubjectMasterModel model = new TimeTable_SubjectMasterModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_SubjectMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                model.timetablesubjectList = ds.Tables[0].AsEnumerable().Select(r => new timetable_subject
                {
                    SubjectId = r.Field<string>("SubjectId"),
                    SubjectName = r.Field<string>("SubjectName"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                    IsActive = r.Field<bool>("IsActive"),
                    groupName = r.Field<string>("groupName"),
                    subjectTeacher = r.Field<int>("subjectTeacher"),
                    timeTableClass = r.Field<int>("timeTableClass")


                }).ToList();
            }
            return model;
        }

        public TimeTable_SubjectMasterModel TimeTable_SubjectMaster_Transaction(string subjectId)
        {
            TimeTable_SubjectMasterModel model = new TimeTable_SubjectMasterModel();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_SubjectMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                
                if (dt.Rows.Count>0)
                {
                    timetable_subject subject = new timetable_subject();
                    subject.SubjectId = dt.Rows[0]["SubjectId"].ToString();
                    subject.SubjectName = dt.Rows[0]["SubjectName"].ToString();
                    subject.PrintOrder = Convert.ToInt32(dt.Rows[0]["PrintOrder"]);
                    subject.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    subject.subjectTeacher = Convert.ToInt32(dt.Rows[0]["subjectTeacher"]);
                    subject.timeTableClass = Convert.ToInt32(dt.Rows[0]["timeTableClass"]);

                    model.timeTableSubject = subject;
                }
                
            }
            return model;
        }
        
        public Tuple<int, string> TimeTable_SubjectMaster_CRUD(TimeTable_SubjectMasterModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_SubjectMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubjectId", model.timeTableSubject.SubjectId);
                cmd.Parameters.AddWithValue("@SubjectName", model.timeTableSubject.SubjectName);
                cmd.Parameters.AddWithValue("@PrintOrder", model.timeTableSubject.PrintOrder);
                cmd.Parameters.AddWithValue("@IsActive", model.timeTableSubject.IsActive);
                cmd.Parameters.AddWithValue("@UserId", model.timeTableSubject.userId);
                cmd.Parameters.AddWithValue("@groupName", model.timeTableSubject.groupName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

    }
}
