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
    public class Academic_HomeWorkMasterRepo : IAcademic_HomeWorkMasterRepo
    {
        public List<Academic_HomeWorkMasterModels> Get_Academic_HomeWorkMasterList(string SessionId, string UserId, string CategoryId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HomeWorkMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "HomeWorkList");
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@HomeWorkCategory", CategoryId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new Academic_HomeWorkMasterModels
                {
                    HomeWorkId = r.Field<string>("HomeWorkId"),
                    Title = r.Field<string>("Title"),
                    CategoryId = r.Field<string>("HomeWorkCategory"),
                    UploadDate = r.Field<string>("UploadDate"),
                    SubmissionDate = r.Field<string>("SubmissionDate"),
                    SubjectName = r.Field<string>("SubjectName"),
                    ClassName = r.Field<string>("ClassName"),
                    CreatedBy = r.Field<string>("CreatedBy"),
                    //ClassSectionId = r.Field<string>("ClassSectionId") == null ? null : r.Field<string>("ClassSectionId").Split(','),  //.Select(r => r.Value).ToArray();,
                    textEditorPDFFilePath = r.Field<string>("DiscriptionReference"),               
                    UploadedBy = r.Field<string>("UploadedBy"),
                }).ToList();
            }
        }
        public List<Academic_HomeWorkMasterModels> Get_Academic_HomeWorkMasterList(string Date, string UserId, string ERPNo, string Category)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HomeWork_ForStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //  if (!string.IsNullOrEmpty(Date))
                cmd.Parameters.AddWithValue("@Date", null);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@HWcategory", Category);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new Academic_HomeWorkMasterModels
                {
                    Title = r.Field<string>("Title"),
                    AttachmentReference = r.Field<string>("AttachmentReference"),
                    textEditorPDFFilePath = r.Field<string>("DiscriptionReference"),
                    UploadDate = r.Field<string>("HomeWorkDate"),
                    SubmissionDate = r.Field<string>("SubmissionDate"),
                }).ToList();
            }
        }

        public Academic_HomeWorkMasterModels Get_HomeWorkMasterById(string HomeWorkId, string SessionId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Academic_HomeWorkMasterModels model = null;
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HomeWorkMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HomeWorkId", HomeWorkId);
                cmd.Parameters.AddWithValue("@Mode", "Edit");
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model = ds.Tables[4].AsEnumerable().Select(r => new Academic_HomeWorkMasterModels
                {
                    HomeWorkId = r.Field<string>("HomeWorkId"),
                    Title = r.Field<string>("Title"),
                    CategoryId = r.Field<string>("HomeWorkCategory"),
                    UploadDate = r.Field<string>("UploadDate"),
                    SubmissionDate = r.Field<string>("SubmissionDate"),
                    SubjectId = r.Field<string>("SubjectId"),
                    ClassSectionId = r.Field<string>("ClassSectionId") == null ? null : r.Field<string>("ClassSectionId").Split(','),  //.Select(r => r.Value).ToArray();,
                    AllStudent = r.Field<bool>("AllStudent"),
                    AttachmentReference = r.Field<string>("AttachmentReference"),
                    Discription = r.Field<string>("Discription"),
                    UploadedBy = r.Field<string>("UploadedBy"),
                    textEditorPDFFilePath = r.Field<string>("DiscriptionReference"),
                    // StudentId = r.Field<string>("StudentId"),
                }).FirstOrDefault();

                if (model == null)
                    model = new Academic_HomeWorkMasterModels();

                model.SubjectList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId"),
                    Selected = r.Field<bool>("IsSelected")
                }).ToList();

                model.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Class"),
                    Value = r.Field<string>("ClassSectionId"),
                    Selected = r.Field<bool>("IsSelected")
                }).ToList();

                model.StudentList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Student"),
                    Value = r.Field<string>("ERPNo"),
                    Selected = r.Field<bool>("IsSelected")
                }).ToList();

                model.UploadedByList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("UserName"),
                    Value = r.Field<string>("userId"),
                    Selected = r.Field<bool>("IsSelected")
                }).ToList();

                model.StudentId = model.StudentList.Where(r => r.Selected).Select(r => r.Value).ToArray();

                return model;
            }
        }

        public Academic_HomeWorkMasterModels Get_Academic_HomeWorkMaster_FormLoad(string SessionId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Academic_HomeWorkMasterModels model = new Academic_HomeWorkMasterModels();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HomeWorkMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
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
                    //Text = r.Field<string>("Class"),
                    Text = r.Field<string>("ClassNameWithSection"),
                    Value = r.Field<string>("ClassSectionId"),
                }).ToList();

                model.UploadedByList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("UserName"),
                    Value = r.Field<string>("userId"),
                }).ToList();

                return model;
            }
        }
        public List<SelectListItem> Get_StudentListByClassSectionId(string ClassSectionId, string SessionId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HomeWorkMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "GetStudentList");
                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("Student"),
                    Value = r.Field<string>("ERPNo"),
                    Selected = r.Field<bool>("IsSelected")
                }).ToList();
            }
        }

        public Tuple<int, string> Academic_HomeWorkMaster_CRUD(Academic_HomeWorkMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HomeWorkMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HomeWorkId", model.HomeWorkId);
                cmd.Parameters.AddWithValue("@HomeWorkCategory", model.CategoryId);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Discription", model.Discription);

                if (string.IsNullOrEmpty(model.SubjectId))
                    cmd.Parameters.AddWithValue("@SubjectId", "-1");
                else
                    cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);

                //cmd.Parameters.AddWithValue("@ClassSectionId", model.ClassSectionId);

                if (model.ClassSectionId != null && model.ClassSectionId.Count() > 0)
                    cmd.Parameters.AddWithValue("@ClassSectionId", string.Join(",", model.ClassSectionId));


                cmd.Parameters.AddWithValue("@AllStudent", model.AllStudent);

                if (model.StudentId != null && model.StudentId.Count() > 0)
                    cmd.Parameters.AddWithValue("@StudentId", string.Join(",", model.StudentId));

                cmd.Parameters.AddWithValue("@AttachmentReference", model.AttachmentReference);
                cmd.Parameters.AddWithValue("@DiscriptionReference", model.textEditorPDFFilePath);

                cmd.Parameters.AddWithValue("@UploadedBy", model.UploadedBy);

                if (!string.IsNullOrEmpty(model.UploadDate))
                    cmd.Parameters.AddWithValue("@UploadDate", Convert.ToDateTime(model.UploadDate).Date);

                if (!string.IsNullOrEmpty(model.SubmissionDate))
                    cmd.Parameters.AddWithValue("@SubmissionDate", Convert.ToDateTime(model.SubmissionDate).Date);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<int, string> Academic_HomeWorkMaster_DELETE(string HomeWorkId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HomeWorkMaster_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HomeWorkId", HomeWorkId);
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
        public Tuple<int, string> Academic_HomeWorkMaster_CRUD(string HomeWorkId, string ToBeRemovedAttach, string AllAttachments, string UserId,string mode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string attachmentRef = "";
                List<string> attchRefList = AllAttachments.Split(',').ToList();
                attchRefList.Remove(ToBeRemovedAttach);
                attachmentRef = string.Join(",", attchRefList);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HomeWorkMaster_UpdateAttachment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HomeWorkId", HomeWorkId);
                cmd.Parameters.AddWithValue("@AttachmentReferenceId", attachmentRef);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Mode", mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Academic_HomeWorkMasterModels getResponseList(string homeworkid, string sessionid, string responselistname,string userid)
        {
            Academic_HomeWorkMasterModels model = new Academic_HomeWorkMasterModels();
            model.responseListName = responselistname;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Teacher_StudentHomework_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "ResponseList");
                cmd.Parameters.AddWithValue("@homeworkId", homeworkid);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@userId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.responseList = ds.Tables[0].AsEnumerable().Select(r => new response
                {
                    SNo = r.Field<Int64>("SNo"),
                    HomeWorkId = r.Field<string>("HomeWorkId"),
                    ERP = r.Field<string>("ERP"),
                    Student = r.Field<string>("Student"),
                    Class = r.Field<string>("Class"),
                    SubmitDate = r.Field<string>("SubmitDate"),
                    havingAttachment = r.Field<string>("havingAttachment"),
                    havingLink = r.Field<string>("havingLink"),
                    isReviewed= r.Field<bool>("isReviewed"),
                }).ToList();
            }
            return model;
        }
        public answerSheet getAnswersheet(string homeworkid, string studentid, string studentname, string homeworkname)
        {
            answerSheet obj = new answerSheet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Teacher_StudentHomework_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "GetAnswerSheet");
                cmd.Parameters.AddWithValue("@homeworkId", homeworkid);
                cmd.Parameters.AddWithValue("@studentId", studentid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    obj.ERP = ds.Tables[0].Rows[0]["ERPNo"].ToString();
                    obj.HomeWorkId = ds.Tables[0].Rows[0]["HomeWorkId"].ToString();
                    obj.AttachFiles = ds.Tables[0].Rows[0]["AttachFiles"].ToString();
                    obj.VedioLink1 = ds.Tables[0].Rows[0]["VedioLink1"].ToString();
                    obj.VedioLink2 = ds.Tables[0].Rows[0]["VedioLink2"].ToString();
                    obj.RevertAttachFilesPath = ds.Tables[0].Rows[0]["RevertAttachFile"].ToString();
                    obj.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                    obj.Student = studentname;
                    obj.homeworkname = homeworkname;
                    obj.isReviewed = Convert.ToBoolean(ds.Tables[0].Rows[0]["isReviewed"]);
                    obj.isSubmited = Convert.ToBoolean(ds.Tables[0].Rows[0]["isSubmited"]);
                }
            }
            return obj;
        }
        public void UpdateReview(string homeworkid, string studentid, bool isreviewed)
        {
            answerSheet obj = new answerSheet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Teacher_StudentHomework_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "Reviewed");
                cmd.Parameters.AddWithValue("@homeworkId", homeworkid);
                cmd.Parameters.AddWithValue("@studentId", studentid);
                cmd.Parameters.AddWithValue("@isReviewed", isreviewed);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }

        }
        public void UpdateSubmitStatus(string homeworkid, string studentid, bool issubmitted)
        {
            answerSheet obj = new answerSheet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Teacher_StudentHomework_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "SubmitStatus");
                cmd.Parameters.AddWithValue("@homeworkId", homeworkid);
                cmd.Parameters.AddWithValue("@studentId", studentid);
                cmd.Parameters.AddWithValue("@issubmitted", issubmitted);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }

        }

        public Tuple<int, string> Teacher_Answersheet_CRUD(string homeworkid, string studentid, string revertAttachments, string remark,string mode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string attachmentRef = "";
                if (revertAttachments != null)
                {
                    List<string> attchRefList = revertAttachments.Split(',').ToList();
                    attachmentRef = string.Join(",", attchRefList);
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Teacher_Answersheet_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HomeWorkId", homeworkid);
                cmd.Parameters.AddWithValue("@StudentId", studentid);
                cmd.Parameters.AddWithValue("@RevertAttachmentReference", attachmentRef);
                cmd.Parameters.AddWithValue("@Remark", remark);
                cmd.Parameters.AddWithValue("@mode", mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

    }
}