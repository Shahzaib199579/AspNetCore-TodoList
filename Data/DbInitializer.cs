using System.Linq;
using Todo_List_Project.Models;

namespace Todo_List_Project.Data
{
    public enum TodoState
    {
        Todo,
        InProgress,
        Completed
    }
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any todos.
            if (context.Todos.Any())
            {
                return;   // DB has been seeded
            }

            var todos = new Todo[]{
                new Todo{Id= 1, Title = "Buy Milk", State = TodoState.Todo.ToString(), isCompleted = false},
                new Todo{Id= 2, Title = "Buy Eggs", State = TodoState.Todo.ToString(), isCompleted = false},
                new Todo{Id= 3, Title = "Buy Bread", State = TodoState.Todo.ToString(), isCompleted = false},
            };

            context.Todos.AddRange(todos);
            context.SaveChanges();

        
        }
    }
}