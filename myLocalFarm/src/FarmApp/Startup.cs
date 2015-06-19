// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System;

// BLL
using FarmBLL.Repository;
using FarmBLL.Models;

// DAL
using FarmDAL.Repository;
using FarmDAL.Models;

namespace FarmApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            this.ConfigurePersistenceServices(services);
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseMvc(routes => {

            });

            this.ConfigurePersistence(serviceProvider);

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello World.");
            });
        }

        private void ConfigurePersistence(IServiceProvider serviceProvider)
        {
            var itemsToSeed = 50;
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            for (var i = 0; i < itemsToSeed; ++i)
            {
                unitOfWork.Create(new TodoItem()
                {
                    Title = string.Format("Item Number {0}", i)
                });
            }

            unitOfWork.Save();
        }

        private void ConfigurePersistenceServices(IServiceCollection services)
        {
            // choose the persistence mechanism
            services
                .AddEntityFramework()
                .AddInMemoryStore()
                .AddDbContext<FarmContext>(options =>
                {
                    options.UseInMemoryStore(persist: true);
                });

            // choose the unit of work implementation
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}
