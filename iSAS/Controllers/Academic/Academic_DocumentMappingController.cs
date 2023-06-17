using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Repository.Academic.Repository;
using ISas.Web.Models;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_DocumentMappingController : Controller
    {
        private IAcademic_DocumentMappingRepo _documentMappingRepo;

        public Academic_DocumentMappingController(IAcademic_DocumentMappingRepo documentMappingRepo)
        {
            _documentMappingRepo = documentMappingRepo;

        }
        public ActionResult DocumentMapping()
        {
            Academic_DocumentMappingModel model = new Academic_DocumentMappingModel();
            model.DepartmentList = _documentMappingRepo.DocumentMapping_Transaction_DepartmentList();
            return View(model);
        }
        public PartialViewResult _DocumentList(string departmentId)
        {
            return PartialView(_documentMappingRepo.DocumentMapping_Transaction_DocumentList(departmentId));
        }

        [HttpPost]
        public JsonResult Academic_DocumentMapping_CRUD(Academic_DocumentMappingModel model)
        {
            if (ModelState.IsValid)
            {
                string docIds = "";
                string departmentId = "";
                departmentId = model.DepartmentId;
                docIds = string.Join(",", model.DocumentList.Where(r => r.Selected).Select(r => r.DocumentId));
                Tuple<int, string> res = _documentMappingRepo.DocumentMapping_CRUD(departmentId,docIds,Session["UserId"].ToString());
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

    }
}