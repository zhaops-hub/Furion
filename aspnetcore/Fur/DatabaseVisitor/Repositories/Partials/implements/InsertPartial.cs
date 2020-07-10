﻿using Fur.DatabaseVisitor.Entities;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 新增 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 新增操作 + public virtual EntityEntry<TEntity> Insert(TEntity entity)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> Insert(TEntity entity)
        {
            SetInsertMaintenanceFields(entity);
            return Entity.Add(entity);
        }
        #endregion

        #region 新增操作 + public virtual void Insert(params TEntity[] entities)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Insert(params TEntity[] entities)
        {
            SetInsertMaintenanceFields(entities);
            Entity.AddRange(entities);
        }
        #endregion

        #region 新增操作 + public virtual void Insert(IEnumerable<TEntity> entities)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            SetInsertMaintenanceFields(entities.ToArray());
            Entity.AddRange(entities);
        }
        #endregion

        #region 新增操作 + public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            SetInsertMaintenanceFields(entity);
            return Entity.AddAsync(entity);
        }
        #endregion

        #region 新增操作 + public virtual Task InsertAsync(params TEntity[] entities)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task InsertAsync(params TEntity[] entities)
        {
            SetInsertMaintenanceFields(entities);
            return Entity.AddRangeAsync();
        }
        #endregion

        #region 新增操作 + public virtual Task InsertAsync(IEnumerable<TEntity> entities)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task InsertAsync(IEnumerable<TEntity> entities)
        {
            SetInsertMaintenanceFields(entities.ToArray());
            return Entity.AddRangeAsync();
        }
        #endregion


        #region 新增操作并立即保存 + public virtual EntityEntry<TEntity> InsertSaveChanges(TEntity entity)
        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> InsertSaveChanges(TEntity entity)
        {
            var trackEntity = Insert(entity);
            SaveChanges();
            return trackEntity;
        }
        #endregion

        #region 新增操作并立即保存 + public virtual void InsertSaveChanges(params TEntity[] entities)
        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void InsertSaveChanges(params TEntity[] entities)
        {
            Insert(entities);
            SaveChanges();
        }
        #endregion

        #region 新增操作并立即保存 + public virtual void InsertSaveChanges(IEnumerable<TEntity> entities)
        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void InsertSaveChanges(IEnumerable<TEntity> entities)
        {
            Insert(entities);
            SaveChanges();
        }
        #endregion

        #region 新增操作并立即保存 + public virtual async ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity)
        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        public virtual async ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity)
        {
            var trackEntity = await InsertAsync(entity);
            await SaveChangesAsync();
            return trackEntity;
        }
        #endregion

        #region 新增操作并立即保存 + public virtual async Task InsertSaveChangesAsync(params TEntity[] entities)
        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task InsertSaveChangesAsync(params TEntity[] entities)
        {
            await InsertAsync(entities);
            await SaveChangesAsync();
            await Task.CompletedTask;
        }
        #endregion

        #region 新增操作并立即保存 + public virtual async Task InsertSaveChangesAsync(IEnumerable<TEntity> entities)
        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task InsertSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await InsertAsync(entities);
            await SaveChangesAsync();
            await Task.CompletedTask;
        }
        #endregion


        #region 设置新增时维护字段 + private void SetInsertMaintenanceFields(params TEntity[] entities)
        /// <summary>
        /// 设置新增时维护字段
        /// </summary>
        /// <param name="entities">多个实体</param>
        private void SetInsertMaintenanceFields(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                var entityEntry = EntityEntry(entity);

                var createdTimeProperty = EntityEntryProperty(entityEntry, _maintenanceProvider?.GetCreatedTimePropertyName() ?? nameof(DbEntityBase.CreatedTime));
                if (createdTimeProperty != null)
                {
                    createdTimeProperty.CurrentValue = DateTime.Now;
                }

                var tenantIdProperty = EntityEntryProperty(entityEntry, nameof(DbEntity.TenantId));
                if (tenantIdProperty != null)
                {
                    tenantIdProperty.CurrentValue = _tenantProvider.GetTenantId();
                }
            }
        }
        #endregion
    }
}
