using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_GenerateBarcodeRepo
    {
        List<string> GetAccNoList(string fromaccno, string toaccno);
    }
}
