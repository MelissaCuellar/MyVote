namespace MyVote.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using System.Linq;

    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<VotingEvent> VotingEvents { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Candidate> Candidates { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model
                .G­etEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }


            base.OnModelCreating(modelBuilder);
        }
    }

}
