using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class Common_NECNRepo : ICommon_NECNRepo
    {
        public Common_NECN_SelectionGroup GetSelectionGroupDetails(string UploadType,string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Common_NECN_SelectionGroup selectionGroupDetails = new Common_NECN_SelectionGroup();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("Dashboard_Upload_Formload", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UploadType", UploadType);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                selectionGroupDetails.WingList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("WingName"),
                    Value = r.Field<string>("WingID")
                }).ToList();

                selectionGroupDetails.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassId")
                }).ToList();

                selectionGroupDetails.DeptList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DeptName"),
                    Value = r.Field<string>("DeptID")
                }).ToList();

                selectionGroupDetails.StaffList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StaffName"),
                    Value = r.Field<string>("StaffID")
                }).ToList();

                selectionGroupDetails.UploadedByList = ds.Tables[4].AsEnumerable().Select(
                    r => new SelectListItem
                    {
                        Text = r.Field<string>("UserName"),
                        Value = r.Field<string>("UserId")
                    }).ToList();
                selectionGroupDetails.UploadedBy = UserId;
                return selectionGroupDetails;
            }
        }
        public IEnumerable<Common_NECN_LandingModel> LandingPageDetails(string UploadType)
        {
            List<Common_NECN_LandingModel> listofDetails = new List<Common_NECN_LandingModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Dashboard_Upload_LandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UploadType", UploadType);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                listofDetails = ds.Tables[0].AsEnumerable().Select(r => new Common_NECN_LandingModel
                {
                    CreationDate = r.Field<string>("CreationDate"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    UploadID = r.Field<string>("UploadID"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),
                    UploadTitle = r.Field<string>("UploadTitle"),
                }).ToList();
            }
            return listofDetails;
        }

        public Common_NECN_MainModel GetDetailsById(string UploadType, string UploadID,string UserId)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Common_NECN_MainModel model = new Common_NECN_MainModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("Dashboard_Upload_Edit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UploadType", UploadType);
                cmd.Parameters.AddWithValue("@UploadId", UploadID);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.BasicDetails = ds.Tables[0].AsEnumerable().Select(r => new Common_NECN_LandingModel
                {
                    CreationDate = r.Field<string>("CreationDate"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    UploadID = r.Field<string>("UploadID"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),
                    UploadTitle = r.Field<string>("UploadTitle")
                    
                }).FirstOrDefault();

                model.UploadDocList = ds.Tables[1].AsEnumerable().Select(r => new Common_NECN_PhotoUploads
                {
                    UploadID = r.Field<string>("UploadID"),
                    AttachPath = r.Field<string>("AttachPath"),
                    FileName = r.Field<string>("FileName")
                }).ToList();

                model.SelectionGroups.WingList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("WingName"),
                    Value = r.Field<string>("WingID"),
                    Selected = r.Field<bool>("Selected")
                }).ToList();

                model.SelectionGroups.ClassList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassId"),
                    Selected = r.Field<bool>("Selected")
                }).ToList();

                model.SelectionGroups.StudentDetailsList = ds.Tables[4].AsEnumerable().Select(r => new Common_NECN_StudentDetails
                {
                    ERPNo = r.Field<string>("ERPNo"),
                    ClassName = r.Field<string>("ClassName"),
                    StudentName = r.Field<string>("Student"),
                    Selected = r.Field<bool>("Selected")
                }).ToList();

                model.SelectionGroups.DeptList = ds.Tables[5].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DeptName"),
                    Value = r.Field<string>("DeptID"),
                    Selected = r.Field<bool>("Selected")
                }).ToList();

                model.SelectionGroups.StaffList = ds.Tables[6].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StaffName"),
                    Value = r.Field<string>("StaffID"),
                    Selected = r.Field<bool>("Selected")
                }).ToList();

                model.SelectionGroups.UploadedByList = ds.Tables[7].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("UserName"),
                    Value = r.Field<string>("userId"),
                    Selected = r.Field<bool>("IsSelected")
                }).ToList();

                model.WingIds = model.SelectionGroups.WingList.Where(r => r.Selected).Select(r => r.Value).ToArray();
                model.ClassIds = model.SelectionGroups.ClassList.Where(r => r.Selected).Select(r => r.Value).ToArray();
                model.DeprtmentIds = model.SelectionGroups.DeptList.Where(r => r.Selected).Select(r => r.Value).ToArray();
                model.StaffIds = model.SelectionGroups.StaffList.Where(r => r.Selected).Select(r => r.Value).ToArray();
                return model;
            }
        }

        public Tuple<int, string> Common_NECN_CRUD(Common_NECN_MainModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt1 = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("Dashboard_Upload_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                string wingIds = ""; string classIds = ""; string studentIds = "";
                string deptIds = ""; string staffIds = ""; string attachedPaths = "";

                if (model.WingIds != null)
                    wingIds = string.Join(",", model.WingIds);

                if (model.ClassIds != null)
                    classIds = string.Join(",", model.ClassIds);

                if (model.SelectionGroups != null && model.SelectionGroups.StudentDetailsList != null)
                    studentIds = string.Join(",", model.SelectionGroups.StudentDetailsList.Where(r => r.Selected).Select(r => r.ERPNo).ToList());

                if (model.DeprtmentIds != null)
                    deptIds = string.Join(",", model.DeprtmentIds);

                if (model.StaffIds != null)
                    staffIds = string.Join(",", model.StaffIds);

                if (model.UploadDocList != null && model.UploadDocList.Count > 0)
                    attachedPaths = string.Join(",", model.UploadDocList.Select(r => r.AttachPath).ToList());


                if (!string.IsNullOrEmpty(studentIds))
                {
                    wingIds = "";
                    classIds = "";
                }

                cmd.Parameters.AddWithValue("@UploadType", model.UploadType);
                cmd.Parameters.AddWithValue("@UploadId", model.BasicDetails.UploadID);
                cmd.Parameters.AddWithValue("@WingIds", wingIds);
                cmd.Parameters.AddWithValue("@ClassIds", classIds);
                cmd.Parameters.AddWithValue("@StudentIds", studentIds);
                cmd.Parameters.AddWithValue("@DeptIds", deptIds);
                cmd.Parameters.AddWithValue("@StaffIds", staffIds);
                cmd.Parameters.AddWithValue("@UploadTitle", model.BasicDetails.UploadTitle);
                cmd.Parameters.AddWithValue("@UploadDescription", model.BasicDetails.UploadDescription);
                cmd.Parameters.AddWithValue("@UploadStartDate", Convert.ToDateTime(model.BasicDetails.UploadStartDate).Date);
                cmd.Parameters.AddWithValue("@UploadEndDate", Convert.ToDateTime(model.BasicDetails.UploadEndDate).Date);
                cmd.Parameters.AddWithValue("@IsExpiry", model.BasicDetails.IsExpiry);
                cmd.Parameters.AddWithValue("@CreationDate", Convert.ToDateTime(model.BasicDetails.CreationDate));
                cmd.Parameters.AddWithValue("@UserId", model.SelectionGroups.UploadedBy);
                cmd.Parameters.AddWithValue("@AttachmentPath", attachedPaths);
                cmd.Parameters.AddWithValue("@Function", model.Function);
                cmd.Parameters.AddWithValue("@UploadedBy", model.SelectionGroups.UploadedBy);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }


        public int DeleteUploadedDocument(string DocId, string UploadID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoard_DeleteAttachment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UploadId", UploadID);
                cmd.Parameters.AddWithValue("@AttachPath", DocId);

                int effectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return effectedRows;
            }
        }

        public Common_NECN_DisplayListModel GetNewAndOld_NECNList(string UserId, string UserRole, string UploadType)
        {
            Common_NECN_DisplayListModel model = new Common_NECN_DisplayListModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DashBoardUploadHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserRole", UserRole);
                cmd.Parameters.AddWithValue("@UploadType", UploadType);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.NewList = ds.Tables[0].AsEnumerable().Select(r => new Common_NECN_LandingModel
                {
                    UploadTitle = r.Field<string>("UploadTitle"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadID = r.Field<string>("UploadId"),

                    CreationDate = r.Field<string>("CreationDate"),
                    HavingAttachment = r.Field<bool>("HavingAttachment"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    UploadAttachment = r.Field<string>("UploadAttachment"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),

                }).ToList();
                model.OldList = ds.Tables[1].AsEnumerable().Select(r => new Common_NECN_LandingModel
                {
                    UploadTitle = r.Field<string>("UploadTitle"),
                    UploadDescription = r.Field<string>("UploadDescription"),
                    UploadID = r.Field<string>("UploadId"),

                    CreationDate = r.Field<string>("CreationDate"),
                    HavingAttachment = r.Field<bool>("HavingAttachment"),
                    IsExpiry = r.Field<bool>("IsExpiry"),
                    UploadAttachment = r.Field<string>("UploadAttachment"),
                    UploadEndDate = r.Field<string>("UploadEndDate"),
                    UploadStartDate = r.Field<string>("UploadStartDate"),

                }).ToList();
            }
            return model;
        }
    }
}
