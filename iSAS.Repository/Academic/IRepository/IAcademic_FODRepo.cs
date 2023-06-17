using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Academic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_FODRepo
    {
        List<Academic_FOD> GetFOD_Transaction();
        Tuple<int, string> GetFOD_CRUD(Academic_FOD model, string userId);
        Academic_FOD GetFOD_Transaction(int fodId);
    }
}
