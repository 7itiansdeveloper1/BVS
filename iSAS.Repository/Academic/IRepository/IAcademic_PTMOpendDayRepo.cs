using ISas.Entities.Academic;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_PTMOpendDayRepo
    {
        List<Academic_PTMOpendDayEntities> GetOpenDayList(string SessionId, string ClassId, string UserId, string Category);
        Tuple<int, string> Academic_PTMOpendDay_CRUD(Academic_PTMOpendDayEntities model);
        Tuple<int, string> Academic_PTMOpendDay_CopyToClass(string SessionId, string UserId, string Category, string FromClassId, string ToClass);
    }
}
