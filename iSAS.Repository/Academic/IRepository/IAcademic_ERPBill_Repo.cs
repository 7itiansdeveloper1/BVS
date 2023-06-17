using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_ERPBill_Repo
    {
        Academic_ERPBillModel GetERPBill();
    }
}
