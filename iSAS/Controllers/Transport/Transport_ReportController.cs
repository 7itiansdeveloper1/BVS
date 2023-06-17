using ISas.Entities.TransportEntities;
using ISas.Repository.TransportRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Transport
{
    [Authorize]
    [ExceptionHandler]
    public class Transport_ReportController : Controller
    {
        #region Report Type 1
        private ITransport_ReportRepo _transportReportRepo;
        public Transport_ReportController(ITransport_ReportRepo transportReportRepo)
        {
            _transportReportRepo = transportReportRepo;
        }
        // GET: Transport_Report
        public ActionResult Transport_Report_RouteDetails()
        {
            ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Images/System/loginBgleft.jpg"))));
            return View(_transportReportRepo.Transport_Report_RouteDetails());
        }
        // GET: Transport_Report
        public PartialViewResult _Transport_Report_BusStudentDetails(string VehID, string RouteName, string DriverName)
        {
            ViewBag.selectedRoute = RouteName;
            ViewBag.selectedDriver = DriverName;
            return PartialView(_transportReportRepo.Transport_Report_BusStudentDetails(VehID));
        }
        #endregion END Report Type 1
        #region Report Type 2
        public ViewResult Transport_ReportFilter()
        {
            return View(_transportReportRepo.Transport_Report_FormLoad());
        }

        public PartialViewResult _TransportReportDetails(Transport_ReportModel paramVal)
        {
            paramVal.SessionId = Session["SessionId"].ToString();
            Transport_ReportModel model =  _transportReportRepo.Transport_ReportDetails(paramVal);
            model.ReportId = paramVal.ReportId;

            //if (System.IO.File.Exists(Server.MapPath("~/" + model.ReportHeaders.LogoURL + "")))
            //    ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/" + model.ReportHeaders.LogoURL + ""))));
            return PartialView(model);
        }

        public JsonResult GetVehicleDetails(string RouteId)
        {
            return Json(_transportReportRepo.GetRouteOrVehicleList(RouteId, "GetVehicleListByRouteId"), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}