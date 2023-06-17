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
    public class Examination_RemarksEntryController : Controller
    {
        private IExamination_RemarksEntryRepo _remarksEntryRepo;
        public Examination_RemarksEntryController(IExamination_RemarksEntryRepo remarksEntryRepo)
        {
            _remarksEntryRepo = remarksEntryRepo;
        }
        // GET: Examination_RemarksEntry
        public ViewResult Examination_RemarksEntry_Search()
        {
            return View(new Examination_RemarksEntryModels { ExamList = _remarksEntryRepo .Get_RemarksEntryDropDowns(Session["SessionId"].ToString(), Session["UserId"].ToString(), null, "FormLoad", null) });
        }
        public JsonResult GetRemarksEntryDropDowns(string ClassId, string Mode, string ExamId)
        {
            return Json( _remarksEntryRepo.Get_RemarksEntryDropDowns(Session["SessionId"].ToString(), Session["UserId"].ToString(),ClassId, Mode, ExamId), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _RemarksEntry_StudentDetails(string ClassId, string SectionId, string ExamId)
        {
            return PartialView(_remarksEntryRepo.Get_RemarksEntryStudentDetails(Session["SessionId"].ToString(), Session["UserId"].ToString(), ClassId, SectionId, ExamId));
        }
        [HttpPost]
        public JsonResult Examination_RemarksEntry_CRUD(Examination_RemarksEntryModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.SessionId = Session["SessionId"].ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add("ERPNo");
                dt.Columns.Add("RemarkId");
                for (int x = 0; x < model.StudentDetails.Count; x++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = model.StudentDetails[x].ERPNo.Trim();
                    row[1] = model.StudentDetails[x].RemarkId;
                    dt.Rows.Add(row);
                }
                Tuple<int, string> res = _remarksEntryRepo.Examination_RemarksEntry_CRUD(dt, model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}