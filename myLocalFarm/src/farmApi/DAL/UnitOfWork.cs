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
        private FarmContext context;

        private GenericRepository<TodoItem> todoItemRepository;
        public GenericRepository<TodoItem> TodoItemRepository
        {
            get
            {
                if (this.todoItemRepository == null)
                {
                    // TODO Should we be using DI here?
                    this.todoItemRepository = new GenericRepository<TodoItem>(context);
                }
                return this.todoItemRepository;
            }
        }

        public UnitOfWork([FromServices] FarmContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool displosed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.displosed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.displosed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
