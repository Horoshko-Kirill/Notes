using Microsoft.EntityFrameworkCore;
using WebAppNotes.Data.Models;
using WebAppNotes.DataAccess.Data;
using WebAppNotes.DataAccess.Interfaces;

namespace WebAppNotes.DataAccess.Repositories
{
    public abstract class EntityRepository<T> : IRepository<T> where T : Entity
    {

        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _entites;

        public EntityRepository(AppDbContext context)
        {
            _context = context;
            _entites = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _entites.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = _entites.FirstOrDefault(ent => ent.Id == id);

            if (entity != null)
            {
                _entites.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entites.ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _entites.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _entites.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
