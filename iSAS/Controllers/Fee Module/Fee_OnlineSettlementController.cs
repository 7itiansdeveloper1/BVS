using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Repository.FeeModuleRepo.Repository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_OnlineSettlementController : Controller
    {
        private IFee_OnlineSettlementRepo _onlineSettlementRepo;

        int settlementDateindex = 36;
        int settlementReferenceindex = 26;
        public Fee_OnlineSettlementController( Fee_OnlineSettlementRepo onlineSettlementRepo)
        {
            _onlineSettlementRepo = onlineSettlementRepo;
        }

        [HttpGet]
        public ActionResult Index(string daterange="")
        {
            string fromdate="", todate="";
            int l = daterange.Length;
            if (l>0)
            {
                fromdate = daterange.Substring(0, 10);
                todate = daterange.Substring(13, 10);

            }
            Fee_OnlineSettlementEntities model = _onlineSettlementRepo.OnlineSettlement_Transaction(fromdate, todate, Session["SessionId"].ToString());
            model.dateRange = daterange;
            //return PartialView(model);
            return View(model);
        }
        public PartialViewResult _GetOnlineSettlementList(string daterange = "")
        {
            string fromdate = "", todate = "";
            int l = daterange.Length;
            if (l > 0)
            {
                fromdate = daterange.Substring(0, 10);
                todate = daterange.Substring(13, 10);

            }
            
            Fee_OnlineSettlementEntities model = _onlineSettlementRepo.OnlineSettlement_Transaction(fromdate, todate, Session["sessionid"].ToString());
            model.dateRange = daterange;
            return PartialView(model);
        }

        //public PartialViewResult _GetOnlineSettlementList(List<OnlineSettlementList> osl)
        //{
        //    Fee_OnlineSettlementEntities model = new Fee_OnlineSettlementEntities();
        //    model.onlineSettlementList = osl;
        //    return PartialView(model);
        //}
        //[ActionName("_GetOnlineSettlementList")]
        [HttpPost]
        public ActionResult Importexcel(Fee_OnlineSettlementEntities model)
        {
            Fee_OnlineSettlementEntities updatedmodel = new Fee_OnlineSettlementEntities();
            if (Request.Files["SettlementInputFile"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["SettlementInputFile"].FileName).ToLower();
                string query = null;
                string connString = "";
                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };
                DataTable Exceldt = new DataTable();
                DataTable Formdt = new DataTable();

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/FeeContenct/Uploads"), Request.Files["SettlementInputFile"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/FeeContenct/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["SettlementInputFile"].SaveAs(path1);

                    //Connection String to Excel Workbook  
                    if (extension == ".csv")
                    {
                        Exceldt = Utility.ConvertCSVtoDataTable(path1);
                        //ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        Exceldt = Utility.ConvertXSLXtoDataTable(path1, connString);
                        //ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        Exceldt = Utility.ConvertXSLXtoDataTable(path1, connString);
                        //ViewBag.Data = dt;
                    }

                    ListtoDataTableConverter abc = new ListtoDataTableConverter();
                    Formdt = abc.ToDataTable(model.onlineSettlementList);

                    //foreach (DataRow row in Formdt.Rows)
                    //{
                    //    foreach (DataRow row1 in Exceldt.Rows)
                    //    {
                    //        if (row["TransReferenceNo"].ToString() == row1["Qfix Reference Number"].ToString())
                    //        {
                    //            row["settlementDate"] = row1["Settlement Date"].ToString();
                    //        }
                    //    }
                    //}
                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                }
                List<OnlineSettlementList> settlementlist = new List<OnlineSettlementList>();
                //System.Threading.Thread threadForCulture = new System.Threading.Thread(delegate () { });
                //string format = threadForCulture.CurrentCulture.DateTimeFormat.ShortDatePattern;//

                string format =CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern;
                string dateseprator = CultureInfo.InvariantCulture.DateTimeFormat.DateSeparator;
                for (int i = 0; i < Formdt.Rows.Count; i++)
                {
                    OnlineSettlementList onlineSettlement = new OnlineSettlementList();
                    onlineSettlement.isSettled = Convert.ToBoolean(Formdt.Rows[i]["isSettled"]);
                    onlineSettlement.ERPNo = Formdt.Rows[i]["ERPNo"].ToString();
                    onlineSettlement.Student = Formdt.Rows[i]["Student"].ToString();
                    onlineSettlement.AdmNo = Formdt.Rows[i]["AdmNo"].ToString();
                    onlineSettlement.Father = Formdt.Rows[i]["Father"].ToString();
                    onlineSettlement.Class = Formdt.Rows[i]["Class"].ToString();
                    onlineSettlement.TransRefNo = Formdt.Rows[i]["TransRefNo"].ToString();
                    onlineSettlement.Transdate = Formdt.Rows[i]["Transdate"].ToString();
                    onlineSettlement.recamount = Convert.ToInt32(Formdt.Rows[i]["recamount"]);
                    onlineSettlement.TransReferenceNo = Formdt.Rows[i]["TransReferenceNo"].ToString();
                    foreach (DataRow row1 in Exceldt.Rows)
                    {
                        
                        if (Formdt.Rows[i]["TransReferenceNo"].ToString() == row1[settlementReferenceindex].ToString() && Convert.ToBoolean(Formdt.Rows[i]["isSettled"])==false)
                        {
                            string sDate = row1[settlementDateindex].ToString();
                            if (dateseprator=="/")
                            onlineSettlement.settlementDate = DateTime.ParseExact(sDate, format, CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            else
                                onlineSettlement.settlementDate = DateTime.ParseExact(sDate, format, CultureInfo.InvariantCulture).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);



                        }
                    }
                    
                    settlementlist.Add(onlineSettlement);
                }
                updatedmodel.onlineSettlementList = settlementlist;

            }
            updatedmodel.dateRange = model.dateRange;
            TempData["OnlineSettlementData"] = updatedmodel;
            return RedirectToAction("Importexcel1");

        }

        
        public ActionResult Importexcel1()
        {
            Fee_OnlineSettlementEntities model=TempData["OnlineSettlementData"] as Fee_OnlineSettlementEntities;

             return View(model);
        }
        [HttpPost]
        public JsonResult Fee_OnlineSettlement_CRUD(Fee_OnlineSettlementEntities model)
        {
            if (ModelState.IsValid)
            {
                   try
                    {
                        DataTable paramDt = new DataTable();
                        paramDt.Columns.Add("TransRefNo");
                        paramDt.Columns.Add("settlementDate");
                        int count = 0;
                        foreach (var item in model.onlineSettlementList.Where(r => r.isSettled ==false))
                        {
                                DataRow row = paramDt.NewRow();
                                row[0] = item.TransRefNo;
                                row[1] = item.settlementDate;
                                paramDt.Rows.Add(row);
                                count++;
                        }

                        Tuple<int, string> res = _onlineSettlementRepo.OnlineSettlement_CRUD(paramDt, Session["UserId"].ToString());
                        return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { status = "success", Msg = ex.Message, Color = "Warning" }, JsonRequestBehavior.AllowGet);
                    }
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        
    }
}