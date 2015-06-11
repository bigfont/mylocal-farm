

using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.SqlServer;
using Microsoft.Data.Entity.InMemory;
using Microsoft.AspNet.Http;
using farmApi.DAL.Interfaces;
using farmApi.DAL;
using farmApi.Models;
using System;

namespace farmApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // entity framework
            services
                .AddEntityFramework()
                .AddInMemoryStore()
                .AddDbContext<FarmContext>(options =>
                {
                    options.UseInMemoryStore(persist: true);
                });

            // dependency injection
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseMvc();

            // TODO 
            // Add an authorization server endpoint.
            // Is it even possible?
            // E.g. /Token

            // entity framework
            var itemsToSeed = 50;
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            for (var i = 0; i < itemsToSeed; ++i)
            {
                unitOfWork
                    .TodoItemRepository
                    .Add(new TodoItem()
                    {
                        Id = 0,
                        Title = string.Format("Item Number {0}", i)
                    });
            }

            unitOfWork.Save();

            app.Run(async context => {
                await context.Response.WriteAsync("Hello World.");
            });
        }
    }
}
