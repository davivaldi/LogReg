using Microsoft.EntityFrameworkCore;
 
namespace LogReg.Models
{
    public class LogRegContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public LogRegContext(DbContextOptions options) : base(options) { }

          public DbSet<User> Users {get;set;}
    }
}