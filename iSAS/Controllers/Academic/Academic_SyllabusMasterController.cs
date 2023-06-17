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
    public class Academic_SyllabusMasterController : Controller
    {
        private IAcademic_SyllabusMasterRepo _syllabusMstRepo;
        public Academic_SyllabusMasterController(IAcademic_SyllabusMasterRepo syllabusMstRepo)
        {
            _syllabusMstRepo = syllabusMstRepo;
        }

        public ViewResult LandingPage(string Msg, string Color)
        {
            ViewBag.Msg = Msg; ViewBag.Color = Color;
            return View(_syllabusMstRepo.Get_Academic_SyllabusMasterList(Session["UserId"].ToString()));
        }

        public ViewResult New()
        {
            Academic_SyllabusMasterModels model = new Academic_SyllabusMasterModels();
            model.UploadDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.UploadedBy = Session["UserId"].ToString();
            model.IsActive = true;
            Academic_SyllabusMasterModels tempModel = _syllabusMstRepo.Get_Academic_SyllabusMaster_FormLoad(Session["UserId"].ToString());
            if (tempModel != null)
            {
                model.SubjectList = tempModel.SubjectList;
                model.ClassList = tempModel.ClassList;
                model.UploadedByList = tempModel.UploadedByList;
            }
            return View(model);
        }

        [EncryptedActionParameter]
        public ViewResult Update(string SyllabusId)
        {
            return View(_syllabusMstRepo.Get_SyllabusMasterById(SyllabusId, Session["UserId"].ToString()));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Academic_SyllabusMaster_CRUD(Academic_SyllabusMasterModels model, HttpPostedFileBase[] files)
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
                    string tempFilePath = "Images/ExportFile/" + Guid.NewGuid() + "@" + "Description.pdf";
                    string filePath = Server.MapPath("~/" + tempFilePath);
                    pdf.Save(filePath);
                    model.textEditorPDFFilePath = tempFilePath;
                }

                model.UserId = Session["UserID"].ToString();
                Tuple<int, string> res = _syllabusMstRepo.Academic_SyllabusMaster_CRUD(model);
                return RedirectToAction("LandingPage", new { Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" });
                // return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            return View(model.ReturnViewName, model);
        }


        public JsonResult Academic_SyllabusMaster_RemoveAttachment(string SyllabusId, string ToBeRemovedAttach, string AllAttachments)
        {
            Tuple<int, string> res = _syllabusMstRepo.Academic_SyllabusMaster_CRUD(SyllabusId, ToBeRemovedAttach, AllAttachments, Session["UserId"].ToString());
            if (res.Item1 == 1)
            {
                //Removing exesting PDF file, so that not two file having for same Home work
                FileInfo file = new FileInfo(Server.MapPath("~/" + ToBeRemovedAttach));
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Syllabus_UserDisplayList(string ClassSectionId)
        {
            return View(_syllabusMstRepo.Get_Academic_SyllabusMasterList(ClassSectionId, Session["LoginStudentERPNo"].ToString()));
        }
    }
}