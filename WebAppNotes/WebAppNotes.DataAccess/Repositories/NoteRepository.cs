using WebAppNotes.Data.Models;
using WebAppNotes.DataAccess.Data;
using WebAppNotes.DataAccess.Interfaces;

namespace WebAppNotes.DataAccess.Repositories
{
    internal class NoteRepository : EntityRepository<Note>, INoteRepository
    {
        public NoteRepository(AppDbContext context) : base(context) { }
    }
}
