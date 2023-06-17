using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Repository.ExamRepository.IRepository;
using ISas.Web.Models;
using ISas.Entities.Exam_Entities;
namespace ISas.Web.Controllers.Exam_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_LockingController : Controller
    {

        private IExam_LockingRepo _eLockingRepo;
        public Exam_LockingController (IExam_LockingRepo exam_LockingRepo)
        {
            this._eLockingRepo = exam_LockingRepo;
        }
        
        public ActionResult Index()
        {
            Exam_LockingModels model = _eLockingRepo.Exam_LockingModels_Cascading();
            return View(model);
        }
        public PartialViewResult _AssessmentListPartial(string classId)
        {
            List<classAssessmentList> classAssessment = new List<classAssessmentList>();
            classAssessment = _eLockingRepo.Exam_LockingModels_Cascading(classId);
            return PartialView(classAssessment);
        }

        [HttpPost]
        public JsonResult Exam_LockingModels_CRUD(string classId,string assId,string value)
        {
            Tuple<int, string> res = _eLockingRepo.Exam_AssessmentLock_CRUD(classId, assId ,Convert.ToBoolean(value));
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}