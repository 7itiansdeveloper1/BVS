using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_DueSetupController : Controller
    {
        private IFee_DueSetupRepo _dueSetupRepo;
        private IStudentClass _studentClassRepos;
        private IStudentSection _studentSectionRepos;
        public Fee_DueSetupController(IFee_DueSetupRepo dueSetupRepo, IStudentClass ClassRepo, IStudentSection sectionRepo)
        {
            _dueSetupRepo = dueSetupRepo;
            _studentClassRepos = ClassRepo;
            _studentSectionRepos = sectionRepo;
        }

        public ActionResult SummaryPage(string StructId, string StructName)
        {
            return PartialView(_dueSetupRepo.GetClassDueSummeryList(StructId, StructName, Session["SessionId"].ToString()));
        }


        // GET: Fee_DueSetup
        public ActionResult LandingPage(string StructId, string StructName, string ClassId, string SectionId)
        {
            return PartialView(_dueSetupRepo.GetClassDueList(StructId, StructName, ClassId, SectionId, Session["SessionId"].ToString()));
        }

        [CustomAuthorizeFilter("NEW")]
        public ActionResult New(string StructId, string StructName, string ClassId = "", string SectionId = "")
        {
            Fee_DueSetupModels model = new Fee_DueSetupModels();
            model.StructId = StructId;
            model.StructName = StructName;

            var classes = this._studentClassRepos.GetAllClasses(Session["UserID"].ToString());
            if (classes != null && classes.Count() > 0)
                model.ClassList = classes.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.ClassName,
                    Value = r.ClassId,
                }).ToList();

            model.SectionId = SectionId;
            model.ClassId = ClassId;
            model.CopyToClassList = model.ClassList;

            if (!string.IsNullOrEmpty(SectionId))
                model.SectionList = _studentSectionRepos.GetAllSections(ClassId, Session["UserID"].ToString()).OrderBy(r=> r.PrintOrder).Select(r => new SelectListItem
                {
                     Text = r.SecName,
                     Value = r.SecId
                }).ToList();

            Tuple<List<SelectListItem>, List<SelectListItem>> headWithInstallmentList = _dueSetupRepo.GetHeadWithInstallmentList(StructId, Session["SessionId"].ToString());
            model.HeadList = headWithInstallmentList.Item1;
            model.InstallmentList = headWithInstallmentList.Item2;

            return View(model);
        }


        [HttpPost]
        public JsonResult Fee_DueSetup_CRUD(Fee_DueSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _dueSetupRepo.Fee_DueSetup_CRUD(model,Session["SessionId"].ToString());
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeFilter("DELETE")]
        public JsonResult Fee_DueSetup_Delete(int RecordId)
        {
            Tuple<int, string> res = _dueSetupRepo.Fee_DueSetup_CRUD(RecordId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fee_DueSetup_CRUD_CopyToClass(string StructId, string FromClassId, string FromSectionId, string ToClass)
        {
            Tuple<int, string> res = _dueSetupRepo.Fee_DueSetup_CRUD(StructId, FromClassId, FromSectionId, ToClass, Session["UserId"].ToString(), Session["SessionId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}