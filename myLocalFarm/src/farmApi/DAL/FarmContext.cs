// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FarmApi.Models;
using Microsoft.Data.Entity;
using FarmApi.DAL.Interfaces;

namespace FarmApi.DAL
{
    public class FarmContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoItem>().Key(e => e.Id);

            base.OnModelCreating(builder);
        }
    }
}
