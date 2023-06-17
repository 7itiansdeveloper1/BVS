using ISas.Entities;
using ISas.Repository.Implementation;
using ISas.Repository.Interface;
using ISas.Repository.StaffRepository.IRepository;
using ISas.Repository.StaffRepository.Repository;
using ISas.Web.Controllers.Staff;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;


namespace iSAS.Test.Controllers.StaffModule
{
    [TestClass]
    public class Staff_StaffDetailMasterControllerTest
    {
        private Mock<IStaff_StaffDetailMasterRepo> _staffRepo;
        private Mock<IStudentAttendance> _studentAttendanceRepos;
        //private CommonController commonContr;

        public Staff_StaffDetailMasterControllerTest()
        {
            _staffRepo = new Mock<IStaff_StaffDetailMasterRepo>();
            _studentAttendanceRepos = new Mock<IStudentAttendance>();
        }

        //[TestMethod]
        //public void Staff_StaffDetailMasterLandingPageTest()
        //{
        //    //Arrange
        //    Staff_StaffDetailMasterController _Staff = new Staff_StaffDetailMasterController(_staffRepo.Object, _studentAttendanceRepos.Object);
        //    //Act
        //    ActionResult result = _Staff.Staff_StaffDetailMasterLandingPage() as ActionResult;
        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Staff_StaffDetailMasterLandingPage", ((System.Web.Mvc.ViewResultBase)result).ViewName);
        //}

        //[TestMethod]
        //public void NewStaff_StaffDetailMasterTest()
        //{
        //    //Arrange
        //    Staff_StaffDetailMasterController _Staff = new Staff_StaffDetailMasterController(_staffRepo.Object, _studentAttendanceRepos.Object);
        //    //Act
        //    ActionResult result = _Staff.NewStaff_StaffDetailMaster() as ActionResult;
        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Staff_StaffDetailMasterLandingPage", ((System.Web.Mvc.ViewResultBase)result).ViewName);
        //}

    }
}
