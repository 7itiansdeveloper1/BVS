using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ISas.Entities.DashboardEntities;
namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface ICertificateRepo
    {
        //CertificateModels Dashboard_CertificatePath(string erpNo, string sessionId);
        List<CertificateModels> Dashboard_CertificatePath(string erpNo, string sessionId);
        DataSet GetCertifricateReport(string certificateId, string ERPNo);
    }
}
