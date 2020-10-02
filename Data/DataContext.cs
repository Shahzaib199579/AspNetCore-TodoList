using Microsoft.EntityFrameworkCore;
using Todo_List_Project.Models;

namespace Todo_List_Project.Data
{
    public class DataContext: DbContext
    {
        public DataContext (DbContextOptions<DataContext> options): base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}