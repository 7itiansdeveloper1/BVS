using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ISas.Entities;
using System.Configuration;
using System.Data;
using ISas.Repository.Interface;
using System.Web.Mvc;
using System.Linq;

namespace ISas.Repository.Implementation
{

    public class StudentUpdationRepository : IStudentUpdation
    {
        //private string ClassTeacherName = "";
        public IEnumerable<UpdationParametersList> GetUpdationParametersList(string sessionid, string classid, string userid)
        {
            List<UpdationParametersList> updationParametersList = new List<UpdationParametersList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetClassUpdationField");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //SqlDataReader dr = cmd.ExecuteReader();
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    UpdationParametersList updateparameter = new UpdationParametersList();
                    updateparameter.FieldName = ds.Tables[0].Rows[x][0].ToString();
                    updateparameter.FieldDisplayName = ds.Tables[0].Rows[x][1].ToString();
                    updationParametersList.Add(updateparameter);
                }
                con.Close();
            }
            return updationParametersList;
        }
        public Tuple<IEnumerable<ClassofJoiningList>, string> GetClassofJoiningList(string sessionid, string classid, string sectionid, string userid,string mode)
        {
            string colType = "";
            List<ClassofJoiningList> _classofJoiningList = new List<ClassofJoiningList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", mode);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                if (mode == "Student" || mode == "Father" || mode == "Mother" || mode == "MOT" || mode == "BUSStop")
                {
                    for (int x = 0; x < count; x++)
                    {
                        ClassofJoiningList _classofjoining = new ClassofJoiningList();
                        _classofjoining.Student = new Student();
                        _classofjoining.Student.StudentERPNo = ds.Tables[0].Rows[x][0].ToString();
                        _classofjoining.Student.StudentAdmNo = ds.Tables[0].Rows[x][1].ToString();
                        _classofjoining.Student.StudentRollNo = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                        _classofjoining.Student.StudentName = ds.Tables[0].Rows[x][3].ToString();
                        _classofjoining.Parameter1 = ds.Tables[0].Rows[x][4].ToString();
                        //if (mode == "Student" || mode == "Father" || mode == "Mother" || mode == "MOT")
                        //{
                            _classofjoining.Parameter2 = ds.Tables[0].Rows[x][5]!=null? ds.Tables[0].Rows[x][5].ToString():"";
                            _classofjoining.Parameter3 = ds.Tables[0].Rows[x][6]!=null? ds.Tables[0].Rows[x][6].ToString():"";
                        //}
                        _classofJoiningList.Add(_classofjoining);
                    }

                }
                else
                {
                    for (int x = 0; x < count; x++)
                    {
                        ClassofJoiningList _classofjoining = new ClassofJoiningList();
                        _classofjoining.Student = new Student();
                        _classofjoining.Student.StudentERPNo = ds.Tables[0].Rows[x][0].ToString();
                        _classofjoining.Student.StudentAdmNo = ds.Tables[0].Rows[x][1].ToString();
                        _classofjoining.Student.StudentRollNo = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                        _classofjoining.Student.StudentName = ds.Tables[0].Rows[x][3].ToString();
                        _classofjoining.Parameter1 = ds.Tables[0].Rows[x][4].ToString();
                        _classofJoiningList.Add(_classofjoining);
                    }
                }

                colType = ds.Tables[1].Rows[0][2].ToString();

                con.Close();
            }
            return new Tuple<IEnumerable<ClassofJoiningList>, string>(_classofJoiningList, colType);
        }

        public List<SelectListItem> GetClassSectionList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<SelectListItem> sectionList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetSectionList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    SelectListItem listItm = new SelectListItem();

                    listItm.Text = ds.Tables[0].Rows[x][0].ToString();
                    listItm.Value = ds.Tables[0].Rows[x][1].ToString();
                    sectionList.Add(listItm);
                }
                con.Close();
            }
            return sectionList;
        }


        public IEnumerable<ReligionList> GetReligionList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<ReligionList> _religionList = new List<ReligionList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetReligionList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ReligionList _religion = new ReligionList();

                    _religion.ReligionId = ds.Tables[0].Rows[x][0].ToString();
                    _religion.ReligionName = ds.Tables[0].Rows[x][1].ToString();
                    _religion.IsDefault = Convert.ToBoolean (ds.Tables[0].Rows[x][2]);
                    _religion.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][3]);
                    _religionList.Add(_religion);
                }
                con.Close();
            }
            return _religionList;
        }

        public IEnumerable<StreamList> GetStreamList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<StreamList> _streamList = new List<StreamList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetStreamList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    StreamList _stream = new StreamList();

                    _stream.SteremId = ds.Tables[0].Rows[x][0].ToString();
                    _stream.StreamName = ds.Tables[0].Rows[x][1].ToString();
                    _stream.IsDefault = Convert.ToBoolean(ds.Tables[0].Rows[x][2]);
                    _stream.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][3]);
                    _streamList.Add(_stream);
                }
                con.Close();
            }
            return _streamList;
        }
        public IEnumerable<CategoryList1> GetCategoryList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<CategoryList1> _categoryList = new List<CategoryList1>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetCategoryList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    CategoryList1 _category = new CategoryList1();

                    _category.CategoryId = ds.Tables[0].Rows[x][0].ToString();
                    _category.CategoryName = ds.Tables[0].Rows[x][1].ToString();
                    _category.IsDefault = Convert.ToBoolean(ds.Tables[0].Rows[x][2]);
                    _category.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][3]);
                    _categoryList.Add(_category);
                }
                con.Close();
            }
            return _categoryList;
        }
        public IEnumerable<SelectListItem> GetSnacksList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<SelectListItem> _snackList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetSnacksList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                if(count>0)
                {
                    _snackList = ds.Tables[0].AsEnumerable().Select( r=> new SelectListItem{

                        Text=r.Field<string>("SnackName"),
                        Value=r.Field<string>("SnackId"),
                        Selected=r.Field<bool>("IsDefault")

                    }).ToList();
                }
                con.Close();
            }
            return _snackList;
        }
        public IEnumerable<SelectListItem> GetDefaulterList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<SelectListItem> _snackList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetDefaulterList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                if (count > 0)
                {
                    _snackList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                    {

                        Text = r.Field<string>("DefaulterName"),
                        Value = r.Field<string>("DefaulterId"),
                        Selected = r.Field<bool>("IsDefault")

                    }).ToList();
                }
                con.Close();
            }
            return _snackList;
        }
        public IEnumerable<HouseList> GetHouseList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<HouseList> _houseList = new List<HouseList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetHouseList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    HouseList _house = new HouseList();
                    _house.HouseId = ds.Tables[0].Rows[x][0].ToString();
                    _house.HouseName = ds.Tables[0].Rows[x][1].ToString();
                    _house.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                    _houseList.Add(_house);
                }
                con.Close();
            }
            return _houseList;
        }




        public List<SelectListItem> GetClubList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<SelectListItem> _clubList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetClubList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                _clubList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClubName"),
                    Value = r.Field<string>("ClubId"),
                }).ToList();


                con.Close();
            }
            return _clubList;
        }








        public IEnumerable<BloodGroupList> GetBloodGroupList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<BloodGroupList> _bloodGroupList = new List<BloodGroupList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetBloodGroupList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    BloodGroupList _bloodgroup = new BloodGroupList();
                    _bloodgroup.BloodGroupId = ds.Tables[0].Rows[x][0].ToString();
                    _bloodgroup.BloodGroupName = ds.Tables[0].Rows[x][1].ToString();
                    _bloodgroup.IsDefault = Convert.ToBoolean(ds.Tables[0].Rows[x][2]);
                    _bloodgroup.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][3]);
                    _bloodGroupList.Add(_bloodgroup);
                }
                con.Close();
            }
            return _bloodGroupList;
        }
        public IEnumerable<ProfessionList> GetProfessionList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<ProfessionList> _professionList = new List<ProfessionList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetProfessionList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ProfessionList _profession = new ProfessionList();
                    _profession.ProfessionId = ds.Tables[0].Rows[x][0].ToString();
                    _profession.ProfessionName = ds.Tables[0].Rows[x][1].ToString();
                    _profession.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                    _professionList.Add(_profession);
                }
                con.Close();
            }
            return _professionList;
        }
        public IEnumerable<ModeofTransportList> GetModeofTransportList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<ModeofTransportList> _modeoftransportList = new List<ModeofTransportList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetModeofTransportList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ModeofTransportList _modeoftransport = new ModeofTransportList();
                    _modeoftransport.ModeofTransportId = ds.Tables[0].Rows[x][0].ToString();
                    _modeoftransport.ModeofTransportName = ds.Tables[0].Rows[x][1].ToString();
                    _modeoftransport.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                    _modeoftransportList.Add(_modeoftransport);
                }
                con.Close();
            }
            return _modeoftransportList;
        }
        public IEnumerable<PickedUpByList> GetPickedUpByList(string sessionid, string classid, string sectionid, string userid, string mode)
        {
            List<PickedUpByList> _pickedUpByList = new List<PickedUpByList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetPickedUpByList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    PickedUpByList _pickedUpBy = new PickedUpByList();
                    _pickedUpBy.PickedUpId = ds.Tables[0].Rows[x][0].ToString();
                    _pickedUpBy.PickedUpBy = ds.Tables[0].Rows[x][1].ToString();
                    _pickedUpBy.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                    _pickedUpByList.Add(_pickedUpBy);
                }
                con.Close();
            }
            return _pickedUpByList;
        }
        public Tuple<int, string> StudentUpdation_CRUD(DataTable dt, string userid,string mode)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_Updation_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FiledName", mode);
                cmd.Parameters.AddWithValue("@Stduent_Update_CRUD_Type", dt);
                cmd.Parameters.AddWithValue("@LoginUserId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }
    }
}
