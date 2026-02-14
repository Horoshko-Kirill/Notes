using WebAppNotes.Application.DTO.Create;
using WebAppNotes.Application.DTO.Response;

namespace WebAppNotes.Application.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TagDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(CreateTagDto entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, CreateTagDto entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
