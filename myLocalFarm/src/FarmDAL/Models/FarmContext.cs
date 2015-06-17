// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FarmDAL.Models;
using Microsoft.Data.Entity;

namespace FarmDAL.Models
{
    public class FarmContext : DbContext
    {
        internal DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoItem>().Key(e => e.Id);

            base.OnModelCreating(builder);
        }
    }
}
