using ISas.Entities;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Student_NSOController : Controller
    {
        private IStudent_NSORepo _studentNsoRepo;
        public Student_NSOController(IStudent_NSORepo studnetNSO)
        {
            this._studentNsoRepo = studnetNSO;
        }

        public ActionResult Student_NSOLandingPage()
        {
            return View(this._studentNsoRepo.GetAllStudent_NSOList());
        }

        public ActionResult NewStudent_NSO()
        {
            Student_NSOModel model = new Student_NSOModel();
            model.Mode = "SAVE";

            model.DropDownList = this._studentNsoRepo.GetStudent_NSODropDownList(Session["UserID"].ToString(), model.ClassId, model.SectionId);
            model.LastNSONo = model.DropDownList.LastNSONo;
            model.CurrentSessionNSOCount = model.DropDownList.CurrentSessionCount;
            model.SessionId = Session["SessionId"].ToString();
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string NSONo)
        {
            Student_NSOModel model = this._studentNsoRepo.GetStudent_NSONSONo(NSONo);
            model.Mode = "SAVE";
            model.DropDownList = this._studentNsoRepo.GetStudent_NSODropDownList(Session["UserID"].ToString(), model.ClassId, model.SectionId);
            return View(model);
        }

        public JsonResult Student_NSO_CRUD(Student_NSOModel model)
        {
            if (ModelState.IsValid)
            {
                Tuple<int, string> res = _studentNsoRepo.Student_NSO_CRUD(model, Session["UserID"].ToString());
                //return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }

            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

    }
}
