using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using HRE.Core.Extensions;
using HRE.Core.Shared.Auditing;
using HRE.Core.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRE.Core.Repositories
{
    public class Repository<TEntity> : Repository<TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IPrimaryKey, new()
    {
        public Repository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IPrimaryKey<TKey>, new()
    {
        protected readonly DbContext _context;

        public Repository(DbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public virtual async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Add(entity);
            await Task.CompletedTask;
        }

        public virtual async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().AddRange(entities);
            await Task.CompletedTask;
        }

        public virtual async Task<List<TEntity>> GetAllListAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return _context.Set<TEntity>().WhereIf(predicate != null, predicate);
        }

        public virtual async Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FindAsync(keyValues: new object[] { id }, cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Delete(TEntity entity, bool softDelete = false, int? deleterUserId = null)
        {
            if (softDelete && entity is IDeletionAudited delete)
            {
                delete.DeleterUserId = deleterUserId;
                delete.DeletionTime = DateTime.Now;
                delete.IsDeleted = true;
            }
            else
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }

        public virtual void DeleteMultiple(List<TEntity> entities, bool softDelete = false, int? deleterUserId = null)
        {
            foreach (var entity in entities)
            {
                if (softDelete && entity is IDeletionAudited delete)
                {
                    delete.DeleterUserId = deleterUserId;
                    delete.DeletionTime = DateTime.Now;
                    delete.IsDeleted = true;
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Deleted;
                }
            }
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            return _context.Set<TEntity>().WhereIf(predicate != null, predicate).AnyAsync(cancellationToken);
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            return _context.Set<TEntity>().WhereIf(predicate != null, predicate).CountAsync(cancellationToken);
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            return _context.Set<TEntity>().WhereIf(predicate != null, predicate).LongCountAsync(cancellationToken);
        }

        public async Task<TProperty> EnsureLoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression) where TProperty : class
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TProperty>> EnsureLoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression) where TProperty : class
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}