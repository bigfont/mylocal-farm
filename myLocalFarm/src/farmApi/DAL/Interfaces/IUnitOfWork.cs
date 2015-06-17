// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmApi.Models;

namespace FarmApi.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        GenericRepository<TodoItem> TodoItemRepository { get; }
        void Save();
    }
}
