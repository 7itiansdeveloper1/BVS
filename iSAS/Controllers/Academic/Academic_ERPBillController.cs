using ISas.Repository.Academic.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_ERPBillController : Controller
    {
        IAcademic_ERPBill_Repo _erpBillRepo;

        public Academic_ERPBillController(IAcademic_ERPBill_Repo erpBillRepo)
        {
            _erpBillRepo = erpBillRepo;
        }

        public ActionResult Index()
        {
            return View(_erpBillRepo.GetERPBill());
        }
        public ActionResult InvoicePrint()
        {
            return View(_erpBillRepo.GetERPBill());
        }

    }
}