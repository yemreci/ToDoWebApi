using ToDoApp.Data;

namespace ToDoApp.Repositories
{
    public interface IToDoRepository
    {
        Task<bool> CreateList(string name);
        Task<bool> DeleteList(string name);
        Task<List<ListEntity>> GetToDoLists();
        Task<ListEntity> GetToDoListByName(string listName);
        Task<bool> MergeLists(string List1, string List2);
        Task<bool> AddToDoToList(string listName, ToDoEntity toDo);
        Task<bool> DeleteListItem(string listName, int index);
        Task<bool> UpdateListName(string name, string updateName);
        Task<bool> UpdateItemStatus(string listName, int itemIndex, bool status);
        Task<bool> UpdateItemDescription(string listName,int itemIndex, string description);
        Task<bool> MoveToDoItem(string fromList, int index, string toList);
    }
}
