using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, DataContext context)
        {
            if(await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var seedData = JsonSerializer.Deserialize<SeedData>(userData, options);

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
            };
            
            foreach(var role in roles) 
            {
                await roleManager.CreateAsync(role);
            }

            foreach(var user in seedData.Users)
            {
                user.UserName = user.UserName.ToLower();                
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser 
            {
                UserName = "admin",
                FullName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] {"Admin"});

            var symbols = new List<Symbol>
            {
                new Symbol{Name = "Elephant"},
                new Symbol{Name = "Lotus"},
                new Symbol{Name = "Sickle"},
                new Symbol{Name = "Hand"},
                new Symbol{Name = "Clock"},
                new Symbol{Name = "Lion"}
            };

            await context.Symbols.AddRangeAsync(symbols);
            await context.SaveChangesAsync();

            await context.MPSeats.AddRangeAsync(seedData.MPSeats);
            await context.Parties.AddRangeAsync(seedData.Parties);
            await context.SaveChangesAsync();
        }
    }
}