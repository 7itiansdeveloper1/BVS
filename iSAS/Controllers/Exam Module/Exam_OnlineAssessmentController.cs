using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Repository.ExamRepository.IRepository;
using ISas.Entities.Exam_Entities;
using System.IO;

namespace ISas.Web.Controllers.Exam_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_OnlineAssessmentController : Controller
    {

        public IExam_OnlineAssessmentRepo _onlineAssessmentRepo;
        public Exam_OnlineAssessmentController(IExam_OnlineAssessmentRepo onlineAssessmentRepo )
        {
            _onlineAssessmentRepo = onlineAssessmentRepo;
        }
        public ActionResult Exam_StudentOnlineAssessment()
        {
            List<Exam_OnlineAssessmentModels> onlieassessmentList = new List<Exam_OnlineAssessmentModels>();
            onlieassessmentList = _onlineAssessmentRepo.Exam_OnlineAssessment_FormLoad(Session["UserId"].ToString());
            return View(onlieassessmentList);
        }
        public PartialViewResult _Exam_BrowseAssessment(string qpId, string qId, string qNo, string subjectName, string maxMark, string btnclick,string qNature,string ans)
        {
            Exam_QuestionMasterModels Oquestion = new Exam_QuestionMasterModels();
            int qno = Convert.ToInt32(qNo);
            if (btnclick == "NEXT")
            {
                if (qNature == "DI")
                    _onlineAssessmentRepo.Exam_StudentDisriptiveResult_CRUD(Session["UserId"].ToString(), Convert.ToInt32(qId), ans);
                qno = qno + 1;
            }
            else if (btnclick == "PREV")
            {
                if (qNature == "DI")
                    _onlineAssessmentRepo.Exam_StudentDisriptiveResult_CRUD(Session["UserId"].ToString(), Convert.ToInt32(qId), ans);
                qno = qno - 1;
            }
            Oquestion = _onlineAssessmentRepo.Exam_BrowseAssessment(Convert.ToInt32(qpId), qno, subjectName, Convert.ToInt32(maxMark),Session["UserId"].ToString());
            return PartialView(Oquestion);
        }
        public void submit(string qId,string ans)
        {
            _onlineAssessmentRepo.Exam_StudentDisriptiveResult_CRUD(Session["UserId"].ToString(), Convert.ToInt32(qId), ans);
        }
        [EncryptedActionParameter]
        public ActionResult Exam_BrowseAssessment(string qpId, string qNo, string subjectName, string maxMark)
        {
            
            Exam_QuestionMasterModels  Oquestion = new Exam_QuestionMasterModels();
            Oquestion = _onlineAssessmentRepo.Exam_BrowseAssessment(Convert.ToInt32(qpId), Convert.ToInt32(qNo), subjectName, Convert.ToInt32(maxMark), Session["UserId"].ToString());
            Oquestion.defaultbtnclick = "NEXT";
            return View(Oquestion);
        }
        
        
        public void Exam_StudentResult_CRUD(string qId,string ans)
        {
            _onlineAssessmentRepo.Exam_StudentResult_CRUD(Session["UserId"].ToString(),Convert.ToInt32(qId), ans);
        }

        public JsonResult UploadAnswerSheet(string qpId)
        {
            string myFullFileName = "";
            string mySavetoFileName = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    //if (file.ContentLength > 2100000)
                    //{
                    //    return Json("File size is too big..! max file size is allowed is approx 2mb", JsonRequestBehavior.AllowGet);
                    //}
                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg" || ext == ".pdf")
                    {
                        myFullFileName = Path.GetFileName(file.FileName);
                        //myFullFileName = Guid.NewGuid() + myFullFileName;
                        mySavetoFileName = "Images/Answersheet/" + myFullFileName;
                        myFullFileName = "~/Images/Answersheet/" + myFullFileName;
                        file.SaveAs(Server.MapPath(myFullFileName));
                    }
                    else
                    {
                        return Json("Only supports jpg, png, gif, jpeg formats ..!", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            Tuple<int, string> res = _onlineAssessmentRepo.AnswerSheetUpload_CRUD(Convert.ToInt32(qpId), mySavetoFileName, Session["UserId"].ToString(),"UPDATE");
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteAnswersheet(string qpId)

        {
            Tuple<int, string> res = _onlineAssessmentRepo.AnswerSheetUpload_CRUD(Convert.ToInt32(qpId), null, Session["UserId"].ToString(), "DELETE");
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}