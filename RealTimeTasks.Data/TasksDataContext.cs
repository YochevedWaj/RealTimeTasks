using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RealTimeTasks.Data
{
    public class TasksDataContext : DbContext
    {
        private readonly string _connectionString;

        public TasksDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
