using WebAppNotes.Application.DTO.Create;
using WebAppNotes.Data.Models;

namespace WebAppNotes.Application.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(CreateTagDto entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(CreateTagDto entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
