using Microsoft.EntityFrameworkCore;
using Todo_List__API.Entity;

namespace Todo_List__API.DatabaseTables
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext( DbContextOptions options) :base(options)
        {
            
        }

        public DbSet<TodoEntity> Todos { get; set; }

    }
}
