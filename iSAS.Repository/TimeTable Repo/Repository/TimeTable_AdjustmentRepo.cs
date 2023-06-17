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
    public class TimeTable_AdjustmentRepo : ITimeTable_AdjustmentRepo
    {

        public TimeTable_Adjustment_FormLoadModel TimeTable_Adjustment_FormLoad()
        {
            DataTable dt = new DataTable();
            TimeTable_Adjustment_FormLoadModel model = new TimeTable_Adjustment_FormLoadModel();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_Adjustment_Transaction_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                model.wingList = dt.AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("WingName"),
                    Value = r.Field<string>("WingID"),
                    Selected= r.Field<bool>("IsSelected"),

                }).ToList();
                model.AdjustmentDate=DateTime.Now.Date.ToShortDateString().Replace("-", "/");
            }
            return model;
        }

        public TimeTable_Adjustment_FormLoadModel TimeTable_Adjustment_FormLoad(string Date)
        {
            TimeTable_Adjustment_FormLoadModel model = new TimeTable_Adjustment_FormLoadModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_Adjustment_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(Date))
                    cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date).Date);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.AbsentTeacherList = ds.Tables[0].AsEnumerable().Select(r => new Adjustment_TeacherDetailModel
                {
                    NoofPeriod = r.Field<int>("NoofPeriod"),
                    StaffId = r.Field<string>("StaffId"),
                    StaffName = r.Field<string>("Staff Name"),

                }).ToList();
                model.AdjustmentTeacherList = ds.Tables[1].AsEnumerable().Select(r => new Adjustment_TeacherDetailModel
                {
                    AdjustPeriod = r.Field<string>("AdjustPeriod"),
                    NoofAdjustPeriod = r.Field<int>("NoofAdjustPeriod"),

                    StaffId = r.Field<string>("StaffId"),
                    StaffName = r.Field<string>("Staff Name"),
                }).ToList();
            }
            return model;
        }

        public List<SelectListItem> GetEffectedClassList(string Date, string TeacherId)
        {
            List<SelectListItem> feectedClassList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_Adjustment_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(Date))
                    cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date).Date);

                cmd.Parameters.AddWithValue("@TeacherId", TeacherId);
                cmd.Parameters.AddWithValue("@Mode", "GetStaffImpactClasses");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                feectedClassList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("FullClassName"),
                    Value = r.Field<string>("ClassSectionId"),
                }).ToList();
            }
            return feectedClassList;
        }

        public TimeTable_Adjustment_FormLoadModel DayAdjustment(string AdjustmentDate,string wingid,string reportName)
        {
            TimeTable_Adjustment_FormLoadModel model = new TimeTable_Adjustment_FormLoadModel();
            model.ReportHeader.reportDisplayName = reportName;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Timetable_DayAdjustment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdjustmentDate", Convert.ToDateTime(AdjustmentDate).Date);
                cmd.Parameters.AddWithValue("@wingId", wingid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                model.DayAdjustmentList = ds.Tables[0].AsEnumerable().Select(r => new DayAdjustment
                {
                    Sno= r.Field<int>("Sno"),
                    StaffId = r.Field<string>("StaffId"),
                    StaffName = r.Field<string>("StaffName"),
                    SubjectName = r.Field<string>("SubjectName"),
                    PeriodDay = r.Field<string>("PeriodDay"),
                    PeriodName = r.Field<string>("PeriodName"),
                    ClassName = r.Field<string>("ClassName"),
                    PossibleAdjustment = r.Field<string>("PossibleAdjustment"),
                }).ToList();
            }
            return model;
        }

        public TimeTable_Adjustment_TransactionModel GetEffectedPeriodWithAvailableStaff(string TeacherId, string Date, string ClassSecId)
        {
            TimeTable_Adjustment_TransactionModel model = new TimeTable_Adjustment_TransactionModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_Adjustment_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(Date))
                    cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date).Date);
                cmd.Parameters.AddWithValue("@TeacherId", TeacherId);
                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSecId);
                cmd.Parameters.AddWithValue("@PeriodName", null);
                cmd.Parameters.AddWithValue("@Mode", "GetStaffImpactPeriods&AvailableStaff");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.EffectedPeriodList = ds.Tables[0].AsEnumerable().Select(r => new Adjustment_PeriodDetailModel
                {
                    PeriodName = r.Field<string>("PeriodName"),
                    PeriodWithTime = r.Field<string>("PeriodWithTime"),
                    SubjectName = r.Field<string>("SubjectName"),
                    TeacherId = r.Field<string>("StaffId"),
                    TeacherName = r.Field<string>("StaffName"),
                }).ToList();
                model.AvailableTeacherList = ds.Tables[1].AsEnumerable().Select(r => new Adjustment_PeriodDetailModel
                {
                    PeriodName = r.Field<string>("PeriodName"),
                    TeacherId = r.Field<string>("StaffId"),
                    TeacherName = r.Field<string>("FreeTeachers"),
                }).ToList();
            }
            return model;
        }

        public Tuple<int, string> TimeTable_Adjustment_CRUD(TimeTable_Adjustment_TransactionModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_AdjustmentMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(model.AbsentDate))
                    cmd.Parameters.AddWithValue("@AdjustmentDate", Convert.ToDateTime(model.AbsentDate).Date);
                cmd.Parameters.AddWithValue("@ClassSectionId", model.EffectedClassId);
                cmd.Parameters.AddWithValue("@PeriodAndStaffId", string.Join(",", model.EffectedPeriodList.Where(r=> !string.IsNullOrEmpty(r.TeacherId)).Select(r=> r.PeriodName + r.TeacherId)));
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
