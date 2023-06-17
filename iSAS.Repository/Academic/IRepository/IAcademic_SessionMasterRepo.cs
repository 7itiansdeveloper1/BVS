using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_SessionMasterRepo
    {
        List<Academic_SessionMasterModel> getSessionMasterList(string sessionId);
        Academic_SessionMasterModel sessionMasterDetailsById(string sessionId);
        Tuple<int, string> Academic_SessionMaster_CRUD(Academic_SessionMasterModel model);
        List<SelectListItem> getAllSessionList();
        List<SelectListItem> getAllSessionWithDefaultSelected();
    }
}
