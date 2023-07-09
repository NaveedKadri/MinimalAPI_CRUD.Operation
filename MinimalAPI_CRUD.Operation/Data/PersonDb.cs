using Microsoft.EntityFrameworkCore;
using MinimalAPI_CRUD.Operation.Model;

namespace MinimalAPI_CRUD.Operation.Data
{
    public class PersonDb : DbContext
    {
        protected readonly IConfiguration configuration;

        public PersonDb (IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
        }

        public DbSet<Person> people { get; set; }
    }
}
