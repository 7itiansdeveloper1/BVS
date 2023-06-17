using ISas.Entities;
using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_MiscDuesController : Controller
    {
        private IFee_MiscDuesRepo _miscDueRepo;
        public Fee_MiscDuesController(IFee_MiscDuesRepo miscRepo)
        {
            _miscDueRepo = miscRepo;
        }

        // GET: Fee_MiscDues
        [EncryptedActionParameter]
        [CustomAuthorizeFilter("NEW")]
        public ActionResult Fee_MiscDue(string erpNo = null)
        {
            ViewBag.erpNo = erpNo;
            return View();
        }


        public PartialViewResult Fee_MiscDue_ClassWise(string erpNo = null)
        {
            Fee_MiscDueModel model = _miscDueRepo.GetMist_FromLoadDropDownList(Session["SessionId"].ToString(), Session["UserId"].ToString()); //new Fee_MiscDueModel();
            // model.HeadList = _miscDueRepo.GetMistHeadList();
            ViewBag.erpNo = erpNo;
            return PartialView(model);
        }

        public PartialViewResult Fee_MiscDue_StudentWise(string erpNo = null)
        {
            Fee_MiscDueModel model = _miscDueRepo.GetMist_FromLoadDropDownList(Session["SessionId"].ToString(), Session["UserId"].ToString());
            ViewBag.erpNo = erpNo;
            return PartialView(model);
        }

        //public JsonResult GetInstallmentList(string SessionId, string ERPNo)
        //{
        //    return Json(_miscDueRepo.GetMiscInstallmentList(SessionId, ERPNo), JsonRequestBehavior.AllowGet);
        //}

        //public PartialViewResult _MiscDueDetail(string SessionId, string ERPNo)
        //{
        //    Fee_MiscDueModel model = new Fee_MiscDueModel();
        //    model.MiscDueList = _miscDueRepo.GetMiscDueList(SessionId, ERPNo);
        //    return PartialView(model);
        //}

        public PartialViewResult _MiscDueDetail(string ClassId, string SectionId, string StrectureId, string Installment, string FeeHeadId)
        {
            Fee_MiscDueModel model = new Fee_MiscDueModel();
            model.MiscDueStudentDetails = _miscDueRepo.getStudentDetailsListList(Session["SessionId"].ToString(), Session["UserId"].ToString(), ClassId, SectionId, StrectureId, Installment, FeeHeadId); //_miscDueRepo.GetMiscDueList(SessionId, ERPNo);
            return PartialView(model);
        }

        public JsonResult getSectionList(string ClassId, string FeeHeadId)
        {
            return Json(_miscDueRepo.getSectionList(Session["SessionId"].ToString(), Session["UserId"].ToString(), ClassId, FeeHeadId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getStructureList(string ClassId, string SectionId, string FeeHeadId)
        {
            return Json(_miscDueRepo.getStructureList(Session["SessionId"].ToString(), Session["UserId"].ToString(), ClassId, SectionId, FeeHeadId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getInstallmentList(string ClassId, string SectionId, string StrectureId, string FeeHeadId)
        {
            return Json(_miscDueRepo.getInstallmentList(Session["SessionId"].ToString(), Session["UserId"].ToString(), ClassId, SectionId, StrectureId, FeeHeadId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fee_MiscDues_CRUD(Fee_MiscDueModel model) //, StudentSearchModel studentDetails
        {
            if (ModelState.IsValid)
            {
                model.CRUDFor = "SAVE";
                model.SessionId = Session["SessionId"].ToString();
                model.UserId = Session["UserId"].ToString();
                // model.StudentDetails = studentDetails;
                model.ERPNos = string.Join(",", model.MiscDueStudentDetails.Where(r => r.Selected).Select(r => r.ERPNo).ToList());
                Tuple<int, string> res = _miscDueRepo.Fee_MiscDues_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeFilter("CANCEL")]
        public JsonResult Fee_MiscDues_Cancel(Fee_MiscDueModel model)
        {
            model.CRUDFor = "CANCEL";
            model.SessionId = Session["SessionId"].ToString();
            model.UserId = Session["UserId"].ToString();
            Tuple<int, string> res = _miscDueRepo.Fee_MiscDues_CRUD(model);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeFilter("UPDATE")]
        public JsonResult Fee_MiscDues_Update(Fee_MiscDueModel model, StudentSearchModel studentDetails)
        {
            model.CRUDFor = "UPDATE";
            model.SessionId = Session["SessionId"].ToString();
            model.UserId = Session["UserId"].ToString();
            Tuple<int, string> res = _miscDueRepo.Fee_MiscDues_CRUD(model );
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getStudentDueHeadList(string erpNo)
        {
            return Json(_miscDueRepo.getStudentDueHeadList(Session["SessionId"].ToString(), erpNo), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _studentHeadWiseDuesDetails(string erpNo, string headId)
        {
            Fee_MiscDueModel model = new Fee_MiscDueModel();
            model.StudentDuesList = _miscDueRepo.getStudentDuesHeadWise(Session["SessionId"].ToString(), erpNo, headId);
            return PartialView(model);
        }


        public JsonResult Fee_MiscDues_StudentWise_CRUD(Fee_MiscDueModel model)
        {
            if (ModelState.IsValid)
            {
                model.CRUDFor = "SAVE";
                model.SessionId = Session["SessionId"].ToString();
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _miscDueRepo.Fee_MiscDues_StudentWise_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fee_MiscDues_StudentWise_Cancel(string erpNo, string headId, string dueDate, string invRefNo)
        {
            Tuple<int, string> res = _miscDueRepo.Fee_MiscDues_StudentWise_CRUD(erpNo, headId, Session["SessionId"].ToString(), dueDate, invRefNo,Session["UserId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}