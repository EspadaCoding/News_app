using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Picabu.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();
           using (var container = host.Services.CreateScope())
           {
                var userManager = container.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = container.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var configuration = container.ServiceProvider.GetRequiredService<IConfiguration>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
                var user = await userManager.FindByNameAsync("IvanAdmin");
                if (user == null)
                {
                    var adminUserName = configuration["AdminInfo:Username"];
                    var adminEmail = configuration["AdminInfo:Email"];
                    var adminPassword = configuration["AdminInfo:Password"];
                    var adminFullName = configuration["AdminInfo:Fullname"];
               
                    user = new AppUser
                    {
                        UserName = adminUserName,
                        Email = adminEmail,
                        Fullname = adminFullName,
                        Year = 1995,
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };
               
                    var result = await userManager.CreateAsync(user, adminPassword);
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
               
                    if (!await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        var result2 = await userManager.AddToRoleAsync(user, "Admin");
                        if (!result2.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }
                }
           }
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
