using ToDoApp.Data;
using ToDoApp.Repositories;

namespace ToDoApp.Domain
{
    public class ToDoListOperations : IToDoListOperations
    {
        private readonly IToDoRepository _repository;
        private readonly ILogger<ToDoListOperations> _logger;

        public ToDoListOperations(IToDoRepository repository, ILogger<ToDoListOperations> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> CreateList(string listName)
        {
            return await _repository.CreateList(listName);
        }

        public async Task<bool> DeleteList(string name)
        {
            return await _repository.DeleteList(name);
        }

        public async Task<ListEntity> GetListByName(string name)
        {
            var list = await _repository.GetToDoListByName(name);
            if (list == null)
            {
                throw new Exception("There are no lists with such name.");
            }
            return list;
        }

        public async Task<List<ListEntity>> GetToDoLists()
        {
            var lists = await _repository.GetToDoLists();
            if (lists == null)
            {
                throw new Exception("There are no lists.");
            }
            return lists;
        }

        public async Task<bool> MergeLists(string firstListName, string secondListName)
        {
            return await _repository.MergeLists(firstListName, secondListName);
        }

        public async Task<bool> UpdateListName(string listName, string newName)
        {
            return await _repository.UpdateListName(listName, newName);
        }
    }
}
