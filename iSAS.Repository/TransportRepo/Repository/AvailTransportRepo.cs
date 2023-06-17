using ISas.Entities.TransportEntities;
using ISas.Repository.TransportRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.TransportRepo.Repository
{
    public class AvailTransportRepo : IAvailTransportRepo
    {
        public List<SelectListItem> Get_AvailTransportDropDowns(string RouteId, string QueryFor, string StopId, string ERPNo, string SessionId)
        {
            List<SelectListItem> concessionCategoryList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_Transportation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RouteId", RouteId);
                cmd.Parameters.AddWithValue("@QueryFor", QueryFor);
                cmd.Parameters.AddWithValue("@StopId", StopId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@VehicleId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                concessionCategoryList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Text"),
                    Value = r.Field<string>("Value"),
                    Selected=r.Field<bool>("isSelected")
                }).ToList();
            }
            return concessionCategoryList;
        }

        public List<TransportDuesDetails> Get_TransportDueList(string RouteId, string QueryFor, string StopId, string ERPNo, string SessionId)
        {
            List<TransportDuesDetails> transportDueList = new List<TransportDuesDetails>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_Transportation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RouteId", RouteId);
                cmd.Parameters.AddWithValue("@QueryFor", QueryFor);
                cmd.Parameters.AddWithValue("@StopId", StopId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@VehicleId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                transportDueList = ds.Tables[0].AsEnumerable().Select(r => new TransportDuesDetails
                {
                    Balance = r.Field<int>("Balance"),
                    Due = r.Field<int>("Due"),
                    DueDate = r.Field<string>("DueDate"),
                    Excess = r.Field<int>("Excess"),
                    HeadID = r.Field<string>("HeadID"),
                    HeadName = r.Field<string>("HeadName"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    Paid = r.Field<int>("Paid"),
                    AvailTransport = r.Field<bool>("AvailTransport"),
                    WithDrawlTransport = r.Field<bool>("WithDrawlTransport"),
                    TransRefNo = r.Field<string>("TransRefNo"),
                }).ToList();
            }
            return transportDueList;
        }

        public AvailTransportModel Get_TransportDetails(string RouteId, string QueryFor, string StopId, string ERPNo, string SessionId)
        {
            AvailTransportModel model = new AvailTransportModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_Transportation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RouteId", RouteId);
                cmd.Parameters.AddWithValue("@QueryFor", QueryFor);
                cmd.Parameters.AddWithValue("@StopId", StopId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@VehicleId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.IsAvailTransport = Convert.ToBoolean(ds.Tables[0].Rows[0][0].ToString());
                    model.RouteId = ds.Tables[0].Rows[0][1].ToString();
                    model.RouteList.Add(new SelectListItem { Value = ds.Tables[0].Rows[0][1].ToString(), Text = ds.Tables[0].Rows[0][2].ToString() });

                    model.StopId = ds.Tables[0].Rows[0][3].ToString();
                    model.StopList.Add(new SelectListItem { Value = ds.Tables[0].Rows[0][3].ToString(), Text = ds.Tables[0].Rows[0][4].ToString() });

                    model.VehicleId = ds.Tables[0].Rows[0][5].ToString();
                    model.VehicleList.Add(new SelectListItem { Value = ds.Tables[0].Rows[0][5].ToString(), Text = ds.Tables[0].Rows[0][6].ToString() });

                    model.SeatCapacity = Convert.ToInt32(ds.Tables[0].Rows[0][7]);
                    model.SeatOccupied = Convert.ToInt32(ds.Tables[0].Rows[0][8]);

                    model.DriverName = ds.Tables[0].Rows[0][9].ToString();
                    model.DriverMobile = ds.Tables[0].Rows[0][10].ToString();

                    model.HelperName = ds.Tables[0].Rows[0][11].ToString();
                    model.HelperNo = ds.Tables[0].Rows[0][12].ToString();

                    model.Incharge = ds.Tables[0].Rows[0][13].ToString();
                    model.InchargeMobile = ds.Tables[0].Rows[0][14].ToString();
                    model.FacilityId = ds.Tables[0].Rows[0][15].ToString().Replace(" ", "");

                    model.PickCharge = Convert.ToInt32(ds.Tables[0].Rows[0][16]);
                    model.DropCharge = Convert.ToInt32(ds.Tables[0].Rows[0][17]);
                    model.PickAndDropCharge = Convert.ToInt32(ds.Tables[0].Rows[0][18]);
                    model.Date = ds.Tables[0].Rows[0][19].ToString();
                }
            }
            return model;
        }

        public Tuple<int, string> AvailTransport_CRUD(AvailTransportModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_TransportTransaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", model.StudentDetails.StudentId);
                cmd.Parameters.AddWithValue("@CRUDFor", model.CRUDFor);
                cmd.Parameters.AddWithValue("@FeeHeadId", model.Selected_FeeHeadId);
                cmd.Parameters.AddWithValue("@SessionId", model.StudentDetails.SessionId);

                if (!string.IsNullOrEmpty(model.Selected_DueDate))
                    cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(model.Selected_DueDate).Date);

                cmd.Parameters.AddWithValue("@TransportAmount", model.Selected_TransportAmount);
                cmd.Parameters.AddWithValue("@TransRefNo", model.Selected_TransRefNo);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<int, string> Transportation_CRUD(AvailTransportModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Trasnport_Transportation_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", model.StudentDetails.SessionId);
                cmd.Parameters.AddWithValue("@TransportId", "");
                cmd.Parameters.AddWithValue("@ERPNo", model.StudentDetails.ERPNo);
                cmd.Parameters.AddWithValue("@RouteId", model.RouteId);
                cmd.Parameters.AddWithValue("@StopId", model.StopId);
                cmd.Parameters.AddWithValue("@VehicleId", model.VehicleId);
                cmd.Parameters.AddWithValue("@Facility", model.FacilityId);
                cmd.Parameters.AddWithValue("@Charge", model.Selected_TransportAmount);
                cmd.Parameters.AddWithValue("@AppliedDate", Convert.ToDateTime(model.Date).Date);
                cmd.Parameters.AddWithValue("@WithDrawlDate", Convert.ToDateTime(model.Date).Date);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDFor", model.CRUDFor);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public Tuple<int, int, string, string, string, string, string> Get_VehicleDetails(string VehicleId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_Transportation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RouteId", "");
                cmd.Parameters.AddWithValue("@QueryFor", "GetVehicleDetails");
                cmd.Parameters.AddWithValue("@StopId", "");
                cmd.Parameters.AddWithValue("@ERPNo", "");
                cmd.Parameters.AddWithValue("@SessionId", "");
                cmd.Parameters.AddWithValue("@VehicleId", VehicleId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return new Tuple<int, int, string, string, string, string, string>
                        (Convert.ToInt32(ds.Tables[0].Rows[0][0]), Convert.ToInt32(ds.Tables[0].Rows[0][1]),
                        ds.Tables[0].Rows[0][2].ToString(), ds.Tables[0].Rows[0][3].ToString(),
                        ds.Tables[0].Rows[0][4].ToString(), ds.Tables[0].Rows[0][5].ToString(),
                        ds.Tables[0].Rows[0][6].ToString());
                }
            }
            return new Tuple<int, int, string, string, string, string, string>(0, 0, "", "", "", "", "");
        }
        public Tuple<int, int, int> Get_ChargeDetails(string StopId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_Transportation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RouteId", "");
                cmd.Parameters.AddWithValue("@QueryFor", "GetStopDetails");
                cmd.Parameters.AddWithValue("@StopId", StopId);
                cmd.Parameters.AddWithValue("@ERPNo", "");
                cmd.Parameters.AddWithValue("@SessionId", "");
                cmd.Parameters.AddWithValue("@VehicleId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return new Tuple<int, int, int>(
                        Convert.ToInt32(ds.Tables[0].Rows[0][0]),
                        Convert.ToInt32(ds.Tables[0].Rows[0][1]),
                        Convert.ToInt32(ds.Tables[0].Rows[0][2]));
                }
            }
            return new Tuple<int, int, int>(0, 0, 0);
        }
    }
}
