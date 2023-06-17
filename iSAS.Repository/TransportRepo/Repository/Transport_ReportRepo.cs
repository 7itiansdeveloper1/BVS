using ISas.Entities.TransportEntities;
using ISas.Repository.TransportRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.TransportRepo.Repository
{
    public class Transport_ReportRepo : ITransport_ReportRepo
    {

        #region Report Type 1
        public List<Transport_RouteDetailsModel> Transport_Report_RouteDetails()
        {
            List<Transport_RouteDetailsModel> routeList = new List<Transport_RouteDetailsModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_Report", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReportName", "RouteList");
                cmd.Parameters.AddWithValue("@VehId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                routeList = ds.Tables[0].AsEnumerable().Select(r => new Transport_RouteDetailsModel
                {
                    Availability = r.Field<int>("Availability"),
                    DriverName = r.Field<string>("DriverName"),
                    RouteName = r.Field<string>("RouteName"),
                    Sno = r.Field<int>("Sno"),
                    TotalSeat = r.Field<int>("TotalSeat"),
                    Vehicle = r.Field<string>("Vehicle"),
                    VehId = r.Field<string>("VehId"),
                }).ToList();
            }
            return routeList;
        }
        public List<Transport_BusStudentDetails> Transport_Report_BusStudentDetails(string VehicleID)
        {
            List<Transport_BusStudentDetails> busStudentList = new List<Transport_BusStudentDetails>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_Report", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReportName", "BusStudentList");
                cmd.Parameters.AddWithValue("@VehId", VehicleID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                busStudentList = ds.Tables[0].AsEnumerable().Select(r => new Transport_BusStudentDetails
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    Charge = r.Field<int>("Charge"),
                    Class = r.Field<string>("Class"),
                    Sno = r.Field<int>("Sno"),
                    Facility = r.Field<string>("Facility"),
                    StopName = r.Field<string>("StopName"),
                    Student = r.Field<string>("Student"),
                }).ToList();
            }
            return busStudentList;
        }
        #endregion END  Report Type 1


        #region Report Type 2
        public Transport_ReportModel Transport_Report_FormLoad()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Transport_ReportModel model = new Transport_ReportModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModuleName", "Transport");
                cmd.Parameters.AddWithValue("@ReportType", "Details");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.ReportNameList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ReportDisplayName"),
                    Value = r.Field<string>("ReportName"),
                }).ToList();


                model.FeeCategoryList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StructName"),
                    Value = r.Field<string>("StructID"),
                }).ToList();

                model.RouteList = GetRouteOrVehicleList(null, "GetRouteList");
                return model;
            }
        }
        public Transport_ReportModel Transport_ReportDetails(Transport_ReportModel parmVal)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Transport_ReportModel model = new Transport_ReportModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_Reports", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InstallmentId", parmVal.InstallmentId);
                cmd.Parameters.AddWithValue("@SessionId", parmVal.SessionId);
                cmd.Parameters.AddWithValue("@RouteId", parmVal.RouteId);
                cmd.Parameters.AddWithValue("@VehicleId", parmVal.VehicleId);
                cmd.Parameters.AddWithValue("@ReportName", parmVal.ReportId);
                cmd.Parameters.AddWithValue("@FeeStructure", parmVal.FeeCategoryId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

               
                model.ReportHeaders.Header1 = ds.Tables[0].Rows[0][0].ToString();
                model.ReportHeaders.Header2 = ds.Tables[0].Rows[0][1].ToString();
                model.ReportHeaders.Header3 = ds.Tables[0].Rows[0][2].ToString();
                model.ReportHeaders.Header4 = ds.Tables[0].Rows[0][3].ToString();
                model.ReportHeaders.LogoURL = ds.Tables[0].Rows[0][4].ToString();
                model.ReportHeaders.ReportName = ds.Tables[1].Rows[0][0].ToString();

                if (ds.Tables[2] != null)
                {
                    for (int i = 0; i < ds.Tables[2].Columns.Count; i++)
                    {
                        model.HeaderNameList.Add(ds.Tables[2].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        List<string> rowVal = new List<string>();
                        for (int j = 0; j < ds.Tables[2].Columns.Count; j++)
                        {
                            rowVal.Add(ds.Tables[2].Rows[i][j].ToString());
                        }
                        model.ValueList.Add(rowVal);
                    }
                }

                if(parmVal.ReportId == "Trans_TransportList")
                {
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        model.OtherReportHeader.RouteName = ds.Tables[3].Rows[0][0].ToString();
                        model.OtherReportHeader.VehName = ds.Tables[3].Rows[0][1].ToString();
                        model.OtherReportHeader.DName = ds.Tables[3].Rows[0][2].ToString();
                        model.OtherReportHeader.DMob = ds.Tables[3].Rows[0][3].ToString();
                        model.OtherReportHeader.DDLNo = ds.Tables[3].Rows[0][4].ToString();
                        model.OtherReportHeader.DBedgeNo = ds.Tables[3].Rows[0][5].ToString();
                        model.OtherReportHeader.HName = ds.Tables[3].Rows[0][6].ToString();
                        model.OtherReportHeader.HMob = ds.Tables[3].Rows[0][7].ToString();
                        model.OtherReportHeader.InchargeName = ds.Tables[3].Rows[0][8].ToString();
                        model.OtherReportHeader.InchargeMobile = ds.Tables[3].Rows[0][9].ToString();
                        model.OtherReportHeader.Parent1Name = ds.Tables[3].Rows[0][10].ToString();
                        model.OtherReportHeader.Parent1Mobile = ds.Tables[3].Rows[0][11].ToString();
                        model.OtherReportHeader.Parent2Name = ds.Tables[3].Rows[0][12].ToString();
                        model.OtherReportHeader.Parent2Mobile = ds.Tables[3].Rows[0][13].ToString();
                    }
                }
                else
                {
                    if(ds.Tables[3].Rows.Count > 0)
                    model.TotalAmount = ds.Tables[3].Rows[0][1].ToString();
                }

                return model;
            }
        }


        public List<SelectListItem> GetRouteOrVehicleList(string RouteId, string QueryFor)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("exec sp_Transport_Transportation @RouteId=N'"+RouteId+"',@QueryFor=N'"+QueryFor+"',@StopId=N'',@ERPNo=N'',@SessionId=N'',@VehicleId=N''", con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Text"),
                    Value = r.Field<string>("Value"),
                }).ToList();
            }
        }

        #endregion END Report Type 2
    }
}
