using ISas.Entities.Student_Entities;


namespace ISas.Repository.StudentRegistrationRepository.IRepository
{
    public interface ISiblingAllotmentRepo
    {
        SiblingAllotmentModels GetStudentList(string ClassId, string SectionId, string SessionId, string UserId);
        string SiblingAllotment_CRUD(SiblingAllotmentModels model);
        SiblingAllotmentModels GetStudentPossibleSiblingList(string selectederpno, string selectedstudentname, string sessionid);
    }
}
