// Copyright (c) BigFont. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        IQueryable<TDomainObject> Query<TDomainObject>() where TDomainObject : class;

        IQueryable<TDomainObject> Load<TDomainObject>() where TDomainObject : class;
        void Create<TDomainObject>(TDomainObject entity) where TDomainObject : class;
        void Update<TDomainObject>(TDomainObject entity) where TDomainObject : class;
        void Delete<TDomainObject>(TDomainObject entity) where TDomainObject : class;

        void Save();
    }
}
