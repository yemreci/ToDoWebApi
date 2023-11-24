using ToDoApp.Data;
using ToDoApp.Repositories;

namespace ToDoApp.Domain
{
    public class ToDoOperations : IToDoOperations
    {
        private readonly IToDoRepository _repository;
        private readonly ILogger<ToDoOperations> _logger;

        public ToDoOperations(IToDoRepository repository, ILogger<ToDoOperations> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<bool> AddToDo(string listName, ToDoEntity todo)
        {
            return await _repository.AddToDoToList(listName, todo);
        }

        public async Task<bool> CheckToDoMark(string listName, int toDoItemIndex)
        {
            return await _repository.UpdateItemStatus(listName, toDoItemIndex, true);
        }

        public async Task<bool> DeleteToDo(string listName, int index)
        {
            return await _repository.DeleteListItem(listName, index);
        }

        public async Task<ToDoEntity> GetToDoItem(string listName, int toDoItemIndex)
        {
            var toDoList = await _repository.GetToDoListByName(listName);
            if (toDoList == null)
            {
                throw new Exception("List with such name does not exist");
            }
            var toDo = toDoList.Todos.FirstOrDefault(t => t.Id == toDoItemIndex);
            if (toDo == null)
            {
                throw new Exception("List does not contain an item with such Index");
            }
            var result = new ToDoEntity();
            result = toDo;
            return result;
        }

        public async Task<List<ToDoEntity>> GetToDoItems(string listName)
        {
            var toDoList = await _repository.GetToDoListByName(listName);
            var resultList = new List<ToDoEntity>();
            if (toDoList == null)
            {
                throw new Exception("List with such name does not exist");
            }
            if (!toDoList.Todos.Any())
            {
                return resultList;
            }
            foreach (var item in toDoList.Todos)
            {
                resultList.Add(new ToDoEntity
                {
                    Id = item.Id,
                    Description = item.Description,
                    IsComplete = item.IsComplete
                });
            }

            return resultList;
        }

        public async Task<bool> MoveToDo(string fromList, int index, string toList)
        {
            return await _repository.MoveToDoItem(fromList, index, toList);
        }

        public async Task<bool> UnCheckToDoMark(string listName, int toDoItemIndex)
        {
            return await _repository.UpdateItemStatus(listName, toDoItemIndex, false);
        }

        public async Task<bool> UpdateToDoDescription(string listName, int toDoItemIndex,string description)
        {
            return await _repository.UpdateItemDescription(listName,toDoItemIndex,description);
        }
    }
}
