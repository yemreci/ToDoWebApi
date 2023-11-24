using ToDoApp.Data;

namespace ToDoApp.Models
{
    public class ListDTO
    {
        public string Name { get; set; }
        public List<ToDoEntity> Todos { get; set; } = new List<ToDoEntity>();
    }
}
