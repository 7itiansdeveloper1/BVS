using ISas.Entities.DashboardEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IToDo_TaskRepo
    {
        List<ToDo_TaskEntitiesModel> GetToDo_TaskList(int Id);
        ToDo_TaskEntitiesModel GetToDo_TaskById(int Id);
        Tuple<int, string> ToDo_Task_CRUD(ToDo_TaskEntitiesModel model);
        Tuple<int, string> ToDo_Task_CRUD(int ToDoID);
    }
}
