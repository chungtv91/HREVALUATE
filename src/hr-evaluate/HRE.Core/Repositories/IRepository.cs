using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using HRE.Core.Shared.Entities;

namespace HRE.Core.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : class, IPrimaryKey, new()
    {
    }

    public interface IRepository<TEntity, TKey>
        where TEntity : class, IPrimaryKey<TKey>, new()
    {
        int Commit();

        Task<int> CommitAsync(CancellationToken cancellationToken);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListAsync(CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null);

        Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        void Update(List<TEntity> entities);

        /// <summary>
        /// Xoá dữ liệu một đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng muốn xoá.</param>
        /// <param name="softDelete">Nếu là true và kế thừa ISoftDelete thì sẽ đánh dấu xoá. Ngược lại sẽ xoá hẳn khỏi db.</param>
        /// <param name="deleterUserId">Mã người xoá.</param>
        void Delete(TEntity entity, bool softDelete = false, int? deleterUserId = null);

        /// <summary>
        /// Xoá dữ liệu của một danh sách đối tượng.
        /// </summary>
        /// <param name="entities">Danh sách đối tượng muốn xoá.</param>
        /// <param name="softDelete">Nếu là true và kế thừa ISoftDelete thì sẽ đánh dấu xoá. Ngược lại sẽ xoá hẳn khỏi db.</param>
        /// <param name="deleterUserId">Mã người xoá.</param>
        void DeleteMultiple(List<TEntity> entities, bool softDelete = false, int? deleterUserId = null);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

        Task<TProperty> EnsureLoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression) where TProperty : class;

        Task<IEnumerable<TProperty>> EnsureLoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression) where TProperty : class;
    }
}