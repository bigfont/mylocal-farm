// Copyright (c) BigFont. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Microsoft.Data.Entity;
using FarmBLL.Repository;
using FarmDAL.Models;

namespace FarmDAL.Repository
{
    /// <summary>
    /// Unit of Work implementation that uses an Entity Framework DbContext.
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private FarmContext _context;

        // FarmContext comes in from dependency injection
        // which to me means that the UnitOfWork class should
        // be in the same project as the Startup.cs class.
        public UnitOfWork(FarmContext context)
        {
            _context = context;
        }

        public IQueryable<TDomainObject> Query<TDomainObject>() where TDomainObject : class
        {
            return _context.Set<TDomainObject>().AsNoTracking();
        }

        public IQueryable<TDomainObject> Load<TDomainObject>() where TDomainObject : class
        {
            return _context.Set<TDomainObject>();
        }

        public void Create<TDomainObject>(TDomainObject entity) where TDomainObject : class
        {
            _context.Set<TDomainObject>().Add(entity);
        }

        public void Update<TDomainObject>(TDomainObject entity) where TDomainObject : class
        {
            throw new NotImplementedException();
        }

        public void Delete<TDomainObject>(TDomainObject entity) where TDomainObject : class
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable

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

        #endregion
    }
}
