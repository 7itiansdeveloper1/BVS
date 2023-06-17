using ISas.Entities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.Implementation
{
    public class Student_NSORepo : IStudent_NSORepo
    {
        public DropDownListFor_Student_NSO GetStudent_NSODropDownList(string UserId, string ClassID, string SectinID)
        {
            DropDownListFor_Student_NSO dropDownMaster = new DropDownListFor_Student_NSO();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_NSO_DorpDownList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassId", ClassID);
                cmd.Parameters.AddWithValue("@SectionId", SectinID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                dropDownMaster.LastNSONo = ds.Tables[0].Rows[0][0].ToString();
                dropDownMaster.CurrentSessionCount = Convert.ToInt32(ds.Tables[0].Rows[0][1]);

                dropDownMaster.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID"),
                }).ToList();

                dropDownMaster.SessionList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SessionDisplayName"),
                    Value = r.Field<string>("SessID"),
                   // Selected = r.Field<bool>("Default"),
                }).ToList();

                dropDownMaster.SectionList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SecName"),
                    Value = r.Field<string>("SecID"),
                }).ToList();

                dropDownMaster.StudentList = ds.Tables[4].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Student"),
                    Value = r.Field<string>("ERPNo")
                }).ToList();
            }
            return dropDownMaster;
        }

        public Tuple<int, string> Student_NSO_CRUD(Student_NSOModel model,string userid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_NSO_CRUD_NEW", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", model.Mode);

                #region Student Info
                cmd.Parameters.AddWithValue("@NSO_No", model.NSO_No);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@ERPNO", model.ERPNO);
                cmd.Parameters.AddWithValue("@NSO_AppliedDate", Convert.ToDateTime(model.NSO_AppliedDate).Date);
                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", model.SectionId);
                cmd.Parameters.AddWithValue("@Refund_Amount", model.Refund_Amount);
                cmd.Parameters.AddWithValue("@TC_Reason", model.NSO_Reason);
                cmd.Parameters.AddWithValue("@TC_Description", model.NSO_Description);
                cmd.Parameters.AddWithValue("@userId", userid);
                #endregion


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public IEnumerable<Student_NSOModel> GetAllStudent_NSOList(string NSONo)
        {
            List<Student_NSOModel> ListOfStudentNSO = new List<Student_NSOModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_NSO_LandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NSONo", NSONo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                ListOfStudentNSO = ds.Tables[0].AsEnumerable().Select(r => new Student_NSOModel
                {
                    SessionId = r.Field<string>("SessionId"),
                    NSO_No = r.Field<string>("NSO_No"),
                    NSO_AppliedDate = r.Field<string>("NSO_AppliedDate"),
                    ERPNO = r.Field<string>("ERPNO"),
                    AdmissionNo = r.Field<string>("AdmNo"),
                    ClassId = r.Field<string>("ClassId"),
                    //ClassName = r.Field<string>("StaffID"),
                    SectionId = r.Field<string>("SectionId"),
                    //SectionName = r.Field<string>("StaffID"),
                    StudentName = r.Field<string>("Student"),
                    //MotherName = r.Field<string>("StaffID"),
                    FatherName = r.Field<string>("FatherName"),
                    StudentID = r.Field<string>("ERPNO"),
                    NSO_Reason = r.Field<string>("NSO_Reason"),
                    NSO_Description = r.Field<string>("NSO_Description"),
                    IsNSOCancelled = r.Field<bool>("IsNSOCancelled"),
                    Refund_Amount = r.Field<int>("Refund_Amount"),
                    ModifiedBy= r.Field<string>("ModifiedBy"),
                    ModifiedDate = r.Field<string>("ModifiedDate"),
                }).ToList();
            }
            return ListOfStudentNSO;
        }

        public Student_NSOModel GetStudent_NSONSONo(string NSONo)
        {
            return GetAllStudent_NSOList(NSONo).FirstOrDefault();
        }

    }
}
