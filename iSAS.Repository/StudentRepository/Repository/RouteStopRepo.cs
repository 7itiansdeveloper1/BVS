using ISas.Repository.StudentRegistrationRepository.IRepository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.StudentRegistrationRepository.Repository
{
    public class RouteStopRepo : IRouteStopRepo
    {
        public IEnumerable<SelectListItem> GetRouteStopByRouteId(int RouteId)
        {
            List<SelectListItem> stopList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                
                SqlCommand cmd = new SqlCommand("select * from GetRouteStop('" + RouteId + "')", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                stopList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StopName"),
                    Value = r.Field<int>("StopId").ToString(),
                }).ToList();
            }
            return stopList;
        }
    }
}
