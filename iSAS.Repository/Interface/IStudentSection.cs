using System.Collections.Generic;
using ISas.Entities;

namespace ISas.Repository.Interface
{
    public interface IStudentSection
    {
        IEnumerable<StudentSection> GetAllSections(string studentclass, string userid);
    }
}
