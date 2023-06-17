using ISas.Entities.Student_Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace ISas.Repository.StudentRepository.IRepository
{
    public interface IStudent_CertificateRepo
    {
        List<Student_CertificateModels> getCertificateList(string sessionId, string studcertificateId);
        Student_CertificateModels getCertificateById(string sessionId, string studcertificateId);
        List<SelectListItem> getCertificateListDropDown();
        Tuple<int, string> Student_Certificate_CRUD(Student_CertificateModels model);
        Tuple<int, string> Student_Certificate_CRUD(string studCertificateId);
        DataSet getStudentCertificateDetails(string erpNo, string certificateId, string userId, string sessionId);
        DataTable Student_Undertaking_DownloadForm(string erpNo, string sessionId);
    }
}
