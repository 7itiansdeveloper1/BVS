using ISas.Entities;
using ISas.Entities.TransportEntities;
using ISas.Repository.TransportRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Transport
{
    [Authorize]
    [ExceptionHandler]
    public class AvailTransportController : Controller
    {
        private IAvailTransportRepo _availTransportRepo;
        public AvailTransportController(IAvailTransportRepo availTransport)
        {
            _availTransportRepo = availTransport;
        }

        // GET: AvailTransport
        [EncryptedActionParameter]
        public ActionResult AvailTransport(string erpNo = null)
        {
            AvailTransportModel model = new AvailTransportModel();
            model.RouteList = _availTransportRepo.Get_AvailTransportDropDowns("", "GetRouteList", "", erpNo, "");
            ViewBag.erpNo = erpNo;
            return View(model);
        }

        public PartialViewResult _TransportDueDetails(string ERPNo, string SessionId)
        {
            AvailTransportModel model = new AvailTransportModel();
            model.DueList = _availTransportRepo.Get_TransportDueList("", "GetTransportDueDetails", "", ERPNo, SessionId);
            return PartialView(model);
        }

        public JsonResult Get_AvailTransportDetails(string ERPNo, string SessionId)
        {
            AvailTransportModel model = _availTransportRepo.Get_TransportDetails("", "GetStduentStopDetails", "", ERPNo, SessionId);
            if (model == null)
                model = new AvailTransportModel();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_AvailTransportDropDowns(string RouteId, string QueryFor, string StopId, string ERPNo, string SessionId)
        {
            return Json(_availTransportRepo.Get_AvailTransportDropDowns(RouteId, QueryFor, StopId, ERPNo, SessionId), JsonRequestBehavior.AllowGet);
        }


        public JsonResult Trasnport_Transportation_CRUD(AvailTransportModel model, StudentSearchModel studentDetails)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                model.StudentDetails = studentDetails;
                Tuple<int, string> res = _availTransportRepo.Transportation_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AvailTransport_CRUD(AvailTransportModel model, StudentSearchModel studentDetails)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                model.StudentDetails = studentDetails;
                if (model.IsSingleSave)
                {
                    Tuple<int, string> res = _availTransportRepo.AvailTransport_CRUD(model);
                    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    DateTime wefDate = Convert.ToDateTime(model.Date).Date;

                    try
                    {
                        if (model.CRUDFor == "SAVE")
                        {
                            try
                            {
                                foreach (var item in model.DueList.Where(r => r.IsEditable))
                                {
                                    DateTime dueDate = Convert.ToDateTime(item.DueDate).Date;
                                    if (dueDate >= wefDate)
                                    {
                                        AvailTransportModel tempModel = model;

                                        tempModel.Selected_TransportAmount = item.Due;
                                        tempModel.Selected_FeeHeadId = item.HeadID;
                                        tempModel.Selected_DueDate = item.DueDate;
                                        tempModel.Selected_TransRefNo = item.TransRefNo;
                                        _availTransportRepo.AvailTransport_CRUD(tempModel);
                                    }
                                }
                                return Json(new { status = "success", Msg = "Successfully Saved/Updated", Color = "Success" }, JsonRequestBehavior.AllowGet);
                            }
                            catch
                            {
                                return Json(new { status = "success", Msg = "Failed to Save..!", Color = "Warning" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (model.CRUDFor == "CANCEL")
                        {
                            foreach (var item in model.DueList.Where(r => r.WithDrawlTransport))
                            {
                                DateTime dueDate = Convert.ToDateTime(item.DueDate).Date;
                                if (dueDate >= wefDate)
                                {
                                    AvailTransportModel tempModel = model;

                                    tempModel.Selected_TransportAmount = item.Due;
                                    tempModel.Selected_FeeHeadId = item.HeadID;
                                    tempModel.Selected_DueDate = item.DueDate;
                                    tempModel.Selected_TransRefNo = item.TransRefNo;

                                    Tuple<int, string> res = _availTransportRepo.AvailTransport_CRUD(tempModel);
                                    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(new { status = "success", Msg = ex.Message, Color = "Warning" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AvailTransport_Cancel(string SessionId, string ERPNo, string DueDate, string FeeHeadId, string CreditNoteId)
        {
            //string messege = _availTransportRepo.AvailTransport_CRUD(SessionId, ERPNo, DueDate, FeeHeadId, CreditNoteId);
            return Json(new { status = "success", Msg = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_ChargeDetails(string StopId)
        {
            return Json(_availTransportRepo.Get_ChargeDetails(StopId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_VehicleDetails(string VehicleId)
        {
            return Json(_availTransportRepo.Get_VehicleDetails(VehicleId), JsonRequestBehavior.AllowGet);
        }

    }
}