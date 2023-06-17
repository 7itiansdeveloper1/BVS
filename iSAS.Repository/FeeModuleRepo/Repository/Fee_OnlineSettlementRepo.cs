using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.FeesEntities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ISas.Repository.FeeModuleRepo.IRepository;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_OnlineSettlementRepo: IFee_OnlineSettlementRepo
    {
        public Fee_OnlineSettlementEntities OnlineSettlement_Transaction(string fromdate,string todate,string sessionid)
        {
            DataTable dt = new DataTable();
            Fee_OnlineSettlementEntities model = new Fee_OnlineSettlementEntities();
            using ( SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_OnlineSettlement_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromDate", fromdate);
                cmd.Parameters.AddWithValue("@toDate", todate);
                cmd.Parameters.AddWithValue("@sessionId", sessionid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

            }
            if( dt.Rows.Count>0 )
            {
                model.onlineSettlementList = dt.AsEnumerable().Select(r => new OnlineSettlementList
                {
                    isSettled=r.Field<bool>("isSettled"),
                    ERPNo= r.Field<string>("ERPNo"),
                    AdmNo = r.Field<string>("AdmNo"),
                    Student = r.Field<string>("Student"),
                    Father = r.Field<string>("Father"),
                    Class = r.Field<string>("ClassName"),
                    TransRefNo = r.Field<string>("TransRefNo"),
                    Transdate = r.Field<string>("Transdate"),
                    recamount = r.Field<int>("recamount"),
                    TransReferenceNo = r.Field<string>("TransReferenceNo"),
                    settlementDate = r.Field<string>("settlementDate")
                }).ToList();
            }

            return model;
        }
        public Tuple<int, string> OnlineSettlement_CRUD(DataTable sdt, string sessionid )
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_OnlineSettlement_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Dt", sdt);
                cmd.Parameters.AddWithValue("@sessionId", sessionid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
