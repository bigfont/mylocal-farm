

using System;
using farmApi.Models;
using farmApi.DAL.Interfaces;
using Microsoft.AspNet.Mvc;

namespace farmApi.DAL
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
                    // TODO Should we be using DI here?
                    _todoItemRepository = new GenericRepository<TodoItem>(_context);
                }
                return _todoItemRepository;
            }
        }

        public UnitOfWork([FromServices] FarmContext context)
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
