using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public class ToDoInMemoryDBRepository : IToDoRepository
    {
        private readonly ToDoAppContext _context;
        public ToDoInMemoryDBRepository(ToDoAppContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToDoToList(string listName, ToDoEntity toDo)
        {
            var list = await GetToDoListByName(listName);
            if (list == null)
            {
                throw new NullReferenceException("No such list with given name.");
            }
            if (toDo == null)
            {
                throw new NullReferenceException("toDo item can't be null.");
            }
            list.Todos.Add(toDo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateList(string name)
        {
            _context.Add(new ListEntity
            {
                Name = name,
                Todos = new List<ToDoEntity>()
            });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteList(string name)
        {
            var list = await GetToDoListByName(name);
            if (list == null)
            {
                throw new NullReferenceException("No such list with given name.");
            }
            _context.ToDoLists.Remove(list);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteListItem(string listName, int index)
        {
            var list = await GetToDoListByName(listName);
            if (list == null)
            {
                throw new NullReferenceException("No such list with given name.");
            }
            var todo = list.Todos.FirstOrDefault(t => t.Id == index);
            if (todo == null)
            {
                throw new NullReferenceException("No such item in list");
            }
            if (!list.Todos.Remove(todo))
            {
                return false;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ListEntity> GetToDoListByName(string listName)
        {
            return await _context.ToDoLists.Include(t => t.Todos).FirstOrDefaultAsync(t => t.Name == listName);
        }

        public async Task<List<ListEntity>> GetToDoLists()
        {
            return await _context.ToDoLists.Include(t => t.Todos).ToListAsync();
        }

        public async Task<bool> MergeLists(string List1, string List2)
        {
            var firstList = await GetToDoListByName(List1);
            var secondList = await GetToDoListByName(List2);
            if (firstList == null || secondList == null)
            {
                throw new NullReferenceException("No such list with given name.");
            }
            var todos = secondList.Todos.ToList();
            if ( todos == null || todos.Count == 0)
            {
                throw new NullReferenceException("No item in second list");
            }
            foreach ( var todo in todos ) {
                firstList.Todos.Add(todo);
                secondList.Todos.Remove(todo);
            }
            _context.Remove(secondList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MoveToDoItem(string fromList, int index, string toList)
        {
            var fromlist = await GetToDoListByName(fromList);
            var tolist = await GetToDoListByName(toList);
            if (fromlist == null || tolist == null)
            {
                throw new NullReferenceException("No such list with given name.");
            }
            var todo = fromlist.Todos.FirstOrDefault(t => t.Id == index);
            if (todo == null)
            {
                throw new NullReferenceException("No such item in list");
            }
            if (!fromlist.Todos.Remove(todo))
            {
                return false;
            }
            tolist.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateItemDescription(string listName, int itemIndex, string description)
        {
            var List = await _context.ToDoLists.Include(t => t.Todos).FirstOrDefaultAsync(t => t.Name == listName);
            if (List == null)
            {
                throw new NullReferenceException("Such list could not be found.");
            }
            var item = List.Todos.FirstOrDefault(i => i.Id == itemIndex);
            if (item == null)
            {
                throw new NullReferenceException("Such item does not exist in list.");
            }
            item.Description = description;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateItemStatus(string listName, int itemIndex, bool status)
        {
            var List = await _context.ToDoLists.Include(t => t.Todos).FirstOrDefaultAsync(t => t.Name == listName);
            if (List == null)
            {
                throw new NullReferenceException("Such list could not be found.");
            }
            var item = List.Todos.FirstOrDefault(i => i.Id == itemIndex);
            if(item == null)
            {
                throw new NullReferenceException("Such item does not exist in list.");
            }
            item.IsComplete = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateListName(string name, string updateName)
        {
            var List = await _context.ToDoLists.Include(t => t.Todos).FirstOrDefaultAsync(t => t.Name == name);
            if (List == null)
            {
                throw new NullReferenceException("Such list could not be found.");
            }
            List.Name = updateName;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
