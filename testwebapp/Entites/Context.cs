using Microsoft.EntityFrameworkCore;

namespace testwebapp.Entites
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        public DbSet<ToDoEntity> ToDoEntities { get; set; }
    }
}
