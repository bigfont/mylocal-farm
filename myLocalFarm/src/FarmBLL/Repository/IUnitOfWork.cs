// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;

namespace FarmBLL.Repository
{
    /// <remarks>
    /// This interface is here so we can write our controllers
    /// against a Unit of Work that doesn't depend on a particular data store.    
    /// </remarks>
    public interface IUnitOfWork
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        IQueryable<TEntity> Load<TEntity>() where TEntity : class;
        void Create<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        void Save();
    }
}
