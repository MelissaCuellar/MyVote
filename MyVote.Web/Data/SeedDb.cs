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

            if (!this.context.Countries.Any())
            {
                await this.AddCountriesAndCitiesAsync();
            }

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
                    Birthdate = new DateTime(1994, 01, 17),
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
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

        private async Task AddCountriesAndCitiesAsync()
        {
            this.AddCountry("Colombia", new string[] { "Medellín", "Bogota", "Calí", "Barranquilla", "Bucaramanga", "Cartagena", "Pereira" });
            this.AddCountry("Argentina", new string[] { "Córdoba", "Buenos Aires", "Rosario", "Tandil", "Salta", "Mendoza" });
            this.AddCountry("Estados Unidos", new string[] { "New York", "Los Ángeles", "Chicago", "Washington", "San Francisco", "Miami", "Boston" });
            this.AddCountry("Ecuador", new string[] { "Quito", "Guayaquil", "Ambato", "Manta", "Loja", "Santo" });
            this.AddCountry("Peru", new string[] { "Lima", "Arequipa", "Cusco", "Trujillo", "Chiclayo", "Iquitos" });
            this.AddCountry("Chile", new string[] { "Santiago", "Valdivia", "Concepcion", "Puerto Montt", "Temucos", "La Sirena" });
            this.AddCountry("Uruguay", new string[] { "Montevideo", "Punta del Este", "Colonia del Sacramento", "Las Piedras" });
            this.AddCountry("Bolivia", new string[] { "La Paz", "Sucre", "Potosi", "Cochabamba" });
            this.AddCountry("Venezuela", new string[] { "Caracas", "Valencia", "Maracaibo", "Ciudad Bolivar", "Maracay", "Barquisimeto" });
            this.AddCountry("Paraguay", new string[] { "Asunción", "Ciudad del Este", "Encarnación", "San  Lorenzo", "Luque", "Areguá" });
            this.AddCountry("Brasil", new string[] { "Rio de Janeiro", "São Paulo", "Salvador", "Porto Alegre", "Curitiba", "Recife", "Belo Horizonte", "Fortaleza" });
            this.AddCountry("Panamá", new string[] { "Chitré", "Santiago", "La Arena", "Agua Dulce", "Monagrillo", "Ciudad de Panamá", "Colón", "Los Santos" });
            this.AddCountry("México", new string[] { "Guadalajara", "Ciudad de México", "Monterrey", "Ciudad Obregón", "Hermosillo", "La Paz", "Culiacán", "Los Mochis" });
            await this.context.SaveChangesAsync();
        }

        private void AddCountry(string country, string[] cities)
        {
            var theCities = cities.Select(c => new City { Name = c }).ToList();
            this.context.Countries.Add(new Country
            {
                Cities = theCities,
                Name = country
            });
        }

    }
}
