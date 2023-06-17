using ISas.Entities.LibraryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Library.IRepository
{
    public interface IeLibraryRepo
    {
        eLibraryModels eLibrary_Transaction(string classids, int bookno,string function);
        eLibraryModels eLibrary_Transaction(int bookno,string function);
        Tuple<int, string> eLibrary_CRUD(eLibraryBook model,string function,string userid);
        List<eLibraryBook> eLibrary_Student(string erpno);
        Tuple<int, string> Download_Count(int ebookno, string userid);
    }
}
