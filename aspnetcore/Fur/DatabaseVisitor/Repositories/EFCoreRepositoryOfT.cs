﻿using Fur.DatabaseVisitor.Dependencies;
using Fur.DatabaseVisitor.Extensions;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IEntity, new()
    {
        public EFCoreRepositoryOfT(DbContext dbContext)
        {
            DbContext = dbContext;
            Entity = DbContext.Set<TEntity>();
        }

        public virtual DbContext DbContext { get; }
        public virtual DbSet<TEntity> Entity { get; }
        public virtual DatabaseFacade Database => DbContext.Database;
        public virtual DbConnection DbConnection => DbContext.Database.GetDbConnection();

        public EntityEntry<TEntity> Delete(TEntity entity)
        {
            return Entity.Remove(entity);
        }

        // 新增操作
        public virtual EntityEntry<TEntity> Insert(TEntity entity)
        {
            return Entity.Add(entity);
        }

        public virtual void Insert(params TEntity[] entities)
        {
            Entity.AddRange(entities);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            Entity.AddRange(entities);
        }

        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            return Entity.AddAsync(entity);
        }

        public virtual Task InsertAsync(params TEntity[] entities)
        {
            return Entity.AddRangeAsync();
        }

        public virtual Task InsertAsync(IEnumerable<TEntity> entities)
        {
            return Entity.AddRangeAsync();
        }

        public virtual EntityEntry<TEntity> InsertSaveChanges(TEntity entity)
        {
            var trackEntity = Insert(entity);
            SaveChanges();
            return trackEntity;
        }

        public virtual void InsertSaveChanges(params TEntity[] entities)
        {
            Insert(entities);
            SaveChanges();
        }

        public virtual void InsertSaveChanges(IEnumerable<TEntity> entities)
        {
            Insert(entities);
            SaveChanges();
        }

        public virtual async ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity)
        {
            var trackEntity = await InsertAsync(entity);
            await SaveChangesAsync();
            return trackEntity;
        }

        public virtual async Task InsertSaveChangesAsync(params TEntity[] entities)
        {
            await InsertAsync(entities);
            await SaveChangesAsync();
            await Task.CompletedTask;
        }

        public virtual async Task InsertSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await InsertAsync(entities);
            await SaveChangesAsync();
            await Task.CompletedTask;
        }

        // 更新操作
        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return Entity.Update(entity);
        }

        public virtual void Update(params TEntity[] entities)
        {
            Entity.UpdateRange(entities);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            Entity.UpdateRange(entities);
        }

        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            var trackEntity = Entity.Update(entity);
            return Task.FromResult(trackEntity);
        }

        public virtual Task UpdateAsync(params TEntity[] entities)
        {
            Entity.UpdateRange(entities);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            Entity.UpdateRange(entities);
            return Task.CompletedTask;
        }

        public virtual EntityEntry<TEntity> UpdateSaveChanges(TEntity entity)
        {
            var trackEntity = Update(entity);
            SaveChanges();
            return trackEntity;
        }

        public virtual void UpdateSaveChanges(params TEntity[] entities)
        {
            Update(entities);
            SaveChanges();
        }

        public virtual void UpdateSaveChanges(IEnumerable<TEntity> entities)
        {
            Update(entities);
            SaveChanges();
        }

        public virtual async Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity)
        {
            var trackEntities = await UpdateAsync(entity);
            await SaveChangesAsync();
            return trackEntities;
        }

        public virtual async Task UpdateSaveChangesAsync(params TEntity[] entities)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync();
        }

        public virtual async Task UpdateSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync();
        }

        // 删除功能
        public virtual void Delete(params TEntity[] entities)
        {
            Entity.RemoveRange(entities);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            Entity.RemoveRange(entities);
        }

        public virtual EntityEntry<TEntity> Delete(object id)
        {
            var entity = Entity.Find(id);
            return Delete(entity);
        }

        public virtual Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(Entity.Remove(entity));
        }

        public virtual Task DeleteAsync(params TEntity[] entities)
        {
            Entity.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            Entity.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual async Task<EntityEntry<TEntity>> DeleteAsync(object id)
        {
            var entity = await Entity.FindAsync(id);
            return await DeleteAsync(entity);
        }

        public virtual EntityEntry<TEntity> DeleteSaveChanges(TEntity entity)
        {
            var trackEntity = Delete(entity);
            SaveChanges();
            return trackEntity;
        }

        public virtual void DeleteSaveChanges(params TEntity[] entities)
        {
            Delete(entities);
            SaveChanges();
        }

        public virtual void DeleteSaveChanges(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            SaveChanges();
        }

        public virtual EntityEntry<TEntity> DeleteSaveChanges(object id)
        {
            var trackEntity = Delete(id);
            SaveChanges();
            return trackEntity;
        }

        public virtual async Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(TEntity entity)
        {
            var trackEntity = await DeleteAsync(entity);
            await SaveChangesAsync();
            return trackEntity;
        }

        public virtual async Task DeleteSaveChangesAsync(params TEntity[] entities)
        {
            await DeleteAsync(entities);
            await SaveChangesAsync();
        }

        public virtual async Task DeleteSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await DeleteAsync(entities);
            await SaveChangesAsync();
        }

        public virtual async Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(object id)
        {
            var trackEntity = await DeleteAsync(id);
            await SaveChangesAsync();
            return trackEntity;
        }

        public virtual TEntity Find(object id)
        {
            return Entity.Find(id);
        }

        public virtual ValueTask<TEntity> FindAsync(object id)
        {
            return Entity.FindAsync(id);
        }

        public virtual TEntity Single()
        {
            return Entity.Single();
        }

        public virtual Task<TEntity> SingleAsync()
        {
            return Entity.SingleAsync();
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.Single(expression);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleAsync(expression);
        }

        public virtual TEntity SingleOrDefault()
        {
            return Entity.SingleOrDefault();
        }

        public virtual Task<TEntity> SingleOrDefaultAsync()
        {
            return Entity.SingleOrDefaultAsync();
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleOrDefault(expression);
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleOrDefaultAsync(expression);
        }

        public virtual TEntity Single(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().Single();
            else return Single();
        }

        public virtual Task<TEntity> SingleAsync(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleAsync();
            else return SingleAsync();
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().Single(expression);
            else return Single(expression);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleAsync(expression);
            else return SingleAsync(expression);
        }

        public virtual TEntity SingleOrDefault(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleOrDefault();
            else return SingleOrDefault();
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleOrDefaultAsync();
            else return SingleOrDefaultAsync();
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleOrDefault(expression);
            else return SingleOrDefault(expression);
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().SingleOrDefaultAsync(expression);
            else return SingleOrDefaultAsync(expression);
        }

        public virtual TEntity First()
        {
            return Entity.First();
        }

        public virtual Task<TEntity> FirstAsync()
        {
            return Entity.FirstAsync();
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.First(expression);
        }

        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstAsync(expression);
        }

        public virtual TEntity FirstOrDefault()
        {
            return Entity.FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync()
        {
            return Entity.FirstOrDefaultAsync();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstOrDefault(expression);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstOrDefaultAsync(expression);
        }

        public virtual TEntity First(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().First();
            else return First();
        }

        public virtual Task<TEntity> FirstAsync(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstAsync();
            else return FirstAsync();
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().First(expression);
            else return First(expression);
        }

        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstAsync(expression);
            else return FirstAsync(expression);
        }

        public virtual TEntity FirstOrDefault(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstOrDefault();
            else return FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstOrDefaultAsync();
            else return FirstOrDefaultAsync();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstOrDefault(expression);
            else return FirstOrDefault(expression);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return Entity.AsNoTracking().FirstOrDefaultAsync(expression);
            else return FirstOrDefaultAsync(expression);
        }

        public virtual IQueryable<TEntity> Get(bool noTracking = false, bool ignoreQueryFilters = false)
        {
            return GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
        }
        public virtual Task<List<TEntity>> GetAsync(bool noTracking = false, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
            return query.ToListAsync();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null, bool noTracking = false, bool ignoreQueryFilters = false)
        {
            return GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
        }

        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression = null, bool noTracking = false, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
            return query.ToListAsync();
        }

        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChanges(acceptAllChangesOnSuccess);
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        public virtual IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
        {
            return Entity.FromSqlRaw(sql, parameters);
        }

        public virtual IQueryable<TEntity> FromSqlRaw<TParameterModel>(string sql, TParameterModel parameterModel) where TParameterModel : class
        {
            return Entity.FromSqlRaw(sql, parameterModel.ToSqlParameters());
        }

        public virtual DataTable FromSqlOriginal(string sql, params object[] parameters)
        {
            return Database.DbSqlQuery(sql, parameters);
        }

        public virtual Task<DataTable> FromSqlOriginalAsync(string sql, params object[] parameters)
        {
            return Database.DbSqlQueryAsync(sql, parameters);
        }

        public virtual IEnumerable<T> FromSqlOriginal<T>(string sql, params object[] parameters)
        {
            return Database.DbSqlQuery<T>(sql, parameters);
        }

        public virtual Task<IEnumerable<T>> FromSqlOriginalAsync<T>(string sql, params object[] parameters)
        {
            return Database.DbSqlQueryAsync<T>(sql, parameters);
        }

        private IQueryable<TEntity> GetQueryConditionCombine(Expression<Func<TEntity, bool>> expression = null, bool noTracking = false, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> entities = Entity;
            if (noTracking) entities = entities.AsNoTracking();
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (expression != null) entities = entities.Where(expression);

            return entities;
        }
    }
}