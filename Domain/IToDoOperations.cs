using ToDoApp.Data;

namespace ToDoApp.Domain
{
    public interface IToDoOperations
    {
        Task<ToDoEntity> GetToDoItem(string listName, int toDoItemIndex);
        Task<List<ToDoEntity>> GetToDoItems(string listName);
        Task<bool> CheckToDoMark(string listName, int toDoItemIndex);
        Task<bool> UnCheckToDoMark(string listName, int toDoItemIndex);
        Task<bool> UpdateToDoDescription(string listName, int toDoItemIndex,string description);
        Task<bool> AddToDo(string listName, ToDoEntity todo);
        Task<bool> DeleteToDo(string listName,int index);
        Task<bool> MoveToDo(string fromList, int index, string toList);
    }
}
