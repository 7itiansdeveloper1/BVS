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
    public class Exam_QuestionPaperController : Controller
    {

        private IExam_QuestionPaperRepo _questionPaperRepos;
        public Exam_QuestionPaperController(IExam_QuestionPaperRepo questionPaperRepos)
        {
            this._questionPaperRepos = questionPaperRepos;
        }

        public ActionResult Exam_QuestionPaperMaster()
        {
            var model = new Exam_QuestionPaperModels();
            model = _questionPaperRepos.QuestionPaper_FormLoad(Session["SessionId"].ToString(), Session["UserId"].ToString());
            return View(model);
        }

        public JsonResult Exam_QuestionPaperMaster_CRUD(string qpId, string classId, string subjectId, string assessmentId, string maxMark,string isActive)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    Tuple<int, string> res = _questionPaperRepos.QuestionPaper_CRUD(Convert.ToInt32(qpId), classId, subjectId, assessmentId, Convert.ToInt32(maxMark), Session["UserId"].ToString(),Convert.ToBoolean(isActive));
                    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { status = "success", Msg = ex.Message, Color = "Warning" }, JsonRequestBehavior.AllowGet);
                }

            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys, Color = "Warning" }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult UploadQuestionPaper(string qpId, HttpPostedFileBase file)

        //{
        //    string myFullFileName = "";
        //    string mySavetoFileName = "";
        //    if (file != null)
        //    {
        //        //HttpPostedFileBase file = files[] Request.Files[0];

        //        if (file != null && file.ContentLength > 0)
        //        {
        //            string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
        //            //if (file.ContentLength > 2100000)
        //            //{
        //            //    return Json("File size is too big..! max file size is allowed is approx 2mb", JsonRequestBehavior.AllowGet);
        //            //}
        //            if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg" || ext == ".pdf")
        //            {
        //                myFullFileName = Path.GetFileName(file.FileName);
        //                //myFullFileName = Guid.NewGuid() + myFullFileName;
        //                mySavetoFileName = "Images/QuestionPaper/" + myFullFileName;
        //                myFullFileName = "~/Images/QuestionPaper/" + myFullFileName;
        //                file.SaveAs(Server.MapPath(myFullFileName));
        //            }
        //            else
        //            {
        //                return Json("Only supports jpg, png, gif, jpeg formats ..!", JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //    }
        //    Tuple<int, string> res = _questionPaperRepos.QuestionPaperUpload_CRUD(Convert.ToInt32(qpId),  mySavetoFileName, Session["UserId"].ToString());
        //    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult UploadQuestionPaper(string qpId)

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
                        mySavetoFileName = "Images/QuestionPaper/" + myFullFileName;
                        myFullFileName = "~/Images/QuestionPaper/" + myFullFileName;
                        file.SaveAs(Server.MapPath(myFullFileName));
                    }
                    else
                    {
                        return Json("Only supports jpg, png, gif, jpeg formats ..!", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            Tuple<int, string> res = _questionPaperRepos.QuestionPaperUpload_CRUD(Convert.ToInt32(qpId), mySavetoFileName, Session["UserId"].ToString(),"UPDATE");
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteQuestionPaper(string qpId)

        {
            Tuple<int, string> res = _questionPaperRepos.QuestionPaperUpload_CRUD(Convert.ToInt32(qpId), null, Session["UserId"].ToString(),"DELETE");
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        [EncryptedActionParameter]
        public ActionResult Answersheets(string className,string assessmentName,string subjectName,string qpId )
        {
            var model = new Exam_AnswersheetModels();
            model = _questionPaperRepos.Exam_Answersheet_TRANSACTION(Session["SessionId"].ToString(), Session["UserId"].ToString(), Convert.ToInt32(qpId), className, assessmentName, subjectName);
            return View(model);
        }
    }
    
}