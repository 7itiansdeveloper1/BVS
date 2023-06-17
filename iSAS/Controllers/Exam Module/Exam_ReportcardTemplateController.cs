using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Repository.ExamRepository.IRepository;
using ISas.Entities.Exam_Entities;
using ISas.Web.Models;

namespace ISas.Web.Controllers.Exam_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_ReportcardTemplateController : Controller
    {
        private IExamReportcardTemplateRepo _examReportcardTemplateRepo;

        public Exam_ReportcardTemplateController(IExamReportcardTemplateRepo examReportcardTemplateRepo)
        {
            _examReportcardTemplateRepo = examReportcardTemplateRepo;
        }
        public ActionResult ReportcardTemplate()
        {
            ExamReportcardTemplateModels model = new ExamReportcardTemplateModels();
            model = _examReportcardTemplateRepo.ExamReportcardTemplate_Transaction_FormLoad();
            return View(model);
        }
        public PartialViewResult _ReportcardTemplateList(string examId)
        {
            return PartialView(_examReportcardTemplateRepo.ExamReportcardTemplate_Transaction_GetTemplateList(examId));
        }
        [HttpPost]
        public JsonResult ReportcardTemplate_CRUD(ExamReportcardTemplateModels model)
        {
            Tuple<int, string> res = this._examReportcardTemplateRepo.ExamReportcardTemplate_CRUD(model);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TermLock_CRUD(string classId, string examId, string value)
        {
            Tuple<int, string> res = this._examReportcardTemplateRepo.Exam_TermLock_CRUD(classId, examId, Convert.ToBoolean(value));
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}