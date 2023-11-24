using ToDoApp.Data;

namespace ToDoApp.Domain
{
    public interface IToDoListOperations
    {
        Task<List<ListEntity>> GetToDoLists();
        Task<ListEntity> GetListByName(string name);
        Task<bool> UpdateListName(string listName,string newName);
        Task<bool> CreateList(string listName);
        Task<bool> DeleteList(string name);
        Task<bool> MergeLists(string firstListName,string secondListName);
    }
}
