using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Picabu.Models;
using Picabu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Helpers
{
    public static class NewsDbInitializer
    {
        public static void Init(IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var context = services.ServiceProvider.GetRequiredService<NewsDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category { Name = "IT" });
                context.Categories.Add(new Category { Name = "Games" });
                context.Categories.Add(new Category { Name = "Films" });
                context.Categories.Add(new Category { Name = "Sport" });

            }
            if (!context.Tags.Any())
            {
                context.Tags.Add(new Tag { Name = "Funny" });
                context.Tags.Add(new Tag { Name = "Informative" });
                context.Tags.Add(new Tag { Name = "Education" });
                context.Tags.Add(new Tag { Name = "Relax" });
            }
            context.SaveChanges();
        }
    }
}
