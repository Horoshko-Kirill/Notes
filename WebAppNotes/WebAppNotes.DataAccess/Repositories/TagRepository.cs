using WebAppNotes.Data.Models;
using WebAppNotes.DataAccess.Data;
using WebAppNotes.DataAccess.Interfaces;

namespace WebAppNotes.DataAccess.Repositories
{
    public class TagRepository : EntityRepository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context) { }
    }
}
