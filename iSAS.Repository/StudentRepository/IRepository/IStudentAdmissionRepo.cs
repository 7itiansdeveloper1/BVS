using ISas.Entities.RegistrationEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.StudentRegistrationRepository.IRepository
{
    public interface IStudentAdmissionRepo
    {
        DropDownListFor_StudentAdmission GetAdmissionDropDownList(string ClassID, 
            string FatherCountryID, string FatherStateID,string MotherCountryID, string MotherStateID, 
            string CorresCountryID, string CorresStateID, string PermCountryID, string PermStateID,
            string GuardianCountryID, string GuardinaStateID, string RouteID, string SessionId, bool AvaildTransport
            );

        Tuple<int, string> Student_Admission_CRUD(Student_AdmissionMaster_Model model);
        IEnumerable<Student_AdmissionLandingPageModel> GetAllStudent_AdmissionsList(string UserId = "1", int FilterPercent = 10);
        Student_AdmissionMaster_Model GetStudent_AdmissionDetailsByERPNo(string ErpNom, string SessionId);

        List<SelectListItem> GetModeOfTransportList(bool AvaildTransport);
    }
}
