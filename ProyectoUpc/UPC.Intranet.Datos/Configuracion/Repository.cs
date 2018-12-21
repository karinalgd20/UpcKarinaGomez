using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace UPC.Intranet.Datos
{
    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TEntity">The type of underlying entity in this repository</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Members

        readonly IQueryableUnitOfWork _UnitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members        
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }
        public virtual void Add(TEntity item)
        {

            if (item != (TEntity)null)
                GetSet().Add(item); // add new item in this set
        }

        public virtual void AddReplicate(TEntity item)
        {

            if (item != (TEntity)null)
                _UnitOfWork.Attach<TEntity>(item); // add new item in this set

        }

        public virtual void Add(List<TEntity> items)
        {

            if (items != (List<TEntity>)null)
            {
                items.ForEach(x => Add(x));
            }
        }


        public virtual void Remove(TEntity item)
        {
            if (item != (TEntity)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
        }
        public virtual void Remove(List<TEntity> items)
        {
            if (items != (List<TEntity>)null)
            {
                items.ForEach(x => Remove(x));
            }
        }
        public virtual void TrackItem(TEntity item)
        {
            if (item != (TEntity)null)
                _UnitOfWork.Attach<TEntity>(item);
        }

        public void ActualizarPorCampos(TEntity entity, params Expression<Func<TEntity, object>>[] campos)
        {
            RemoveHoldingEntityInContext(entity);

            ((DbContext)UnitOfWork).Entry(entity).State = EntityState.Unchanged;

            if (campos.Length > 0)
            {
                foreach (var propertyAccessor in campos)
                {
                    ((DbContext)UnitOfWork).Entry(entity).Property(propertyAccessor).IsModified = true;
                }
            }
        }

        private bool RemoveHoldingEntityInContext(TEntity entity)
        {
            var objContext = ((IObjectContextAdapter)((DbContext)UnitOfWork)).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);
            object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            if (exists)
            {
                objContext.Detach(foundEntity);
            }
            return (exists);
        }

        public virtual void Modify(TEntity item)
        {
            if (item != (TEntity)null)
                _UnitOfWork.SetModified(item);
        }
        public virtual TEntity Get(params object[] keyValues)
        {
            return GetEntity(keyValues);
        }
        public virtual TEntity GetAsNoTracking(params object[] keyValues)
        {
            var entity = GetEntity(keyValues);
            if (entity != null)
            {
                ((DbContext)UnitOfWork).Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }
        private TEntity GetEntity(params object[] keyValues)
        {
            if (keyValues != null)
                return GetSet().Find(keyValues);
            else
                return null;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetSet();
        }
        public virtual IEnumerable<TEntity> GetAllAsNoTracking()
        {
            return GetSet().AsNoTracking();
        }
    

        public virtual IEnumerable<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return Filtered(filter);
        }
        public virtual IEnumerable<TEntity> GetFilteredAsNoTracking(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return Filtered(filter).AsNoTracking();
        }
        private IQueryable<TEntity> Filtered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }

        public void EnabledEntityValidation(bool enabled)
        {
            ((DbContext)UnitOfWork).Configuration.ValidateOnSaveEnabled = enabled;
        }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }

        #endregion

        #region Private Methods
        IDbSet<TEntity> GetSet()
        {
            return _UnitOfWork.CreateSet<TEntity>();
        }

        public DbContextTransaction BeginTransaction()
        {
            return _UnitOfWork.BeginTransaction();
        }

       
        #endregion
    }
}
