using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class ToDo_TaskRepo : IToDo_TaskRepo
    {
        public List<ToDo_TaskEntitiesModel> GetToDo_TaskList(int Id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Dashboard_ToDoList_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ToDoId", Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new ToDo_TaskEntitiesModel
                {
                    ToDoDate = r.Field<string>("toDoDate"),
                    ToDoDescription = r.Field<string>("ToDoDescription"),
                    ToDoId = r.Field<int>("Id"),
                    ToDoSubject = r.Field<string>("ToDoSubject"),
                    ReferenceId = r.Field<string>("ReferenceId"),
                }).ToList();
            }
        }

        public ToDo_TaskEntitiesModel GetToDo_TaskById(int Id)
        {
            return GetToDo_TaskList(Id).FirstOrDefault();
        }

        public Tuple<int, string> ToDo_Task_CRUD(ToDo_TaskEntitiesModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Dashboard_ToDoList_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ToDoId", model.ToDoId);
                cmd.Parameters.AddWithValue("@ReferenceId", model.ReferenceId);
                if (!string.IsNullOrEmpty(model.ToDoDate))
                    cmd.Parameters.AddWithValue("@ToDoDate", Convert.ToDateTime(model.ToDoDate).Date);

                cmd.Parameters.AddWithValue("@ToDoSubject", model.ToDoSubject);
                cmd.Parameters.AddWithValue("@ToDoDescription", model.ToDoDescription);

                //cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> ToDo_Task_CRUD(int ToDoID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Dashboard_ToDoList_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ToDoId", ToDoID);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
