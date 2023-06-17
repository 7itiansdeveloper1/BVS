using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_InvoiceCreationController : Controller
    {
        private IStudentClass _studentClassRepos;
        private IFee_InvoiceCreationRepo _invoiceCreationRepo;
        private IStudentSection _studentSectionRepos;
        public Fee_InvoiceCreationController(IStudentClass classRepo, IFee_InvoiceCreationRepo invoiceCreationRepo, IStudentSection sectionRepo)
        {
            _studentClassRepos = classRepo;
            _invoiceCreationRepo = invoiceCreationRepo;
            _studentSectionRepos = sectionRepo;
        }

        // GET: Fee_InvoiceCreation
        [CustomAuthorizeFilter("NEW")]
        public ActionResult InvoiceCreation(string ClassId = "", string SectionId = "")
        {
            Fee_InvoiceCreationModels model = new Fee_InvoiceCreationModels();
            var classes = _studentClassRepos.GetAllClasses(Session["UserID"].ToString());
            if (classes != null && classes.Count() > 0)
                model.ClassList = classes.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.ClassName,
                    Value = r.ClassId,
                }).ToList();

            if (!string.IsNullOrEmpty(SectionId))
                model.SectionList = _studentSectionRepos.GetAllSections(ClassId, Session["UserID"].ToString()).OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.SecName,
                    Value = r.SecId
                }).ToList();


            model.ClassId = ClassId;
            model.SectionId = SectionId;
            model.FeeCategoryList = _invoiceCreationRepo.GetFeeCategoryList();
            return View(model);
        }

        public PartialViewResult _InvoiceDetails(string ClassId, string SectionId)
        {
            Fee_InvoiceCreationModels model = new Fee_InvoiceCreationModels();
            model.FeeInvoiceList = _invoiceCreationRepo.GetInvoiceDetailsList(ClassId, SectionId, Session["SessionId"].ToString());
            return PartialView(model);
        }

        public PartialViewResult _FeeStrectureDetails(string ClassId, string SectionId, string StructId)
        {
            return PartialView(_invoiceCreationRepo.GetFeeStrectureDetailsList(ClassId, SectionId, StructId, Session["SessionId"].ToString()));
        }


        //[HttpPost]
        public JsonResult Fee_InvoiceCreation_CRUD(Fee_InvoiceCreationModels model)
        {
            if (ModelState.IsValid)
            {


                if (model.IsSingleSave)
                {
                    Tuple<int, string> res = _invoiceCreationRepo.Fee_InvoiceCreation_CRUD(Session["SessionId"].ToString(), model.SelectERPNoId, model.FeeCategoryId,Session["UserId"].ToString());
                    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        foreach (var item in model.FeeInvoiceList.Where(r => r.IsCreateInvoice))
                        {
                            Tuple<int, string> res = _invoiceCreationRepo.Fee_InvoiceCreation_CRUD(Session["SessionId"].ToString(), item.ERPNo, model.FeeCategoryId, Session["UserId"].ToString());
                        }
                        return Json(new { status = "success", Msg = "Saved Successfully", Color = "Success" }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { status = "success", Msg = ex.Message, Color = "Warning" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys , Color = "Warning" }, JsonRequestBehavior.AllowGet);
        }


        #region Cancel Invoice
        //[EncryptedActionParameter]
        public PartialViewResult CancelInvoice_InvoiceList(string ERPNo)
        {
            ViewBag.feeConcList = _invoiceCreationRepo.ConcessionCategoryList(ERPNo);
            ViewBag.ERPNo = ERPNo;
            return PartialView(_invoiceCreationRepo.CancelInvoice_InvoiceList(ERPNo,Session["SessionId"].ToString()));
        }

        [HttpPost]
        public JsonResult CancelInvoice_CRUD(CancelInvoice_InvoiceDetailsModel model, string Conc_Category)
        {
            if (ModelState.IsValid)
            {
                Tuple<int, string> res = _invoiceCreationRepo.CancelInvoice_CRUD(model.ERPNo, model.TransRefNo, Conc_Category, Session["UserId"].ToString());
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys, Color = "Warning" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}