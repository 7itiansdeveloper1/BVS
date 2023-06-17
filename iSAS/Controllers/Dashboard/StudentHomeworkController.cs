using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
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

namespace ISas.Web.Controllers.Dashboard
{
    
    [Authorize]
    [ExceptionHandler]
    public class StudentHomeworkController : Controller
    {
        private IStudentHomeworkRepo _studentHomeworkRepo;

        public StudentHomeworkController(IStudentHomeworkRepo studentHomeworkRepo)
        {
            _studentHomeworkRepo = studentHomeworkRepo;
        }

        public ActionResult HomeWork()
        {
            StudentHomeworkModels model = new StudentHomeworkModels();
            string todate = DateTime.Now.ToShortDateString();
            string fromdate= DateTime.Now.AddDays(-180).ToShortDateString();
            model.fromDate = fromdate.Replace("-", "/");
            model.toDate = todate.Replace("-", "/");
            model.subjectList = _studentHomeworkRepo.StudentHomework_FormLoad(Session["UserId"].ToString(), Session["SessionId"].ToString());
           return View(model);
        }

        public ActionResult _GetHomeWorkList(string fromdate ,string todate,string subjectid=null,string status = "ALL",string hwcategory="AC")
        {
            return PartialView(_studentHomeworkRepo.StudentHomework_GetHomeWorkList(Session["UserId"].ToString(), Session["SessionId"].ToString(), fromdate, todate, hwcategory, subjectid,status));
        }

        [EncryptedActionParameter]
        public ActionResult HomeWorkDetail(string homeworkid)
        {
            return View(_studentHomeworkRepo.StudentHomework_HomeWorkDetail(Session["UserId"].ToString(), homeworkid));
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Student_HomeWorkMaster_CRUD(homework model, HttpPostedFileBase[] files)
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
                    model.AttachFiles = attachedFilePath;
                }
                if (!string.IsNullOrEmpty(model.descriptionAttachment))
                {
                    //model.textEditorPDFFilePath
                    string html = "<!DOCTYPE html><html><body>"+ model.descriptionAttachment + "</body></html> ";
                    PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
                    string tempFilePath = "Images/ExportFile/" + Guid.NewGuid() + "@" + "HomeWritternFile.pdf";
                    string filePath = Server.MapPath("~/" + tempFilePath);
                    pdf.Save(filePath);
                    model.textEditorPDFFilePath = tempFilePath;
                }
                model.studentId = Session["UserID"].ToString();
                model.isSubmited = false;
                Tuple<int, string> res = _studentHomeworkRepo.Student_HomeWorkMaster_CRUD(model,"SAVE");
                //return RedirectToAction("LandingPage", new { Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" });
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Student_HomeWorkMaster_RemoveAttachment(string homeworkid, string ToBeRemovedAttach, string allattachments)
        {
            //Tuple<int, string> res = _homeWorkMstRepo.Academic_HomeWorkMaster_CRUD(HomeWorkId, ToBeRemovedAttach, AllAttachments, Session["UserId"].ToString());
            homework model = new homework();
            model.homeWorkId = homeworkid;
            string attachmentRef = "";
            List<string> attchRefList = allattachments.Split(',').ToList();
            attchRefList.Remove(ToBeRemovedAttach);
            attachmentRef = string.Join(",", attchRefList);
            model.AttachFiles = attachmentRef;
            Tuple<int, string> res = _studentHomeworkRepo.Student_HomeWorkMaster_CRUD(model,"DELETE");
            if (res.Item1 == 1)
            {
                //Removing exesting PDF file, so that not two file having for same Home work
                FileInfo file = new FileInfo(Server.MapPath("~/" + ToBeRemovedAttach));
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Student_HomeWorkMaster_SUBMIT(string homeworkid)
        {
            homework model = new homework();
            model.homeWorkId = homeworkid;
            model.studentId = Session["UserId"].ToString();
            Tuple<int, string> res = _studentHomeworkRepo.Student_HomeWorkMaster_CRUD(model, "SUBMIT");
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
    
}
