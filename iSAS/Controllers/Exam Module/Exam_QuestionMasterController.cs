using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Web.Models;
using ISas.Repository.ExamRepository.IRepository;
using ISas.Entities.Exam_Entities;
namespace ISas.Web.Controllers.Exam_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_QuestionMasterController : Controller
    {
        public IExam_QuestionMasterRepo _questionMasterRepo;

        public Exam_QuestionMasterController( IExam_QuestionMasterRepo questionMasterRepo)
        {
            _questionMasterRepo = questionMasterRepo;
        }
        public ActionResult LandingPage()
        {
            List<Exam_QuestionMasterModels> questionList = new List<Exam_QuestionMasterModels>();
            questionList = _questionMasterRepo.QuestionMasterFormLoad();
            return PartialView(questionList);
        }
        [EncryptedActionParameter]
        public ActionResult New( string qpId, string className,string subjectName,string assessmentName,string maxMark)
        {
            var model = new Exam_QuestionMasterModels();
            model.className = className;
            model.subjectName = subjectName;
            model.assessmentName = assessmentName;
            model.qpId = Convert.ToInt32(qpId);
            model.maxMark = Convert.ToInt32(maxMark);
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult Update(string qpId, string className, string subjectName, string assessmentName, string maxMark,string qid)
        {
            return View(_questionMasterRepo.Get_QuestionDetails(qpId, className, subjectName, assessmentName, Convert.ToInt32(maxMark), Convert.ToInt32(qid)));
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Exam_QuestionMaster_CRUD(Exam_QuestionMasterModels model)
        {
            if (ModelState.IsValid)
            {
                Tuple<int, string> res = _questionMasterRepo.QuestionMaster_CRUD(model,Session["UserID"].ToString());
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }

    
}