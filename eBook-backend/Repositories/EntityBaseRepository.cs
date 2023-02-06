using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using surfis_backend.Utils.Time;
using System.Linq.Expressions;
using eBook_backend.Models.Entites;

namespace eBook_backend.Repositories
{
    /// <summary>
    /// Базовый и единый репозиторий для сущностей
    /// </summary>
    public interface IEntityBaseRepository<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// Получить все сущности
        /// </summary>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Получить количество сущностей в базе данных
        /// </summary>
        int Count();

        /// <summary>
        /// Получить единую сущность по ID
        /// </summary>
        Task<T?> GetSingle(Guid id);

        /// <summary>
        /// Получить одну сущность с условием
        /// </summary>
        Task<T?> GetSingle(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Найти все элементы с условием
        /// </summary>
        Task<List<T>> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Добавить элемент
        /// </summary>
        void Add(T entity);

        /// <summary>
        /// Обновить элемент
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Удалить элемент
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Удалить все элементы с уловием
        /// </summary>
        void DeleteWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Сохранить данные
        /// </summary>
        Task Commit();
    }

    /// <summary>
    /// Базовый репозиторий для всех сущностей
    /// </summary>
    public sealed class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : BaseEntity, new()
    {
        private readonly ApplicationContext _context;

        /// <summary>.ctor</summary>
        public EntityBaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
        /// <inheritdoc />
        public int Count() => _context.Set<T>().Count();

        /// <inheritdoc />
        public async Task<T?> GetSingle(Guid id) => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        /// <inheritdoc />
        public async Task<T?> GetSingle(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        /// <inheritdoc />
        public async Task<List<T>> FindBy(Expression<Func<T, bool>> predicate) => await _context.Set<T>().Where(predicate).ToListAsync();
        /// <inheritdoc />
        public void Add(T entity)
        {
            if (entity is BaseTrackEntity trackedEntity)
            {
                trackedEntity.CreateDate = DateTime.Now.SetKindUtc();
                trackedEntity.Id = Guid.NewGuid();

                _context.Entry<T>(trackedEntity as T);
                _context.Set<T>().Add(trackedEntity as T);
            }
            else
            {
                entity.Id = Guid.NewGuid();
                _context.Entry(entity);
                _context.Set<T>().Add(entity);
            }
        }

        /// <inheritdoc />
        public void Update(T entity)
        {
            if (entity is BaseTrackEntity trackedEntity)
            {
                trackedEntity.UpdateDate = DateTime.Now.SetKindUtc();

                EntityEntry dbTrackedEntity = _context.Entry(trackedEntity);
                dbTrackedEntity.State = EntityState.Modified;
            }
            else
            {
                EntityEntry dbEntityEntry = _context.Entry(entity);
                dbEntityEntry.State = EntityState.Modified;
            }
        }

        /// <inheritdoc />
        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        /// <inheritdoc />
        public void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }

        /// <inheritdoc />
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
