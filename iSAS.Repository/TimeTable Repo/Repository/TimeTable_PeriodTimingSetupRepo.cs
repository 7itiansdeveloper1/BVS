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
    public class TimeTable_PeriodTimingSetupRepo : ITimeTable_PeriodTimingSetupRepo
    {
        public List<SelectListItem> GetSeasonList()
        {
            List<SelectListItem> seasonList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_PeriodTimings_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", "FormLoad");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                seasonList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SeasonName"),
                    Value = r.Field<string>("PeriodSeasonId"),
                }).ToList();
            }
            return seasonList;
        }


        public TimeTable_PeriodTimingSetupModels GetPeriodTimingDetails(string ClassSectionId, string SeasonId)
        {
            TimeTable_PeriodTimingSetupModels model = new TimeTable_PeriodTimingSetupModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_PeriodTimings_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                cmd.Parameters.AddWithValue("@SeasonId", SeasonId);
                cmd.Parameters.AddWithValue("@Mode", "ClassSeasonTiming");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.PeriodTimingList = ds.Tables[0].AsEnumerable().Select(r => new PeriodTimingDetailModels
                {
                    EndTime = r.Field<string>("PeriodEndTime"),
                    PeriodName = r.Field<string>("Period"),
                    StartTime = r.Field<string>("PeriodStartTime"),
                }).ToList();

                model.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassSectionId"),
                }).ToList();
            }
            return model;

        }

        public Tuple<int, string> TimeTable_PeriodTimingSetup_CRUD(TimeTable_PeriodTimingSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string timingStr = "";
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_PeriodTimings_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                timingStr = string.Join(",", model.PeriodTimingList.Where(r => !string.IsNullOrEmpty(r.StartTime) && !string.IsNullOrEmpty(r.EndTime)).Select(r => r.PeriodName + "_" + r.StartTime + "_" + r.EndTime).ToList());

                cmd.Parameters.AddWithValue("@ClassSectionId", model.ClassSectionId);
                cmd.Parameters.AddWithValue("@SeasonId", model.SeasonId);
                cmd.Parameters.AddWithValue("@Timging", timingStr);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public Tuple<int, string> TimeTable_PeriodTimingSetup_CRUD(string SeasonId, string FromClassSecId, string ToClassSecId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_PeriodTimings_Copy", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SeasonId", SeasonId);
                cmd.Parameters.AddWithValue("@FromClassSectionId", FromClassSecId);
                cmd.Parameters.AddWithValue("@ToClassSectionId", ToClassSecId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
