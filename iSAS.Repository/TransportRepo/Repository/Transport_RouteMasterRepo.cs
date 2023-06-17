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
    public class Transport_RouteMasterRepo : ITransport_RouteMasterRepo
    {
        public List<Transport_RouteMasterModels> GetRouteMasterList(string RouteId)
        {
            List<Transport_RouteMasterModels> routeList = new List<Transport_RouteMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_RouteMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetRouteList");
                cmd.Parameters.AddWithValue("@RouteId", RouteId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                routeList = ds.Tables[0].AsEnumerable().Select(r => new Transport_RouteMasterModels
                {
                    Active = r.Field<bool>("Active"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    NoofStop = r.Field<int>("NoofStop"),
                    NoofVehicle = r.Field<int>("NoofVehicle"),
                    RouteId = r.Field<int>("RouteId").ToString(),
                    RouteName = r.Field<string>("RouteName"),
                    Strength = r.Field<int>("Strength"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                }).ToList();
            }
            return routeList;
        }
        public List<SelectListItem> getRouteMstDropDownList()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_RouteMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetRouteList");
                cmd.Parameters.AddWithValue("@RouteId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("RouteName"),
                    Value = r.Field<int>("RouteId").ToString(),
                }).ToList();
            }
        }

        public Transport_RouteMasterModels GetRouteMasterById(string RouteId)
        {
            return GetRouteMasterList(RouteId).FirstOrDefault();
        }

        public Tuple<int, string> Transport_RouteMaster_CRUD(Transport_RouteMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_RouteMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RouteId", model.RouteId);
                cmd.Parameters.AddWithValue("@RouteName", model.RouteName);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@Active", model.Active);

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

        public Tuple<int, string> Transport_RouteMaster_CRUD(string RouteId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_RouteMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RouteId", RouteId);
                cmd.Parameters.AddWithValue("@RouteName", "");
                cmd.Parameters.AddWithValue("@PrintOrder", "");
                cmd.Parameters.AddWithValue("@Active", "");

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
