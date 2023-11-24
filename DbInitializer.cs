using ToDoApp.Data;

namespace ToDoApp
{
    public class DbInitializer
    {
        public static void Initialize(ToDoAppContext context)
        {
            context.Database.EnsureCreated(); // Ensure that the database is created

            // Check if the database is already seeded
            if (context.ToDoLists.Any())
            {
                return;   // Database has been seeded
            }

            // Seed your data here
            var initialData = new ListEntity[]
            {
            new ListEntity { Name = "ToDoList1", Todos = {
                    new ToDoEntity
                    {
                        Description = "Desc1",
                        IsComplete = false
                    },
                    new ToDoEntity
                    {
                        Description = "Desc2",
                        IsComplete = false
                    },
                    new ToDoEntity
                    {
                        Description = "Desc3",
                        IsComplete = false
                    }
                }
            },
            new ListEntity { Name = "ToDoList2", Todos = {
                    new ToDoEntity
                    {
                        Description = "Desc21",
                        IsComplete = false
                    },
                    new ToDoEntity
                    {
                        Description = "Desc22",
                        IsComplete = false
                    },
                    new ToDoEntity
                    {
                        Description = "Desc23",
                        IsComplete = false
                    }
                }
            },
                // Add more entities as needed
            };

            context.ToDoLists.AddRange(initialData);
            context.SaveChanges();
        }
    }
}
