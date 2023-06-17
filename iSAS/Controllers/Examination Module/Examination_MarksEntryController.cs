using ISas.Entities.Examination_Entities;
using ISas.Repository.ExaminationRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Examination_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Examination_MarksEntryController : Controller
    {
        private IExamination_MarksEntryRepo _marksEntryRepo;
        public Examination_MarksEntryController(IExamination_MarksEntryRepo marksEntry)
        {
            _marksEntryRepo = marksEntry;
        }

        // GET: Examination_MarksEntry
        public ActionResult Examination_MarksEntry_Filter()
        {
            return View(_marksEntryRepo.GetMarksEntryFormLoadDetails(Session["UserID"].ToString()));
        }

        public JsonResult GetClassList(string SessionId, string ExamId)
        {
            return Json(_marksEntryRepo.GetClassList(SessionId, Session["UserID"].ToString(), ExamId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSectionList(string SessionId, string ExamId, string ClassId)
        {
            return Json(_marksEntryRepo.GetSectionList(SessionId, Session["UserID"].ToString(), ExamId, ClassId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubjectList(string SessionId, string ExamId, string ClassId,string SectionId)
        {
            return Json(_marksEntryRepo.GetSubjectList(SessionId, Session["UserID"].ToString(), ExamId, ClassId, SectionId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssessmentList(string SessionId, string ExamId, string ClassId, string SectionId, string SubjectId)
        {
            return Json(_marksEntryRepo.GetAssismentList(SessionId, Session["UserID"].ToString(), ExamId, ClassId, SectionId, SubjectId), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _MarksEntryFrom(Examination_MarksEntryModels param)
        {
            param.UserId = Session["UserID"].ToString();
            return PartialView(_marksEntryRepo.GetStudentDetails(param));
        }

        [HttpPost]
        public JsonResult Examination_MarksEntry_CRUD(Examination_MarksEntryModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add("ERP");
                dt.Columns.Add("IsAbsent");
                dt.Columns.Add("IsML");
                dt.Columns.Add("IsExempt");
                dt.Columns.Add("Mark");
                dt.Columns.Add("Grade");

                for (int x = 0; x < model.StudentsMarkList.Count; x++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = model.StudentsMarkList[x].ERPNo.Trim();
                    row[1] = model.StudentsMarkList[x].IsAbsent;
                    row[2] = model.StudentsMarkList[x].IsML;
                    row[3] = model.StudentsMarkList[x].IsExempt;
                    row[4] = model.StudentsMarkList[x].MarkObtained ?? "";
                    row[5] = model.StudentsMarkList[x].Grade ?? "";
                    dt.Rows.Add(row);
                }
                Tuple<int, string> res = _marksEntryRepo.Examination_MarksEntry_CRUD(dt, model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}