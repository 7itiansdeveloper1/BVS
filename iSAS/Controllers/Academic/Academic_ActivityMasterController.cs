using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Web.Models;
using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_ActivityMasterController : Controller
    {

        private IAcademic_HomeWorkMasterRepo _homeWorkMstRepo;
        public Academic_ActivityMasterController(IAcademic_HomeWorkMasterRepo homeWorkMstRepo)
        {
            _homeWorkMstRepo = homeWorkMstRepo;
        }

        public ViewResult LandingPage()
        {
            return View(_homeWorkMstRepo.Get_Academic_HomeWorkMasterList(Session["SessionId"].ToString(), Session["UserId"].ToString(), "AC"));
        }

        public ViewResult New()
        {
            Academic_HomeWorkMasterModels model = new Academic_HomeWorkMasterModels();
            model.UploadDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.AllStudent = true;
            model.UploadedBy = Session["UserId"].ToString();
            Academic_HomeWorkMasterModels tempModel = _homeWorkMstRepo.Get_Academic_HomeWorkMaster_FormLoad(Session["SessionId"].ToString(), Session["UserId"].ToString());
            if (tempModel != null)
            {
                model.SubjectList = tempModel.SubjectList;
                model.ClassList = tempModel.ClassList;
                model.UploadedByList = tempModel.UploadedByList;
            }
            return View(model);
        }

        [EncryptedActionParameter]
        public ViewResult Update(string HomeWorkId)
        {
            return View(_homeWorkMstRepo.Get_HomeWorkMasterById(HomeWorkId, Session["SessionId"].ToString(), Session["UserId"].ToString()));
        }

        public JsonResult HomeWork_Delete(string Homeworkid)
        {
            Tuple<int, string> res = _homeWorkMstRepo.Academic_HomeWorkMaster_DELETE(Homeworkid);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Academic_HomeWorkMaster_CRUD(Academic_HomeWorkMasterModels model, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.textEditorPDFFilePath))
                {
                    //Removing exesting PDF file, so that not two file having for same Home work
                    FileInfo file = new FileInfo(Server.MapPath("~/" + model.textEditorPDFFilePath));
                    if (file.Exists)//check file exsit or not
                        file.Delete();
                }

                if (files != null)
                {
                    string attachedFilePath = model.AttachmentReference;
                    foreach (HttpPostedFileBase file in files)
                    {
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            string filePath = "Images/ExportFile/" + Guid.NewGuid() + "@" + InputFileName;
                            var ServerSavePath = Path.Combine(Server.MapPath("~/" + filePath));
                            file.SaveAs(ServerSavePath);
                            if (string.IsNullOrEmpty(attachedFilePath))
                                attachedFilePath = filePath;
                            else
                                attachedFilePath += "," + filePath;
                        }
                    }
                    model.AttachmentReference = attachedFilePath;
                }
                if (!string.IsNullOrEmpty(model.Discription))
                {
                    //model.textEditorPDFFilePath
                    string html = "<!DOCTYPE html><html><body>"
                         + model.Discription + "</body></html> ";

                    PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
                    string tempFilePath = "Images/ExportFile/" + Guid.NewGuid() + "@" + "Topic of Activity.pdf";
                    string filePath = Server.MapPath("~/" + tempFilePath);
                    pdf.Save(filePath);
                    model.textEditorPDFFilePath = tempFilePath;
                }
                model.UserId = Session["UserID"].ToString();
                Tuple<int, string> res = _homeWorkMstRepo.Academic_HomeWorkMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Get_StudentListByClassSectionId(string ClassSectionId)
        {
            return Json(_homeWorkMstRepo.Get_StudentListByClassSectionId(ClassSectionId, Session["SessionId"].ToString(), Session["UserId"].ToString()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_HomeWorkMaster_RemoveAttachment(string HomeWorkId, string ToBeRemovedAttach, string AllAttachments)
        {
            Tuple<int, string> res = _homeWorkMstRepo.Academic_HomeWorkMaster_CRUD(HomeWorkId, ToBeRemovedAttach, AllAttachments, Session["UserId"].ToString(),"DELETE");
            if (res.Item1 == 1)
            {
                //Removing exesting PDF file, so that not two file having for same Home work
                FileInfo file = new FileInfo(Server.MapPath("~/" + ToBeRemovedAttach));
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        public ViewResult HomeWork_UserDisplayList(string Category)
        {
            ViewBag.labelName = Category == "HW" ? "Homework" : "Class Notes";
            return View(_homeWorkMstRepo.Get_Academic_HomeWorkMasterList(DateTime.Now.ToShortDateString().Replace("-", "/"), Session["UserId"].ToString(), Session["LoginStudentERPNo"].ToString(), Category));
        }
        [EncryptedActionParameter]
        public ActionResult ResponseList(string homeworkid, string responselistname)
        {
            return View(_homeWorkMstRepo.getResponseList(homeworkid, Session["SessionId"].ToString(), responselistname, Session["UserId"].ToString()));
        }
        [EncryptedActionParameter]
        public ActionResult GetAnswerSheet(string homeworkid, string studentid, string studentname, string homeworkname)
        {
            return View(_homeWorkMstRepo.getAnswersheet(homeworkid, studentid, studentname, homeworkname));
        }
        [HttpPost]
        public void UpdateReviewed(bool isreviewed, string homeworkid, string studentid)
        {
            _homeWorkMstRepo.UpdateReview(homeworkid, studentid, isreviewed);
        }
        [HttpPost]
        public JsonResult Teacher_AnswerSheet_CRUD(answerSheet model, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.textEditorPDFFilePath))
                {
                    //Removing exesting PDF file, so that not two file having for same Home work
                    FileInfo file = new FileInfo(Server.MapPath("~/" + model.textEditorPDFFilePath));
                    if (file.Exists)//check file exsit or not
                        file.Delete();
                }

                if (files != null)
                {
                    string attachedFilePath = model.AttachFiles;
                    foreach (HttpPostedFileBase file in files)
                    {
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            string filePath = "Images/ExportFile/" + Guid.NewGuid() + "@" + InputFileName;
                            var ServerSavePath = Path.Combine(Server.MapPath("~/" + filePath));
                            file.SaveAs(ServerSavePath);
                            if (string.IsNullOrEmpty(attachedFilePath))
                                attachedFilePath = filePath;
                            else
                                attachedFilePath += "," + filePath;
                        }
                    }
                    model.RevertAttachFiles = attachedFilePath;
                }

                Tuple<int, string> res = _homeWorkMstRepo.Teacher_Answersheet_CRUD(model.HomeWorkId, model.ERP, model.RevertAttachFiles, model.Remark, "SAVE");
                //return RedirectToAction("LandingPage", new { Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" });
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }


    }
}