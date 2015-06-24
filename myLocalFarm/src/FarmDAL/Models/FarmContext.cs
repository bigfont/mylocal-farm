// Copyright (c) BigFont. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Data.Entity;
using FarmBLL.Models;

namespace FarmDAL.Models
{
    public class FarmContext : DbContext
    {
        internal DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoItem>();

            base.OnModelCreating(builder);
        }
    }
}
