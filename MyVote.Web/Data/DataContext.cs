namespace MyVote.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<VotingEvent> VotingEvents { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }

}
