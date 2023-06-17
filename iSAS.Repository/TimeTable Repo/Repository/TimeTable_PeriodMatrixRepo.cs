using ISas.Entities.TimeTable_Entities;
using ISas.Repository.TimeTable_Repo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.TimeTable_Repo.Repository
{
    public class TimeTable_PeriodMatrixRepo : ITimeTable_PeriodMatrixRepo
    {
        public List<TimeTable_PeriodMatrixModels> GetMatrixList(string MatrixId)
        {
            List<TimeTable_PeriodMatrixModels> matrixList = new List<TimeTable_PeriodMatrixModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_PeriodMatrixMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MatrixId", MatrixId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                matrixList = ds.Tables[0].AsEnumerable().Select(r => new TimeTable_PeriodMatrixModels
                {
                    MatrixId  = r.Field<string>("MatrixId"),
                    MatrixName   = r.Field<string>("MatrixName"),
                    Classes = r.Field<string>("Classes"),
                    IsActive = r.Field<bool>("IsActive"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    PeriodMatrix  = r.Field<string>("PeriodMatrix"),
                    NoOfDays= r.Field<int>("NoOfDays"),
                    NoOfPeriod = r.Field<int>("NoOfPeriod"),
                    saturdayDayNo = r.Field<int>("saturdayDayNo"),

                }).ToList();
            }
            return matrixList;
        }

        public TimeTable_PeriodMatrixModels GetMatrixById(string MatrixId)
        {
            return GetMatrixList(MatrixId).FirstOrDefault();
        }

        public Tuple<int, string> TimeTable_PeriodMatrix_CRUD(TimeTable_PeriodMatrixModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_PeriodMatrixMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MatrixId", model.MatrixId);
                cmd.Parameters.AddWithValue("@MatrixName", model.MatrixName);
                cmd.Parameters.AddWithValue("@NoofPeriod", model.NoOfPeriod);
                cmd.Parameters.AddWithValue("@NoofDays", model.NoOfDays);
                cmd.Parameters.AddWithValue("@saturdayDayNo", model.saturdayDayNo);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<int, string> TimeTable_PeriodMatrix_CRUD(string MatrixId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_PeriodMatrixMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MatrixId", MatrixId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }



        public PeriodMatrixClassSetupModels PeriodMatrixClassSetup_FormLoad(string MatrixId)
        {
            PeriodMatrixClassSetupModels model = new PeriodMatrixClassSetupModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_PeriodMatrixClassSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MatrixId", MatrixId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.ClassList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("FullClassName"),
                    Value = r.Field<string>("ClassSectionId"),
                    Selected = r.Field<bool>("Selected"),
                }).ToList();
                model.MatrixId = MatrixId;
            }
            return model;
        }
        public Tuple<int, string> PeriodMatrixClassSetup_CRUD(PeriodMatrixClassSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_PeriodMatrixClassSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MatrixId", model.MatrixId);
                cmd.Parameters.AddWithValue("@ClassSecId",string.Join(",", model.ClassList.Where(r=> r.Selected).Select(r=> r.Value).ToList()));
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
