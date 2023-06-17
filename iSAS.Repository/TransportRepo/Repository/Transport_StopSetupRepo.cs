using ISas.Entities.TransportEntities;
using ISas.Repository.TransportRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.TransportRepo.Repository
{
    public class Transport_StopSetupRepo : ITransport_StopSetupRepo
    {
        public List<Transport_StopSetupModels> GetStopList(string RouteId, string StopId)
        {
            List<Transport_StopSetupModels> stopList = new List<Transport_StopSetupModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_StopMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetRouteList");
                cmd.Parameters.AddWithValue("@RouteId", RouteId);
                cmd.Parameters.AddWithValue("@StopId", StopId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                stopList = ds.Tables[0].AsEnumerable().Select(r => new Transport_StopSetupModels
                {
                    Active = r.Field<bool>("Active"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    DCharge = r.Field<int>("DCharge"),
                    RouteId = r.Field<string>("RouteId"),
                    RouteName = r.Field<string>("RouteName"),
                    Strength = r.Field<int>("Strength"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                    DropTime = r.Field<string>("DropTime"),
                    PCharge = r.Field<int>("PCharge"),
                    PDCharge = r.Field<int>("PDCharge"),
                    PickupTime = r.Field<string>("PickupTime"),
                    StopID = r.Field<string>("StopID"),
                    StopName = r.Field<string>("StopName"),
                }).ToList();
            }
            return stopList;
        }
        public Transport_StopSetupModels GetStopById(string RouteId, string StopId)
        {
            return GetStopList(RouteId, StopId).FirstOrDefault();
        }

        public Tuple<int, string> Transport_StopSetup_CRUD(Transport_StopSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_StopMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StopID", model.StopID);
                cmd.Parameters.AddWithValue("@StopName", model.StopName);
                cmd.Parameters.AddWithValue("@PDCharge", model.PDCharge);
                cmd.Parameters.AddWithValue("@PCharge", model.PCharge);
                cmd.Parameters.AddWithValue("@DCharge", model.DCharge);
                cmd.Parameters.AddWithValue("@Arrival", model.PickupTime);
                cmd.Parameters.AddWithValue("@Depart", model.DropTime);
                cmd.Parameters.AddWithValue("@Print", model.PrintOrder);
                cmd.Parameters.AddWithValue("@Active", model.Active);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);
                cmd.Parameters.AddWithValue("@RouteId", model.RouteId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Transport_StopSetup_CRUD(string StopId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_StopMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StopID", StopId);

                cmd.Parameters.AddWithValue("@UserId", "");
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");


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
