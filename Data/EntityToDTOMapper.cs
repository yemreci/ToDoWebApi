using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class EntityToDTOMapper
    {
        public static ToDoDTO ToDoEntitytoDTO(ToDoEntity entity)
        {
            ToDoDTO dto = new ToDoDTO()
            {
                Description = entity.Description,
                IsComplete = entity.IsComplete,
            };
            return dto;
        }

        public static ListDTO ListEntitytoDTO(ListEntity entity)
        {
            ListDTO dto = new ListDTO()
            {
                Name = entity.Name,
                Todos = entity.Todos,
            };
            return dto;
        }
    }
}
