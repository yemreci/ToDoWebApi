using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Data
{
    public class ToDoEntity
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}