using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Entities.StaffEntities;
using ISas.Repository.StaffRepository.Repository;
using ISas.Repository.StaffRepository.IRepository;
using ISas.Web.Models;
using System.IO;

namespace ISas.Web.Controllers.Staff
{
    [Authorize]
    [ExceptionHandler]
    public class Staff_DocumentUploadController : Controller
    {
        private IStaff_DocumentUploadRepo _staffDocumentUpload;

        public Staff_DocumentUploadController(Staff_DocumentUploadRepo staffDocumentUpload)
        {
            _staffDocumentUpload = staffDocumentUpload;
        }
        
        public ActionResult Staff_DocumentUploadMaster()
        {
            string staffId = Session["UserId"].ToString();
            Staff_DocumentUploadModels model = new Staff_DocumentUploadModels();
            model = _staffDocumentUpload.Staff_DocumentUpload_Transaction(staffId, "");
            return View (model);
        }

        [HttpPost]
        public JsonResult UploadStaffDocumentFile(string staffId, string docId, string docAlias, string certificationDate, string trainedby)
        //public JsonResult UploadStaffDocumentFile(string id, string staffId, string docId,string docAlias)
        {
            string myFileName = "";
            string myFullFileName = "";
            string mySavetoFileName = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (file.ContentLength > 5100000)
                    {
                        //return Json("File size is too big..! max file size is allowed is approx 2mb", JsonRequestBehavior.AllowGet);
                        return Json(new { status = "Failed", Msg = "File size is too big..! max file size is allowed is approx 4.5mb", Color = "Warning" }, JsonRequestBehavior.AllowGet);
                    }
                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg"|| ext == ".pdf")
                    {
                        myFileName = Path.GetFileName(file.FileName);
                        myFullFileName = Guid.NewGuid() + myFileName;
                        mySavetoFileName = "Images/StaffDocuments/" + myFullFileName;
                        myFullFileName = "~/Images/StaffDocuments/" + myFullFileName;
                        file.SaveAs(Server.MapPath(myFullFileName));
                    }
                    else
                    {
                        return Json("Only supports jpg, png, gif, jpeg formats ..!", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            Tuple<int, string> res = _staffDocumentUpload.StaffDocumentUpload_CRUD(staffId, docId, mySavetoFileName,Session["UserId"].ToString(), docAlias,myFileName, certificationDate, trainedby);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Certificate_DELETE(int docno)
        {
            Tuple<int, string> res = _staffDocumentUpload.StaffDocumentUpload_DELETE(docno);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}