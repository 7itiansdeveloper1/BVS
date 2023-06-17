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
    public class Transport_VehicleSetupRepo : ITransport_VehicleSetupRepo
    {
        public List<SelectListItem> GetBloodGroupList()
        {
            List<SelectListItem> bloodGroupList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_VehMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "BloodGrp");
                cmd.Parameters.AddWithValue("@VehId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                bloodGroupList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("BldGrpName"),
                    Value = r.Field<string>("BldGrpId"),
                }).ToList();
            }
            return bloodGroupList;
        }

        public List<Transport_VehicleSetupModel> GetVehicleList(string VehicleId)
        {
            List<Transport_VehicleSetupModel> vehicleList = new List<Transport_VehicleSetupModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_VehMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetVehList");
                cmd.Parameters.AddWithValue("@VehId", VehicleId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                vehicleList = ds.Tables[0].AsEnumerable().Select(r => new Transport_VehicleSetupModel
                {
                    DAddress = r.Field<string>("DAddress"),
                    DBedgeNo = r.Field<string>("DBedgeNo"),
                    DBldGrp = r.Field<string>("DBldGrp"),
                    DDLNo = r.Field<string>("DDLNo"),
                    DMob = r.Field<string>("DMob"),
                    DName = r.Field<string>("DName"),
                    HAddress = r.Field<string>("HAddress"),
                    HBedgeNo = r.Field<string>("HBedgeNo"),
                    HBldGrp = r.Field<string>("HBldGrp"),
                    HDLNo = r.Field<string>("HDLNo"),
                    HMob = r.Field<string>("HMob"),
                    HName = r.Field<string>("HName"),
                    InchargeMobile = r.Field<string>("InchargeMobile"),
                    InchargeName = r.Field<string>("InchargeName"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    Parent1Mobile = r.Field<string>("Parent1Mobile"),
                    Parent1Name = r.Field<string>("Parent1Name"),
                    Parent2Mobile = r.Field<string>("Parent2Mobile"),
                    Parent2Name = r.Field<string>("Parent2Name"),
                    SCapacity = r.Field<int>("SCapacity"),
                    Strength = r.Field<int>("Strength"),
                    VehID = r.Field<string>("VehID"),
                    VehName = r.Field<string>("VehName"),
                    VehNo = r.Field<string>("VehNo"),
                    VehType = r.Field<string>("VehType"),
                }).ToList();
            }
            return vehicleList;
        }
        public Transport_VehicleSetupModel GetVehicleById(string VehicleId)
        {
            return GetVehicleList(VehicleId).FirstOrDefault();
        }

        public Tuple<int, string> Transport_VehicleSetup_CRUD(Transport_VehicleSetupModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_VehMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehID", model.VehID);
                cmd.Parameters.AddWithValue("@VehName", model.VehName);
                cmd.Parameters.AddWithValue("@VehNo", model.VehNo);
                cmd.Parameters.AddWithValue("@VehType", model.VehType);
                cmd.Parameters.AddWithValue("@DName", model.DName);
                cmd.Parameters.AddWithValue("@DMob", model.DMob);
                cmd.Parameters.AddWithValue("@DBldGrp", model.DBldGrp);
                cmd.Parameters.AddWithValue("@DDLNo", model.DDLNo);
                cmd.Parameters.AddWithValue("@HName", model.HName);
                cmd.Parameters.AddWithValue("@HMob", model.HMob);
                cmd.Parameters.AddWithValue("@HBldGrp", model.HBldGrp);
                cmd.Parameters.AddWithValue("@HDLNo", model.HDLNo);
                cmd.Parameters.AddWithValue("@SCapacity", model.SCapacity);
                cmd.Parameters.AddWithValue("@DBedgeNo", model.DBedgeNo);
                cmd.Parameters.AddWithValue("@DAddress", model.DAddress);
                cmd.Parameters.AddWithValue("@HBedgeNo", model.HBedgeNo);
                cmd.Parameters.AddWithValue("@HAddress", model.HAddress);
                cmd.Parameters.AddWithValue("@InchargeName", model.InchargeName);
                cmd.Parameters.AddWithValue("@InchargeMobile", model.InchargeMobile);
                cmd.Parameters.AddWithValue("@Parent1Name", model.Parent1Name);
                cmd.Parameters.AddWithValue("@Parent1Mobile", model.Parent1Mobile);
                cmd.Parameters.AddWithValue("@Parent2Name", model.Parent2Name);
                cmd.Parameters.AddWithValue("@Parent2Mobile", model.Parent2Mobile);

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
        public Tuple<int, string> Transport_VehicleSetup_CRUD(string VehicleId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_VehMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehID", VehicleId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        #region Vehicle Route Setup
        public VehicleRouteSetupModels GetVehicleRouteDetails(string VehicleId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                VehicleRouteSetupModels model = new VehicleRouteSetupModels();
                model.VehicleId = VehicleId;

                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_VehicleRouteMapping_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.VehicleRouteDetails = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("RouteName"),
                    Value = r.Field<string>("RouteID"),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();
                return model;
            }
        }
        public Tuple<int, string> VehicleRouteSetup_CRUD(VehicleRouteSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Transport_VehicleRouteMapping_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleId", model.VehicleId);
                cmd.Parameters.AddWithValue("@RouteIds", string.Join(",", model.VehicleRouteDetails.Where(r => r.Selected).Select(r=> r.Value).ToList()));
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
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
