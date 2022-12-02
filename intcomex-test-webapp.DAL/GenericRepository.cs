using intcomex_test_webapp.DL.IntcomexDBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace intcomex_test_webapp.DAL
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal IntcomexDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(IntcomexDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = ""
       )
        {
            IQueryable<TEntity> query = dbSet.AsNoTracking();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public async virtual Task<TEntity> GetByIdAsync(object id) => await dbSet.FindAsync(id);

        public async virtual Task<bool> ExistEntityByFilter(Expression<Func<TEntity, bool>> filter = null)
                => await dbSet.Where(filter).AsNoTracking().FirstOrDefaultAsync() != null ? true : false;

        public async virtual Task<TEntity> GetSingleAsync(
              Expression<Func<TEntity, bool>> filter = null,
              string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.AsNoTracking().SingleOrDefaultAsync();
        }

        public async virtual Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return orderBy != null ? await orderBy(query).AsNoTracking().FirstOrDefaultAsync() : await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async virtual Task<EntityEntry<TEntity>> InsertAsync(TEntity entity) => await dbSet.AddAsync(entity);

        public async virtual Task<TEntity> DeleteByIdAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);

            return entityToDelete;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);

            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
