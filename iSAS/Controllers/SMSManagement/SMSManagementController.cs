using ISas.Entities.SMSManagement;
using ISas.Repository.Interface;
using ISas.Repository.SMSManagement.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;


namespace ISas.Web.Controllers.SMSManagement
{
    [Authorize]
    [ExceptionHandler]
    public class SMSManagementController : Controller
    {
        private ISMSManagementRepo _smsManagementRepo;
        private ICommonRepo _commonRepo;
        public SMSManagementController(ISMSManagementRepo smsManagement, ICommonRepo commonRepo)
        {
            this._smsManagementRepo = smsManagement;
            _commonRepo = commonRepo;
        }

        public ActionResult SMSManagementMainPage()
        {
            SMS_MainModel model = new SMS_MainModel();
            model.SMSText = "Dear Parents\n" +
                "Kindly click http://balvikasschoolpanipat.edu.in/?news=daily-updates to read an important notice.\n" +
                "DIRECTOR\n" +
                "ACTING PRINCIPAL\n" +
                "Bal Vikas School, Panipat";
            model.StudentSMSFilterType = "ClassWise";
            model.StudentSMSFilterType = "DeptWise";
            model.DropDownList = this._smsManagementRepo.GetSMS_ManagementDropDownList();
            model.OutBoxList = _smsManagementRepo.GetSMSOutboxDetails();
            return View(model);
        }
        #region Student SMS Management
        public ActionResult SMStoStudentMainPage()
        {
            return View();
        }
        public ActionResult _StudentDetails(string ClassIds, string StudentGroupId)
        {
            SMS_MainModel model = new SMS_MainModel();
            model.StudentList = this._smsManagementRepo.GetStudentDetails(ClassIds, StudentGroupId, Session["SessionID"].ToString());
            return PartialView(model);
        }

        #region Old SMS Sending Code
        //[HttpPost]
        //public JsonResult SendSMS(SMS_MainModel model)
        //{
        //    List<SMS_StudentModel> filteredStudList = model.StudentList.Where(r => r.IsSelected && !string.IsNullOrEmpty(r.SMSNo)).ToList();
        //    List<SMS_StudentModel> duplicate = filteredStudList.Select((t) => new SMS_StudentModel
        //    {
        //        AdmNo = t.AdmNo,
        //        SMSNo = t.SMSNo,
        //        Class = t.Class,
        //        ERP = t.ERP,
        //        Father = t.Father,
        //        IsSelected = t.IsSelected,
        //        Student = t.Student
        //    }).GroupBy(g => g.SMSNo).Where(g => g.Count() > 1).SelectMany(r => r).ToList();
        //    filteredStudList = filteredStudList.DistinctBy(r => r.SMSNo).ToList();
        //    duplicate = duplicate.Where(r => !filteredStudList.Select(p => p.ERP).Contains(r.ERP)).ToList();
        //    string smsNos = string.Join(",", filteredStudList.Select(r => r.SMSNo).ToList());
        //    string erpNos = string.Join(",", filteredStudList.Select(r => r.ERP).ToList());
        //    string duplicatesmsNos = string.Join(",", duplicate.Select(r => r.SMSNo).ToList());
        //    string duplicateerpNos = string.Join(",", duplicate.Select(r => r.ERP).ToList());
        //    Tuple<int, string> res = _commonRepo.SendSMS(smsNos, model.SMSText, erpNos, false, duplicatesmsNos, duplicateerpNos, model.MsgType_Stud);

