using Microsoft.EntityFrameworkCore;
using WebAppNotes.Data.Models;
using WebAppNotes.DataAccess.Data;
using WebAppNotes.DataAccess.Interfaces;

namespace WebAppNotes.DataAccess.Repositories
{
    public class EntityRepository<T> : IRepository<T> where T : Entity
    {

        private readonly AppDbContext _context;
        private readonly DbSet<T> _entites;

        public EntityRepository(AppDbContext context)
        {
            _context = context;
            _entites = context.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _entites.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = _entites.FirstOrDefault(ent => ent.Id == id);

            if (entity != null)
            {
                _entites.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entites.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _entites.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _entites.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
