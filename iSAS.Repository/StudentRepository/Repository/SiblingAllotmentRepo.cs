
using ISas.Entities.Student_Entities;
using ISas.Repository.StudentRegistrationRepository.IRepository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ISas.Repository.StudentRegistrationRepository.Repository
{
    public class SiblingAllotmentRepo : ISiblingAllotmentRepo
    {
        public SiblingAllotmentModels GetStudentList(string ClassId, string SectionId, string SessionId, string UserId)
        {
            SiblingAllotmentModels model = new SiblingAllotmentModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                //SqlCommand cmd = new SqlCommand("sp_Student_Siblings_Transaction", con);
                SqlCommand cmd = new SqlCommand("sp_Student_Siblings_Transaction_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "ClassList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.StudentDetailList = ds.Tables[0].AsEnumerable().Select(r => new Sibling_StudentDetailModel
                {
                     AdmNo = r.Field<string>("AdmNo"),
                     ERPNo = r.Field<string>("ERPNo"),
                     RollNo = r.Field<int>("RollNo"),
                     SiblingName   = r.Field<string>("SiblingName"),
                     Student = r.Field<string>("Student"),
                     IsSiblingStudent=r.Field<bool>("IsSiblingStudent")
                }).ToList();
            }
            return model;
        }


        public SiblingAllotmentModels GetStudentPossibleSiblingList(string selectederpno,string selectedstudentname,string sessionid)
        {
            SiblingAllotmentModels model = new SiblingAllotmentModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_Siblings_Transaction_V1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ERPNo", selectederpno);
                cmd.Parameters.AddWithValue("@Mode", "Sibling");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                model.ERPNo = selectederpno;
                model.studentName = selectedstudentname;
                model.possibleSiblingList = ds.Tables[0].AsEnumerable().Select(r => new possibleSibling
                {
                    isSelected = r.Field<bool>("isSelected"),
                    SiblingClass = r.Field<string>("SiblingClass"),
                    SiblingERP = r.Field<string>("SiblingERP"),
                    SiblingName = r.Field<string>("SiblingName"),
                }).ToList();
            }
            return model;
        }
        public string SiblingAllotment_CRUD(SiblingAllotmentModels model)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_Siblings_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ERPNo", model.ERPNo);
                cmd.Parameters.AddWithValue("@SiblingId", model.SiblingIds);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                message = dt.Rows[0][1].ToString();
                return message;
            }
        }

    }
}
