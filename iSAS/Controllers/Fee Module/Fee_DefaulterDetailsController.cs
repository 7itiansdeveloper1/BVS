using ISas.Entities.FeesEntities;
using ISas.Repository.Academic.IRepository;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_DefaulterDetailsController : Controller
    {
        private IFee_FeeStructureMasterRepo _feeStrectureRepo;
        private IFee_DefaulterDetailsRepo _defaulterRepo;
        private IFee_DueSetupRepo _dueSetupRepo;
        private ICommonRepo _commonRepo;
        private IAcademic_ClassSetupRepo _classSetup;


        //private System.ComponentModel.Container components;
        //private System.Windows.Forms.Button printButton;
        //private Font printFont;
        //private StreamReader streamToPrint;

        public Fee_DefaulterDetailsController(IFee_FeeStructureMasterRepo feestrecture, IFee_DefaulterDetailsRepo defaulterRepo
          , IFee_DueSetupRepo dueSetup, ICommonRepo commonRepo, IAcademic_ClassSetupRepo classSetup)
        {
            _feeStrectureRepo = feestrecture;
            //_studentClassRepos = classRepo;
            _defaulterRepo = defaulterRepo;
            _dueSetupRepo = dueSetup;
            _commonRepo = commonRepo;
            _classSetup = classSetup;
        }

        // GET: Fee_DefaulterDetails
        [CustomAuthorizeFilter("VIEW")]
        public ActionResult FilterDefaulter()
        {
            Session["DefaultListDetails"] = null;

            Fee_FilterDefaulterDetailModel model = new Fee_FilterDefaulterDetailModel();
            model.FeeCategoryList = _feeStrectureRepo.GetFeeStructureList("", Session["SessionId"].ToString()).Select(r => new SelectListItem
            {
                Text = r.StructName,
                Value = r.StructId
            }).ToList();

            //var classes = _studentClassRepos.GetAllClasses(Session["UserID"].ToString());
            //if (classes != null && classes.Count() > 0)
            //    model.ClassList = classes.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            //    {
            //        Text = r.ClassName,
            //        Value = r.ClassId,
            //    }).ToList();

            model.ClassList = _classSetup.All_ClassWithSectionList_DropDown(Session["UserId"].ToString());

            model.SMSType = "Default";
            model.DefaulterType = "FT";
            model.ReportType = "Defaulter";
            model.Date = DateTime.Now.ToShortDateString().Replace("-", "/");
            return View(model);
        }

        public PartialViewResult _DefaulterDetails(string DueDate, string ClassId, string FeeCategoryId, string DefaulterType,string ReportType) //string SectionId,
        {
            Fee_FilterDefaulterDetailModel model = _defaulterRepo.GetDefaulterDetails(Session["SessionId"].ToString(), DueDate, ClassId, FeeCategoryId, DefaulterType, ReportType); //SectionId,
            Session["DefaultListDetails"] = model;
            return PartialView(model);
        }

        [CustomAuthorizeFilter("REPORT")]
        public ActionResult DefaulterDetails_Print()
        {
            Fee_FilterDefaulterDetailModel model = new Fee_FilterDefaulterDetailModel();

            if (Session["DefaultListDetails"] != null)
                model = Session["DefaultListDetails"] as Fee_FilterDefaulterDetailModel;

            return View(model);
        }

        [CustomAuthorizeFilter("SEND_SMS")]
        [HttpPost]
        public JsonResult SendSMS(Fee_FilterDefaulterDetailModel model)
        {
            //string msg = "No Msg Send ..!";
            if (model != null && model.DefaulterList != null && model.DefaulterList.Where(r => r.Selected).Count() > 0)
            {
                //string smsText = "";
                //smsText = _defaulterRepo.GetSMSTextForFee();
                try
                {
                    for (int i = 0; i < model.DefaulterList.Count; i++)
                    {
                        if (model.DefaulterList[i].Selected && !string.IsNullOrEmpty(model.DefaulterList[i].SMSNo))
                        {
                            string tempSMSText = model.SMSText;
                            if (model.SMSType == "Default")
                            {
                                tempSMSText = tempSMSText.Replace("#Name#", model.DefaulterList[i].Student);
                                tempSMSText = tempSMSText.Replace("#Installment#", model.DefaulterList[i].Duration);
                                tempSMSText = tempSMSText.Replace("#Father#", model.DefaulterList[i].FatherName);
                                tempSMSText = tempSMSText.Replace("#Due#", model.DefaulterList[i].Balance.ToString());
                                tempSMSText = tempSMSText.Replace("#Class#", model.DefaulterList[i].Class);
                            }
                            Tuple<int, string> res = _commonRepo.SendSMS(tempSMSText, "SMS", model.DefaulterList[i].ERPNo, model.DefaulterList[i].SMSNo);
                        }
                    }
                    return Json(new { status = "success", Msg = "SMS send successfully", Color = "Success"}, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { status = "Failed", Msg = "Failed to send all  SMS", Color = "Warning" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = "Failed", Msg = "Failed to Send SMS", Color = "Warning" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Fee_DefaulterDetails_Report(Fee_FilterDefaulterDetailModel param)
        {
            DefaulterLetter_ReportModel model = new DefaulterLetter_ReportModel();
            param.SessionId = Session["SessionId"].ToString();
            DataSet ds = _defaulterRepo.GetDefaulterLetterDetails(param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.HeaderDetails.Header1 = ds.Tables[0].Rows[0][0].ToString();
                model.HeaderDetails.Header2 = ds.Tables[0].Rows[0][1].ToString();
                model.HeaderDetails.Header3 = ds.Tables[0].Rows[0][2].ToString();
                model.HeaderDetails.Header4 = ds.Tables[0].Rows[0][3].ToString();
                model.HeaderDetails.LogoURL = ds.Tables[0].Rows[0][4].ToString();
            }

            if (ds.Tables[1].Rows.Count > 0)
                model.ReportName = ds.Tables[1].Rows[0][0].ToString();

            model.StudentDetails = ds.Tables[2].AsEnumerable().Select(r => new DefaultLetter_Report_StudDetailsModel
            {
                AdmNo = r.Field<string>("AdmNo"),
                Balance = r.Field<int>("Balance"),
                ByDate = r.Field<string>("ByDate"),
                Class = r.Field<string>("Class"),
                Duration = r.Field<string>("Duration"),
                ERPNo = r.Field<string>("ERPNo"),
                Father = r.Field<string>("Father"),
                SMSNo = r.Field<string>("SMSNo"),
                Student = r.Field<string>("Student"),
            }).ToList();

            return View("DefaulterLetter_HtmlReport", model);


            //byte[] pdfContent = null;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    string html = CommonHelpers.RenderPartialToString(this, "DefaulterLetter_HtmlReport", model);
            //    var pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
            //    pdf.Save(ms);
            //    pdfContent = ms.ToArray();
            //}
            //return File(pdfContent, "application/pdf");


            //var actionPDF = new Rotativa.ViewAsPdf
            //{
            //    //FileName = "FeeReceipt.pdf",
            //    ViewName = "DefaulterLetter_HtmlReport",
            //    Model = model,
            //    PageSize = Rotativa.Options.Size.A4,
            //    PageOrientation = Rotativa.Options.Orientation.Portrait,
            //    IsGrayScale = false,
            //    // PageMargins = { Left = 1, Right = 1 }
            //};

            //PdfPrintingNet.PdfPrint obj = new PdfPrintingNet.PdfPrint("Demo", "Demo");
            //obj.Print(actionPDF.BuildPdf(ControllerContext));

            // return actionPDF;
        }

        public ViewResult DefaulterLetter_HtmlReport()
        {
            return View();
        }

        public JsonResult Fee_Installments(string StructId)
        {
            Tuple<List<SelectListItem>, List<SelectListItem>> headWithInstallmentList = _dueSetupRepo.GetHeadWithInstallmentList(StructId, Session["SessionId"].ToString());
            return Json(headWithInstallmentList.Item2, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSMSTextForFee()
        {
            return Json(_defaulterRepo.GetSMSTextForFee(), JsonRequestBehavior.AllowGet);
        }
    }
}