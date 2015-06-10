

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

        [FromServices]
        public IGenericRepository<TodoItem> TodoItemRepository { get; }

        [FromServices]
        public IUserManager<User> UserManager { get; }

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
