namespace MyVote.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Helpers;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("meli.cuellar0117@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Melissa",
                    LastName = "Cuellar",
                    Email = "meli.cuellar0117@gmail.com",
                    UserName = "meli.cuellar0117@gmail.com",
                    PhoneNumber = "3014747485",
                    Ocupation = "Programer",
                    Gender = "Female",
                    Stratum = "3",
                    Birthdate = new DateTime(1994, 01, 17)
                };

                var result = await this.userHelper.AddUserAsync(user, "888888");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.VotingEvents.Any())
            {
                this.AddVotingEvent("Hidroituango Dam", 
                    "Do you think that the Hidroituango dam project should continue?",
                    DateTime.Parse("04/18/2019"),
                    DateTime.Parse("04/18/2019"),
                    user);
                this.AddVotingEvent("Social Network",
                    "What is the social network that you use most in the day?",
                    DateTime.Parse("04/18/2019"),
                    DateTime.Parse("04/18/2019"),
                    user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddVotingEvent(string name, string description, DateTime startDate, DateTime endDate, User user)
        {
            this.context.VotingEvents.Add(new VotingEvent
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                User = user
            });
        }

    }
}
