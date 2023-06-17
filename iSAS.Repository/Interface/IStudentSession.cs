using System.Collections.Generic;
using ISas.Entities;

namespace ISas.Repository.Interface
{
    public  interface IStudentSession
    {
         IEnumerable<Session> GetAllSessions();
    }
}
