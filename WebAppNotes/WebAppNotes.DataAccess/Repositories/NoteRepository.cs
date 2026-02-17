using Microsoft.EntityFrameworkCore;
using WebAppNotes.Data.Models;
using WebAppNotes.DataAccess.Data;
using WebAppNotes.DataAccess.Interfaces;

namespace WebAppNotes.DataAccess.Repositories
{
    internal class NoteRepository : EntityRepository<Note>, INoteRepository
    {
        public NoteRepository(AppDbContext context) : base(context) { }

        public override Task<List<Note>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _entites.Include(n => n.Tags).ToListAsync(cancellationToken);
        }

        public override Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _entites.Include(n => n.Tags).FirstOrDefaultAsync(n =>  n.Id == id, cancellationToken);
        }
    }
}
