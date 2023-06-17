using ISas.Entities.Student_Entities;
using ISas.Repository.DashboardRepository.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Student_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Student_PhotoUpload_SingleStudentController : Controller
    {
        private IStudentAttendance _studentAttendanceRepos;
        private IStudentProfileRepo _studentProfileRepo;
        public Student_PhotoUpload_SingleStudentController(IStudentAttendance studnetAttnRepo, IStudentProfileRepo studProfile)
        {
            _studentAttendanceRepos = studnetAttnRepo;
            _studentProfileRepo = studProfile;
        }
        public ActionResult PhotoUpload_SingleStudent(string type)
        {
            return View(_studentProfileRepo.GetStudentImages(Session["UserID"].ToString(), type));
        }


        [HttpPost]
        public JsonResult UploadFile(Student_ImagesModels model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.StudentImageURL_Croped))
                    UploadStudentImage(model.StudentImageURL_Croped, model.UploadFor, model.ERPNo);

                return Json("Image uploaded successfully..!", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Failed to upload Image..!", JsonRequestBehavior.AllowGet);
            }
        }

        private string UploadStudentImage(string imgCroppedStr, string UploadFor,string erpNo)
        {
            string imgFileName = "Images/StudentIamges/" + Guid.NewGuid() + "_Cropped.jpg";
            string myFullFileName = "~/" + imgFileName;

            string base64 = imgCroppedStr;
            byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
            using (FileStream stream = new FileStream(Server.MapPath(myFullFileName), FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            Tuple<int, string> res = _studentAttendanceRepos.UploadStudentPhoto(erpNo, imgFileName, UploadFor);
            return res.Item2;
        }
    }
}