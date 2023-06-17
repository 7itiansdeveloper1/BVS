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
    public class Examination_ProfileEntryController : Controller
    {
        private IExamination_ProfileEntryRepo _profileEntryRepo;
        public Examination_ProfileEntryController(IExamination_ProfileEntryRepo profileEntryRepo)
        {
            _profileEntryRepo = profileEntryRepo;
        }
        // GET: Examination_RemarksEntry
        public ViewResult Examination_ProfileEntry_Search()
        {
            return View(new Examination_ProfileEntryModels { ExamList = _profileEntryRepo.Get_ProfileEntryDropDowns(Session["SessionId"].ToString(), Session["UserId"].ToString(), null, "FormLoad", null) });
        }

        public JsonResult GetProfileEntryDropDowns(string ClassId, string Mode, string ExamId)
        {
            return Json(_profileEntryRepo.Get_ProfileEntryDropDowns(Session["SessionId"].ToString(), Session["UserId"].ToString(),ClassId, Mode, ExamId), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _ProfileEntry_StudentDetails(string ClassId, string SectionId, string ExamId)
        {
            return PartialView(_profileEntryRepo.Get_ProfileEntryStudentDetails(Session["SessionId"].ToString(), Session["UserId"].ToString(), ClassId, SectionId, ExamId));
        }

        [HttpPost]
        public JsonResult Examination_ProfileEntry_CRUD(Examination_ProfileEntryModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.SessionId = Session["SessionId"].ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add("ERPNo");
                dt.Columns.Add("Hgt");
                dt.Columns.Add("Wgt");
                dt.Columns.Add("Attendance");
                dt.Columns.Add("PTMAttendance");
                for (int x = 0; x < model.StudentDetails.Count; x++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = model.StudentDetails[x].ERPNo.Trim();
                    row[1] = model.StudentDetails[x].Hgt;
                    row[2] = model.StudentDetails[x].Wgt;
                    row[3] = model.StudentDetails[x].Attendance + "/" + model.StudentDetails[x].Attendance1;
                    row[4] = model.StudentDetails[x].PTMAttendance + "/" + model.StudentDetails[x].PTMAttendance1;
                    dt.Rows.Add(row);
                }
                Tuple<int, string> res = _profileEntryRepo.Examination_ProfileEntry_CRUD(dt, model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}