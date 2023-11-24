using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Data
{
    public class ListEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ToDoEntity> Todos { get; set; } = new List<ToDoEntity>();
    }
}
