using System;
using System.Collections.Generic;
using System.Linq;
using ISas.Repository.StudentRegistrationRepository.IRepository;
using ISas.Entities.RegistrationEntities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ISas.Repository.StudentRegistrationRepository.Repository
{
    public class StudentRegistrationRepo : IStudentRegistrationRepo
    {
        public Tuple<int, string> Student_Registration_CRUD(Student_RegistrationMaster model)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentRegistration_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //model.FormID = "TESTFORMID";

                //cmd.Parameters.AddWithValue("@ClassMarkListDT", dt);
                cmd.Parameters.AddWithValue("@Sess", model.Sess);
                cmd.Parameters.AddWithValue("@RegClassId", model.RegClassId);
                cmd.Parameters.AddWithValue("@RegID", model.RegID);
                cmd.Parameters.AddWithValue("@FormID", model.FormID);
                cmd.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(model.RegDate).Date);
                cmd.Parameters.AddWithValue("@AdmCategoryId", model.AdmCategoryId);
                cmd.Parameters.AddWithValue("@Amt", model.Amt);
                if (model.IntrvwDate!=null)
                cmd.Parameters.AddWithValue("@IntrvwDate", Convert.ToDateTime(model.IntrvwDate).Date);
                cmd.Parameters.AddWithValue("@Remark", model.Remark);
                cmd.Parameters.AddWithValue("@Student_FirstName", model.Student_FirstName);
                cmd.Parameters.AddWithValue("@Student_MiddleName", model.Student_MiddleName);
                cmd.Parameters.AddWithValue("@Student_LastName", model.Student_LastName);
                cmd.Parameters.AddWithValue("@Student_Gender", model.Student_Gender);
                cmd.Parameters.AddWithValue("@Student_DOB", Convert.ToDateTime(model.Student_DOB).Date);
                cmd.Parameters.AddWithValue("@Father_FirstName", model.Father_FirstName);
                cmd.Parameters.AddWithValue("@Father_MiddleName", model.Father_MiddleName);
                cmd.Parameters.AddWithValue("@Father_LastName", model.Father_LastName);
                cmd.Parameters.AddWithValue("@Father_MoblieNo", model.Father_MoblieNo);
                cmd.Parameters.AddWithValue("@Mother_FirstName", model.Mother_FirstName);
                cmd.Parameters.AddWithValue("@Mother_MiddleName", model.Mother_MiddleName);
                cmd.Parameters.AddWithValue("@Mother_LastName", model.Mother_LastName);
                cmd.Parameters.AddWithValue("@Mother_MobileNo", model.Mother_MobileNo);
                cmd.Parameters.AddWithValue("@Student_CorrAdd", model.Student_CorrAdd);
                cmd.Parameters.AddWithValue("@Student_CorrArea", model.Student_CorrArea);
                cmd.Parameters.AddWithValue("@Student_CorrCity", model.Student_CorrCity);
                cmd.Parameters.AddWithValue("@Student_CorrState", model.Student_CorrState);
                cmd.Parameters.AddWithValue("@Student_CorrCountry", model.Student_CorrCountry);
                cmd.Parameters.AddWithValue("@Student_CorrPinCode", model.Student_CorrPinCode);
                cmd.Parameters.AddWithValue("@Student_CorrMobile", model.Student_CorrMobile);
                cmd.Parameters.AddWithValue("@Student_PermAdd", model.Student_PermAdd);
                cmd.Parameters.AddWithValue("@Student_PermArea", model.Student_PermArea);
                cmd.Parameters.AddWithValue("@Student_PermCity", model.Student_PermCity);
                cmd.Parameters.AddWithValue("@Student_PermState", model.Student_PermState);
                cmd.Parameters.AddWithValue("@Student_PermCountry", model.Student_PermCountry);
                cmd.Parameters.AddWithValue("@Student_PermPinCode", model.Student_PermPinCode);
                cmd.Parameters.AddWithValue("@Student_PermMobile", model.Student_PermMobile);
                cmd.Parameters.AddWithValue("@RegStatus", model.RegStatus ?? "");
                cmd.Parameters.AddWithValue("@CBy", model.CBy ?? "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }

        public IEnumerable<Student_RegistrationMaster> Student_Registration_MasterDetails(string RegID)
        {
            List<Student_RegistrationMaster> studnetRegMasterList = new List<Student_RegistrationMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("exec sp_StudentRegistation_Master_Details '" + RegID + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //SqlDataReader dr = cmd.ExecuteReader();
                //ds.Tables[0].Rows
                int count = ds.Tables[0].Rows.Count;

                for (int x = 0; x < count; x++)
                {
                    Student_RegistrationMaster regDetails = new Student_RegistrationMaster();

                    regDetails.Sess = ds.Tables[0].Rows[x][0].ToString();
                    regDetails.RegClassId = ds.Tables[0].Rows[x][1].ToString();
                    regDetails.RegID = ds.Tables[0].Rows[x][2].ToString();
                    regDetails.FormID = ds.Tables[0].Rows[x][3].ToString();
                    regDetails.RegDate = ds.Tables[0].Rows[x][4].ToString();
                    regDetails.AdmCategoryId = ds.Tables[0].Rows[x][5].ToString();
                    regDetails.Amt = Convert.ToInt32(ds.Tables[0].Rows[x][6]);
                    regDetails.IntrvwDate = ds.Tables[0].Rows[x][7].ToString();
                    regDetails.Remark = ds.Tables[0].Rows[x][8].ToString();
                    regDetails.Student_FirstName = ds.Tables[0].Rows[x][9].ToString();
                    regDetails.Student_MiddleName = ds.Tables[0].Rows[x][10].ToString();
                    regDetails.Student_LastName = ds.Tables[0].Rows[x][11].ToString();
                    regDetails.Student_Gender = Convert.ToChar(ds.Tables[0].Rows[x][12]);
                    regDetails.Student_DOB = ds.Tables[0].Rows[x][13].ToString();
                    regDetails.Father_FirstName = ds.Tables[0].Rows[x][14].ToString();
                    regDetails.Father_MiddleName = ds.Tables[0].Rows[x][15].ToString();
                    regDetails.Father_LastName = ds.Tables[0].Rows[x][16].ToString();
                    regDetails.Father_MoblieNo = ds.Tables[0].Rows[x][17].ToString();
                    regDetails.Mother_FirstName = ds.Tables[0].Rows[x][18].ToString();
                    regDetails.Mother_MiddleName = ds.Tables[0].Rows[x][19].ToString();
                    regDetails.Mother_LastName = ds.Tables[0].Rows[x][20].ToString();
                    regDetails.Mother_MobileNo = ds.Tables[0].Rows[x][21].ToString();
                    regDetails.Student_CorrAdd = ds.Tables[0].Rows[x][22].ToString();
                    regDetails.Student_CorrArea = ds.Tables[0].Rows[x][23].ToString();
                    regDetails.Student_CorrCity = ds.Tables[0].Rows[x][24].ToString();
                    regDetails.Student_CorrState = ds.Tables[0].Rows[x][25].ToString();
                    regDetails.Student_CorrCountry = ds.Tables[0].Rows[x][26].ToString();
                    regDetails.Student_CorrMobile = ds.Tables[0].Rows[x][27].ToString();
                    regDetails.Student_PermAdd = ds.Tables[0].Rows[x][28].ToString();
                    regDetails.Student_PermArea = ds.Tables[0].Rows[x][29].ToString();
                    regDetails.Student_PermCity = ds.Tables[0].Rows[x][30].ToString();
                    regDetails.Student_PermState = ds.Tables[0].Rows[x][31].ToString();
                    regDetails.Student_PermCountry = ds.Tables[0].Rows[x][32].ToString();
                    regDetails.Student_PermMobile = ds.Tables[0].Rows[x][33].ToString();
                    regDetails.RegStatus = ds.Tables[0].Rows[x][34].ToString();
                    regDetails.Student_CorrPinCode= ds.Tables[0].Rows[x][35].ToString();
                    regDetails.Student_PermPinCode = ds.Tables[0].Rows[x][36].ToString();
                    regDetails.AdmCategoryId = ds.Tables[0].Rows[x][37].ToString();
                    regDetails.RegistrationClass = ds.Tables[0].Rows[x][38].ToString();
                    studnetRegMasterList.Add(regDetails);
                }
                con.Close();
            }
            return studnetRegMasterList;
        }
        public Student_RegistrationMaster GetStudentRegistrationByRegID(string RegID)
        {
            Student_RegistrationMaster model = Student_Registration_MasterDetails(RegID).FirstOrDefault();
            return model;
        }
        public Tuple<string, string> GetReleatedAutoNos(string SessionId)
        {
            string RegNo = ""; string FormNo = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString());
            try
            {
                SqlCommand cmd1 = new SqlCommand("sp_School_GetMaxID", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@SessionId", SqlDbType.NVarChar, 10).Value = SessionId;
                cmd1.Parameters.Add("@MaxIdCategory", SqlDbType.NVarChar, 20).Value = "RegId";

                SqlCommand cmd2 = new SqlCommand("sp_School_GetMaxID", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@SessionId", SqlDbType.NVarChar, 10).Value = SessionId;
                cmd2.Parameters.Add("@MaxIdCategory", SqlDbType.NVarChar, 20).Value = "FormId";

                con.Open();
                RegNo = cmd1.ExecuteScalar().ToString();
                FormNo = cmd2.ExecuteScalar().ToString();
            }
            finally
            {
                con.Close();
            }
            return new Tuple<string, string>(RegNo, FormNo);
        }
        public RegistrationSlipModel RegistrationSlipDetails(string RegId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                RegistrationSlipModel model = new RegistrationSlipModel();
                SqlCommand cmd = new SqlCommand("sp_StudentRegistation_Print", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegID", RegId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.RegID = ds.Tables[0].Rows[0]["RegID"].ToString();
                    model.Class = ds.Tables[0].Rows[0]["Class"].ToString();
                    model.RegDate = ds.Tables[0].Rows[0]["RegDate"].ToString();
                    model.AdmCategoryName = ds.Tables[0].Rows[0]["AdmCategoryName"].ToString();
                    model.IntrvwDate = ds.Tables[0].Rows[0]["IntrvwDate"].ToString();
                    model.Student = ds.Tables[0].Rows[0]["Student"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                    model.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                    model.Father = ds.Tables[0].Rows[0]["Father"].ToString();
                    model.FMobileNo = ds.Tables[0].Rows[0]["FMobileNo"].ToString();
                    model.Mother = ds.Tables[0].Rows[0]["Mother"].ToString();
                    model.MMobileNo = ds.Tables[0].Rows[0]["MMobileNo"].ToString();
                    model.RegAmount = Convert.ToInt32(ds.Tables[0].Rows[0]["RegAmount"]);
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                    model.Address1 = ds.Tables[0].Rows[0]["Address1"].ToString();
                    model.SessionName = ds.Tables[0].Rows[0]["SessionName"].ToString();
                }
                return model;
            }
        }

        public DataTable ValidateDOB(string ClassId, string DOB, DateTime AOD)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                RegistrationSlipModel model = new RegistrationSlipModel();
                SqlCommand cmd = new SqlCommand("sp_GetStudentAgeInYMD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@AsonDate", AOD);
                cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(DOB).Date);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }

        //public Tuple<bool, string> Student_Registration_ValidateDOB(string ClassId, string DOB,string AOD)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        DataTable dt1 = new DataTable();
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_GetStudentAgeInYMD", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@AsonDate", Convert.ToDateTime(AOD).Date);
        //        cmd.Parameters.AddWithValue("@ClassId", ClassId);
        //        cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(DOB).Date);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt1);
        //        con.Close();
        //        return new Tuple<bool, string>(Convert.ToBoolean(dt1.Rows[0][0]), dt1.Rows[0][1].ToString());
        //    }
        //}
    }
}
