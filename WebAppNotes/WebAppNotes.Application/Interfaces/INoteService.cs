using WebAppNotes.Application.DTO.Create;
using WebAppNotes.Application.DTO.Response;

namespace WebAppNotes.Application.Interfaces
{
    public interface INoteService
    {
        Task<List<NoteDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<NoteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(CreateNoteDto entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, CreateNoteDto entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
