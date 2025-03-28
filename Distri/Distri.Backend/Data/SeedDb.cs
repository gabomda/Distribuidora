using Distri.Backend.UnitsOfWork.Implementations;
using Distri.Backend.UnitsOfWork.Interfaces;
using Distri.Shared.Entities;
using Distri.Shared.Enums;

namespace Distri.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;

        public SeedDb(DataContext context,IUsersUnitOfWork usersUnitOfWork)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1234", "Gabriel", "Barjau", "zulu@yopmail.com", "322 311 4620", "Blanco Encalada 1732", UserType.Admin);
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }


        private async Task CheckRolesAsync()
        {
            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Crema Corporal" });
                _context.Categories.Add(new Category { Name = "Shampoo" });
                _context.Categories.Add(new Category { Name = "Crema Enjuague" });
                _context.Categories.Add(new Category { Name = "Peines" });
                _context.Categories.Add(new Category { Name = "Tinturas" });
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States =
                    [
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = [
                                new City() { Name = "Medellín" },
                                new City() { Name = "Itagüí" },
                                new City() { Name = "Envigado" },
                                new City() { Name = "Bello" },
                                new City() { Name = "Rionegro" },
                            ]
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = new List<City>() {
                                new City() { Name = "Usaquen" },
                                new City() { Name = "Champinero" },
                                new City() { Name = "Santa fe" },
                                new City() { Name = "Useme" },
                                new City() { Name = "Bosa" },
                            }
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    States =
                    [
                        new State()
                        {
                            Name = "Buenos Aires",
                            Cities = [
                                new City() { Name = "La Plata" },
                                new City() { Name = "Capital Federal" },
                                new City() { Name = "Gran Buenos Aires" },
                                new City() { Name = "Bahia Blanca" },
                                new City() { Name = "Mar del Plata" },
                            ]
                        },
                        new State()
                        {
                            Name = "Neuquen",
                            Cities = [
                                new City() { Name = "Neuquen" },
                                new City() { Name = "Cutral Co" },
                                new City() { Name = "Plaza Huincul" },
                                new City() { Name = "Zapala" },
                                new City() { Name = "Anielo" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country { Name = "Brasil" });
                _context.Countries.Add(new Country { Name = "Uruguay" });
                _context.Countries.Add(new Country { Name = "Paraguay" });
                _context.Countries.Add(new Country { Name = "Bolivia" });
                _context.Countries.Add(new Country { Name = "Chile" });
                _context.Countries.Add(new Country { Name = "Estados Unidos" });
            }

            await _context.SaveChangesAsync();
        }
    }
}