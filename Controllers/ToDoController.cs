using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Domain;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/todolists/{listName}")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoOperations _toDoOperations;
        public ToDoController(ILogger<ToDoController> logger, IToDoOperations toDoOperations)
        {
            _logger = logger;
            _toDoOperations = toDoOperations;
        }
        //Should not return ToDoEntity need to map result need fix.
        [HttpGet("listItems")]
        public async Task<ActionResult<List<ToDoDTO>>> GetToDoItems(string listName)
        {
            if(string.IsNullOrEmpty(listName))
            {
                return BadRequest("List name can't be null or empty");
            }
            var result = await _toDoOperations.GetToDoItems(listName);
            if (result == null || !result.Any())
            {
                return BadRequest("No items have been found with given parameters.");
            }
            return Ok(result);
        }
        [HttpGet("{toDoItemIndex}")]
        public async Task<ActionResult<ToDoDTO>> GetToDoItem(string listName, int toDoItemIndex)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return BadRequest("Name of the list can't be empty");
            }
            var result = await _toDoOperations.GetToDoItem(listName, toDoItemIndex);
            if (result == null)
            {
                return BadRequest("No such Item has been found with the given parameters.");
            }
            return Ok(result);
        }
        [HttpPost("{toDoItemIndex}/check")]
        public async Task<ActionResult> CheckToDoMark(string listName, int toDoItemIndex)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return BadRequest("Name of the list can't be empty");
            }
            var done = await _toDoOperations.CheckToDoMark(listName, toDoItemIndex);
            if (!done)
            {
                return BadRequest("Operation failed.");
            }
            return Ok("Operation succeeded.");
        }
        [HttpPost("{toDoItemIndex}/uncheck")]
        public async Task<ActionResult> UnCheckToDoMark(string listName, int toDoItemIndex)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return BadRequest("Name of the list can't be empty");
            }
            var done = await _toDoOperations.UnCheckToDoMark(listName, toDoItemIndex);
            if (!done)
            {
                return BadRequest("Operation failed.");
            }
            return Ok("Operation succeeded.");
        }
        [HttpPost("{toDoItemIndex}/updatedescription")]
        public async Task<ActionResult> UpdateToDoDescription(string listName, int toDoItemIndex,string description)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return BadRequest("Name of the list can't be empty");
            }
            var done = await _toDoOperations.UpdateToDoDescription(listName,toDoItemIndex,description);
            if (!done)
            {
                return BadRequest("Operation failed.");
            }
            return Ok("Operation succeeded.");
        }
        [HttpPost("addToDo")]
        public async Task<ActionResult> AddToDo(string listName, ToDoDTO todo)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return BadRequest("Listname can't be null or empty");
            }
            var item = new ToDoEntity
            {
                Description = todo.Description,
                IsComplete = todo.IsComplete
            };
            var done = await _toDoOperations.AddToDo(listName, item);
            if (!done)
            {
                return BadRequest("Operation failed.");
            }
            return Ok("Operation succeeded.");
        }
        [HttpPost("{index}/delete")]
        public async Task<ActionResult> DeleteToDoFromList(string listName, int index)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return BadRequest("Listname can't be null or empty");
            }
            var done = await _toDoOperations.DeleteToDo(listName, index);
            if (!done)
            {
                return BadRequest("Operation failed.");
            }
            return Ok("Operation succeeded.");
        }
        [HttpPost("{index}/move")]
        public async Task<ActionResult> MoveToDoToAnotherList(string listName, int index, string toList)
        {
            if (string.IsNullOrEmpty(listName) || string.IsNullOrEmpty(toList)) 
            {
                return BadRequest("Lists can't be empty");
            }
            var done = await _toDoOperations.MoveToDo(listName, index,toList);
            if (!done)
            {
                return BadRequest("Operation failed.");
            }
            return Ok("Operation succeeded.");
        }
    }
}