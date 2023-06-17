using ISas.Repository.Library.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Dashboard
{
    [Authorize]
    [ExceptionHandler]
    public class eLibrary_StudentController : Controller
    {
        private IeLibraryRepo _eLibraryRepo;
        public eLibrary_StudentController(IeLibraryRepo eLibraryRepo)
        {
            this._eLibraryRepo = eLibraryRepo;
        }

        public ActionResult eLibrary()
        {
            return View(this._eLibraryRepo.eLibrary_Student(Session["UserId"].ToString()));
        }

        public JsonResult CollectDownloadCount(string bookno)
        {
            Tuple<int, string> res = _eLibraryRepo.Download_Count(Convert.ToInt32(bookno), Session["UserId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}