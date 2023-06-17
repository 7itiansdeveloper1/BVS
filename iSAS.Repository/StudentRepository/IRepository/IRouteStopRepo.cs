using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.StudentRegistrationRepository.IRepository
{
    public interface IRouteStopRepo
    {
        IEnumerable<SelectListItem> GetRouteStopByRouteId(int RouteId);
    }
}
