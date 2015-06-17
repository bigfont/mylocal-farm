﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using FarmApi.Models;
using FarmApi.DAL.Interfaces;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;

namespace FarmApi.DAL
{
    /// <summary>
    /// Lets all repositories shared the same context instance.
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private FarmContext _context;

        private GenericRepository<TodoItem> _todoItemRepository;
        public GenericRepository<TodoItem> TodoItemRepository
        {
            get
            {
                if (_todoItemRepository == null)
                {
                    _todoItemRepository = new GenericRepository<TodoItem>(_context);
                }
                return _todoItemRepository;
            }
        }

        public UnitOfWork(FarmContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _displosed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_displosed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _displosed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
