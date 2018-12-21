using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace UPC.Intranet.Datos
{
    /// <summary>
    /// Base interface for implement a "Repository Pattern", for
    /// more information about this pattern see http://martinfowler.com/eaaCatalog/repository.html
    /// or http://blogs.msdn.com/adonet/archive/2009/06/16/using-repository-and-unit-of-work-patterns-with-entity-framework-4-0.aspx
    /// </summary>
    /// <remarks>
    /// Indeed, one might think that IDbSet already a generic repository and therefore
    /// would not need this item. Using this interface allows us to ensure PI principle
    /// within our domain model
    /// </remarks>
    /// <typeparam name="TEntity">Type of entity for this repository </typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Get the unit of work in this repository
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Add(TEntity item);

        /// <summary>
        /// Add item into repository, using the unit of work
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void AddReplicate(TEntity item);

        /// <summary>
        /// Add list of items
        /// </summary>
        /// <param name="items">list of items to add to repository</param>
        void Add(List<TEntity> items);

        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="item">Item to delete</param>
        void Remove(TEntity item);

        /// <summary>
        /// Delete list items
        /// </summary>
        /// <param name="items">list of items to delete</param>
        void Remove(List<TEntity> items);

        /// <summary>
        /// Modifica por campos especificos en
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="campos"></param>
        void ActualizarPorCampos(TEntity entity, params Expression<Func<TEntity, object>>[] campos);

        /// <summary>
        /// Set item as modified
        /// </summary>
        /// <param name="item">Item to modify</param>
        void Modify(TEntity item);

        /// <summary>
        ///Track entity into this repository, really in UnitOfWork. 
        ///In EF this can be done with Attach and with Update in NH
        /// </summary>
        /// <param name="item">Item to attach</param>
        void TrackItem(TEntity item);

        /// <summary>
        /// Sets modified entity into the repository. 
        /// When calling Commit() method in UnitOfWork 
        /// these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
        void Merge(TEntity persisted, TEntity current);

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="keyValues">Entity key value</param>
        /// <returns></returns>
        TEntity Get(params object[] keyValues);

        /// <summary>
        /// Get elemento by entity key dettached from context
        /// </summary>
        /// <param name="keyValues">Entity key value</param>
        /// <returns></returns>
        TEntity GetAsNoTracking(params object[] keyValues);

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get all elements of type TEntity in repository dettached from context
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetAllAsNoTracking();

        /// <summary>
        /// Get all elements of type TEntity that matching a
        /// Specification <paramref name="specification"/>
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <returns></returns>
        
        /// <summary>
        /// Get  elements of type TEntity in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Get  elements of type TEntity in repository dettached from context
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetFilteredAsNoTracking(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// habilita o inhabilita las validaciones de entity
        /// </summary>
        /// <param name="enabled"></param>
        void EnabledEntityValidation(bool enabled);

        /// <summary>
        /// Inicia una transaccion EF
        /// </summary>
        /// <returns>Es el objeto transaccion</returns>
        DbContextTransaction BeginTransaction();
    }
}