        //    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        [HttpPost]
        public JsonResult SendSMS(SMS_MainModel model)
        {
            List<SMS_StudentModel> filteredStudList = model.StudentList.Where(r => r.IsSelected && !string.IsNullOrEmpty(r.SMSNo) && r.SMSNo.Length == 10).ToList();
            List<SMS_StudentModel> duplicate = filteredStudList.Select((t) => new SMS_StudentModel
            {
                AdmNo = t.AdmNo,
                SMSNo = t.SMSNo,
                Class = t.Class,
                ERP = t.ERP,
                Father = t.Father,
                IsSelected = t.IsSelected,
                Student = t.Student
            }).GroupBy(g => g.SMSNo).Where(g => g.Count() > 1).SelectMany(r => r).ToList();
            filteredStudList = filteredStudList.DistinctBy(r => r.SMSNo).ToList();
            duplicate = duplicate.Where(r => !filteredStudList.Select(p => p.ERP).Contains(r.ERP)).ToList();
            Tuple<int, string> res = _commonRepo.SendSMS(model.SMSText, model.MsgType_Stud, filteredStudList, duplicate);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult StudentSMSGroup_CRUD(SMS_MainModel model)
        {
            List<SMS_StudentModel> selectedStudentList = model.StudentList.Where(r => r.IsSelected).ToList();
            Tuple<int, string> res = _smsManagementRepo.StudentSMSGroup_CRUD(model.SMSStudentGroupName, model.PostStudentSMSGroupType, selectedStudentList);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Staff SMS Management
        public ActionResult SMStoStaffMainPage()
        {
            return View();
        }

        public ActionResult _StaffDetails(string DeptIds, string StaffGroupId, string IsForAdminStaff = "NO")
        {
            SMS_MainModel model = new SMS_MainModel();
            model.AdminAndStaffList = this._smsManagementRepo.GetStaffDetails(DeptIds, StaffGroupId, IsForAdminStaff);
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult StaffSMSGroup_CRUD(SMS_MainModel model)
        {
            List<SMS_AdminAndStaffModel> selectedStaffList = model.AdminAndStaffList.Where(r => r.IsSelected).ToList();
            Tuple<int, string> res = _smsManagementRepo.StaffSMSGroup_CRUD(model.SMSStaffGroupName, model.PostStaffSMSGroupType, selectedStaffList);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SendSMS_Staff(SMS_MainModel model)
        {
            //string smsNos = string.Join(",", model.AdminAndStaffList.Where(r => r.IsSelected && !string.IsNullOrEmpty(r.Mobile)).Select(r => r.Mobile).ToList());
            //string staffCode = string.Join(",", model.AdminAndStaffList.Where(r => r.IsSelected && !string.IsNullOrEmpty(r.Mobile)).Select(r => r.StaffID).ToList());
            Tuple<int, string> res = _commonRepo.SendSMS(model.SMSText_Staff, model.MsgType_Staff, model.AdminAndStaffList.Where(r => r.IsSelected && !string.IsNullOrEmpty(r.Mobile) && r.Mobile.Length == 10).ToList());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Outbox
        public PartialViewResult OutboxMainPage()
        {
            SMS_MainModel model = new SMS_MainModel();
            model.OutBoxList = _smsManagementRepo.GetSMSOutboxDetails();
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult SendOutBoxSMS(SMS_MainModel model)
        {
            try
            {
                List<SMS_OutboxModel> selectedList = model.OutBoxList.Where(r => r.Selected).Where(r =>
                !string.IsNullOrEmpty(r.MobileNo) && !string.IsNullOrEmpty(r.SMSText) && r.MobileNo.Length == 10
                ).ToList();
                for (int i = 0; i < selectedList.Count; i++)
                {
                    Tuple<int, string> status = _commonRepo.SendSMS(selectedList[i].MobileNo, selectedList[i].SMSText);
                    selectedList[i].SendSMS_Status = status.Item1;
                }

                List<SMS_OutboxModel> sendSMSOnly = selectedList.Where(r => r.SendSMS_Status == 1).ToList();
                if (sendSMSOnly.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MobileNo");
                    dt.Columns.Add("RecordSendDate");
                    for (int x = 0; x < sendSMSOnly.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = sendSMSOnly[x].MobileNo;
                        row[1] = sendSMSOnly[x].RecordSendDate.ToString("MM-dd-yyyy HH:mm:ss");
                        dt.Rows.Add(row);
                    }
                    _smsManagementRepo.Delete_OutBoxSMS(dt); //Delete Send SMS

                    return Json(new { status = "success", Msg = "SMS  send successfully..!", Color = "Success" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { status = "failed", Msg = "Failed to send sms, try again later...!", Color = "Warning" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = "failed", Msg = "Failed to send sms, try again later...!", Color = "Warning" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteOutBoxSMS(SMS_MainModel model)
        {
            try
            {
                List<SMS_OutboxModel> selectedList = model.OutBoxList.Where(r => r.Selected).ToList();
                if (selectedList.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MobileNo");
                    dt.Columns.Add("RecordSendDate");
                    for (int x = 0; x < selectedList.Count; x++)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = selectedList[x].MobileNo;
                        row[1] = selectedList[x].RecordSendDate.ToString("MM-dd-yyyy HH:mm:ss");
                        dt.Rows.Add(row);
                    }
                    _smsManagementRepo.Delete_OutBoxSMS(dt); //Delete Send SMS

                    return Json(new { status = "success", Msg = "SMS  removed successfully..!", Color = "Success" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { status = "failed", Msg = "Failed to remove sms, try again later...!", Color = "Warning" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = "failed", Msg = "Failed to  remove sms, try again later...!", Color = "Warning" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
