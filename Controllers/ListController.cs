using Microsoft.AspNetCore.Mvc;
using ToDoApp.Domain;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/todolists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ILogger<ListController> _logger;
        private readonly IToDoListOperations _toDoListOperations;
        public ListController(ILogger<ListController> logger, IToDoListOperations ToDoListOperations)
        {
            _logger = logger;
            _toDoListOperations = ToDoListOperations;
        }
        [HttpGet()]
        public async Task<ActionResult<List<ListDTO>>> GetToDoLists()
        {
            var result = await _toDoListOperations.GetToDoLists();
            if (result == null || !result.Any())
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<ListDTO>> GetToDoListByName(string name)
        {
            var result = await _toDoListOperations.GetListByName(name);
            if (result == null)
            {
                return BadRequest("No such list have been found with given parameters.");
            }
            return Ok(result);
        }
        [HttpPost("{listName}/update")]
        public async Task<ActionResult> UpdateListName(string listName, string newName)
        {
            var done = await _toDoListOperations.UpdateListName(listName, newName);
            if (!done)
            {
                return BadRequest("Request has failed.");
            }
            return Ok("Operation succeeded.");
        }
        [HttpPost("createList")]
        public async Task<ActionResult> CreateToDoList(string listName)
        {
            var done = await _toDoListOperations.CreateList(listName);
            if (!done)
            {
                return BadRequest("Operation failed.");
            }
            return Ok("Operation succeeded.");
        }
        [HttpPost("deleteList")]
        public async Task<ActionResult> DeleteToDoList(string name)
        {
            var done = await _toDoListOperations.DeleteList(name);
            if (!done)
            {
                return BadRequest("Operation failed.");
            }
            return Ok("Operation succeeded.");
        }
        [HttpPost("mergeList")]
        public async Task<ActionResult> MergeLists(string firstListName, string secondListName)
        {
            var done = await _toDoListOperations.MergeLists(firstListName,secondListName);
            if (!done)
            {
                return BadRequest("Operation failed.");
            }
            return Ok("Operation succeeded.");
        }

    }
}
