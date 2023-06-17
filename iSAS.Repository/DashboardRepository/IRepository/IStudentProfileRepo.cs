using ISas.Entities.DashboardEntities;
using ISas.Entities.Student_Entities;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IStudentProfileRepo
    {
        StudentProfile GetStudentProfileById(string Stud_UID);
        Student_ImagesModels GetStudentImages(string Stud_UID, string type);
    }
}
