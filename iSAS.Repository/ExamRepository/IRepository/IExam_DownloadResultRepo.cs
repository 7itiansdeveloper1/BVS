using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;
namespace ISas.Repository.ExamRepository.IRepository
{
    public interface IExam_DownloadResultRepo
    {
        List<Exam_DownloadResultModels> DownloadResult(string erpNo, string sessionId);
    }
}
