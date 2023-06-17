using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_SyllabusMasterRepo : IAcademic_SyllabusMasterRepo
    {
        public List<Academic_SyllabusMasterModels> Get_Academic_SyllabusMasterList(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SyllabusMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", "SyllabusList");
                cmd.Parameters.AddWithValue("@UserId", UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new Academic_SyllabusMasterModels
                {
                    SyllabusId = r.Field<string>("SyllabusId"),
                    Title = r.Field<string>("Title"),
                    IsActive = r.Field<bool>("IsActive"),
                    UploadDate = r.Field<string>("UploadDate"),
                    UploadedBy = r.Field<string>("UploadedBy"),
                    textEditorPDFFilePath = r.Field<string>("DescriptionReferenctURL"),
                }).ToList();
            }
        }
        public List<Academic_SyllabusMasterModels> Get_Academic_SyllabusMasterList(string ClassSectionId, string ErpNo)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Syllabus_ForStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ERPNo", ErpNo);
                //cmd.Parameters.AddWithValue("@ERPNo", ErpNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new Academic_SyllabusMasterModels
                {
                    SyllabusId = r.Field<string>("SyllabusId"),
                    Title = r.Field<string>("Title"),
                    AttachmentReference = r.Field<string>("AttachmentReference"),
                    UploadDate = r.Field<string>("UploadDate"),
                    UploadedBy = r.Field<string>("UploadBy"),
                }).ToList();
            }
        }

        public Academic_SyllabusMasterModels Get_SyllabusMasterById(string SyllabusId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Academic_SyllabusMasterModels model = null;
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SyllabusMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SyllabusId", SyllabusId);
                cmd.Parameters.AddWithValue("@Mode", "Edit");
                cmd.Parameters.AddWithValue("@UserId", UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model = ds.Tables[3].AsEnumerable().Select(r => new Academic_SyllabusMasterModels
                {
                    SyllabusId = r.Field<string>("SyllabusId"),
                    Title = r.Field<string>("Title"),
                    Discription = r.Field<string>("Discription"),
                    SubjectId = r.Field<string>("SubjectId"),
                    AttachmentReference = r.Field<string>("AttachmentReference"),
                    UploadedBy = r.Field<string>("UploadedBy"),
                    UploadDate = r.Field<string>("UploadDate"),
                    IsActive = r.Field<bool>("IsActive"),
                    ClassId = r.Field<string>("ClassId"),
                    textEditorPDFFilePath = r.Field<string>("DescriptionReferenctURL"),
                }).FirstOrDefault();

                if (model == null)
                    model = new Academic_SyllabusMasterModels();

                model.SubjectList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId"),
                    Selected = r.Field<bool>("IsSelected")
                }).ToList();

                model.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Class"),
                    Value = r.Field<string>("ClassId"),
                    Selected = r.Field<bool>("IsSelected")
                }).ToList();

                model.UploadedByList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("UserName"),
                    Value = r.Field<string>("userId"),
                    Selected = r.Field<bool>("IsSelected")
                }).ToList();
                return model;
            }
        }
        public Academic_SyllabusMasterModels Get_Academic_SyllabusMaster_FormLoad(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Academic_SyllabusMasterModels model = new Academic_SyllabusMasterModels();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SyllabusMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.SubjectList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId"),
                }).ToList();

                model.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Class"),
                    Value = r.Field<string>("ClassId"),
                }).ToList();

                model.UploadedByList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Staff"),
                    Value = r.Field<string>("userId"),
                }).ToList();

                return model;
            }
        }
        public Tuple<int, string> Academic_SyllabusMaster_CRUD(Academic_SyllabusMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SyllabusMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SyllabusId", model.SyllabusId);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@Discription", model.Discription);
                cmd.Parameters.AddWithValue("@AttachmentReference", model.AttachmentReference);
                cmd.Parameters.AddWithValue("@DescriptionReferenctURL", model.textEditorPDFFilePath);
                cmd.Parameters.AddWithValue("@UploadedBy", model.UploadedBy);

                if (!string.IsNullOrEmpty(model.UploadDate))
                    cmd.Parameters.AddWithValue("@UploadDate", Convert.ToDateTime(model.UploadDate).Date);

                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        /// <summary>
        /// This function is used to Update attachment References
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Tuple<int, string> Academic_SyllabusMaster_CRUD(string SyllabusId, string ToBeRemovedAttach, string AllAttachments, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string attachmentRef = "";
                List<string> attchRefList = AllAttachments.Split(',').ToList();
                attchRefList.Remove(ToBeRemovedAttach);
                attachmentRef = string.Join(",", attchRefList);

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SyllabusMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SyllabusId", SyllabusId);
                cmd.Parameters.AddWithValue("@DescriptionReferenctURL", attachmentRef);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
